using UnityEngine;

namespace DuckFriend.UI
{
    /// <summary>
    /// Manages UI panel visibility and transitions.
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Panel References")]
        [SerializeField] private GameObject moodCheckPanel;
        [SerializeField] private GameObject mainInteractionPanel;
        [SerializeField] private GameObject eggRewardsPanel;
        [SerializeField] private GameObject inventoryPanel;

        /// <summary>
        /// Hide all panels on start.
        /// </summary>
        private void Awake()
        {
            HideAllPanels();
        }

        /// <summary>
        /// Hides all UI panels.
        /// </summary>
        public void HideAllPanels()
        {
            moodCheckPanel.SetActive(false);
            mainInteractionPanel.SetActive(false);
            eggRewardsPanel.SetActive(false);
            inventoryPanel.SetActive(false);
        }

        /// <summary>
        /// Shows the Mood Check Panel.
        /// </summary>
        public void ShowMoodCheckPanel()
        {
            HideAllPanels();
            moodCheckPanel.SetActive(true);
        }

        /// <summary>
        /// Shows the Main Interaction Panel.
        /// </summary>
        public void ShowMainInteractionPanel()
        {
            HideAllPanels();
            mainInteractionPanel.SetActive(true);
        }

        /// <summary>
        /// Shows the Egg Rewards Panel.
        /// </summary>
        public void ShowEggRewardsPanel()
        {
            HideAllPanels();
            eggRewardsPanel.SetActive(true);
        }

        /// <summary>
        /// Shows the Inventory Panel.
        /// </summary>
        public void ShowInventoryPanel()
        {
            HideAllPanels();
            inventoryPanel.SetActive(true);
        }
    }
} 