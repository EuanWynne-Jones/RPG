using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RPG.Core;
using System;
using RPG.Utils;

namespace RPG.Dialogue
{
   
    public class DialogueNode: ScriptableObject
    {
        [SerializeField] bool isPlayerSpeaking = false;
        [SerializeField] private string dialogueText;
        [SerializeField] private List<string> dialogueChildren = new List<string>();
        [SerializeField] private Rect nodePosition = new Rect(0,0,200,100);
        [SerializeField] string onEntryAction;
        [SerializeField] string onExitAction;
        [SerializeField] Condition condition;



#if UNITY_EDITOR
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            nodePosition.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void SetDialogueText(string newText)
        {
            if(newText != dialogueText)
            {
                Undo.RecordObject(this, "Update Dialogue Text");
                dialogueText = newText;
                EditorUtility.SetDirty(this);
            }

        }
        public void SetPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }

        public void AddDialogueChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            dialogueChildren.Add(childID);
            EditorUtility.SetDirty(this);
        }

        public void RemoveDialogueChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            dialogueChildren.Remove(childID);
            EditorUtility.SetDirty(this);
        }
#endif
        public string GetDialogueText()
        {
            return dialogueText;
        }

        public List<string> GetDialogueChildren()
        {
            return dialogueChildren;
        }
        public Rect GetNodePosition()
        {
            return nodePosition;
        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        public string GetOnEntryAction()
        {
            return onEntryAction;
        }
        public string GetOnExitAction()
        {
            return onExitAction;
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }
    }
}

