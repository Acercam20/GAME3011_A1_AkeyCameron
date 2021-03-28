using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Mining Game
    public bool scanMode = true;
    public bool miningGameToggled = false;
    public float resourcesCollected = 0;
    public GameObject miningGameCanvas;
    public Text resourcesCollectedText;
    public Text minesRemainingText;
    public Text scansRemainingText;
    public Button scanButton;
    public Button mineButton;
    public Image buttonHighlight;
    public Image endImage;
    public Text endText;
    public int numberOfScans;
    public int maxNumberOfScans = 5;
    public int numberOfMines;
    public int maxNumberOfMines = 3;
    public int numberOfMaxResources;
    public int tileRowLength;
    public int tileColLength;
    public GameObject tilePrefab;
    private List<Vector2> maxResourceLocations = new List<Vector2> { };

    //Lockpick Game
    public GameObject stopBar1, stopBar2, stopBar3, stopBar4, stopBar5;
    public GameObject targetZone1, targetZone2, targetZone3, targetZone4, targetZone5;
    public GameObject centreTarget;
    public Text lockPickStatusText;
    public int barDifficulty;
    public int maxAttempts = 5;
    public int currentAttempts = 5;
    public float acceptedRange = 15;
    private int barNum = 1;
    public Text attemptsText;
    public List<GameObject> highDifficultyObjects;
    public List<Vector3> oldPos;
    public bool lockToggle = false;
    public GameObject lockCanvas;

    //Match 3 Game //All of these. Probably don't need any of them.
    public int matchDifficulty = 0; // 0 = easy, 1 = medium, 2 = hard
    public float remainingTime;
    public float maxTime;
    public int remainingMoves;
    public int maxMoves;
    public int tilesCleared = 0;
    public int tilesGoal;
    public bool bombsEnabled = false, blockTilesEnabled = false;
    private float timer = 0;
    public Text timerText;
    public Text difficultyText;
    public Text movesText;
    public Text tilesClearedText;
    public GameObject match3Object;
    public bool resetBool = false;
    public bool match3Active = true;

    public Text victoryText;
    public GameObject endScreenObject;
    void Start()
    {
        endScreenObject.SetActive(false);
        matchDifficulty = GameObject.FindWithTag("Tracker").GetComponent<DifficultyTracker>().difficulty;

        if (matchDifficulty == 0)
        {
            maxMoves = 200;
            remainingMoves = maxMoves;
            maxTime = 300;
            remainingTime = maxTime;
            tilesGoal = 100;
            difficultyText.text = "Easy";
        }
        else if (matchDifficulty == 1)
        {
            maxMoves = 150;
            remainingMoves = maxMoves;
            maxTime = 180;
            remainingTime = maxTime;
            bombsEnabled = true;
            tilesGoal = 100;
            difficultyText.text = "Medium";
        }
        else if (matchDifficulty == 2)
        {
            maxMoves = 100;
            remainingMoves = maxMoves;
            maxTime = 60;
            remainingTime = maxTime;
            bombsEnabled = true;
            blockTilesEnabled = true;
            tilesGoal = 100;
            difficultyText.text = "Hard";
        }
        else
        {
            matchDifficulty = 0;
            maxMoves = 200;
            remainingMoves = maxMoves;
            maxTime = 300;
            remainingTime = maxTime;
            tilesGoal = 100;
            difficultyText.text = "Easy";
        }
        movesText.text = remainingMoves.ToString();
        tilesClearedText.text = "0";

        VariableCheck();
        InstatiateTiles();
        miningGameCanvas.SetActive(false);
        endImage.enabled = false;
        endText.enabled = false;
        if (barDifficulty == 3)
        {
            targetZone1.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone2.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone3.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone4.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone5.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
        }
        oldPos.Add(targetZone1.transform.position);
        oldPos.Add(targetZone2.transform.position);
        oldPos.Add(targetZone3.transform.position);
        oldPos.Add(targetZone4.transform.position);
        oldPos.Add(targetZone5.transform.position);

        lockCanvas.SetActive(false);
        lockToggle = false;
    }

    void Update()
    {
        if (resetBool)
        {
            resetBool = false;
            SceneManager.LoadScene("SampleScene");
        }

        if (numberOfMines == 0)
        {
            endImage.enabled = true;
            endImage.transform.SetAsLastSibling();
            endText.enabled = true;
            endText.transform.SetAsLastSibling();
            endText.text = "Alertium Obtained: " + resourcesCollected.ToString();
        }

        timer += Time.deltaTime;
        //remainingTime = maxTime - Mathf.FloorToInt(timer % 60);
        if (Mathf.FloorToInt(timer % 60) == 1)
        {
            timer = 0;
            remainingTime--;
        }
        timerText.text = "Timer: \r" + remainingTime;
        if (remainingTime <= 0 || remainingMoves <= 0)
        {
            MatchOver(false);
        }
        if (tilesCleared >= tilesGoal)
        {
            MatchOver(true);
        }
    }

    public void MatchOver(bool win)
    {
        endScreenObject.SetActive(true);
        if (win)
        {
            victoryText.text = "Victory!";
        }
        else
        {
            victoryText.text = "Defeat";
        }
    }

    public void MineResources(float amountToAdd)
    {
        if (amountToAdd < 0)
        {
            Debug.Log("No");
        }
        if (numberOfMines > 0)
        {
            resourcesCollected = resourcesCollected + amountToAdd;
            resourcesCollectedText.text = resourcesCollected.ToString();
            numberOfMines--;
            minesRemainingText.text = numberOfMines.ToString();
        }
    }

    public void ScanResources()
    {
        if (numberOfScans > 0)
        {
            numberOfScans--;
            scansRemainingText.text = numberOfScans.ToString();
        }
    }

    public void ToggleLockGame()
    {
        if (lockToggle)
        {
            lockCanvas.SetActive(false);
            lockToggle = false;
        }
        else
        {
            lockCanvas.SetActive(true);
            lockToggle = true;
        }
    }
    public void ToggleMiningGame()
    {
        if (miningGameToggled)
        {
            miningGameCanvas.SetActive(false);
            miningGameToggled = false;
        }
        else
        {
            miningGameCanvas.SetActive(true);
            miningGameToggled = true;
        }
    }
    public void ToggleScanMode(bool on)
    {
        scanMode = on;
        if (scanMode)
        {
            buttonHighlight.transform.position = scanButton.transform.position;
        }
        else
        {
            buttonHighlight.transform.position = mineButton.transform.position;
        }
    }

    void VariableCheck()
    {
        numberOfScans = maxNumberOfScans;
        numberOfMines = maxNumberOfMines;

        if (tileColLength > 32)
            tileColLength = 32;
        if (tileRowLength > 32)
            tileRowLength = 32;
        if (tileRowLength < 0)
            tileRowLength = 0;
        if (tileColLength < 0)
            tileColLength = 0;

    }

    void InstatiateTiles()
    {
        for (int i = 0; i < numberOfMaxResources; i++)
        {
            maxResourceLocations.Add(new Vector2(Random.Range(0, tileRowLength), Random.Range(0, tileColLength)));
        }

        for (int i = 0;  i < tileRowLength; i++)
        {
            for (int j = 0; j < tileColLength; j++)
            {
                Vector3 pos = new Vector3(-300 + (i * 25), 375 - (j * 25), 0);
                var tile = Instantiate(tilePrefab, pos, Quaternion.identity);
                tile.transform.SetParent(miningGameCanvas.transform, false);
                for (int k = 0; k < numberOfMaxResources; k++)
                {
                    if (i == maxResourceLocations[k].x && j == maxResourceLocations[k].y)
                    {
                        tile.GetComponent<TileButtonBehaviour>().isMaxResource = true;
                    }
                }
            }
        }
    }

    public void StopBar()
    {
        if (barNum == 1)
        {
            stopBar1.GetComponent<StopBarBehaviour>().isRotating = false;
            if (CheckBar(1))
            {
                barNum = 2;
            }
            else
            {
                ResetBars(false);
            }
        }
        else if (barNum == 2)
        {
            stopBar2.GetComponent<StopBarBehaviour>().isRotating = false;
            if (CheckBar(2))
            {
                barNum = 3;
            }
            else
            {
                ResetBars(false);
            }

        }
        else if (barNum == 3)
        {
            stopBar3.GetComponent<StopBarBehaviour>().isRotating = false;
            if (CheckBar(3))
            {
                if (barDifficulty == 1)
                {
                    Debug.Log("Victory!");
                    lockPickStatusText.text = "Lock Picked!";
                    Invoke("ResetBars", 2);
                }
                else
                {
                    barNum = 4;
                }
            }
            else
            {
                ResetBars(false);
            }

        }
        else if (barNum == 4)
        {
            stopBar4.GetComponent<StopBarBehaviour>().isRotating = false;
            if (CheckBar(4))
            {
                barNum = 5;
            }
            else
            {
                ResetBars(false);
            }

        }
        else if (barNum == 5)
        {
            stopBar5.GetComponent<StopBarBehaviour>().isRotating = false;
            if (CheckBar(5))
            {
                Debug.Log("Victory!");
                lockPickStatusText.text = "Lock Picked!";
                Invoke("ResetBars", 2);
                //ResetBars(true);
            }
            else
            {
                ResetBars(false);
            }
        }
        else
        {
            Debug.Log("Problem with stop bar numbers (GameManager.cs)");
        }
    }

    public void SetDifficulty(int i)
    {
        barDifficulty = i;
        if (i == 1)
        {
            for (int d = 0; d < highDifficultyObjects.Count; d++)
            {
                highDifficultyObjects[d].SetActive(false);
            }
        }
        else if (i == 2)
        {
            for (int d = 0; d < highDifficultyObjects.Count; d++)
            {
                highDifficultyObjects[d].SetActive(true);
            }
            targetZone1.transform.eulerAngles = new Vector3(0, 0, 0);
            targetZone2.transform.eulerAngles = new Vector3(0, 0, 0);
            targetZone3.transform.eulerAngles = new Vector3(0, 0, 0);
            targetZone4.transform.eulerAngles = new Vector3(0, 0, 0);
            targetZone5.transform.eulerAngles = new Vector3(0, 0, 0);

            targetZone1.transform.position = oldPos[0];
            targetZone2.transform.position = oldPos[1];
            targetZone3.transform.position = oldPos[2];
            targetZone4.transform.position = oldPos[3];
            targetZone5.transform.position = oldPos[4];
        }
        else if (i == 3)
        {
            for (int d = 0; d < highDifficultyObjects.Count; d++)
            {
                highDifficultyObjects[d].SetActive(true);
            }
            targetZone4.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone5.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone1.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone2.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone3.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
        }
    }

    public bool CheckBar(int i)
    {
        bool toReturn = false;
        if (i == 1)
        {
            Debug.Log(stopBar1.transform.eulerAngles.z.ToString());
            Debug.Log(targetZone1.transform.eulerAngles.z.ToString());
            if (stopBar1.GetComponent<StopBarBehaviour>().inZone) //stopBar1.transform.eulerAngles.z > targetZone1.transform.eulerAngles.z - acceptedRange && stopBar1.transform.eulerAngles.z < targetZone1.transform.eulerAngles.z + acceptedRange
            {
                toReturn = true;
            }
        }
        else if (i == 2)
        {
            if (stopBar2.GetComponent<StopBarBehaviour>().inZone) //stopBar1.transform.eulerAngles.z > targetZone1.transform.eulerAngles.z - acceptedRange && stopBar1.transform.eulerAngles.z < targetZone1.transform.eulerAngles.z + acceptedRange
            {
                toReturn = true;
            }
        }
        else if (i == 3)
        {
            if (stopBar3.GetComponent<StopBarBehaviour>().inZone) //stopBar3.transform.eulerAngles.z > targetZone3.transform.eulerAngles.z - acceptedRange && stopBar3.transform.eulerAngles.z < targetZone3.transform.eulerAngles.z + acceptedRange
            {
                toReturn = true;
            }
        }
        else if (i == 4)
        {
            if (stopBar4.GetComponent<StopBarBehaviour>().inZone) //stopBar4.transform.eulerAngles.z > targetZone4.transform.eulerAngles.z - acceptedRange && stopBar4.transform.eulerAngles.z < targetZone4.transform.eulerAngles.z + acceptedRange
            {
                toReturn = true;
            }
        }
        else if (i == 5)
        {
            if (stopBar5.GetComponent<StopBarBehaviour>().inZone) //stopBar5.transform.eulerAngles.z > targetZone5.transform.eulerAngles.z - acceptedRange && stopBar5.transform.eulerAngles.z < targetZone5.transform.eulerAngles.z + acceptedRange
            {
                toReturn = true;
            }
        }
        return toReturn;
    }

    private void ResetBars(bool victoryReset = true)
    {
        Debug.Log("ResetBars Called");
        if (!victoryReset)
        {
            currentAttempts--;
            attemptsText.text = "Attempts Remaining: " + currentAttempts.ToString();
            if (currentAttempts <= 0)
            {
                lockPickStatusText.text = "Lock Pick Failed";
                currentAttempts = maxAttempts;
            }
        }
        else
        {
            currentAttempts = maxAttempts;
            attemptsText.text = "Attempts Remaining: " + currentAttempts.ToString();
        }
        Invoke("ResetStatusText", 3);
        stopBar1.GetComponent<StopBarBehaviour>().isRotating = true;
        stopBar2.GetComponent<StopBarBehaviour>().isRotating = true;
        stopBar3.GetComponent<StopBarBehaviour>().isRotating = true;
        if (!(barDifficulty == 1))
        {
            stopBar4.GetComponent<StopBarBehaviour>().isRotating = true;
            stopBar5.GetComponent<StopBarBehaviour>().isRotating = true;
        }
        barNum = 1;
        if (barDifficulty == 3)
        {
            targetZone4.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone5.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone1.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone2.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
            targetZone3.transform.RotateAround(centreTarget.transform.position, Vector3.forward, Random.Range(0, 360));
        }
    }

    public void ResetStatusText()
    {
        lockPickStatusText.text = "";
        attemptsText.text = "Attempts Remaining: " + currentAttempts.ToString();
    }
}
