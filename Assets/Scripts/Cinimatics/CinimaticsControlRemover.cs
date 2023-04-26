using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinimatics
{
    public class CinimaticsControlRemover : MonoBehaviour
    {
        public CanvasGroup hudGroup;
        GameObject player;
        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }
        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionSchedueler>().CancelCurrentAction();
            StartCoroutine(FadeOutHUD());
            player.GetComponent<PlayerController>().enabled = false;


        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
            StartCoroutine(FadeInHUD());
        }

        IEnumerator FadeOutHUD()
        {
            float elapsedTime = 0f;
            float fadeTime = 1f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
                hudGroup.alpha = alpha;
                yield return null;
            }

            hudGroup.alpha = 0f;
        }

        IEnumerator FadeInHUD()
        {
            float elapsedTime = 0f;
            float fadeTime = 1f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
                hudGroup.alpha = alpha;
                yield return null;
            }

            hudGroup.alpha = 1f;
        }

    }
}
