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
        [SerializeField] InventoryItem[] dropLibary;
        [SerializeField] int numberOfDrops = 2;
        //CONSTANTS
        const int attempts = 30;

        public void RandomDrop()
        {
            for(int i= 0; i < numberOfDrops; i++)
            {
                var item = dropLibary[Random.Range(0, dropLibary.Length)];
                DropItem(item, 1);
            }
        }
        protected override Vector3 GetDropLocation()
        {
            for( int i =0; i < attempts; i++)
            {
                Vector3 randomPoint = transform.position + Random.insideUnitSphere * scatterDistance;
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
