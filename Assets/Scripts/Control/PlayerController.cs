using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine.EventSystems;
using System;
using UnityEngine.AI;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;



        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }
        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavmeshProjectionDistance = 1f;
        [SerializeField] float maxNavPathLength = 40f;

        private void Awake()
        {
            health = GetComponent<Health>();
        }
        private void Update()
        {
            if (InteractWithComponent()) return;
            if (interactWithUI()) return;
            
            if (health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            if(InteractWithMovement()) return;
            SetCursor(CursorType.None);

        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RayCastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                {
                    foreach(IRaycastable raycastable in raycastables)
                    {
                        if (raycastable.HandleRaycast(this))
                        {
                            SetCursor(raycastable.GetCursorType());
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        RaycastHit[] RayCastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i ++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private bool interactWithUI()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false;
            
        }




        private bool InteractWithMovement()
        {
            //Ray ray = GetMouseRay();
            //RaycastHit hit;
            //bool hashit = Physics.Raycast(ray, out hit);
            Vector3 target;
            bool hasHit = RaycastNavmesh(out target);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target,1f);
                }
                SetCursor(CursorType.Movement);
                return true;
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

            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
            if (!hasPath) return false;
            if (path.status != NavMeshPathStatus.PathComplete) return false;
            if (GetPathLength(path) > maxNavPathLength) return false;

            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for(int i = 0; i < path.corners.Length -1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return 0;
        }

        private void SetCursor(CursorType cursor)
        {
            CursorMapping mapping = GetCursorMapping(cursor);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
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
    }

}

