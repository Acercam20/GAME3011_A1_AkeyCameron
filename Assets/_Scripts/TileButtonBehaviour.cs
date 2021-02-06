using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TileButtonBehaviour : MonoBehaviour
{
    public float resourceValue;
    public bool isScanned;
    public bool isMaxResource;
    public bool isSet = false;
    public bool scanSpread = false;
    public float spreadValue = 1000;
    public bool isMined = false;
    GameManager gameManager;
    private Vector2 startingSize;
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        startingSize = GetComponent<BoxCollider2D>().size;
        if (isMaxResource)
        {
            resourceValue = 2000;
            isSet = true;
            Invoke("Spread", 0.5f);
            Invoke("ResetCollider", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isSet)
        {
            if (Mathf.Abs(collision.gameObject.transform.position.x - gameObject.transform.position.x) > 45 || Mathf.Abs(collision.gameObject.transform.position.y - gameObject.transform.position.y) > 45)
            {
                resourceValue = collision.gameObject.GetComponent<TileButtonBehaviour>().spreadValue / 2;
            }
            else
            {
                resourceValue = collision.gameObject.GetComponent<TileButtonBehaviour>().spreadValue;
            }
            isSet = true;
        }
        if (collision.gameObject.GetComponent<TileButtonBehaviour>().scanSpread)
        {
            isScanned = true;
        }
    }

    public void OnButtonPressed()
    {
        if (gameManager.scanMode && gameManager.numberOfScans > 0) //Scaning tiles
        {
            GetComponent<BoxCollider2D>().size = startingSize;
            isScanned = true;
            scanSpread = true;
            Spread();
            gameManager.ScanResources();
        }
        else if (!gameManager.scanMode && gameManager.numberOfMines > 0) //Mining tiles
        {
            if (!isMined)
            {
                GetComponent<BoxCollider2D>().size = startingSize;
                isScanned = true;
                gameManager.MineResources(resourceValue);
                resourceValue = 0;
                isMined = true;
            }
            else
            {
                gameManager.MineResources(-1);
            }
        }
        
    }

    void Update()
    {
        if (isScanned)
        {
            GetComponent<Image>().color = new Color(1 * resourceValue / 2000, 0, 0);
        }
    }

    void Spread()
    {
        if (isMaxResource)
            spreadValue = 1000;
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x * 2.2f, GetComponent<BoxCollider2D>().size.y * 2.3f);
        GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<BoxCollider2D>().size.x * 2.2f, GetComponent<BoxCollider2D>().size.y * 2.3f);
    }

    void ResetCollider()
    {
        GetComponent<BoxCollider2D>().size = startingSize;
    }
}
