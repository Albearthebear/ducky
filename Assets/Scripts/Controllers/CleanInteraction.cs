using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckFriend.Controllers
{
    /// <summary>
    /// Handles cleaning interaction: dragging a sponge onto the duck to clean it.
    /// </summary>
    public class CleanInteraction : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Clean Interaction Settings")]
        [SerializeField] private DuckController duckController;
        [SerializeField] private RectTransform spongeTransform;

        private Vector3 originalPosition;
        private bool isDragging;

        private void Awake()
        {
            if (spongeTransform != null)
            {
                originalPosition = spongeTransform.position;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (spongeTransform == null) return;
            isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging || spongeTransform == null) return;
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
            spongeTransform.position = new Vector3(worldPoint.x, worldPoint.y, spongeTransform.position.z);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isDragging || spongeTransform == null) return;
            isDragging = false;

            // Check overlap with duck collider
            Collider2D duckCollider = duckController.GetComponent<Collider2D>();
            if (duckCollider != null)
            {
                Vector2 spongePos = new Vector2(spongeTransform.position.x, spongeTransform.position.y);
                if (duckCollider.OverlapPoint(spongePos))
                {
                    duckController.Clean();
                }
            }

            // Reset sponge position
            spongeTransform.position = originalPosition;
        }
    }
} 