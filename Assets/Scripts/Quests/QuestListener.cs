using RPG.Quests;
using UnityEngine;
using UnityEngine.Events;

public class QuestListener : MonoBehaviour
{
    private UnityEvent entityKilled = new UnityEvent();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
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
