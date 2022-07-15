using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEffect : MonoSingleton<GoldEffect>
{
    public void GoldFade()
    {
        Player.Instance.gameOverSound.PlayOneShot(Player.Instance.goldClip);
    }
}
