using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1;
        Health target = null;
        float damage = 0;

        private void Update()
        {
            if (target == null) return;
            transform.LookAt(GetAimLocation());
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

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return; 
            target.TakeDamage(damage);
            //target.GetComponent<Animator>().SetTrigger("Impact");
            StartCoroutine(WaitBeforeDestory(0.3f));
            
        }

        private IEnumerator WaitBeforeDestory(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(gameObject);
        }
    }
}
