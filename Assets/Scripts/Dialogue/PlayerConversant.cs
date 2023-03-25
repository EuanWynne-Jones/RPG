using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] string playerName;
        Dialogue currentDialogue;
        DialogueNode currendNode = null;
        bool isChoosing;
        public event Action onConversationUpdated;

        AIConversant currentConversant = null;
        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            currentConversant = newConversant;
            currentDialogue = newDialogue;
            currendNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationUpdated();
        }
        public bool isActive()
        {
            return currentDialogue != null;
        }

        public void Quit()
        {
            currentDialogue = null;
            TriggerExitAction();
            currendNode = null;
            isChoosing = false;
            currentConversant = null;
            onConversationUpdated();
        }
        public bool IsChoosing()
        {
            return isChoosing;
        }
        public string GetText()
        {
            if(currendNode == null)
            {
                return "";
            }
            return currendNode.GetDialogueText();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(currentDialogue.GetPlayerChildren(currendNode));
        }

        public string GetCurrentConversentName()
        {
            if (isChoosing)
            {
                return playerName;
            }
            return currentConversant.GetName();
        }
        public void SelectChoice(DialogueNode chosenNode)
        {

            currendNode = chosenNode;
            TriggerEnterAction();
            isChoosing = false;
            Next();
            
        }
        public void Next()
        {
            int numPlayerResponces = FilterOnCondition(currentDialogue.GetPlayerChildren(currendNode)).Count();
            if(numPlayerResponces > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                return;
            }
            DialogueNode[] dialogueChildren = FilterOnCondition(currentDialogue.GetAIChildren(currendNode)).ToArray();
            int randomIndex = UnityEngine.Random.Range(0, dialogueChildren.Count());
            TriggerExitAction();
            currendNode = dialogueChildren[randomIndex];
            TriggerEnterAction();
            onConversationUpdated();
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }
        public bool HasNext()
        {
            return FilterOnCondition(currentDialogue.GetAllChildren(currendNode)).Count() > 0;
            
        }

        private void TriggerEnterAction()
        {
            if(currendNode !=null)
            {
                TriggerAction(currendNode.GetOnEntryAction());
            }
        }
        private void TriggerExitAction()
        {
            if (currendNode != null)
            {
                
                TriggerAction(currendNode.GetOnExitAction());
            }
        }

        private void TriggerAction(string action)
        {
            if (action == "") return;
            foreach(DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
                Debug.Log(action);
                
            }

        }



    }
}
