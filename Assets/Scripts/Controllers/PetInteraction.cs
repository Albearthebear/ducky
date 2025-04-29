using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckFriend.Controllers
{
    /// <summary>
    /// Handles pet interaction by detecting taps and triggering the duck's Pet behavior.
    /// </summary>
    public class PetInteraction : MonoBehaviour, IPointerClickHandler
    {
        [Header("Pet Interaction Settings")]
        [SerializeField] private DuckController duckController;

        /// <summary>
        /// Called when the user taps/clicks on this GameObject.
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (duckController == null)
            {
                Debug.LogWarning("PetInteraction: Missing DuckController reference.");
                return;
            }
            duckController.Pet();
        }
    }
} 