using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;


namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 100f;
        bool isDead = false;
       

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {

            health = Mathf.Max(health - damage, 0);
            if(gameObject.tag != "Player")
            {
                foreach (Transform t in transform)
                {
                    if (t.GetComponent<DamageNumbers>() != null)
                    {
                        t.GetComponent<DamageNumbers>().StartTextPopup(damage);
                    }
                }
            }

            if (health == 0)
            {
                DeathBehaviour();
            }
        }


        public void DeathBehaviour()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionSchedueler>().CancelCurrentAction();
        }
    }


}
