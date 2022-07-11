using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField]
    private Slider staminaBar;
    public float Spd;
    void Awake()
    {
        staminaBar.value = 0;
    }
    private void Update()
    {
        Minus();
    }
    void Minus()
    {
        staminaBar.value += Spd * Time.deltaTime;
    }
}
