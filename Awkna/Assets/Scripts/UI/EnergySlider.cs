using UnityEngine;
using UnityEngine.UI;

// This script controlls the energy slider of the in-game UI

public class EnergySlider : MonoBehaviour
{
    private Slider energySlider;                // Refrence to the slider script.
    public PlayerController playerController;   // Refrence to the player controller.

    private void Start()
    {
        energySlider = GetComponent<Slider>();  // Get the energy slider component.
    }

    private void Update()
    {
        // Energy slider value is betweeen [0, 1],
        // so we need to scale the energy down by dividing it by its maximum value
        energySlider.value = (playerController.energy / playerController.maxEnergy);
    }

}
