using UnityEngine;
using UnityEngine.UI;

public class BombsDisplay : MonoBehaviour
{
    public Text bombsNumberText;

    private void Update()
    {
        bombsNumberText.text = "" + PlayerStats.Instance.BombsNumber;
    }

}
