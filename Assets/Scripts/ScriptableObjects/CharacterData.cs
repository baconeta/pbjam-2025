using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [System.Serializable]
    public class CharacterData :ScriptableObject
    {
        public string characterName;
        public Image characterPortrait;
        
        [Header("Items Lists for this character")]
        [Tooltip("Specific Items this character likes")]
        public List<Item> likedItems;
        [Tooltip("Specific Items this character hates")]
        public List<Item> hatedItems;
        [Tooltip("Maybe a certain character hates candles, for example. Put that here.")]
        public List<string> dislikedTags;

        [Header("Items weightings for this character")] 
        [Tooltip("Minimum good items to take")]
        public int minimumGoodItems;
        [Tooltip("Maximum good items to take")]
        public int maximumGoodItems;
        [Tooltip("Minimum specifically bad items to take")]
        public int minimumBadItems;
        [Tooltip("Maximum specifically bad items to take")]
        public int maximumBadItems;
        [Tooltip("MaxTaggedItems")]
        public int maxTaggedItems;
    }
}