using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuckFriend.Data;
using DuckFriend.Services;

namespace DuckFriend.Services
{
    /// <summary>
    /// Manages spawning eggs and hatching them into various rewards.
    /// </summary>
    public class EggManager : MonoBehaviour
    {
        /// <summary>Prefab for the egg visual.</summary>
        [SerializeField] private GameObject eggPrefab;
        /// <summary>Spawn point for newly created eggs.</summary>
        [SerializeField] private Transform spawnPoint;
        /// <summary>Time in seconds before an egg hatches.</summary>
        [SerializeField] private float hatchDelay = 3f;

        /// <summary>Event invoked when an egg hatches: (rewardType, rewardId or message).</summary>
        public event Action<RewardType, string> OnEggHatched;

        private UserData userData;

        /// <summary>
        /// Load user data on awake.
        /// </summary>
        private void Awake()
        {
            userData = DataPersistenceService.LoadUserData();
        }

        /// <summary>
        /// Spawns a visual egg and begins hatch countdown.
        /// </summary>
        public void SpawnEgg()
        {
            if (eggPrefab == null || spawnPoint == null)
            {
                Debug.LogWarning("EggManager: Missing eggPrefab or spawnPoint.");
                return;
            }
            GameObject egg = Instantiate(eggPrefab, spawnPoint.position, Quaternion.identity);
            StartCoroutine(HatchEggRoutine(egg));
        }

        private IEnumerator HatchEggRoutine(GameObject egg)
        {
            yield return new WaitForSeconds(hatchDelay);

            // Decide reward type
            var rewardPools = GetRewardPools();
            var pool = rewardPools[UnityEngine.Random.Range(0, rewardPools.Count)];
            var reward = pool[UnityEngine.Random.Range(0, pool.Count)];

            // Apply reward to user data
            ApplyReward(reward);
            DataPersistenceService.SaveUserData(userData);

            // Invoke event for UI or further handling
            OnEggHatched?.Invoke(reward.Type, reward.IdOrMessage);

            Destroy(egg);
        }

        /// <summary>
        /// Returns lists of reward entries for random selection.
        /// </summary>
        private List<List<RewardEntry>> GetRewardPools()
        {
            return new List<List<RewardEntry>>
            {
                RewardCatalog.CosmeticItems,
                RewardCatalog.FriendTokens,
                RewardCatalog.DiaryEntries
            };
        }

        /// <summary>
        /// Applies the reward to user data inventory.
        /// </summary>
        private void ApplyReward(RewardEntry entry)
        {
            switch (entry.Type)
            {
                case RewardType.Cosmetic:
                    userData.inventory.unlockedCosmetics.Add(entry.IdOrMessage);
                    break;
                case RewardType.FriendToken:
                    userData.inventory.friendTokens.Add(entry.IdOrMessage);
                    break;
                case RewardType.DiaryNote:
                    // Could store diary entries separately; for now use achievements list as placeholder
                    userData.achievements.unlockedAchievements.Add(entry.IdOrMessage);
                    break;
            }
        }
    }

    /// <summary>
    /// Types of rewards an egg can hatch into.
    /// </summary>
    public enum RewardType
    {
        Cosmetic,
        FriendToken,
        DiaryNote
    }

    /// <summary>
    /// Entry representing a reward option.
    /// </summary>
    public struct RewardEntry
    {
        public RewardType Type;
        public string IdOrMessage;

        public RewardEntry(RewardType type, string idOrMessage)
        {
            Type = type;
            IdOrMessage = idOrMessage;
        }
    }

    /// <summary>
    /// Catalog of possible rewards.
    /// </summary>
    public static class RewardCatalog
    {
        public static readonly List<RewardEntry> CosmeticItems = new List<RewardEntry>
        {
            new RewardEntry(RewardType.Cosmetic, "hat"),
            new RewardEntry(RewardType.Cosmetic, "glasses"),
            new RewardEntry(RewardType.Cosmetic, "bowtie"),
        };

        public static readonly List<RewardEntry> FriendTokens = new List<RewardEntry>
        {
            new RewardEntry(RewardType.FriendToken, "dog"),
            new RewardEntry(RewardType.FriendToken, "cat"),
            new RewardEntry(RewardType.FriendToken, "parrot"),
            new RewardEntry(RewardType.FriendToken, "capybara"),
        };

        public static readonly List<RewardEntry> DiaryEntries = new List<RewardEntry>
        {
            new RewardEntry(RewardType.DiaryNote, "I'm proud of you today 🐣"),
            new RewardEntry(RewardType.DiaryNote, "Keep up the great work!"),
        };
    }
} 