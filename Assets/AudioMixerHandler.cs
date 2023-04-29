using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace RPG.UI
{
    public class AudioMixerHandler : MonoBehaviour
    {

      [SerializeField] public AudioMixer masterMixer;
      [SerializeField] public AudioMixerGroup master;
      [SerializeField] public AudioMixerGroup effects;
      [SerializeField] public AudioMixerGroup voice;
      [SerializeField] public AudioMixerGroup ui;
      [SerializeField] public AudioMixerGroup soundtrack;
      [SerializeField] public AudioMixerGroup ambience;
    }

}