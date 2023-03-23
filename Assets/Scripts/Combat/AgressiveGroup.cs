using RPG.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class AgressiveGroup : MonoBehaviour
    {
        [SerializeField] Fighter[] agressiveNPCList;
        bool activateOnStart = false;

        public void Activate(bool shouldActivate)
        {
            foreach (Fighter agressiveNPC in agressiveNPCList)
            {
                CombatTarget target = agressiveNPC.GetComponent<CombatTarget>();
                if(target != null)
                {
                    target.enabled = shouldActivate;
                }
                else if(target == null)
                {
                    agressiveNPC.gameObject.AddComponent<CombatTarget>();
                }
                agressiveNPC.enabled = shouldActivate;
                AIConversant conversant = agressiveNPC.GetComponent<AIConversant>();
                conversant.enabled = !shouldActivate;
            }
                
            
        }


    }

}