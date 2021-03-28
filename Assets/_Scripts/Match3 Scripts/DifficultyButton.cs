using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DifficultyButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEasyButton()
    {
        GameObject.FindWithTag("Tracker").GetComponent<DifficultyTracker>().difficulty = 0;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().difficultyText.text = "Easy";
        SceneManager.LoadScene("SampleScene");
    }
    public void OnMedButton()
    {
        GameObject.FindWithTag("Tracker").GetComponent<DifficultyTracker>().difficulty = 1;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().difficultyText.text = "Medium";
        SceneManager.LoadScene("SampleScene");
    }
    public void OnHardButton()
    {
        GameObject.FindWithTag("Tracker").GetComponent<DifficultyTracker>().difficulty = 2;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().difficultyText.text = "Hard";
        SceneManager.LoadScene("SampleScene");
    }

    public void CloseMatch3Game()
    {
        if (GameObject.FindWithTag("GameController").GetComponent<GameManager>().match3Active)
        {
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().match3Object.SetActive(false);
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().match3Active = false;

        }
        else
        {
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().match3Object.SetActive(true);
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().match3Active = true;
        }
        
    }
}
