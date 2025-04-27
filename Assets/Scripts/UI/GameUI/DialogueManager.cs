using ScriptableObjects;
using TMPro;
using UnityEngine.UI;
using Utils;

namespace GameUI
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public Image characterImage;
        public TMP_Text characterName;
        public TMP_Text characterMarieText;

        public void SetupCharacterData(CharacterData character)
        {
            if (characterImage != null) characterImage.sprite = character.characterPortrait;
            if (characterName != null) characterName.text = character.characterName;
            if (characterMarieText != null) characterMarieText.text = character.aboutMeToMarie;
        }
    }
}