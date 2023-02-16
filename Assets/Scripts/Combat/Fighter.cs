using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponDamage;
        [SerializeField] float weaponRange = .5f;
        [SerializeField] float timeBetweenAttacks;

        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] Transform handTrasform = null;

        [SerializeField] AnimatorOverrideController attackOverrite = null;
        [SerializeField] AnimatorOverrideController enemyOverride = null;


        Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        bool canAttack;

        private void Start()
        {
            if(weaponPrefab != null)
            {
            SpawnWeapon();
            }
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
        private void SpawnWeapon()
        {

                Instantiate(weaponPrefab, handTrasform);
                Animator animator = GetComponent<Animator>();
                animator.runtimeAnimatorController = attackOverrite;
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("StopAttack");
            GetComponent<Animator>().SetTrigger("Attack");
        }

        void Hit()
        {
            if (target == null) return;
            GetRandomDamage();
            target.TakeDamage(weaponDamage);
            //target.GetComponent<Animator>().SetTrigger("Impact");
        }
        private float GetRandomDamage()
        {
            return weaponDamage = Random.Range(3, 7);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
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

