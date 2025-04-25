using System.Linq;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        private ScoringData _scoringData;
        public int score = 0;

        private void Start()
        {
            _scoringData = GetComponent<ScoringData>();
            if (_scoringData == null)
            {
                Debug.LogWarning("ScoreManager: _scoringData is null");
            }
        }

        public void ProcessChoice(Item item, bool kept, CharacterData character)
        {
            if (_scoringData == null)
            {
                Debug.LogWarning("ScoreManager: _scoringData is null - no score gained");
                return;
            }
            
            float scoreFromThisItem = 0;
            bool joySparks = character.likedItems.Contains(item);
            bool joyReallyNoSparks = character.hatedItems.Contains(item) ||
                                     item.tags.Any(s => character.dislikedTags.Contains(s));

            switch (kept)
            {
                // if kept, handle scoring
                case true when joySparks:
                    scoreFromThisItem += _scoringData.scoredGainedForSparkingJoy;
                    break;
                case true when joyReallyNoSparks:
                    scoreFromThisItem -= _scoringData.scoreLostForVeryBadChoice;
                    break;
                // if not kept, handle scoring
                case false when joySparks:
                    scoreFromThisItem -= _scoringData.scoreLostForMissingSparkItem;
                    break;
                case false when joyReallyNoSparks:
                    scoreFromThisItem += _scoringData.scoreGainedForThrowingAwayBadItem;
                    break;
                // neutral cases for both scenarios
                case true: // kept neutral item
                    scoreFromThisItem -= _scoringData.scoreLostForKeepingNeutralItem;
                    break;
                default: // not kept neutral item
                    scoreFromThisItem += _scoringData.scoreGainedForDiscardingNeutralItem;
                    break;
            }

            Debug.Log(scoreFromThisItem);
            
            score += Mathf.FloorToInt(scoreFromThisItem);
        }
    }
}