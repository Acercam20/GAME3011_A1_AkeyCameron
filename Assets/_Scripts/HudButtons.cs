using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudButtons : MonoBehaviour
{
    public void OnMiningGameButtonPressed()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().ToggleMiningGame();
    }
}
