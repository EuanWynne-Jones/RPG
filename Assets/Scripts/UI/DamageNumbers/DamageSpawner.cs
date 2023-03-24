using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamageSpawner : MonoBehaviour
    {
        [SerializeField] DamageNumbers damageNumbersPrefab = null;
        private void Start()
        {
            Spawn(10);
        }

        public void Spawn(float damageAmount)
        {
            DamageNumbers instance = Instantiate<DamageNumbers>(damageNumbersPrefab, transform);
        }
    }
}
