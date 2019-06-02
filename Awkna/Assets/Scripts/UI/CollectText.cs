using UnityEngine;
using UnityEngine.UI;

public class CollectText : MonoBehaviour
{
    public Text collectText;
    [HideInInspector]
    public string str = "test";
    private float timer;
    public float initialTime;

    private void Start()
    {
        timer = initialTime;
    }

    private void Update()
    {
        
        if (timer <= 0)
        {
            gameObject.SetActive(false);
            timer = initialTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
