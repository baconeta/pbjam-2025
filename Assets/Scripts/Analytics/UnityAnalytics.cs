using Unity.Services.Core;
using UnityEngine;

namespace Analytics
{
    public class UnityAnalytics : MonoBehaviour
    {
        public bool useAnalytics;

        private async void Start()
        {
            if (!useAnalytics)
            {
                return;
            }

            try
            {
                await UnityServices.InitializeAsync();
            }
            catch (RequestFailedException e)
            {
                Debug.Log(e.ToString());
            }
        }
    }
}