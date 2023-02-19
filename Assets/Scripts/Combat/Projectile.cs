using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1;
        [SerializeField] float projectileLifetime = 3f;
        [SerializeField ]bool followToPlayer = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float lifetimeAfterImpact = 0.1f;
        [SerializeField] GameObject[] destroyOnHit = null;

        Health target = null;
        float damage = 0;

        private void Start()
        {
            if(!followToPlayer) 
            {
            transform.LookAt(GetAimLocation());
            }
        }
        private void Update()
        {
            if (target == null) return;
            if (followToPlayer && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
            
  
            

        }

        private Vector3 GetAimLocation()
        {

            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }
        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
            Destroy(gameObject, projectileLifetime);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return;
            target.TakeDamage(damage);
            projectileSpeed = 0;
            if (hitEffect != null)
            {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            foreach (GameObject toDestory in destroyOnHit)
            {  
                 Destroy(toDestory);
            }
            Destroy(gameObject,lifetimeAfterImpact);
            
        }


    }
}
