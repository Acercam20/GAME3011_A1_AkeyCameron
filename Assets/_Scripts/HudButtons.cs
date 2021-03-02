using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudButtons : MonoBehaviour
{
    public void OnMiningGameButtonPressed()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().ToggleMiningGame();
    }

    public void Difficulty1Button()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().SetDifficulty(1);
    }
    public void Difficulty2Button()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().SetDifficulty(2);
    }
    public void Difficulty3Button()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().SetDifficulty(3);
    }
}
