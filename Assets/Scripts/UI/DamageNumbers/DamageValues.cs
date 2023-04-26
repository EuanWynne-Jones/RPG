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
        SettingsHandler settingsHandler;

        private void Awake()
        {
            settingsHandler = FindObjectOfType<SettingsHandler>();
        }

        public void SpawnDamageNumbers(float damage, Transform followedTransform)
        {
            if (settingsHandler == null) settingsHandler = FindObjectOfType<SettingsHandler>();
            if (gameObject.tag == "Player" && settingsHandler.GetPlayerDamageNumbersStatus() == false) return;
            if (gameObject.tag != "Player" && settingsHandler.GetEnemyDamageNumbersStatus() == false) return;
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

