using System.Collections.Generic;
using UnityEngine;

namespace DuckFriend.Services
{
    /// <summary>
    /// Service for providing gentle advice, affirmations, and encouragement messages.
    /// </summary>
    public static class DialogueService
    {
        private static readonly List<string> messages = new List<string>
        {
            "It's okay to rest. You're doing your best.",
            "I believe in you.",
            "You are not alone.",
            "Take a deep breath.",
            "Small steps are progress.",
            "I'm proud of you.",
            "You're stronger than you think.",
            "Keep shining your light."
        };

        /// <summary>
        /// Returns a random affirmation or gentle message.
        /// </summary>
        public static string GetRandomMessage()
        {
            int index = Random.Range(0, messages.Count);
            return messages[index];
        }
    }
} 