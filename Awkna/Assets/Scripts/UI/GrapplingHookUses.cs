using UnityEngine;
using UnityEngine.UI;

public class GrapplingHookUses : MonoBehaviour
{
    public Text numberOfCharges;

    private void Update()
    {
        numberOfCharges.text = "" + RopeSystem.Instance.usesUsed;
    }
}
