using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Inventories;
using RPG.Stats;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName ="Weapon", menuName = "Weapons/ Make New Weapon", order = 0)]
    public class WeaponConfig : EquipableItem, IModifierProvider
    {

        [Header("Prefab Information")]
        [SerializeField] public Weapon equiptPrefab = null;
        [SerializeField] public GameObject dropPrefab = null;

        [Header("Damage")]
        [Range(1f, 20f)]
        [SerializeField] public float minWeaponDamage = 1f;
        [Range(1f, 20f)]
        [SerializeField] public float maxWeaponDamage = 10f;
        [Header("Bonuses")]
        [SerializeField] public float PercentageBonus = 0f;

        [Header("Weapon Range")]
        [SerializeField] float weaponRange = .5f;

        [Header("Equip Hand")]
        [SerializeField] public bool isRightHanded = true;

        [Header("Projectile if applicable")]
        [SerializeField] Projectile projectile = null;

        [Header("Animations")]
        [SerializeField] public AnimatorOverrideController WeaponOverriteController = null;
        [SerializeField] public List<AnimatorOverrideController> AdditionalAttackOverrites = null;


        float weaponDamage;
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
            if (WeaponOverriteController != null)
            {
                animator.runtimeAnimatorController = WeaponOverriteController;
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
            weaponDamage = Random.Range(minWeaponDamage, maxWeaponDamage) + GetPercentageBonus()/100;
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

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
            {
                 
                yield return  weaponDamage;
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat == Stat.Damage)
            {
                yield return PercentageBonus;
            }
        }
    }

}
