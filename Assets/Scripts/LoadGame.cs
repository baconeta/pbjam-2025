using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
