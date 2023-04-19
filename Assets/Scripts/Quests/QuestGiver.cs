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
            if (quest.GetIsComplete())
            {
                DestroyQuestMarker();
            }
            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
        }

        private void SpawnQuestMarker()
        {
            GameObject instantiatedQuestMarker = Instantiate(questMarker, questMarkerTransform);
            questMarkerGameObject = instantiatedQuestMarker;
        }

        public void GiveQuest()
        {

            quest.QuestReset(quest);
            questList.AddQuest(quest);
            DestroyQuestMarker();
        }

        public void DestroyQuestMarker()
        {
            Destroy(questMarkerGameObject);
        }



    }
}
