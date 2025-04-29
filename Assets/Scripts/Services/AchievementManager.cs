using System;
using System.Linq;
using DuckFriend.Data;
using UnityEngine;

namespace DuckFriend.Services
{
    /// <summary>
    /// Manages unlocking achievements and persisting them.
    /// </summary>
    public static class AchievementManager
    {
        /// <summary>Event fired when an achievement is unlocked, passing the achievement ID.</summary>
        public static event Action<string> OnAchievementUnlocked;

        /// <summary>
        /// Unlocks the given achievement if not already unlocked.
        /// </summary>
        /// <param name="achievementId">Unique ID of the achievement.</param>
        public static void UnlockAchievement(string achievementId)
        {
            var data = DataPersistenceService.LoadUserData();
            if (!data.achievements.unlockedAchievements.Contains(achievementId))
            {
                data.achievements.unlockedAchievements.Add(achievementId);
                DataPersistenceService.SaveUserData(data);
                Debug.Log($"Achievement unlocked: {achievementId}");
                OnAchievementUnlocked?.Invoke(achievementId);
            }
        }

        /// <summary>
        /// Checks whether an achievement has already been unlocked.
        /// </summary>
        public static bool IsUnlocked(string achievementId)
        {
            var data = DataPersistenceService.LoadUserData();
            return data.achievements.unlockedAchievements.Contains(achievementId);
        }
    }
} 