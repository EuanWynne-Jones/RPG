using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.SceneManagement
{


    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A,
            B,
            C,
            D
        }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime;
        [SerializeField] float fadeInTime;
        [SerializeField] float fadeWaitTime;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }
            private IEnumerator Transition()
            {
                DontDestroyOnLoad(gameObject);

                Fader fader = FindObjectOfType<Fader>();
                yield return fader.FadeOut(fadeOutTime);
                
                //Save Current Level
                SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
                wrapper.Save();

                yield return SceneManager.LoadSceneAsync(sceneToLoad);

                //Load Current Level
                wrapper.Load();

                Portal otherPortal = GetOtherPortal();
                UpdatePlayer(otherPortal);

                wrapper.Save();

                yield return new WaitForSeconds(fadeWaitTime);
                yield return fader.FadeIn(fadeInTime);


                Destroy(gameObject);
            }

            private void UpdatePlayer(Portal otherPortal)
            {
                GameObject player = GameObject.FindWithTag("Player");

                player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
                //player.transform.position = otherPortal.spawnPoint.position;
                player.transform.rotation = otherPortal.spawnPoint.rotation;

        }

            private Portal GetOtherPortal()
            {
                foreach (Portal portal in FindObjectsOfType<Portal>())
                {
                    if (portal == this) continue;
                    if (portal.destination != destination) continue;

                    return portal;
                }

                return null;
            }
       
    }
}