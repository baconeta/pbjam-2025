using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Managers
{
    public class CharacterManager : Singleton<CharacterManager>
    {
        [Tooltip("Characters available in this level")]
        public List<CharacterData> characters;
        public CharacterData CurrentCharacter { get; private set; }

        public void LoadRandomCharacter()
        {
            CurrentCharacter = characters[Random.Range(0, characters.Count)];
        }
    }
}