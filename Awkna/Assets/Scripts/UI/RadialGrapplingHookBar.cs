using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialGrapplingHookBar : MonoBehaviour
{
    public Image ImgBG;
    private float max;

    private float amount = 0;

    private void Start()
    {
        max = GameObject.FindWithTag("Player").GetComponent<RopeSystem>().startWaitTime;
    }

    public void Update()
    {
        amount = GameObject.FindWithTag("Player").GetComponent<RopeSystem>().waitTime;
        ImgBG.fillAmount = amount / max;
    }
}