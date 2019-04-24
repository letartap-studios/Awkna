using UnityEngine;
using Cinemachine;

public class Bomb : MonoBehaviour
{
    public float timer;
    public float areaOfEffect;
    public GameObject whatIsBomb;
    public float damagePlayer = 1f;

    public LayerMask whatIsDestructible;
    public GameObject effect; //Explosion effect
    public Transform player;


    public float ShakeDuration = 0.3f;      //Time the camera shake effect will last
    public float ShakeAmplitude = 1.2f;     //Cinemacine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;     //Cinemacine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    //Cinemachine Shake
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private void Start()
    {
        damagePlayer /= 2; // divide the damage by 2 bc. the damage the player takes is doubled
        //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Collider2D[] objectsToDamage = Physics2D.OverlapCircleAll(transform.position, areaOfEffect, whatIsDestructible);
            for (int i = 0; i < objectsToDamage.Length; i++)
            {
                if (objectsToDamage[i].CompareTag("Player"))
                {
                    PlayerStats.Instance.TakeDamage(damagePlayer);
                }
                else
                {
                    Destroy(objectsToDamage[i].gameObject);
                }
            }

            ShakeElapsedTime = ShakeDuration;
            Instantiate(effect, transform.position, Quaternion.identity); //Explosion effect

            Destroy(whatIsBomb);

        }
        else
        {
            timer -= Time.deltaTime;
        }


        

        // If the Cinemachine componet is not set, avoid update
        if (virtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, areaOfEffect);
    }
}
