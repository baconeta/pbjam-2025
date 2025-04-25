using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        public int score = 0;
        public float scoredGainedForSparkingJoy = 10;
        public float scoreLostForVeryBadChoice = 10;
        public float scoreLostForMissingSparkItem = 5;
        public float scoreGainedForThrowingAwayBadItem = 5;
        public float scoreLostForKeepingNeutralItem = 5;
        public float scoreGainedForDiscardingNeutralItem = 2;

        public void ProcessChoice(Item item, bool kept, CharacterData character)
        {
            float scoreFromThisItem = 0;
            bool joySparks = character.likedItems.Contains(item);
            bool joyReallyNoSparks = character.hatedItems.Contains(item) ||
                                     item.tags.Any(s => character.dislikedTags.Contains(s));;

            switch (kept)
            {
                // if kept, handle scoring
                case true when joySparks:
                    scoreFromThisItem += scoredGainedForSparkingJoy;
                    break;
                case true when joyReallyNoSparks:
                    scoreFromThisItem -= scoreLostForVeryBadChoice;
                    break;
                // if not kept, handle scoring
                case false when joySparks:
                    scoreFromThisItem -= scoreLostForMissingSparkItem;
                    break;
                case false when joyReallyNoSparks:
                    scoreFromThisItem += scoreGainedForThrowingAwayBadItem;
                    break;
                // neutral cases for both scenarios
                case true: // kept neutral item
                    scoreFromThisItem -= scoreLostForKeepingNeutralItem;
                    break;
                default: // not kept neutral item
                    scoreFromThisItem += scoreGainedForDiscardingNeutralItem;
                    break;
            }

            Debug.Log(scoreFromThisItem);
        }
    }
}