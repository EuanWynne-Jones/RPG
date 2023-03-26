using RPG.Attributes;
using UnityEngine;

namespace RPG.Quests
{
    public class DeathCounter : MonoBehaviour
    {
        [SerializeField] private string identifier;
        [SerializeField] private bool onlyIfInitialized = true;

        private AchievementCounter counter;

        private void Awake()
        {
            counter = GameObject.FindWithTag("Player").GetComponent<AchievementCounter>();
            GetComponent<Health>().OnDie.AddListener(AddToCount);
        }

        private void AddToCount()
        {
            counter.AddToCount(identifier, 1, onlyIfInitialized);
        }
    }
}