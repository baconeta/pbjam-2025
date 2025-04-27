using Audio;
using UnityEngine;

namespace Managers
{
    public class GameAudioLoader : MonoBehaviour
    {
        public void Start()
        {
            AudioManager.Instance.StopAllAudio();
            AudioWrapper.Instance.PlaySound("bgm");
        }
    }
}