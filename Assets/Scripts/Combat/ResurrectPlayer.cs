using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Core;
using RPG.Control;

namespace RPG.Combat
{
    public class ResurrectPlayer : MonoBehaviour
    {
        Resurrect resurrect = null;
        AudioSource AudioSource = null;
        private void Start()
        {
        resurrect = FindObjectOfType<Resurrect>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Health>().isDead = false;
                StartCoroutine(WaitForAnim(2.8f));
                other.GetComponent<Animator>().SetTrigger("Revive");
                StartCoroutine(WaitForFX(1f));

                other.GetComponent<Health>().RestoreHealth();
                DisableControl();



            }
        }
        IEnumerator WaitForFX(float animationTime)
        {
            yield return new WaitForSecondsRealtime(animationTime);
            GameObject player = GameObject.FindWithTag("Player");
            Instantiate(resurrect.resurrectionFX, player.transform.position, Quaternion.identity);

        }
        IEnumerator WaitForAnim(float animationTime)
        {
            yield return new WaitForSecondsRealtime(animationTime);
            resurrect = FindObjectOfType<Resurrect>();
            resurrect.DisableResurrectMode();
            Destroy(resurrect.playerDeadBody);
            resurrect.EnableComponents();
            EnableControl();

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
    }
}
