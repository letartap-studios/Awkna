using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Oxygen : MonoBehaviour
{

    private float timeLeft;
    private Image barImage;
    public float damagePerHit;
    public float timeBetweenHits;
    public float OxygenTime = 10f;
    bool hasOxygen;

    // Start is called before the first frame update
    void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        timeLeft = OxygenTime;
        hasOxygen = true;

    }


    void Update()
    {
        if (timeLeft > 0 && hasOxygen==true)
        {
            timeLeft -= Time.deltaTime;
            barImage.fillAmount = timeLeft / OxygenTime;
        }

        else
        {
            hasOxygen = false;
        }

        if (!hasOxygen)
        {   
            StartCoroutine(dealDamageOverTime(this.timeBetweenHits));               //linia de cod gay.
        }
    }



    public IEnumerator dealDamageOverTime(float timeBetweenHits)
    {


        Debug.Log("damaged player");


        PlayerStats.Instance.TakeDamage(damagePerHit);

        yield return new WaitForSeconds(timeBetweenHits);


    }
}
