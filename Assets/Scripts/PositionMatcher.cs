using UnityEngine;
namespace RPG.Cinimatics
{
    public class PositionMatcher : MonoBehaviour
    {
        public GameObject targetObject;

        private void Update()
        {
            // Get the target object's position
            Vector3 targetPosition = targetObject.transform.position;

            Vector3 currentPosition = transform.position;
            currentPosition.x = targetPosition.x;
            currentPosition.z = targetPosition.z;
            currentPosition.y = targetPosition.y;
            transform.position = currentPosition;
        }
    }
}
