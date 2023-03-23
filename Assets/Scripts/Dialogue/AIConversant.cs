using RPG.Control;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{
    public class AIConversant : MonoBehaviour, IRaycastable
    {
        [SerializeField] string conversantName;
        [SerializeField] public Dialogue NPCDialogue = null;
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
            if (NPCDialogue == null)
            {
                return false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this,NPCDialogue);

            }
            return true;
           
        }

        public string GetName()
        {
            return conversantName;
        }
    }
}
