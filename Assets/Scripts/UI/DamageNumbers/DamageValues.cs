using DamageNumbersPro;
using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.UI
{
    public class DamageValues : MonoBehaviour
    {

        public DamageNumber numberPrefab;
        public PlayerSettings playerSettings;


        public void SpawnDamageNumbers(float damage, Transform followedTransform)
        {
            if (gameObject.tag == "Player" && !playerSettings.displayDamageOnPlayer) return;
            if (gameObject.tag != "Player" && !playerSettings.displayDamageOnNPCS) return;
            GetMaxMinDamage(followedTransform);
            DamageNumber damageNumber = numberPrefab.Spawn(transform.position, damage, followedTransform);
        }

        private void GetMaxMinDamage(Transform followedTransform)
        {
            float minDamage = followedTransform.gameObject.GetComponent<Fighter>().GetCurrentWeaponMinDamage();
            //Debug.Log(minDamage);
            float maxDamage = followedTransform.gameObject.GetComponent<Fighter>().GetCurrentWeaponMaxDamage();
            //Debug.Log(maxDamage);
            numberPrefab.colorByNumberSettings.fromNumber = minDamage;
            numberPrefab.colorByNumberSettings.toNumber = maxDamage;
        }

    }
}

