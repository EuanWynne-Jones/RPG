using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Core
{
    public class ActionSchedueler : MonoBehaviour
    {

        public IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if(currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;

            //Prints what the current action is if troubleshooting
            //Debug.Log(currentAction);

        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }

    }

}
