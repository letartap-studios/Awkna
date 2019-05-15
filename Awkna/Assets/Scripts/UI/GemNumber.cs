using UnityEngine;
using UnityEngine.UI;

public class GemNumber : MonoBehaviour
{
    private PlayerController playerController;
    private Text gemNumberText;

    private void Start()
    {
        gemNumberText = GetComponent<Text>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        gemNumberText.text = "" + playerController.gemNumber;
    }

}