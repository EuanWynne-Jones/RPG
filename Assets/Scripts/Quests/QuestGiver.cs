using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] Quest quest;
        QuestList questList;
        [SerializeField] Transform questMarkerTransform = null;
        [SerializeField] GameObject questMarker = null;
        private GameObject questMarkerGameObject;

        private void Start()
        {
            SpawnQuestMarker();
            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        }

        private void SpawnQuestMarker()
        {
            GameObject instantiatedQuestMarker = Instantiate(questMarker, questMarkerTransform);
            questMarkerGameObject = instantiatedQuestMarker;
        }

        public void GiveQuest()
        {
            
            questList.AddQuest(quest);
            quest.QuestReset(quest);
            Destroy(questMarkerGameObject);
        }


        public void CompleteQuest()
        {

            //questList.CompleteObjective(quest);
        }


    }
}
