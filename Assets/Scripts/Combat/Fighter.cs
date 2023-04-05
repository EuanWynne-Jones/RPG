using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;
using RPG.Stats;
using RPG.Utils;
using RPG.Inventories;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks;
        [SerializeField] WeaponConfig defaultWeapon = null;
        [SerializeField] public Transform rightHandTrasform = null;
        [SerializeField] public Transform leftHandTrasform = null;

        [HideInInspector]
        public WeaponConfig currentWeaponConfig;
        [HideInInspector]
        public LazyValue<Weapon> currentWeapon;
        public Health target;

        Equipment equipment;
        float timeSinceLastAttack = Mathf.Infinity;
        float weaponDamage;
        bool canAttack;

        private void Awake()
        {

            currentWeaponConfig = defaultWeapon;
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
            equipment = GetComponent<Equipment>();
            if (equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }
        }

        
        private Weapon SetupDefaultWeapon()
        {
            return AttachWeapon(defaultWeapon);
        }

        private void Start()
        {
            currentWeapon.ForceInit();

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
            if (target != null && !GetIsInRange(target.transform))
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
        public void EquipWeapon(WeaponConfig weapon)
        {
            currentWeaponConfig = weapon;
            currentWeapon.value = AttachWeapon(weapon);
            GetComponent<SFXHandler>().weaponSFX = GetComponentInChildren<WeaponSFX>();

        }

        private void UpdateWeapon()
        {
            currentWeaponConfig.DestroyOldWeapon(rightHandTrasform, leftHandTrasform);
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as WeaponConfig;
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                EquipWeapon(weapon);
            }
        }
        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            Animator animator = GetComponent<Animator>();
            return weapon.Spawn(rightHandTrasform, leftHandTrasform, animator);
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
            GetComponent<CharacterSFX>().PlayVoiceAttacking();
        }

        void Hit()
        {
            if (target == null) return;
            float damage = Mathf.Round(GetComponent<BaseStats>().GetStat(Stat.Damage)) + currentWeaponConfig.GetWeaponDamage(); 

            if(currentWeapon.value != null)
            {
                currentWeapon.value.OnHit();
            }

            if (currentWeaponConfig.HasProjectile())
            {
                currentWeaponConfig.LaunchProjectile(rightHandTrasform, leftHandTrasform, target, gameObject, damage);
            }
            else
            {

                target.TakeDamage(gameObject, damage);
                
            }
        }


        void Shoot()
        {
            Hit();
        }


        private bool GetIsInRange(Transform targetTransform)
        {
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponConfig.GetWeaponRange();
        }

        public void Attack(GameObject combatTarget)
        {

            GetComponent<ActionSchedueler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            if (!GetComponent<Mover>().CanMoveTo(combatTarget.transform.position) && !GetIsInRange(combatTarget.transform) ) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("StopAttack");
            GetComponent<Animator>().ResetTrigger("Attack");
        }
        public void GetAttackOverrite(Animator animator)
        {
            
            if (currentWeaponConfig.attackOverrites == null || currentWeaponConfig.attackOverrites.Count == 0)
            {
                return;
            }
            int randomIndex = Random.Range(0, currentWeaponConfig.attackOverrites.Count);
            currentWeaponConfig.attackOverrite = currentWeaponConfig.attackOverrites[randomIndex];
            animator.runtimeAnimatorController = currentWeaponConfig.attackOverrite;
            
           

        }

        public object CaptureState()
        {
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponConfig weapon = Resources.Load<WeaponConfig>(weaponName);
            EquipWeapon(weapon);
        }


    }
}

