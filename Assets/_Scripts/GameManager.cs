using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
    public GameObject stopBar1, stopBar2, stopBar3;
    public Text lockPickStatusText;
    private List<Vector2> maxResourceLocations = new List<Vector2> { };
    private int barNum = 1;

    void Start()
    {
        VariableCheck();
        InstatiateTiles();
        miningGameCanvas.SetActive(false);
        endImage.enabled = false;
        endText.enabled = false;
    }

    void Update()
    {
        if (numberOfMines == 0)
        {
            endImage.enabled = true;
            endImage.transform.SetAsLastSibling();
            endText.enabled = true;
            endText.transform.SetAsLastSibling();
            endText.text = "Alertium Obtained: " + resourcesCollected.ToString();
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

    private bool CheckBar(int i)
    {
        bool toReturn = false;
        if (i == 1)
        {
            Debug.Log(stopBar1.transform.rotation.z);
            if (stopBar1.transform.rotation.z > -0.15 && stopBar1.transform.rotation.z < 0.15)
            {
                toReturn = true;
            }
        }
        else if (i == 2)
        {
            if (stopBar2.transform.rotation.z > -0.15 && stopBar2.transform.rotation.z < 0.15)
            {
                toReturn = true;
            }
        }
        else if (i == 3)
        {
            if (stopBar3.transform.rotation.z > -0.15 && stopBar3.transform.rotation.z < 0.15)
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
            lockPickStatusText.text = "Lock Pick Failed";
        }
        Invoke("ResetStatusText", 2);
        stopBar1.GetComponent<StopBarBehaviour>().isRotating = true;
        stopBar2.GetComponent<StopBarBehaviour>().isRotating = true;
        stopBar3.GetComponent<StopBarBehaviour>().isRotating = true;
        barNum = 1;
    }

    public void ResetStatusText()
    {
        lockPickStatusText.text = "";
    }
}
