using System.Collections;
using UnityEngine;

namespace DuckFriend.Controllers
{
    /// <summary>
    /// Handles spawning food and delivering it to the duck for feeding.
    /// </summary>
    public class FeedInteraction : MonoBehaviour
    {
        [Header("Feed Interaction Settings")]
        [SerializeField] private GameObject foodPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private DuckController duckController;
        [SerializeField] private float feedDuration = 0.5f;

        /// <summary>
        /// Starts the feeding sequence by spawning food and moving it to the duck.
        /// </summary>
        public void SpawnAndFeed()
        {
            if (foodPrefab == null || duckController == null || spawnPoint == null)
            {
                Debug.LogWarning("FeedInteraction: Missing references.");
                return;
            }
            GameObject food = Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(DeliverFood(food));
        }

        private IEnumerator DeliverFood(GameObject food)
        {
            Vector3 startPos = food.transform.position;
            Vector3 targetPos = duckController.transform.position;
            float elapsed = 0f;
            while (elapsed < feedDuration)
            {
                food.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / feedDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            food.transform.position = targetPos;
            duckController.Feed();
            Destroy(food);
        }
    }
} 