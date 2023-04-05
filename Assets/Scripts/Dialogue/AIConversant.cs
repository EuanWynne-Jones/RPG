using RPG.Control;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable, ISaveable
    {
        [SerializeField] string conversantName;
        [SerializeField] public Dialogue NPCDialogue = null;
        [SerializeField] public Transform conversantHead;
        [SerializeField] float dialogueTriggerDistance = 3.5f;

        PlayerController player;
        bool dialogueIntention;
        float lookSpeed = 1f;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
        }

        private void Start()
        {
            conversantHead = FindHeadTransform(this.gameObject);
        }
        private void Update()
        {
            float dist = GetDistancetoConversant();
            if (dist <= dialogueTriggerDistance && !player.GetComponent<PlayerConversant>().isInDialogue && dialogueIntention)
            {
                player.GetComponent<PlayerConversant>().StartDialogue(this, NPCDialogue);
                //this.transform.LookAt(player.transform);
                dialogueIntention = false;
            }
        }

        public Transform FindHeadTransform(GameObject parent)
        {
            Transform headTransform = null;

            foreach (Transform child in parent.transform)
            {
                if (child.name == "Head")
                {
                    headTransform = child;
                    break;
                }
                else
                {
                    headTransform = FindHeadTransform(child.gameObject);
                    if (headTransform != null)
                    {
                        break;
                    }
                }
            }

            return headTransform;
        }

        public void startRotation()
        {
            Coroutine LookCoroutine = null;
            if (LookCoroutine != null)
            {
                StopCoroutine(LookCoroutine);
            }
            LookCoroutine = StartCoroutine(LookAt());
        }

        private IEnumerator LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            float time = 0;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
                time += Time.deltaTime * lookSpeed;
                yield return null;
            }


        }

        private float GetDistancetoConversant()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }
        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public void ChangeDialogue(Dialogue OutcomeNPCdialogue)
        {
            NPCDialogue = OutcomeNPCdialogue;
        }
        public bool HandleRaycast(PlayerController callingController)
        {
            if (!enabled) return false;
            Mover mover = callingController.GetComponent<Mover>();
            if (NPCDialogue == null)
            {
                return false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                dialogueIntention = true;
                float dist = GetDistancetoConversant();
                if (dist <= dialogueTriggerDistance && !player.GetComponent<PlayerConversant>().isInDialogue && dialogueIntention)
                {

                    player.GetComponent<PlayerConversant>().StartDialogue(this, NPCDialogue);
                    dialogueIntention = false;
                }
                else
                {
                    mover.StartMoveAction(transform.position, 1f);
                }

            }
            return true;
           
        }

        public string GetName()
        {
            return conversantName;
        }

        public object CaptureState()
        {
            return NPCDialogue.name;
        }

        public void RestoreState(object state)
        {
            string dialogue = (string)state;
            Dialogue NPCDialogue = Resources.Load<Dialogue>(dialogue);
            ChangeDialogue(NPCDialogue);
        }


    }
}
