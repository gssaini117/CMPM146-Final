using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Masterscript_Over : MonoBehaviour
{
    // Buttons
    public Button Exit_Button;

    // Exit Behavior
    private void press_Exit() 
    {
        Debug.Log("Exit has been pressed");
        SceneManager.LoadScene("Scene_Start");
    }

    // Runs once to bind the button behaviors
    void Start() 
    {
        Exit_Button.onClick.AddListener(press_Exit);
    }
}
