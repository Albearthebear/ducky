using UnityEngine;
using DuckFriend.Data;
using DuckFriend.Services;
using DuckFriend.UI;

namespace DuckFriend.Services
{
    /// <summary>
    /// Manages the daily check-in process, including displaying the mood check panel and registering the check-in.
    /// </summary>
    public class DailyCheckInManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UIManager uiManager;
        [SerializeField] private DialogueUI dialogueUI;

        private UserData userData;

        private void Start()
        {
            // Load the user data
            userData = DataPersistenceService.LoadUserData();

            // Check if the user has already checked in today
            if (!TimeService.HasCheckedInToday(userData.lastCheckInDate))
            {
                // Show the mood check panel so the user can provide their mood
                uiManager.ShowMoodCheckPanel();
            }
            else
            {
                // If already checked in, show the main interaction panel
                uiManager.ShowMainInteractionPanel();
            }
        }

        /// <summary>
        /// Called by the check-in button in the MoodCheckPanel.
        /// Registers the daily check-in, saves data, and shows an affirmation.
        /// </summary>
        public void PerformDailyCheckIn()
        {
            // Register today's check-in
            TimeService.RegisterCheckIn(userData);
            DataPersistenceService.SaveUserData(userData);

            // Get a gentle affirmation message
            string message = DialogueService.GetRandomMessage();

            // Display the message using DialogueUI for 2 seconds
            dialogueUI.ShowMessage(message, 2f);

            // Switch to the main interaction panel
            uiManager.ShowMainInteractionPanel();
        }
    }
} 