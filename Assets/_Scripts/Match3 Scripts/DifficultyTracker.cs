using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyTracker : MonoBehaviour
{
    public int difficulty = 0;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
