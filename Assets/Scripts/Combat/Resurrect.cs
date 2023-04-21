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
using static UnityEditor.Rendering.BuiltIn.ShaderGraph.BuiltInBaseShaderGUI;

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
        [SerializeField] public float spiritRealmFadeTime = 0.2f;

        [Header("UI")]
        [SerializeField] public GameObject resurrectUIButton;
        [SerializeField] public GameObject reviveUIButton;
        private GameObject player;

        [Header("NPC")]
        [SerializeField] GameObject spiritGuardian;

        [HideInInspector]
        public Material playerMaterial;
        private List<Material> originalMaterials = new List<Material>();

        private void Awake()
        {
            soundscapeSetter = GameObject.FindWithTag("Soundscape");
            resurrectLocation = GameObject.FindWithTag("ResurrectLocation");
            player = GameObject.FindWithTag("Player");
            playerMaterial = player.GetComponentInChildren<SkinnedMeshRenderer>().material;
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
            RestoreOriginalMaterials();
            Instantiate(resurrectionFX, player.transform.position, Quaternion.identity);
        }

        private void ChangeMaterial()
        {
            // Get all MeshRenderers and SkinnedMeshRenderers in children of player object
            MeshRenderer[] meshRenderers = player.GetComponentsInChildren<MeshRenderer>();
            SkinnedMeshRenderer[] skinnedMeshRenderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();

            // Loop through the MeshRenderers and apply the transparent material
            foreach (MeshRenderer renderer in meshRenderers)
            {
                ApplyTransparentMaterial(renderer);
            }

            // Loop through the SkinnedMeshRenderers and apply the transparent material
            foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
            {
                ApplyTransparentMaterial(renderer);
            }
        }

        void ApplyTransparentMaterial(Renderer renderer)
        {
            // Create a new instance of the original material
            Material originalMaterial = renderer.material;
            originalMaterials.Add(originalMaterial);

            // Create a new instance of the player material
            Material transparentMaterial = new Material(playerMaterial);

            // Change surface type to transparent
            transparentMaterial.SetFloat("_Surface", (float)SurfaceType.Transparent);

            // Change blending mode to additive
            transparentMaterial.SetInt("_BlendMode", (int)UnityEditor.BaseShaderGUI.BlendMode.Additive);

            // Enable emission and set it to black
            transparentMaterial.EnableKeyword("_EMISSION");
            transparentMaterial.SetColor("_EmissionColor", Color.black);

            // Set the shader to use the URP Lit shader
            transparentMaterial.shader = Shader.Find("Universal Render Pipeline/Lit");

            // Apply the new transparent material to the renderer
            renderer.material = transparentMaterial;
        }

        private void RestoreOriginalMaterials()
        {
            // Get all MeshRenderers and SkinnedMeshRenderers in children of player object
            MeshRenderer[] meshRenderers = player.GetComponentsInChildren<MeshRenderer>();
            SkinnedMeshRenderer[] skinnedMeshRenderers = player.GetComponentsInChildren<SkinnedMeshRenderer>();

            // Loop through the MeshRenderers and restore the original material
            foreach (MeshRenderer renderer in meshRenderers)
            {
                RestoreOriginalMaterial(renderer);
            }

            // Loop through the SkinnedMeshRenderers and restore the original material
            foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
            {
                RestoreOriginalMaterial(renderer);
            }
        }

        void RestoreOriginalMaterial(Renderer renderer)
        {
            // Restore the original material on the renderer
            renderer.material = playerMaterial;
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
            player.GetComponent<Animator>().ResetTrigger("Resurrect");
            DisableResurrectMode();
            EnableComponents();
            Destroy(playerDeadBody);
            EnableControl();

        }
    }
}
