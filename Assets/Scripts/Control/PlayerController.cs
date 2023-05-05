using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine.EventSystems;
using System;
using UnityEngine.AI;
using RPG.Inventories;
using UnityEngine.UI;
using RPG.Combat;
using RPG.Core;
using RPG.Dialogue;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour, IAction
    {
        Health health;
        //public Transform lastEnemy = null;
        
        [Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavmeshProjectionDistance = 1f;
        [SerializeField] float raycastRadius = 1f;

        bool isDraggingUI = false;
        bool CanInteractWithMovement = true;

        [HideInInspector]
        public GameObject currentHitObject = null;

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            CheckSpecialAbilityKeys();
            if (InteractWithUI()) return;


            if (InteractWithComponent()) return;

            
            
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            if (InteractWithMovement()) 
            {
                return;
            }
            SetCursor(CursorType.None);

        }



        private bool InteractWithComponent()
        {
            if (GetComponent<Health>().isInSpiritRealm) return false;
            if (!GetComponent<PlayerConversant>().isInDialogue)
            {
                RaycastHit[] hits = RayCastAllSorted();
                foreach (RaycastHit hit in hits)
                {
                    IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();

                    // Check if the hit object has an Outline component
                    Outline outline = hit.transform.GetComponent<Outline>();

                    // Turn off the outline for the previous hit object
                    if (currentHitObject != null && currentHitObject != hit.transform.gameObject)
                    {
                        Outline previousOutline = currentHitObject.GetComponent<Outline>();
                        if (previousOutline != null) previousOutline.enabled = false;
                    }

                    // Turn on the outline for the current hit object
                    if (outline != null)
                    {
                        outline.enabled = true;
                        currentHitObject = hit.transform.gameObject;
                    }

                    // Handle the raycastable object as before
                    foreach (IRaycastable raycastable in raycastables)
                    {
                        if (raycastable.HandleRaycast(this))
                        {
                            SetCursor(raycastable.GetCursorType());
                            return true;
                        }
                    }
                }
                if (hits.Length == 0 && currentHitObject != null)
                    {
                        Outline previousOutline = currentHitObject.GetComponent<Outline>();
                        if (previousOutline != null) previousOutline.enabled = false;
                    }
            }
            return false;
        }

        RaycastHit[] RayCastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i ++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool InteractWithUI()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDraggingUI = false;
            }
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isDraggingUI = true;
                }
                SetCursor(CursorType.UI);
                return true;
            }
            if (isDraggingUI)
            {
                return true;
            }
            return false;
            
        }

        private bool InteractWithMovement()
        {
            if (CanInteractWithMovement)
            {
                Vector3 target;
                bool hasHit = RaycastNavmesh(out target);
                if (hasHit)
                {
                    if (!GetComponent<Mover>().CanMoveTo(target)) return false;
                    if (Input.GetMouseButton(0))
                    {
                        GetComponent<Mover>().StartMoveAction(target,1f);
                    }
                    SetCursor(CursorType.Movement);
                    return true;
                }
            
            }
                return false;
        }
        private bool RaycastNavmesh(out Vector3 target)
        {
            target = new Vector3();
            RaycastHit hit;
            bool hashit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hashit) return false;
            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavmeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;



            return true;
        }

       

        private void SetCursor(CursorType cursor)
        {
            CursorMapping mapping = GetCursorMapping(cursor);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private void CheckSpecialAbilityKeys()
        {
            var actionStore = GetComponent<ActionStore>();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                actionStore.Use(0, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                actionStore.Use(1, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                actionStore.Use(2, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                actionStore.Use(3, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                actionStore.Use(4, gameObject);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                actionStore.Use(5, gameObject);
            }

        }
        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach(CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        public void EnableMovement()
        {
            CanInteractWithMovement = true;
        }

        public void Cancel()
        {
            CanInteractWithMovement = false;
        }
    }

}

