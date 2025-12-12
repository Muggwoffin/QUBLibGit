using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxNoise(int noise)
    {
        slider.maxValue = noise;
        slider.value = 0;
    }
    
    public void SetNoise(int noise)
    {
        slider.value = noise;
    }
}
