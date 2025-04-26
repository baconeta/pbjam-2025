using UnityEngine;

namespace Managers
{
    public class ScoringData : MonoBehaviour
    {
        [Header("Base Scores")]
        public float scoredGainedForSparkingJoy = 10;
        public float scoreLostForVeryBadChoice = 10;
        public float scoreLostForMissingSparkItem = 5;
        public float scoreGainedForThrowingAwayBadItem = 5;
        public float scoreLostForKeepingNeutralItem = 5;
        public float scoreGainedForDiscardingNeutralItem = 2;
        
        [Header("Time multiplier scoring variables")]
        public float maxTimeMultiplier = 5f;
        public float minTimeMultiplier = 0.8f;
        public float timeToMin = 8f;
        public float timeMultiplierCurveStrength = 2f;

        [Header("Overall multiplier for funner big numbers")]
        public float overallMultiplier = 1f;

        [Header("Other variables")] 
        [Tooltip("If we have scores normalised around a certain number of items being shown," +
                 "we can mitigate random chance being a huge driver of score difference")]
        public int scoresNormaliseToXItems = 8;
    }
}