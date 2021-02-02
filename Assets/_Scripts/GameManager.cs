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
    public int numberOfScans;
    public int maxNumberOfScans = 5;
    public int numberOfMines;
    public int maxNumberOfMines = 3;
    public int numberOfMaxResources;
    public int tileRowLength;
    public int tileColLength;
    public GameObject tilePrefab;
    private List<Vector2> maxResourceLocations = new List<Vector2> { };
    
    void Start()
    {
        VariableCheck();
        InstatiateTiles();
        miningGameCanvas.SetActive(false);
    }

    void Update()
    {
        
    }

    public void MineResources(float amountToAdd)
    {
        if (amountToAdd == -1)
        {
            Debug.Log("No");
        }
        else if (numberOfMines > 0)
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
}
