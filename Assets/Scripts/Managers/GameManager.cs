using System;
using GameUI;
using ScriptableObjects;
using UI.StateSwitcher;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public CharacterManager characterManager;
        public ScoreManager scoreManager;
        public SwipeManager swipeController;
        public ItemManager itemManager;
        public CompositeStateSwitcher gameUISwitcher;

        public void Start()
        {
            characterManager.LoadRandomCharacter();
            gameUISwitcher.ChangeState("Start");
            DialogueManager.Instance.SetupCharacterData(characterManager.CurrentCharacter);
        }

        public void StartLevel()
        {
            gameUISwitcher.ChangeState("Running");
            var items = itemManager.GenerateItemsForCharacter(characterManager.CurrentCharacter);

            swipeController.ShowItems(items);
            swipeController.onItemSwiped.AddListener(HandleSwipe);
        }

        private void HandleSwipe(Item item, bool kept)
        {
            scoreManager.ProcessChoice(item, kept, characterManager.CurrentCharacter);
        }

        public void EndLevel()
        {
            Debug.Log("Ending level");
            Debug.Log("The final score was " + scoreManager.score);
            gameUISwitcher.ChangeState("End");
        }
    }
}