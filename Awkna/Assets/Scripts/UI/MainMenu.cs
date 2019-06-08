using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName: "vlod");
        if (PlayerStats.Instance.Level == 0)
        {
            PlayerStats.Instance.ResetStats();
        }
        PlayerStats.Instance.NextLevel();
        Time.timeScale = 1f;
    }
    public void PlayTutorial()
    {
        PlayerStats.Instance.ResetStats();
        SceneManager.LoadScene(sceneName: "Tutorial");
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
