using ScriptableObjects;
using Utils;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public CharacterManager characterManager;
        public ScoreManager scoreManager;
        public SwipeManager swipeController;
        public ItemManager itemManagerPrefab;
        private ItemManager _itemManager;

        private void Start()
        {
            // We do this to allow designers to use prefab to set items globally for all levels
            _itemManager = Instantiate(itemManagerPrefab);
            
            StartLevel();
        }

        private void StartLevel()
        {
            characterManager.LoadRandomCharacter();
            var items = _itemManager.GenerateItemsForCharacter(characterManager.CurrentCharacter);

            swipeController.ShowItems(items);
            swipeController.onItemSwiped.AddListener(HandleSwipe);
        }

        private void HandleSwipe(Item item, bool kept)
        {
            scoreManager.ProcessChoice(item, kept, characterManager.CurrentCharacter);
        }
    }
}