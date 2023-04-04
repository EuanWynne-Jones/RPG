using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;
namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant PlayerConversant;
        [SerializeField] Button nextButton;
        [SerializeField] Button quitButton;

        [SerializeField] GameObject AIResponcePrefab;
        [SerializeField] TextMeshProUGUI AIText;

        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] TextMeshProUGUI conversantName;


        void Start()
        {
            PlayerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            PlayerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(() => PlayerConversant.Next());
            quitButton.onClick.AddListener(() => PlayerConversant.Quit());
            UpdateUI();
            
        }
        private void Update()
        {
            SpacebarToInteract();
        }

        private void SpacebarToInteract()
        {
            if (Input.GetKeyDown("space") && !PlayerConversant.IsChoosing() && nextButton.IsActive())
            {
                PlayerConversant.Next();
            }
            else
            {
                return;
            }
        }

        void UpdateUI()
        {
            gameObject.SetActive(PlayerConversant.isActive());
            if (!PlayerConversant.isActive())
            {
                return;
            }
            conversantName.text = PlayerConversant.GetCurrentConversentName();
            AIResponcePrefab.SetActive(!PlayerConversant.IsChoosing());
            choicePrefab.gameObject.SetActive(PlayerConversant.IsChoosing());

            if (PlayerConversant.IsChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                foreach (Transform item in choiceRoot)
                {
                    Destroy(item.gameObject);
                }
                AIText.text = PlayerConversant.GetText();
                nextButton.gameObject.SetActive(PlayerConversant.HasNext());
            }
        }

        private void BuildChoiceList()
        {

            foreach (DialogueNode choice in PlayerConversant.GetChoices())
            {
                GameObject choiceInstance = Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceInstance.GetComponentInChildren<TextMeshProUGUI>();
                
                textComp.text = choice.GetDialogueText();
                Button button = choiceInstance.GetComponentInChildren<Button>();
                button.onClick.AddListener(() =>
                {
                    PlayerConversant.SelectChoice(choice);
                });
            }
        }
    }
}
