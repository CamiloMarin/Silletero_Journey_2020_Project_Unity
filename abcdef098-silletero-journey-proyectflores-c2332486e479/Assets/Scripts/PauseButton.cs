using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    bool isPaused = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
        }
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
