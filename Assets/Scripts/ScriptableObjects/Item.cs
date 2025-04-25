using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    [Serializable]
    public class Item : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        
        [Tooltip("Used for tagging as categories - i.e. put 'candle' here")]
        public List<string> tags;
    }
}