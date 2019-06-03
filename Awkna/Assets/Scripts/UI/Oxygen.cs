using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{
    public float maxOxygenAmmount;
    private float timeLeft;
    public float timeBetweenHits;
    public float damagePerHit;

    [SerializeField]
    private float oxygenAmmount;
    public float speedOfOxygenUsage;
    private Image barImage;
    private bool lowLevelOxygenAlarm = false;
    private bool zeroOxygenAlarm = false;
    private GameObject dialogue;
    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
    }
    private void Start()
    {
        timeLeft = 0;
        oxygenAmmount = maxOxygenAmmount;
        dialogue = GameObject.FindWithTag("Dialogue");
    }

    private void Update()
    {

        if (oxygenAmmount <= 0)
        {
            if (zeroOxygenAlarm == false)
            {
                zeroOxygenAlarm = true;
                dialogue.GetComponent<DialogueTrigger>().OxygenLevelDialogue("NO MORE OXYGEN!");
            }
            barImage.color = Color.red;
            if (timeLeft <= 0)
            {
                PlayerStats.Instance.TakeDamage(damagePerHit);
                timeLeft = timeBetweenHits;
            }
            else
            {
                timeLeft -= Time.deltaTime;
                barImage.fillAmount = timeLeft / timeBetweenHits;
            }
        }
        else
        {
            oxygenAmmount -= Time.deltaTime * speedOfOxygenUsage;
            barImage.fillAmount = oxygenAmmount / maxOxygenAmmount;

            if (lowLevelOxygenAlarm == false && ((oxygenAmmount / maxOxygenAmmount) <= 0.3f)) 
            {
                lowLevelOxygenAlarm = true;
                dialogue.GetComponent<DialogueTrigger>().OxygenLevelDialogue("Oxygen Level: LOW!");
            }
        }
    }
}