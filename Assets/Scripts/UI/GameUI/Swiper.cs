using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameUI
{
    public class Swiper : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [Header("Swipe Settings")] 
        public float swipeThreshold = 150f;
        public float returnSpeed = 10f;

        [Header("Events")] 
        public UnityEvent onSwipedLeft;
        public UnityEvent onSwipedRight;

        private Vector3 _initialPosition;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private bool _isReturning;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            if (!_canvasGroup) _canvasGroup = gameObject.AddComponent<CanvasGroup>();

            var swipeManager = FindFirstObjectByType<SwipeManager>();
            if (!swipeManager)
            {
                Debug.LogError("No play manager found - Swipe card not setup correctly");
                return;
            }
        }

        private void Start()
        {
            _initialPosition = _rectTransform.anchoredPosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isReturning = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            float distance = _rectTransform.anchoredPosition.x - _initialPosition.x;

            if (Mathf.Abs(distance) >= swipeThreshold)
            {
                if (distance > 0)
                    SwipeRight();
                else
                    SwipeLeft();
            }
            else
            {
                _isReturning = true;
            }
        }

        private void Update()
        {
            if (_isReturning)
            {
                _rectTransform.anchoredPosition = Vector3.Lerp(_rectTransform.anchoredPosition, _initialPosition,
                    Time.deltaTime * returnSpeed);

                if (Vector3.Distance(_rectTransform.anchoredPosition, _initialPosition) < 1f)
                    _isReturning = false;
            }
        }

        private void SwipeLeft()
        {
            //TODO add it flying into the trash
            onSwipedLeft?.Invoke();
        }

        private void SwipeRight()
        {
            //TODO add it flying into the Sparks Joy bucket
            onSwipedRight?.Invoke();
        }
    }
}