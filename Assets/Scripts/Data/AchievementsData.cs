using System.Collections.Generic;

namespace DuckFriend.Data
{
    /// <summary>
    /// Serializable model for tracking achievements unlocked by the user.
    /// </summary>
    [System.Serializable]
    public class AchievementsData
    {
        /// <summary>
        /// List of unlocked achievement IDs.
        /// </summary>
        public List<string> unlockedAchievements;

        /// <summary>
        /// Initializes default achievements data.
        /// </summary>
        public AchievementsData()
        {
            unlockedAchievements = new List<string>();
        }
    }
} 