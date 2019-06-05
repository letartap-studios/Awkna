using UnityEngine;
using UnityEngine.UI;

public class GrapplingHookUses : MonoBehaviour
{
    public Text numberOfCharges;
    public Image grapplingHookIcon;

    private void Update()
    {
        numberOfCharges.text = "" + RopeSystem.Instance.usesUsed;
        if(RopeSystem.Instance.usesUsed <= 0)
        {
            grapplingHookIcon.enabled = false;
        }
        else
        {
            grapplingHookIcon.enabled = true;
        }
    }
}
