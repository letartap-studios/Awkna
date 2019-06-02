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

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
    }
    private void Start()
    {
        timeLeft = 0;
        oxygenAmmount = maxOxygenAmmount;
    }

    private void Update()
    {

        if (oxygenAmmount <= 0)
        {
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
        }
    }
}