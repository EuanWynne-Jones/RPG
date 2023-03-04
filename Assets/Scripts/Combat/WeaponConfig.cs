using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/ Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {


        [SerializeField] public Weapon equiptPrefab = null;
        [SerializeField] public GameObject dropPrefab = null;

        [Range(1f, 20f)]
        [SerializeField] public float minWeaponDamage = 1f;
        [Range(1f, 20f)]
        [SerializeField] public float maxWeaponDamage = 10f;
        [SerializeField] public float PercentageBonus = 0f;

        float weaponDamage;
        [SerializeField] float weaponRange = .5f;
        [SerializeField] public bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        [SerializeField] public List<AnimatorOverrideController> attackOverrites = null;
        [SerializeField] public AnimatorOverrideController attackOverrite = null;


        const string weaponName = "Weapon";

        

        public Weapon Spawn(Transform rightHand,Transform leftHand, Animator animator)
        {
            Weapon weapon = null;

            if (equiptPrefab != null)
            {
                Transform handTrasform = GetHand(rightHand, leftHand);
                weapon = Instantiate(equiptPrefab, handTrasform);
                weapon.gameObject.name = weaponName;
                


            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (attackOverrite != null)
            {
                animator.runtimeAnimatorController = attackOverrite;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            return weapon;
            

        }

        public void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.gameObject.name = "DESTROYING";
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

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance = Instantiate(projectile, GetHand(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target,instigator, calculatedDamage);
        }

        public float GetWeaponDamage()
        {
            weaponDamage = Random.Range(minWeaponDamage, maxWeaponDamage);
            return Mathf.RoundToInt(weaponDamage);
        }

        public float GetPercentageBonus()
        {
            return PercentageBonus;
        }
        public float GetWeaponRange()
        {
            return weaponRange;
        }

    }

}
