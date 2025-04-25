using ScriptableObjects;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public CharacterManager characterManager;
        public ScoreManager scoreManager;
        public SwipeManager swipeController;
        public ItemManager itemManager;

        private void Start()
        {
            StartLevel();
        }

        private void StartLevel()
        {
            characterManager.LoadRandomCharacter();
            var items = itemManager.GenerateItemsForCharacter(characterManager.CurrentCharacter);

            swipeController.ShowItems(items);
            swipeController.onItemSwiped.AddListener(HandleSwipe);
        }

        private void HandleSwipe(Item item, bool kept)
        {
            scoreManager.ProcessChoice(item, kept, characterManager.CurrentCharacter);
        }
    }
}