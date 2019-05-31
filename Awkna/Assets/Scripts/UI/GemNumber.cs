using UnityEngine;
using UnityEngine.UI;

public class GemNumber : MonoBehaviour
{
    private Text gemNumberText;

    private void Start()
    {
        gemNumberText = GetComponent<Text>();
    }

    private void Update()
    {
        gemNumberText.text = "" + PlayerStats.Instance.GemNumber;
    }

}