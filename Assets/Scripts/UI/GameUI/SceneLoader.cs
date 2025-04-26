using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameUI
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadSceneAsync(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}