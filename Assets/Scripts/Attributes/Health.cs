using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;


namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;
        bool isDead = false;

        private void Start()
        {
            health = GetComponent<BaseStats>().GetHealth();
        }

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

        public float GetPercentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetHealth());
        }


        public void DeathBehaviour()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionSchedueler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
            if (health == 0)
            {
                DeathBehaviour();
            }
        }
    }


}
