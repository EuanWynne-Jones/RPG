using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
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
        private GameObject player;

        [Header("Spirit-Mode")]
        [SerializeField] public VolumeProfile resurrectProfile;
        [SerializeField] public GameObject resurrectLocation;
        [SerializeField] public AudioSource resurrectAudioSource;
        [SerializeField] private GameObject soundscapeSetter;
        [SerializeField] public Color resurrectFogColour;
        [SerializeField] public ParticleSystem resurrectionFX;
        [SerializeField] public float spiritRealmFadeTime = 0.2f;

        [Header("UI")]
        [SerializeField] public GameObject resurrectUIButton;
        [SerializeField] public GameObject reviveUIButton;

        [Header("NPC")]
        [SerializeField] GameObject spiritGuardian;


        [Header("Spirit Material")]
        private Material playerMaterial;
        [SerializeField] Material transparentMaterial;

        private void Awake()
        {
            soundscapeSetter = GameObject.FindWithTag("Soundscape");
            resurrectLocation = GameObject.FindWithTag("ResurrectLocation");
            player = GameObject.FindWithTag("Player");
            playerMaterial = player.GetComponentInChildren<Renderer>().material;
            resurrectUIButton.SetActive(false);
            reviveUIButton.SetActive(false);
            spiritGuardian = GameObject.FindWithTag("SpiritGuardian");
            spiritGuardian.SetActive(false);
        }


        private void Update()
        { 
            if(player.GetComponent<Health>().isDead == true)
            {
                resurrectUIButton.SetActive(true);
            }
        }

        public void onGuardianRevive()
        {
            player.GetComponent<Health>().isDead = false;
            player.GetComponent<Animator>().SetTrigger("Resurrect");
            DisableControl();
            StartCoroutine(WaitToResurrect(2.2f));
            RestoreOriginalMaterials();
            Instantiate(resurrectionFX, player.transform.position, Quaternion.identity);
        }
        public void onReviveButton()
        {
            reviveUIButton.SetActive(false);
            player.GetComponent<Health>().isDead = false;
            player.GetComponent<Animator>().SetTrigger("Resurrect");
            DisableControl();
            StartCoroutine(WaitToResurrect(2.2f));
            Instantiate(resurrectionFX, player.transform.position, Quaternion.identity);
        }

        private void ChangeMaterial()
        {
            Renderer[] renderers = player.GetComponentsInChildren<Renderer>(); // get all the renders in the player's game object children

            foreach (Renderer renderer in renderers)
            {
                Material originalMaterial = renderer.material; // save the original material

                // create a new material based on the transparentMaterial with the base map and emission map from the original material
                Material newMaterial = new Material(transparentMaterial);
                newMaterial.SetTexture("_BaseMap", originalMaterial.GetTexture("_BaseMap"));
                newMaterial.SetTexture("_EmissionMap", originalMaterial.GetTexture("_EmissionMap"));

                renderer.material = newMaterial; // set the new material to the renderer
            }
        }


        private void RestoreOriginalMaterials()
        {
            Renderer[] renderers = player.GetComponentsInChildren<Renderer>(); // get all the renders in the player's game object children

            foreach (Renderer renderer in renderers)
            {
                if (playerMaterial != null) // make sure playerMaterial is not null
                {
                    renderer.material = playerMaterial; // set the player's original material to the renderer
                }
            }
        }



        public void EnableResurrectMode()
        {
            StartCoroutine(Resurrection());
        }

        private IEnumerator Resurrection()
        {
            Fader fader = FindObjectOfType<Fader>();
            Image faderImage = fader.GetComponent<Image>();
            faderImage.color = Color.white;
            yield return (fader.FadeOut(spiritRealmFadeTime));
            resurrectUIButton.SetActive(false);
            camera = Camera.main;
            volume = camera.GetComponent<Volume>();
            currentProfile = volume.profile;
            volume.profile = resurrectProfile;
            InsantiatePlayerDeadCopy();
            spiritGuardian.SetActive(true);
            ChangeMaterial();
            UpdatePlayerPostion();
            DisableComponents();
            IsNotDead();
            PlayResurrectAudio();
            UpdateFog();
            yield return fader.FadeIn(spiritRealmFadeTime);
            faderImage.color = Color.black ;
        }

        public void DisableResurrectMode()
        {
            reviveUIButton.SetActive(false);
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
            player.GetComponent<Health>().isInSpiritRealm = true;
            player.GetComponent<Fighter>().enabled = false;
        }

        public void EnableComponents()
        {
            player.GetComponent<Health>().isInSpiritRealm = false;
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
        void DisableControl()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionSchedueler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl()
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
        }
        IEnumerator WaitToResurrect(float animTime)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Health>().RestoreHealthOnResurrect(animTime);
            yield return new WaitForSecondsRealtime(animTime);
            RestoreOriginalMaterials();
            player.GetComponent<Animator>().ResetTrigger("Resurrect");
            DisableResurrectMode();
            EnableComponents();
            Destroy(playerDeadBody);
            EnableControl();

        }
    }
}
