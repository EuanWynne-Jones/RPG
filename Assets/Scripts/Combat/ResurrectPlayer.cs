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
        private void Start()
        {
        resurrect = FindObjectOfType<Resurrect>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Health>().isDead = false;
                other.GetComponent<Animator>().SetTrigger("Revive");
                DisableControl();
                StartCoroutine(WaitToResurrect(2.2f));
                Instantiate(resurrect.resurrectionFX, other.transform.position, Quaternion.identity);
                



            }
        }
        IEnumerator WaitToResurrect(float animTime)
        {
            GameObject player = GameObject.FindWithTag("Player");
            yield return new WaitForSecondsRealtime(animTime);
            resurrect = FindObjectOfType<Resurrect>();
            resurrect.DisableResurrectMode();
            Destroy(resurrect.playerDeadBody);
            resurrect.EnableComponents();
            player.GetComponent<Health>().RestoreHealth();
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
