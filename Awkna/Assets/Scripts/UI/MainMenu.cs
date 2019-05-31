using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName: "vlod");
        Time.timeScale = 1f;
    }
    public void PlayTutorial()
    {
        SceneManager.LoadScene(sceneName: "Tutorial");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
