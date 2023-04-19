using RPG.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SaveLoadUI : MonoBehaviour
    {
        [SerializeField] Transform contentRoot;
        [SerializeField] GameObject SaveGameButtomPrefab;

        private void OnEnable()
        {
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            if (savingWrapper == null) return;

            foreach (Transform child in contentRoot)
            {
                Destroy(child.gameObject);

            }
            foreach (string save in savingWrapper.ListSaves())
            {
                
                GameObject instantiatedSaveFile = Instantiate(SaveGameButtomPrefab, contentRoot);
                TextMeshProUGUI saveName = instantiatedSaveFile.GetComponentInChildren<TextMeshProUGUI>();
                saveName.text = save;
                Button saveFileButton = instantiatedSaveFile.GetComponentInChildren<Button>();
                saveFileButton.onClick.AddListener(() =>
                {
                    savingWrapper.LoadGame(save);
                });

                SaveDeleteUI deleteUI = instantiatedSaveFile.GetComponentInChildren<SaveDeleteUI>();
                Button deleteButton = deleteUI.gameObject.GetComponent<Button>();
                deleteButton.onClick.AddListener(() =>
                {
                    savingWrapper.DeleteGame(save);
                    Destroy(instantiatedSaveFile);
                });
            }
            
        }


    }
}
