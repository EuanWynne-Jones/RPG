using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Control;

namespace RPG.Core
{
    public class CharacterSFX : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] SFXConfig config = null;

        [Header("Voice")]
        [SerializeField] private string VoiceSourceBoneName = "Head";
        [SerializeField] private AudioSource VoiceSource;
        [SerializeField] private List<AudioClip> VoiceGetHitClips;
        [SerializeField] private List<AudioClip> VoiceAttackingClips;

        [Header("Death Voice")]
        [SerializeField] private string DeathVoiceSourceBoneName = "Neck";
        [SerializeField] private AudioSource DeathVoiceSource;
        [SerializeField] private List<AudioClip> VoiceDeathClips;

        [Header("BodySounds")]
        [SerializeField] private string BodySourceBoneName = "Spine_03";
        [SerializeField] private AudioSource BodySource;
        [SerializeField] private List<AudioClip> BodyGetHitClips;

        [Header("Footsteps")]
        [SerializeField] private string FootstepSourceBoneName = "Spine_01";
        [SerializeField] private AudioSource FootstepSource;
        [SerializeField] private List<AudioClip> DefaultFootstepsClips;

        Fighter character = null;
        private void Awake()
        {
            if (character == null) character = gameObject.GetComponent<Fighter>();
        }
        private void Start()
        {
            SetupAudioSources();

            if (VoiceSource != null)
            {
                VoiceSource.loop = false;
            }

            if (BodySource != null)
            {
                BodySource.loop = false;
            }

            if (FootstepSource != null)
            {
                FootstepSource.loop = false;
            }
        }

        private List<AudioClip> VoiceDeathScreamClips
        {
            get
            {
                if (character == null) return VoiceDeathClips;
                else
                {
                    return VoiceDeathClips;
                }
            }
        }


        private AudioClip VoiceGetHitClip
        {
            get
            {
                if (VoiceGetHitClips.Count > 0)
                {
                    return VoiceGetHitClips[Random.Range(0, VoiceGetHitClips.Count)];
                }

                return null;
            }
        }

        private AudioClip BodyGetHitClip
        {
            get
            {
                if (BodyGetHitClips.Count > 0)
                {
                    return BodyGetHitClips[Random.Range(0, BodyGetHitClips.Count)];
                }

                return null;
            }
        }

        private AudioClip VoiceAttackingClip
        {
            get
            {
                if (VoiceAttackingClips.Count > 0)
                {
                    return VoiceAttackingClips[Random.Range(0, VoiceAttackingClips.Count)];
                }

                return null;
            }
        }

        private AudioClip VoiceDeathScreamClip
        {
            get
            {
                if (VoiceDeathScreamClips.Count > 0)
                {
                    return VoiceDeathScreamClips[Random.Range(0, VoiceDeathScreamClips.Count)];
                }

                return null;
            }
        }

        private AudioClip DefaultFootstepsClip
        {
            get
            {
                if (VoiceGetHitClips.Count > 0)
                {
                    return DefaultFootstepsClips[Random.Range(0, DefaultFootstepsClips.Count)];
                }

                return null;
            }
        }

        private void PlayVoice(AudioClip audioClip)
        {
            if (VoiceSource != null && audioClip != null)
            {
                if (VoiceSource.isPlaying) VoiceSource.Stop();
                VoiceSource.clip = audioClip;
                VoiceSource.Play();
            }
        }

        private void PlayDeathVoice(AudioClip audioClip)
        {
            if (DeathVoiceSource != null && audioClip != null)
            {
                if (DeathVoiceSource.isPlaying) DeathVoiceSource.Stop();
                DeathVoiceSource.clip = audioClip;
                DeathVoiceSource.Play();
            }
        }

        public void PlayBody(AudioClip audioClip)
        {
            if (BodySource != null && audioClip != null)
            {
                if (BodySource.isPlaying) BodySource.Stop();
                BodySource.clip = audioClip;
                BodySource.Play();
            }
        }

        private void PlayFootsteps(AudioClip audioClip)
        {
            if (FootstepSource != null && audioClip != null)
            {
                if (FootstepSource.isPlaying) FootstepSource.Stop();
                FootstepSource.clip = audioClip;
                FootstepSource.Play();

            }
            else if (FootstepSource == null)
            {
                SetupAudioSources();
            }
        }

        public void PlayVoiceAttacking()
        {
            if (VoiceAttackingClips.Count > 0)
            {
                PlayVoice(VoiceAttackingClip);
            }
        }

        public void PlayVoiceGetHit()
        {
            if (VoiceGetHitClips.Count > 0)
            {
                PlayVoice(VoiceGetHitClip);
            }
        }

        public void PlayBodyGetHit()
        {
            if (BodyGetHitClips.Count > 0)
            {
                PlayBody(BodyGetHitClip);
            }
        }

        public void PlayBodyGetHit(AudioClip audioClip)
        {
            if (audioClip != null)
            {
                PlayBody(audioClip);
            }
        }

        public void PlayFootstep()
        {
            if (DefaultFootstepsClips.Count > 0)
            {
                PlayFootsteps(DefaultFootstepsClip);
                // Debug.Log("Footstep sound play");
            }
        }

        public void PlayDeathScream()
        {
            if (VoiceDeathScreamClips.Count > 0)
            {
                PlayDeathVoice(VoiceDeathScreamClip);
                //Debug.Log("Death sound play");
            }
        }

        private void SetupAudioSources()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();

            if (VoiceSourceBoneName == "") Debug.LogError(name + " missing VoiceSourceBoneName.");
            if (DeathVoiceSourceBoneName == "") Debug.LogError(name + " missing DeathVoiceSourceBoneName.");
            if (BodySourceBoneName == "") Debug.LogError(name + " missing BodySourceBoneName.");
            if (FootstepSourceBoneName == "") Debug.LogError(name + " missing FootstepSourceBoneName.");


            if (transforms.Length > 0)
            {
                foreach (Transform t in transforms)
                {
                    if (VoiceSource == null && t.name == VoiceSourceBoneName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            VoiceSource = audioSource;
                        }
                        else
                        {
                            VoiceSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                    if (DeathVoiceSource == null && t.name == DeathVoiceSourceBoneName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            DeathVoiceSource = audioSource;
                        }
                        else
                        {
                            DeathVoiceSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                    if (BodySource == null && t.name == BodySourceBoneName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            BodySource = audioSource;
                        }
                        else
                        {
                            BodySource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                    if (FootstepSource == null && t.name == FootstepSourceBoneName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            FootstepSource = audioSource;
                        }
                        else
                        {
                            FootstepSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }
                }
            }

            if (VoiceSource == null) Debug.LogError(name + " VoiceSource not set.");
            if (DeathVoiceSource == null) Debug.LogError(name + " DeathVoiceSource not set.");
            if (BodySource == null) Debug.LogError(name + " BodySource not set.");
            if (FootstepSource == null) Debug.LogError(name + " FootstepSource not set.");

            if (VoiceSource != null)
            {
                VoiceSource.spatialBlend = config.SpacialBlend;
                VoiceSource.maxDistance = config.MaxDistance;
                VoiceSource.volume = config.CharacterVolume;
                if (isPlayer)
                {
                    VoiceSource.maxDistance = config.PlayerMaxDistance;
                }
                else
                {
                    VoiceSource.maxDistance = config.EnemyMaxDistance;
                }
            }

            if (DeathVoiceSource != null)
            {
                DeathVoiceSource.spatialBlend = config.SpacialBlend;
                DeathVoiceSource.maxDistance = config.MaxDistance;
                DeathVoiceSource.volume = config.CharacterVolume;
                if (isPlayer)
                {
                    DeathVoiceSource.maxDistance = config.PlayerMaxDistance;
                }
                else
                {
                    DeathVoiceSource.maxDistance = config.EnemyMaxDistance;
                }
            }

            if (BodySource != null)
            {
                BodySource.spatialBlend = config.SpacialBlend;
                BodySource.maxDistance = config.MaxDistance;
                BodySource.volume = config.CharacterVolume;
                if (isPlayer)
                {
                    BodySource.maxDistance = config.PlayerMaxDistance;
                }
                else
                {
                    BodySource.maxDistance = config.EnemyMaxDistance;
                }
            }

            if (FootstepSource != null)
            {
                FootstepSource.spatialBlend = config.SpacialBlend;
                FootstepSource.maxDistance = config.MaxDistance;
                FootstepSource.volume = config.FootstepVolume;

                if (isPlayer)
                {
                    FootstepSource.maxDistance = config.PlayerMaxDistance;
                }
                else
                {
                    FootstepSource.maxDistance = config.EnemyMaxDistance;
                }
            }
        }
        private bool isPlayer
        {
            get
            {
                PlayerController player = character.GetComponent<PlayerController>();
                return player != null;
            }
        }





    }




}



