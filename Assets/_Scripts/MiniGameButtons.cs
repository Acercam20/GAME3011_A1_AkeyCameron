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
        gameManager.ToggleScanMode(false);
    }

    public void OnScanButtonPressed()
    {
        gameManager.ToggleScanMode(true);
    }

    public void OnStopButtonPressed()
    {
        gameManager.StopBar();
    }
}
