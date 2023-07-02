using RPG.Quests;
using RPG.UI;
using UnityEngine;
using UnityEngine.Events;

public class QuestListener : MonoBehaviour
{
    private UnityEvent entityKilled = new UnityEvent();

    private PopupHandler popupHandler;

    private void Awake()
    {
        popupHandler = FindObjectOfType<PopupHandler>();
        //DontDestroyOnLoad(this.gameObject);
    }

    public void AttachQuestList(QuestList questList)
    {
        entityKilled.AddListener(
                questList.CompleteObjectivesByPredicates);
    }

    public void EntityKilled()
    {
        entityKilled.Invoke();
    }
}
