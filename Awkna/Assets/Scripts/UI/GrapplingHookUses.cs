using UnityEngine;
using UnityEngine.UI;

public class GrapplingHookUses : MonoBehaviour
{
    public Text numberOfCharges;
    public Image grapplingHookIcon;

    private void Update()
    {
        numberOfCharges.text = "" + PlayerStats.Instance.UsesUsed;
        if(PlayerStats.Instance.UsesUsed <= 0)
        {
            grapplingHookIcon.enabled = false;
        }
        else
        {
            grapplingHookIcon.enabled = true;
        }
    }
}
