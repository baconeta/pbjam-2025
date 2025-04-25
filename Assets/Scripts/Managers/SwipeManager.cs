using System;
using System.Collections.Generic;
using GameObjects;
using GameUI;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Managers
{
    public class SwipeManager : Singleton<SwipeManager>
    {
        public UnityEvent<Item, bool> onItemSwiped;
        public GameObject defaultItemCardObject;
        public GameObject instantiationParent;

        private Queue<Item> _itemQueue;
        // ReSharper disable once InconsistentNaming
        private ItemCard _currentCardGO;

        private void OnEnable()
        {
            ScoreManager.Instance.onScoreProcessed.AddListener(NewCard);
        }

        private void NewCard(int currentScore)
        {
            if (_currentCardGO)
            {
                Destroy(_currentCardGO.gameObject);
            }
            
            ShowNextItem();
        }

        public void ShowItems(List<Item> items)
        {
            _itemQueue = new Queue<Item>(items);
            ShowNextItem();
        }

        private void ShowNextItem()
        {
            if (_itemQueue.Count == 0)
            {
                Debug.Log("All items swiped");
                return;
            }

            var item = _itemQueue.Dequeue();
            _currentCardGO = Instantiate(defaultItemCardObject, instantiationParent.transform).GetComponent<ItemCard>();
            _currentCardGO.Setup(item);
            var swiperSystem = _currentCardGO.GetComponent<Swiper>();
            swiperSystem.onSwipedLeft.AddListener(() => onItemSwiped.Invoke(item, false));
            swiperSystem.onSwipedRight.AddListener(() => onItemSwiped.Invoke(item, true));
        }
    }
}