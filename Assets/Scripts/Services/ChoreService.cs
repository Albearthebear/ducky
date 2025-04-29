using UnityEngine;
using DuckFriend.Data;
using DuckFriend.Controllers;

namespace DuckFriend.Services
{
    /// <summary>
    /// Service to track chore actions (feed, pet, clean) and generate eggs.
    /// </summary>
    public class ChoreService : MonoBehaviour
    {
        [Header("Chore Settings")]
        [SerializeField] private DuckController duckController;
        [SerializeField] private EggManager eggManager;

        private UserData userData;

        private void Awake()
        {
            userData = DataPersistenceService.LoadUserData();
            if (duckController != null)
            {
                duckController.OnFeed += HandleChore;
                duckController.OnPet += HandleChore;
                duckController.OnClean += HandleChore;
            }
        }

        private void HandleChore()
        {
            // Increment egg count
            userData.inventory.eggs++;
            DataPersistenceService.SaveUserData(userData);

            // Spawn visual egg
            eggManager?.SpawnEgg();
        }
    }
} 