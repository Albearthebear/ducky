using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DuckFriend.Controllers
{
    /// <summary>
    /// Controls duck interactions: feeding, petting, cleaning, and dragging.
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class DuckController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        /// <summary>Event invoked when the duck is fed.</summary>
        public event Action OnFeed;
        /// <summary>Event invoked when the duck is petted.</summary>
        public event Action OnPet;
        /// <summary>Event invoked when the duck is cleaned.</summary>
        public event Action OnClean;

        private Animator animator;
        private bool isDragging;
        private Vector3 dragOffset;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Handles pointer clicks for petting the duck.
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            Pet();
        }

        /// <summary>
        /// Begins dragging the duck around the screen.
        /// </summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            var worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
            dragOffset = transform.position - new Vector3(worldPoint.x, worldPoint.y, transform.position.z);
        }

        /// <summary>
        /// Dragging logic to move the duck with the pointer.
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;
            var worldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
            transform.position = new Vector3(worldPoint.x, worldPoint.y, transform.position.z) + dragOffset;
        }

        /// <summary>
        /// Ends dragging the duck.
        /// </summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }

        /// <summary>
        /// Triggers the feeding animation and event.
        /// </summary>
        public void Feed()
        {
            animator.SetTrigger("Eating");
            OnFeed?.Invoke();
        }

        /// <summary>
        /// Triggers the petting animation and event.
        /// </summary>
        public void Pet()
        {
            animator.SetTrigger("Petted");
            OnPet?.Invoke();
        }

        /// <summary>
        /// Triggers the cleaning animation and event.
        /// </summary>
        public void Clean()
        {
            animator.SetTrigger("Cleaning");
            OnClean?.Invoke();
        }
    }
} 