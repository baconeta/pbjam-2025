using GameUI;
using UnityEngine;
using Utils;

namespace Managers
{
    /// <summary>
    /// Handles the play for this level. Listens to card inputs and handles behaviour and scoring based on that
    /// Will send information to the Game Manager when no cards remain to handle next steps and UI work
    /// </summary>
    public class PlayManager : Singleton<PlayManager>
    {
        private Swiper _currentCard;

        public Swiper CurrentCard
        {
            get => _currentCard;
            set
            {
                _currentCard = value;
                SetupListeners();
            }
        }

        private void SetupListeners()
        {
            _currentCard.onSwipedLeft.AddListener(HandleSwipeLeft);
            _currentCard.onSwipedRight.AddListener(HandleSwipeRight);
        }

        private void HandleSwipeLeft()
        {
            Debug.Log("Swiped left - maybe reject?");
            // game logic here
        }

        private void HandleSwipeRight()
        {
            Debug.Log("Swiped right - maybe accept?");
            // game logic here
        }
    }
}