using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ItemManager : Singleton<ItemManager>
    {
        [Tooltip("This should contain all items we want to search by tags")]
        public List<Item> genericItemPool;

        public List<Item> GenerateItemsForCharacter(CharacterData character)
        {
            List<Item> joySparksItems = character.likedItems;
            List<Item> joyNoSparksItems = character.hatedItems;
            List<Item> joyNoSparksTags = genericItemPool.Where(i => i.tags.Any(t => character.dislikedTags.Contains(t))).ToList();
            List<Item> generics = genericItemPool.Where(i => i.tags.Any(t => character.dislikedTags.Contains(t))).ToList();

            var mixed = new List<Item>();
            
            Debug.Log("Trying to setup items for level"); // High chance of this crashing out atm
            mixed.AddRange(joySparksItems.OrderBy(x => Random.value).Take(
                Random.Range(character.minimumGoodItems, character.maximumGoodItems + 1)));
            mixed.AddRange(joyNoSparksItems.OrderBy(x => Random.value).Take(
                Random.Range(character.minimumBadItems, character.maximumBadItems + 1)));
            mixed.AddRange(joyNoSparksTags.OrderBy(x => Random.value).Take(
                Random.Range(0, character.maxDislikedTaggedItems + 1)));
            mixed.AddRange(genericItemPool.OrderBy(x => Random.value).Take(
                Random.Range(character.minGenericsToTake, character.maxGenericsToTake + 1)));
            Debug.Log("Successfully setup items for level");
            return mixed.OrderBy(x => Random.value).ToList(); // Shuffle
        }
    }
}