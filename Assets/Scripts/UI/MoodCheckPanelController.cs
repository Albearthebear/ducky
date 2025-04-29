using UnityEngine;
using UnityEngine.UI;
using DuckFriend.Services;

namespace DuckFriend.UI
{
    /// <summary>
    /// Controls the Mood Check Panel, binding UI elements to daily check-in actions.
    /// </summary>
    public class MoodCheckPanelController : MonoBehaviour
    {
        [SerializeField] private Button checkInButton;
        [SerializeField] private DailyCheckInManager dailyCheckInManager;

        private void Awake()
        {
            if (checkInButton == null)
            {
                Debug.LogError("MoodCheckPanelController: Check-In Button reference is missing.");
                return;
            }
            checkInButton.onClick.AddListener(OnCheckInButtonClicked);
        }

        /// <summary>
        /// Called when the check-in button is clicked, triggers the daily check-in process.
        /// </summary>
        private void OnCheckInButtonClicked()
        {
            if (dailyCheckInManager != null)
            {
                dailyCheckInManager.PerformDailyCheckIn();
            }
            else
            {
                Debug.LogError("MoodCheckPanelController: DailyCheckInManager reference is missing.");
            }
        }
    }
} 