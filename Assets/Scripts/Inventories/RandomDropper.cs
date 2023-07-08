using RPG.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Inventories
{
    public class RandomDropper : ItemDropper
    {

        //CONFIG DATA
        [SerializeField] float scatterDistance = 2f;
        [SerializeField] DropLibrary dropLibary;

        //CONSTANTS
        const int attempts = 30;

        public void RandomDrop()
        {
            var baseStats = GetComponent<BaseStats>();
            
            var drops = dropLibary.GetRandomDrops(baseStats.GetLevel());
            foreach (var drop in drops)
            {
                DropItem(drop.item, drop.number);
            }
        }
        protected override Vector3 GetDropLocation()
        {
            for( int i =0; i < attempts; i++)
            {
                Vector3 randomPoint = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + Random.insideUnitSphere * scatterDistance;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit,0.1f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }
            return transform.position;
        }
    }
}
