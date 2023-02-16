using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/ Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {


        [SerializeField] public GameObject equiptPrefab = null;

        [Range(1f, 20f)]
        [SerializeField] float minWeaponDamage = 1f;
        [Range(1f, 20f)]
        [SerializeField] float maxWeaponDamage = 10f;

        float weaponDamage;
        [SerializeField] float weaponRange = .5f;
        [SerializeField] bool isRightHanded = true;

        [SerializeField] AnimatorOverrideController attackOverrite = null;

        public void Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            if(equiptPrefab != null)
            {
                Transform handTrasform;
                if (isRightHanded)
                {
                    handTrasform = rightHand;
                }
                else
                {
                    handTrasform = leftHand; 
                }
                Instantiate(equiptPrefab, handTrasform);
                
            
            }
            if(attackOverrite != null)
            {
            animator.runtimeAnimatorController = attackOverrite;
            }
        }


        public float GetWeaponDamage()
        {
            weaponDamage = Random.Range(minWeaponDamage, maxWeaponDamage);
            return Mathf.RoundToInt(weaponDamage);
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }
    }

}
