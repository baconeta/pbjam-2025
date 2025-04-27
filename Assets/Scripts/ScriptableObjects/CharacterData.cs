using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Character", menuName = "Character Data")]
    [System.Serializable]
    public class CharacterData :ScriptableObject
    {
        [Header("Character Info")]
        public string characterName;
        public Sprite characterPortrait;
        public int age;
        public string gender;
        [TextArea(3,10)] public string bio;
        [TextArea(2,4)]public string hobbies;
        [TextArea(3,10)]public string aboutMeToMarie;
        public string funFact;
        public string nicknames;
        
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
        [Tooltip("MaxDislikedTaggedItems")]
        public int maxDislikedTaggedItems;
        [Tooltip("Minimum Generic Items to Take")]
        public int minGenericsToTake;
        [Tooltip("Maximum Generic Items to Take")]
        public int maxGenericsToTake;
    }
}