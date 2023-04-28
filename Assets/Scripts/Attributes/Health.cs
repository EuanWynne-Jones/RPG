using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using RPG.Utils;
using RPG.Control;
using UnityEngine.Events;


namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float restorePercentage = 100;
        private float healthToGain;
        [SerializeField] float restorePercentageOnResurrect = 70;
        [SerializeField] UnityEvent TakenDamage;
        [SerializeField] public UnityEvent OnDie;

        [HideInInspector]
        public bool isDead = false;
        [HideInInspector]
        public bool isInSpiritRealm = false;

        LazyValue<float> health;
        private void Awake()
        {
            health = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        private void Start()
        {
            health.ForceInit();
        }
        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RestoreHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RestoreHealth;
        }


        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
           
            //print(gameObject.name + "Took Damage:" + damage);
            health.value = Mathf.Max(health.value - damage, 0);
            TakenDamage.Invoke();
            GetComponent<CharacterSFX>().PlayVoiceGetHit();
            if(damage < 1)
            {
                damage = 0;
            }
            if (gameObject.tag != null)
            {
                GetComponent<DamageValues>().SpawnDamageNumbers(damage,this.gameObject.transform);
            }

            if (health.value == 0)
            { 
                DeathBehaviour();
                AwardExperience(instigator);
                
            }
        }

        public void Heal(float amountToHeal)
        {
            
            health.value = Mathf.Min(health.value + amountToHeal, GetMaxHealth());
        }

        public float GetHealth()
        {
            return health.value;
        }

        public float GetMaxHealth()
        {
            return health.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetMaxHealthBase()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.Experience));
        }

        public void RestoreHealth()
        {
            float restoreHealth = GetComponent<BaseStats>().GetStat(Stat.Health) * (restorePercentage/100);
            health.value = Mathf.Max(health.value, restoreHealth);
        }

        public void RestoreHealthOnResurrect(float restoreTime)
        {
            float restoreHealth = GetComponent<BaseStats>().GetStat(Stat.Health) * (restorePercentageOnResurrect / 100);
            StartCoroutine(SlerpHealthRestore(restoreHealth, restoreTime));
        }

        private IEnumerator SlerpHealthRestore(float restoreHealth, float restoreTime)
        {
            float time = 0;
            float initialHealth = health.value;
            while (time < restoreTime)
            {
                time += Time.deltaTime;
                float t = time / restoreTime;
                health.value = Mathf.Lerp(initialHealth, restoreHealth, Mathf.Sin(t * Mathf.PI * 0.5f));
                yield return null;
            }
            health.value = restoreHealth;
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return health.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public void DeathBehaviour()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<CharacterSFX>().PlayDeathScream();
            GetComponent<ActionSchedueler>().CancelCurrentAction();
            OnDie.Invoke();
        }

        public void DeathBehaviourOnRestore()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().Play("Death", 0, 1);
            GetComponent<ActionSchedueler>().CancelCurrentAction();
        }


        public object CaptureState()
        {
            return health.value;
        }

        public void RestoreState(object state)
        {
            health.value = (float)state;
            if (health.value == 0)
            {
                DeathBehaviourOnRestore();
            }
        }
    }


}
