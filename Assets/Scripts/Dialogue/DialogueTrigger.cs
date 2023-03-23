using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        
        [SerializeField] public Actions[] actionList;
            public void Trigger(string actionToTrigger)
            {
                foreach (Actions action in actionList)
                {
                    if (actionToTrigger == action.action)
                        {
                            action.onTrigger.Invoke();
                        }
                }

            }   

        [System.Serializable]
        public struct Actions
        {

            [SerializeField] public string action;
            [SerializeField] public UnityEvent onTrigger;
        }

        
    }
}

