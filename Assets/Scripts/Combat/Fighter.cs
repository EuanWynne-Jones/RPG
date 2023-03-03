using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using GameDevTV.Utils;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] float timeBetweenAttacks;

        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] public Transform rightHandTrasform = null;
        [SerializeField] public Transform leftHandTrasform = null;

        [HideInInspector]
        public LazyValue <Weapon> currentWeapon;
        float weaponDamage;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        bool canAttack;


        private void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
            
        }

        private void Start()
        {
            currentWeapon.ForceInit();

        }
        private Weapon SetupDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }




        private void Update()
        {
            
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;
            CheckDistanceAndMove();
        }



        private void CheckDistanceAndMove()
        {
            if (target != null && !GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 0.8f);

                
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();  
            }
        }

        public Health GetTarget()
        {
            return target;
        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                //Looks at the enemy when attacking
                transform.LookAt(target.transform);
                //This will trigger the attack animation event
                
                GetAttackOverrite(GetComponent<Animator>());
                TriggerAttack();
                timeSinceLastAttack = 0;

            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
            GetComponent<SFXHandler>().weaponSFX = GetComponentInChildren<WeaponSFX>();

        }

        private void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTrasform, leftHandTrasform, animator);
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        void Hit()
        {
            if (target == null) return;
            float damage = Mathf.Round(GetComponent<BaseStats>().GetStat(Stat.Damage));

            if (currentWeapon.value.HasProjectile())
            {
                currentWeapon.value.LaunchProjectile(rightHandTrasform, leftHandTrasform, target, gameObject, damage);
            }
            else
            {

                target.TakeDamage(gameObject, damage);
                GetComponent<CharacterSFX>().PlayVoiceGetHit();
            }
        }


        void Shoot()
        {
            Hit();
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.value.GetWeaponRange();
        }

        public void Attack(GameObject combatTarget)
        {

            GetComponent<ActionSchedueler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetWeaponDamage();
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return currentWeapon.value.GetPercentageBonus();
            }
        }
        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("StopAttack");
            GetComponent<Animator>().ResetTrigger("Attack");
        }
        public void GetAttackOverrite(Animator animator)
        {
            
            if (currentWeapon.value.attackOverrites == null || currentWeapon.value.attackOverrites.Count == 0)
            {
                return;
            }
            int randomIndex = Random.Range(0, currentWeapon.value.attackOverrites.Count);
            currentWeapon.value.attackOverrite = currentWeapon.value.attackOverrites[randomIndex];
            animator.runtimeAnimatorController = currentWeapon.value.attackOverrite;
            
           

        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }


    }
}

