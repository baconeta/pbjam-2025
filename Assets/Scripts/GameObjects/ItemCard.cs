using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameObjects
{
    public class ItemCard : MonoBehaviour
    {
        [Header("UI References")]
        public Image iconImage;
        public TMP_Text nameText;

        [HideInInspector] public Item data;

        public UnityEvent onSwipedLeft;
        public UnityEvent onSwipedRight;

        public void Setup(Item item)
        {
            data = item;
            iconImage.sprite = item.icon;
            nameText.text = item.itemName;
        }
    }
}