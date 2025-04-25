using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ItemManager : Singleton<ItemManager>
    {
        public List<Item> itemPool;

        public List<Item> GenerateItemsForCharacter(CharacterData character)
        {
            List<Item> joySparksItems = itemPool.Where(i  => character.likedItems.Contains(i)).ToList();
            List<Item> joyNoSparksItems = itemPool.Where(i  => character.hatedItems.Contains(i)).ToList();
            List<Item> joyNoSparksTags = itemPool.Where(i => i.tags.Any(t => character.dislikedTags.Contains(t))).ToList();

            var mixed = new List<Item>();
            
            Debug.Log("Trying to setup items for level"); // High chance of this crashing out atm
            mixed.AddRange(joySparksItems.OrderBy(x => Random.value).Take(
                Random.Range(character.minimumGoodItems, character.maximumGoodItems + 1)));
            mixed.AddRange(joyNoSparksItems.OrderBy(x => Random.value).Take(
                Random.Range(character.minimumBadItems, character.maximumBadItems + 1)));
            mixed.AddRange(joyNoSparksTags.OrderBy(x => Random.value).Take(
                Random.Range(0, character.maxDislikedTaggedItems + 1)));
            Debug.Log("Successfully setup items for level");
            return mixed.OrderBy(x => Random.value).ToList(); // Shuffle
        }
    }
}