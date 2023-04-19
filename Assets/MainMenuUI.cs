using RPG.SceneManagement;
using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField newGameNameField;
        LazyValue<SavingWrapper> savingWrapper;

        private void Awake()
        {
            savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);
            
        }

        private SavingWrapper GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapper>();
        }

        public void ContinueGame()
        {
            savingWrapper.value.ContinueGame();
        }

        public void NewGame()
        {
            savingWrapper.value.NewGame(newGameNameField.text); 
        }

    }
}
