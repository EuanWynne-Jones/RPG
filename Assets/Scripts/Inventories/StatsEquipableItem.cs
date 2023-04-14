using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Stats;

namespace RPG.Inventories
{
    [CreateAssetMenu(menuName = ("Inventory/New Equipable Item with Modifiers"))]
    public class StatsEquipableItem : EquipableItem, IModifierProvider
    {
        [SerializeField]
        Modifier[] addativeModifier;
        [SerializeField]
        Modifier[] percentageModifier;


        [System.Serializable]
        struct Modifier
        {
            public Stat stat;
            public float value;
        }
        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            foreach (var modifier in addativeModifier)
            {
                if(modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            foreach (var modifier in percentageModifier)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}
