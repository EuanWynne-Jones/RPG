using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/ Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {


        [SerializeField] GameObject equiptPrefab = null;

        [Range(1f, 20f)]
        [SerializeField] float minWeaponDamage = 1f;
        [Range(1f, 20f)]
        [SerializeField] float maxWeaponDamage = 10f;

        float weaponDamage;
        [SerializeField] float weaponRange = .5f;

        [SerializeField] AnimatorOverrideController attackOverrite = null;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if(equiptPrefab != null)
            {
            Instantiate(equiptPrefab, handTransform);
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
