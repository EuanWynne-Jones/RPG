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
        [SerializeField] public Transform resurrectLocation;
        [SerializeField] public AudioSource resurrectAudioSource;
        [SerializeField] private SoundscapeSetter soundscapeSetter;
        [SerializeField] public Color resurrectFogColour;
        [SerializeField] public ParticleSystem resurrectionFX;

        [Header("UI")]
        [SerializeField] private Button resurrectUIButton;

        private void Start()
        {
            resurrectUIButton.enabled = true;
            soundscapeSetter = FindObjectOfType<SoundscapeSetter>();
        }

        public void EnableResurrectMode()
        {
            resurrectUIButton.enabled = false;
            
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
            soundscapeSetter.worldSFX.worldSFXConfig.SoundtrackTrigger();
            ResetFog();
        }

        private void InsantiatePlayerDeadCopy()
        {
            GameObject player = GameObject.FindWithTag("Player");
            GameObject deadPlayerCopy = Instantiate(player, player.transform.position, Quaternion.identity);
            deadPlayerCopy.AddComponent<ResurrectPlayer>();
            deadPlayerCopy.AddComponent<SphereCollider>();
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
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(resurrectLocation.position);
            player.transform.rotation = resurrectLocation.rotation;
            player.GetComponent<Animator>().SetTrigger("Resurrect");
        }

        private void DisableComponents()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Health>().inSpiritWorld = true;
            player.GetComponent<Fighter>().enabled = false;
        }

        public void EnableComponents()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Health>().inSpiritWorld = false;
            player.GetComponent<Fighter>().enabled = true;
        }

        private void IsNotDead()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Health>().isDead = false;
        }

        private void UpdateFog()
        {
            RenderSettings.fogColor = resurrectFogColour;
        }
        private void ResetFog()
        {
            RenderSettings.fogColor = defaultFogColour;
        }
        public void PlayResurrectAudio()
        {
            soundscapeSetter.worldSFX.worldSFXConfig.soundtrackSource.Stop();
            resurrectAudioSource.Play();
        }
        public void StopResurrectAudio()
        {

            soundscapeSetter.worldSFX.worldSFXConfig.soundtrackSource.Stop();
            resurrectAudioSource.Stop();
        }
    }
}
