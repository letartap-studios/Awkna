using UnityEngine;
using UnityEngine.UI;

public class CurrentLevel : MonoBehaviour
{
    private Text levelText;

    private void Start()
    {
        levelText = GetComponent<Text>();
        levelText.text = "Level " + PlayerStats.Instance.Level;
    }
}