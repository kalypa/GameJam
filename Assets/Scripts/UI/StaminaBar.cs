using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoSingleton<StaminaBar>
{
    public Slider staminaBar;
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
    public void Zero()
    {
        if (staminaBar.value >= 1)
        {
            Player.Instance.Dead();
            Player.Instance.GameOverDelay();
        }
    }
}
