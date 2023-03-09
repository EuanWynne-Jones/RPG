using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{


    public class SFXFader : MonoBehaviour
    {
        AudioSource audioSource;
        Coroutine currentlyActiveFade = null;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = .3f;
        }

        public void FadeOutImmediate()
        {
            audioSource.volume = 0;
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(.3f, time);
        }
        public Coroutine FadeOut(float time)
        {
            return Fade(0, time);
        }

        public Coroutine Fade(float target, float time)
        {
            if (currentlyActiveFade != null)
            {
                audioSource = GetComponent<AudioSource>();
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeRoutine(target, time));
            return currentlyActiveFade;
        }

        private IEnumerator FadeRoutine(float target, float time)
        {
            while (!Mathf.Approximately(audioSource.volume, target))
            {
                audioSource.volume = Mathf.MoveTowards(audioSource.volume, target, Time.deltaTime / time);
                yield return null;
            }
        }

    }
}

