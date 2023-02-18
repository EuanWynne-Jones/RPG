using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/ Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {


        [SerializeField] public GameObject equiptPrefab = null;
        [SerializeField] public GameObject dropPrefab = null;

        [Range(1f, 20f)]
        [SerializeField] float minWeaponDamage = 1f;
        [Range(1f, 20f)]
        [SerializeField] float maxWeaponDamage = 10f;

        float weaponDamage;
        [SerializeField] float weaponRange = .5f;
        [SerializeField] public bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        [SerializeField] AnimatorOverrideController attackOverrite = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            
            if (equiptPrefab != null)
            {
                Transform handTrasform = GetHand(rightHand, leftHand);
                GameObject weapon = Instantiate(equiptPrefab, handTrasform);
                weapon.name = weaponName;

            }
            if (attackOverrite != null)
            {
                animator.runtimeAnimatorController = attackOverrite;
            }
        }


        public void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);

        }

        public Transform GetHand(Transform rightHand, Transform leftHand)
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

            return handTrasform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHand(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target,GetWeaponDamage());
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
