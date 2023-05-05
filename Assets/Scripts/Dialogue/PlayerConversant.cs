using RPG.Cinimatics;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        [SerializeField] string playerName;
        Dialogue currentDialogue;
        DialogueNode currendNode = null;
        [HideInInspector]
        public bool isInDialogue = false;
        bool isChoosing;
        public event Action onConversationUpdated;
        public CanvasGroup hudGroup;

        AIConversant currentConversant = null;
        CameraTransition cameraTransition;
        ActionSchedueler actionSchedueler;

        float lookSpeed = 1f;
        private Coroutine LookCoroutine;

        private void startRotation()
        {
            if (LookCoroutine != null)
            {
                StopCoroutine(LookCoroutine);
            }
            LookCoroutine = StartCoroutine(LookAt());
        }

        private IEnumerator LookAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(currentConversant.transform.position - transform.position);
            float time = 0;
            while (time < 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
                time += Time.deltaTime * lookSpeed;
                yield return null;
            }

            
        }
        public void StartDialogue(AIConversant newConversant, Dialogue newDialogue)
        {
            PlayerController playerController = GetComponent<PlayerController>();
            isInDialogue = true;
            StartCoroutine(FadeOutHUD());
            actionSchedueler = GetComponent<ActionSchedueler>();
            actionSchedueler.CancelCurrentAction();
            playerController.Cancel();
            playerController.currentHitObject = null;
            cameraTransition = GetComponent<CameraTransition>();
            cameraTransition.dialogueCamera.LookAt = newConversant.conversantHead;
            cameraTransition.SwitchCamera(cameraTransition.dialogueCamera);
            currentConversant = newConversant;
            newConversant.startRotation();
            startRotation();
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
            isInDialogue = false;
            StartCoroutine(FadeInHUD());
            GetComponent<PlayerController>().EnableMovement();
            cameraTransition = GetComponent<CameraTransition>();
            cameraTransition.SwitchCamera(cameraTransition.mainCamera);
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
               
                
            }

        }

        IEnumerator FadeOutHUD()
        {
            float elapsedTime = 0f;
            float fadeTime = 1f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
                hudGroup.alpha = alpha;
                yield return null;
            }

            hudGroup.alpha = 0f;
        }

        IEnumerator FadeInHUD()
        {
            float elapsedTime = 0f;
            float fadeTime = 1f;

            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
                hudGroup.alpha = alpha;
                yield return null;
            }

            hudGroup.alpha = 1f;
        }


    }
}
