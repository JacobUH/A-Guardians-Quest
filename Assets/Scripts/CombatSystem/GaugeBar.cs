using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image emptyBar;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetBar(int maxValue)
    {
        if (slider.maxValue == maxValue) return;
        slider.maxValue = maxValue;
        slider.value = maxValue;

        healthBar.color = gradient.Evaluate(1f);
    }

    public void ChangeBar(int currentValue)
    {
        slider.value = currentValue;
        healthBar.color = gradient.Evaluate(slider.normalizedValue);
    }


}
