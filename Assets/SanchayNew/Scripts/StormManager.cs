using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormManager : MonoBehaviour
{
    public ParticleSystem rain, thunder;
    public Material skyBoxCloudy;

    [Range(0f, 15f)] public float maxThunderValue = 15f; 
    [Range(0f, 250f)] public float maxRainValue = 250f; 

    [Range(0f, 1f)] public float rotationSpeed;

    [Range(0.25f, 1f)] public float exposureValue = 0.25f;

    [Range(0f, 1f)] public float masterSlider = 0f; // 0 -> storm is very strong, 1 -> clear sky

    private float lastMasterSliderValue;

    public float rainValue, thunderValue;

    private void Start()
    {
        lastMasterSliderValue = masterSlider;
    }

    private void Update()
    {
        float newRotation = skyBoxCloudy.GetFloat("_Rotation") + rotationSpeed * Time.deltaTime;
        skyBoxCloudy.SetFloat("_Rotation", newRotation);

        if (!Mathf.Approximately(lastMasterSliderValue, masterSlider))
        {
            // Update last value
            lastMasterSliderValue = masterSlider;

            UpdateStormParameters();
        }
    }

    private void UpdateStormParameters()
    {
        thunderValue = Mathf.Lerp(maxThunderValue, 0f, masterSlider);
        rainValue = Mathf.Lerp(maxRainValue, 0f, masterSlider);
        exposureValue = Mathf.Lerp(0.25f, 1f, masterSlider);

        // Apply changes
        ModifyExposure();
        ModifyLightning();
        ModifyRain();
    }

    private void ModifyExposure()
    {
        skyBoxCloudy.SetFloat("_Exposure", exposureValue);
    }

    private void ModifyLightning()
    {
        var thunderEmission = thunder.emission;
        thunderEmission.rateOverTime = thunderValue;
    }

    private void ModifyRain()
    {
        var rainEmission = rain.emission;
        rainEmission.rateOverTime = rainValue;
    }
}
