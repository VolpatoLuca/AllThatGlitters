using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnergyBar : MonoBehaviour
{

    
    Slider slider;
    [SerializeField]
    Image fill;
    [SerializeField]
    Gradient gradient;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        DisplayColor();
    }

    void DisplayColor()
    {
        fill.color = gradient.Evaluate(slider.value);              
    }
}
