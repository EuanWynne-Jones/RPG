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
        [SerializeField] float restorePercentage = 100;
        float health = -1f;
        bool isDead = false;

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RestoreHealth;
            if(health < 0)
            {
            health = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }



        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            //print(gameObject.name + "Took Damage:" + damage);
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
                AwardExperience(instigator);
            }
        }

        public float GetHealth()
        {
            return health;
        }

        public float GetMaxHealth()
        {
            return health / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private  void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.Experience));
        }

        private void RestoreHealth()
        {
            float restoreHealth = GetComponent<BaseStats>().GetStat(Stat.Health) * (restorePercentage/100);
            health = Mathf.Max(health, restoreHealth);
        }

        public float GetPercentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetStat(Stat.Health));
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
