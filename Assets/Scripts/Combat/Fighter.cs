using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks;

        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Transform rightHandTrasform = null;
        [SerializeField] Transform leftHandTrasform = null;

        [SerializeField] AnimatorOverrideController enemyOverride = null;

        Weapon currentWeapon;
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        bool canAttack;

        private void Start()
        {

            EquipWeapon(defaultWeapon);
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

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                //Looks at the enemy when attacking
                transform.LookAt(target.transform);
                //This will trigger the attack animation event
                TriggerAttack();
                if(gameObject.tag != "Player" && enemyOverride != null)
                {
                    Animator animator = GetComponent<Animator>();
                    animator.runtimeAnimatorController = enemyOverride;
                }
                timeSinceLastAttack = 0;

            }
        }
        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTrasform,leftHandTrasform, animator);
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(currentWeapon.GetWeaponDamage());
            //target.GetComponent<Animator>().SetTrigger("Impact");
        }


        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
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

        private void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("StopAttack");
            GetComponent<Animator>().ResetTrigger("Attack");
        }



    }
}

