using Audio;
using Utils;

public class PlaySound : Singleton<PlaySound>
{
        public void PlayASoundOnce(string soundName)
        {
                AudioWrapper.Instance.PlaySoundVoid(soundName);
        }
}