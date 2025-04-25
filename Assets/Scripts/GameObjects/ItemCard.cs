using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects
{
    public class ItemCard : MonoBehaviour
    {
        [Header("UI References")]
        public Image iconImage;
        public TMP_Text nameText;

        [HideInInspector] public Item data;

        public void Setup(Item item)
        {
            Debug.Log("Setting up " + item.itemName);
            data = item;
            if (iconImage != null) iconImage.sprite = item.icon;
            if (nameText != null) nameText.text = item.itemName;
        }
    }
}