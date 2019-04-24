using Cinemachine;
using UnityEngine;

public class SimpleCameraShake : MonoBehaviour
{
    public float ShakeDuration = 0.3f;      //Time the camera shake effect will last
    public float ShakeAmplitude = 1.2f;     //Cinemacine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;     //Cinemacine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;


    //Cinemachine Shake
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    public Bomb bomb;
    float timer = 2.5f;

    // Start is called before the first frame update
    void Start()
    {

        //Get Virtual Camera Noise Profile
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ShakeElapsedTime = 0;
        //timer = 2.5f;
        //if (Input.GetButtonDown("Bomb"))
        //{
        //    if (timer <= 0)
        //    {
        //        ShakeElapsedTime = ShakeDuration;   
        //    }
        //    else
        //    {
        //        timer -= Time.deltaTime;
        //    }
        //    ShakeCamera(ShakeElapsedTime);
        //}

        //ShakeElapsedTime = 0;
        //if (Input.GetKey(KeyCode.K))
        //{
        //    ShakeElapsedTime = ShakeDuration;
        //}
        //ShakeCamera(ShakeElapsedTime);

        if (Input.GetKey(KeyCode.K))
        {
            ShakeElapsedTime = ShakeDuration;
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


    //use default shake settings but change duration
    //public void ShakeCamera(float duration)
    //{

    //    // If the Cinemachine componet is not set, avoid update
    //    if (virtualCamera != null && virtualCameraNoise != null)
    //    {
    //        // If Camera Shake effect is still playing
    //        if (duration > 0)
    //        {
    //            // Set Cinemachine Camera Noise parameters
    //            virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
    //            virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

    //            // Update Shake Timer
    //            duration -= Time.deltaTime;
    //        }
    //        else
    //        {
    //            // If Camera Shake effect is over, reset variables
    //            virtualCameraNoise.m_AmplitudeGain = 0f;
    //            duration = 0f;
    //        }
    //    }
    //}
    //use custom shake settings
}