using UnityEngine;

namespace Managers
{
    public class ScoringData : MonoBehaviour
    {
        public float scoredGainedForSparkingJoy = 10;
        public float scoreLostForVeryBadChoice = 10;
        public float scoreLostForMissingSparkItem = 5;
        public float scoreGainedForThrowingAwayBadItem = 5;
        public float scoreLostForKeepingNeutralItem = 5;
        public float scoreGainedForDiscardingNeutralItem = 2;
    }
}