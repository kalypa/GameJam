using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField]
    private Text textMesh;

    private void Start()
    {
        textMesh.text = "";
    }

    void Update()
    {
        textMesh.text = GameManager.Instance.playerData.playerMoney.ToString();
    }
}
