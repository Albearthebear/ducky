using System;

namespace DuckFriend.Data
{
    /// <summary>
    /// Serializable model for persisting overall user data including last check-in, inventory, and achievements.
    /// </summary>
    [System.Serializable]
    public class UserData
    {
        /// <summary>ISO 8601 date string of the last daily check-in.</summary>
        public string lastCheckInDate;

        /// <summary>User's inventory data.</summary>
        public InventoryData inventory;

        /// <summary>User's achievements data.</summary>
        public AchievementsData achievements;

        /// <summary>
        /// Initializes default user data.
        /// </summary>
        public UserData()
        {
            lastCheckInDate = DateTime.MinValue.ToString("o");
            inventory = new InventoryData();
            achievements = new AchievementsData();
        }
    }
} 