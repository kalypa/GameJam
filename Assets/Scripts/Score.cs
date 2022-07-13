using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoSingleton<Score>
{
    public int score = 0;
    public Text scoreText;

    void Update()
    {
        scoreText.text = score.ToString() + "F";
    }
}
