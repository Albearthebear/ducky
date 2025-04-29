using System;
using System.Collections.Generic;
using UnityEngine;
using DuckFriend.Controllers;

namespace DuckFriend.Services
{
    /// <summary>
    /// Manages unlocking and displaying temporary duck friends and tracking combo achievements.
    /// </summary>
    public class FriendManager : MonoBehaviour
    {
        [Header("Friend Settings")]
        [SerializeField] private List<GameObject> friendPrefabs;
        [SerializeField] private Transform friendSpawnPoint;
        [SerializeField] private float friendDisplayDuration = 5f;

        private UserData userData;
        private List<string> activeFriends = new List<string>();

        private void Awake()
        {
            userData = DataPersistenceService.LoadUserData();
        }

        /// <summary>
        /// Shows a random unlocked friend next to the duck.
        /// </summary>
        public void ShowRandomFriend()
        {
            if (userData.inventory.friendTokens.Count == 0) return;
            string token = userData.inventory.friendTokens[UnityEngine.Random.Range(0, userData.inventory.friendTokens.Count)];
            var prefab = friendPrefabs.Find(p => p.name.Equals(token, StringComparison.OrdinalIgnoreCase));
            if (prefab != null)
            {
                GameObject friend = Instantiate(prefab, friendSpawnPoint.position, Quaternion.identity);
                activeFriends.Add(token);
                Destroy(friend, friendDisplayDuration);

                // Check for combo achievements
                AttemptComboAchievements();
            }
        }

        /// <summary>
        /// Attempts to unlock combo achievements based on currently active friends.
        /// </summary>
        private void AttemptComboAchievements()
        {
            // Simple example: if both 'cat' and 'capybara' are in activeFriends, unlock 'CozyCrew'
            if (activeFriends.Contains("cat") && activeFriends.Contains("capybara"))
            {
                AchievementManager.UnlockAchievement("CozyCrew");
            }
        }
    }
} 