using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using RPG.Core;
using UnityEngine.AI;
using UnityEngine.UI;
using RPG.Control;
using RPG.SceneManagement;
using RPG.Attributes;

namespace RPG.Combat
{
    public class Resurrect : MonoBehaviour
    {
        private Camera camera;
        private Volume volume;
        private VolumeProfile currentProfile;
        private SFXFader sfxFader;
        [HideInInspector]
        public GameObject playerDeadBody = null;
        

        [Header("Default-Mode")]
        [SerializeField] public VolumeProfile defaultProfile;
        [SerializeField] public Color defaultFogColour;

        [Header("Spirit-Mode")]
        [SerializeField] public VolumeProfile resurrectProfile;
        [SerializeField] public GameObject resurrectLocation;
        [SerializeField] public AudioSource resurrectAudioSource;
        [SerializeField] private GameObject soundscapeSetter;
        [SerializeField] public Color resurrectFogColour;
        [SerializeField] public ParticleSystem resurrectionFX;

        [Header("UI")]
        [SerializeField] public GameObject resurrectUIButton;
        private GameObject player;

        private void Awake()
        {
            soundscapeSetter = GameObject.FindWithTag("Soundscape");
            resurrectLocation = GameObject.FindWithTag("ResurrectLocation");
            player = GameObject.FindWithTag("Player");
            resurrectUIButton.SetActive(false);
        }


        private void Update()
        { 
            if(player.GetComponent<Health>().isDead == true)
            {
                resurrectUIButton.SetActive(true);
            }
        }

        public void EnableResurrectMode()
        {
            resurrectUIButton.SetActive(false);

            camera = Camera.main;
            volume = camera.GetComponent<Volume>();
            currentProfile = volume.profile;
            volume.profile = resurrectProfile;
            InsantiatePlayerDeadCopy();
            UpdatePlayerPostion();
            DisableComponents();
            IsNotDead();
            PlayResurrectAudio();
            UpdateFog();
        }

        public void DisableResurrectMode()
        {
            camera = Camera.main;
            volume = camera.GetComponent<Volume>();
            currentProfile = volume.profile;
            volume.profile = defaultProfile;
            StopResurrectAudio();
            soundscapeSetter.GetComponent<SoundscapeSetter>().worldSFX.worldSFXConfig.SoundtrackTrigger();
            ResetFog();
        }

        private void InsantiatePlayerDeadCopy()
        {
            GameObject deadPlayerCopy = Instantiate(player, player.transform.position, Quaternion.identity);
            deadPlayerCopy.GetComponent<CharacterSFX>().DeathVoiceSource.enabled = false;
            deadPlayerCopy.GetComponent<CharacterSFX>().VoiceSource.enabled = false;
            deadPlayerCopy.GetComponent<CharacterSFX>().BodySource.enabled = false;
            deadPlayerCopy.AddComponent<ResurrectPlayer>();
            deadPlayerCopy.AddComponent<SphereCollider>();
            deadPlayerCopy.GetComponent<CharacterSFX>().enabled = false;
            UpdateSphereColider(deadPlayerCopy);
            deadPlayerCopy.GetComponent<Animator>().Play("Death", 0, 1);
            deadPlayerCopy.GetComponent<PlayerController>().enabled = false;
            playerDeadBody = deadPlayerCopy;
        }

        private void UpdateSphereColider(GameObject deadPlayerCopy)
        {
            deadPlayerCopy.GetComponent<SphereCollider>().isTrigger = true;
            deadPlayerCopy.GetComponent<SphereCollider>().radius = 1;
            deadPlayerCopy.GetComponent<SphereCollider>().center = new Vector3(0, 0.4f, -1f);
        }

        private void UpdatePlayerPostion()
        {
            player.GetComponent<NavMeshAgent>().Warp(resurrectLocation.transform.position);
            player.transform.rotation = resurrectLocation.transform.rotation;
            player.GetComponent<Animator>().SetTrigger("Resurrect");
        }

        private void DisableComponents()
        {
            player.GetComponent<Health>().inSpiritWorld = true;
            player.GetComponent<Fighter>().enabled = false;
        }

        public void EnableComponents()
        {
            player.GetComponent<Health>().inSpiritWorld = false;
            player.GetComponent<Fighter>().enabled = true;
        }

        private void IsNotDead()
        {
            player.GetComponent<Health>().isDead = false;
        }

        private void UpdateFog()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = resurrectFogColour;
        }
        private void ResetFog()
        {
            RenderSettings.fogColor = defaultFogColour;
        }
        public void PlayResurrectAudio()
        {
            soundscapeSetter.GetComponent<SoundscapeSetter>().worldSFX.worldSFXConfig.soundtrackSource.Stop();
            resurrectAudioSource.Play();
        }
        public void StopResurrectAudio()
        {
            resurrectAudioSource.Stop();
        }
    }
}
