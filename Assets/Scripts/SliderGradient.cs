using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGradient : MonoBehaviour
{
    public Slider slider;           // Reference to the UI slider
    public Image fillImage;         // The fill area of the slider (must be Image component)

    public Gradient gradient;       // The gradient of colors to apply

    void Start()
    {
        // Initialize the slider and apply the gradient color based on the current value
        slider.onValueChanged.AddListener(UpdateSliderColor);
        UpdateSliderColor(slider.value);  // Apply initial color
    }

    // This method is called whenever the slider's value changes
    public void UpdateSliderColor(float value)
    {
        // Get the normalized value (0 to 1) from the slider
        float normalizedValue = slider.normalizedValue;

        // Set the color of the fill image based on the gradient
        fillImage.color = gradient.Evaluate(normalizedValue);
    }
}
