using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MMSceneSelect : MonoBehaviour
{
    public void LoadSceneMissiles()
    {
        SceneManager.LoadScene("magic_missiles_demo");
    }
    public void LoadSceneCircles()
    {
        SceneManager.LoadScene("magic_missiles_circles");
    }
    public void LoadSceneArea01()
    {
        SceneManager.LoadScene("magic_missiles_area01");
    }
    public void LoadSceneArea02()
    {
        SceneManager.LoadScene("magic_missiles_area02");
    }
    public void LoadSceneArea03()
    {
        SceneManager.LoadScene("magic_missiles_area03");
    }
}