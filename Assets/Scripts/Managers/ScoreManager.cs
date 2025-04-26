using System.Linq;
using Editor;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    /// <summary>
    /// Holds and handles the scores for this round/level
    /// </summary>
    public class ScoreManager : Utils.Singleton<ScoreManager>
    {
        [ReadOnly] public int score = 0;
        [ReadOnly] public UnityEvent<int> onScoreProcessed;
        
        private ScoringData _scoringData;

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

            scoreFromThisItem *= _scoringData.overallMultiplier;
            Debug.Log("Base score gained " + scoreFromThisItem);

            scoreFromThisItem = NormaliseBaseScore(scoreFromThisItem);
            Debug.Log("Normalised score gained " + scoreFromThisItem);
            
            scoreFromThisItem = HandleMultiplierForTimeSpent(scoreFromThisItem);
            Debug.Log("Time affected score gained: " + scoreFromThisItem);
            
            score += Mathf.FloorToInt(scoreFromThisItem);
            Debug.Log("New current total score: " + score);

            onScoreProcessed.Invoke(score);
        }

        private float HandleMultiplierForTimeSpent(float scoreFromThisItem)
        {
            var timeShownOnScreen = SwipeManager.Instance.GetTimeItemOnScreen();
            return scoreFromThisItem * GetScoreTimeMultiplier(timeShownOnScreen);
        }

        private float GetScoreTimeMultiplier(float timeTaken)
        {
            float maxMultiplier = _scoringData.maxTimeMultiplier;
            float minMultiplier = _scoringData.minTimeMultiplier;
            float timeToMin = _scoringData.timeToMin; 
            float curveStrength = _scoringData.timeMultiplierCurveStrength; 

            // Clamp time to be within 0 and timeToMin
            timeTaken = Mathf.Clamp(timeTaken, 0f, timeToMin);

            // Normalized inverse time (1 = instant, 0 = max time)
            float normalized = 1f - (timeTaken / timeToMin);

            // Sharpen curve
            float curved = Mathf.Pow(normalized, curveStrength);

            // Lerp between min and max multipliers
            return Mathf.Lerp(minMultiplier, maxMultiplier, curved);
        }

        private float NormaliseBaseScore(float currentScore)
        {
            var normalisedMultiplier =
                (float) _scoringData.scoresNormaliseToXItems / SwipeManager.Instance.totalItemsInThisRound;
            return normalisedMultiplier * currentScore;
        }
    }
}