using System.Collections.Generic;
using GameObjects;
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

        private Queue<Item> _itemQueue;

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
            var card = Instantiate(defaultItemCardObject).GetComponent<ItemCard>();
            card.Setup(item);
            card.onSwipedLeft.AddListener(() => onItemSwiped.Invoke(item, false));
            card.onSwipedRight.AddListener(() => onItemSwiped.Invoke(item, true));
        }
    }
}