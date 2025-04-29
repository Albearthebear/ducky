using System.Collections.Generic;
using UnityEngine;

namespace DuckFriend.Data
{
    /// <summary>
    /// Serializable model for player inventory, storing eggs, cosmetics, and friend tokens.
    /// </summary>
    [System.Serializable]
    public class InventoryData
    {
        /// <summary>Number of eggs collected by the user.</summary>
        public int eggs;
        /// <summary>List of unlocked cosmetic item IDs.</summary>
        public List<string> unlockedCosmetics;
        /// <summary>List of unlocked friend tokens.</summary>
        public List<string> friendTokens;

        /// <summary>
        /// Initializes default empty inventory.
        /// </summary>
        public InventoryData()
        {
            eggs = 0;
            unlockedCosmetics = new List<string>();
            friendTokens = new List<string>();
        }
    }
} 