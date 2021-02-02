using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameButtons : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    public void OnMiningButtonPressed()
    {
        gameManager.scanMode = false;
    }

    public void OnScanButtonPressed()
    {
        gameManager.scanMode = true;
    }
}
