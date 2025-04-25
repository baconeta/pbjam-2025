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
            List<Item> joyNoSparksItems = itemPool.Where(i => i.tags.Any(t => character.dislikedTags.Contains(t))).ToList();
            List<Item> joyNoSparksTags = itemPool.Where(i => i.tags.Any(t => character.dislikedTags.Contains(t))).ToList();

            var mixed = new List<Item>();
            mixed.AddRange(joySparksItems.OrderBy(x => Random.value).Take(3));
            mixed.AddRange(joyNoSparksItems.OrderBy(x => Random.value).Take(2));
            mixed.AddRange(joyNoSparksTags.OrderBy(x => Random.value).Take(2));

            return mixed.OrderBy(x => Random.value).ToList(); // Shuffle
        }
    }
}