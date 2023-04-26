using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageNumbersPro;
using RPG.Stats;
using RPG.Quests;
using static Cinemachine.DocumentationSortingAttribute;
using static RPG.Quests.Quest;

namespace RPG.UI
{
    public class PopupHandler : MonoBehaviour
    {

        public PlayerSettings playerSettings;

        public DamageNumber popupPrefab;
        [HideInInspector]
        public RectTransform rectParent;

        private void Awake()
        {
            rectParent = FindObjectOfType<NotificationSpawner>().GetComponent<RectTransform>();
        }

        //tutorial Popup Handling

        public void spawnTutorialPopup(string tutorialText)
        {
            if (!playerSettings.displayTutorials) return;
            popupPrefab.leftText = tutorialText;
            DamageNumber notifcation = popupPrefab.Spawn(Vector3.zero, popupPrefab.leftText);
            notifcation.SetAnchoredPosition(rectParent, new Vector2(0, 0));
        }

        //Level Handling
        public void SpawnLevelPopup(string level)
        {
            popupPrefab.leftText = "Advanced to level " + level;   
            DamageNumber notifcation = popupPrefab.Spawn(Vector3.zero,popupPrefab.leftText);
            notifcation.SetAnchoredPosition(rectParent, new Vector2(0, 0));
        }

        public void SpawnObjectiveCompletePopup(string objective)
        {
            popupPrefab.leftText = "Completed Objective:  '" + objective + "'";
            DamageNumber notifcation = popupPrefab.Spawn(Vector3.zero, popupPrefab.leftText);
            notifcation.SetAnchoredPosition(rectParent, new Vector2(0, 0));
        }

        public void SpawnQuestCompletePopup(string quest)
        {
            popupPrefab.leftText = "Completed: " + quest;
            DamageNumber notifcation = popupPrefab.Spawn(Vector3.zero, popupPrefab.leftText);
            notifcation.SetAnchoredPosition(rectParent, new Vector2(0, 0));
        }

        public void SpawnQuestStartedPopup(string quest)
        {
            popupPrefab.leftText = "Started: " + quest;
            DamageNumber notifcation = popupPrefab.Spawn(Vector3.zero, popupPrefab.leftText);
            notifcation.SetAnchoredPosition(rectParent, new Vector2(0, 0));
        }

        public void SpawnQuestFailedPopup(string quest)
        {
            popupPrefab.leftText = "Failed: " + quest;
            DamageNumber notifcation = popupPrefab.Spawn(Vector3.zero, popupPrefab.leftText);
            notifcation.SetAnchoredPosition(rectParent, new Vector2(0, 0));
        }

        public void SpawnGameSavedPopup()
        {
            popupPrefab.leftText = "Game Saved ";
            DamageNumber notifcation = popupPrefab.Spawn(Vector3.zero, popupPrefab.leftText);
            notifcation.SetAnchoredPosition(rectParent, new Vector2(0, 0));
        }

        //Objective Handling
        //private void SpawnObjectiveCompletePopup()
        //{
        //    DamageNumber notifcation = popupPrefab.Spawn(Vector3.zero, GetOnObjectiveCompleteText(this.gameObject));
        //}

        //private string GetOnObjectiveCompleteText(GameObject player, string objective)
        //{
        //    return player.GetComponent<QuestList>().
        //}
    }
}
