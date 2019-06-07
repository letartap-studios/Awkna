using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    private Text levelText;

    private void Start()
    {
        levelText = GetComponent<Text>();
        levelText.text = "Level " + PlayerStats.Instance.Level + " complete!";
    }
}