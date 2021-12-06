using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Masterscript_Start : MonoBehaviour
{
    // Buttons
    public Button Start_Button;
    public Button Tutorial_Button;
    public Button Exit_Button;

    // Enacts start behavior
    private void press_Start()
    {
        Debug.Log("Start has been pressed");
        SceneManager.LoadScene("LevelScene");
    }

    // Enacts Tutorial Behavior
    private void press_Tutorial()
    {
        Debug.Log("Start has been pressed");    
    }

    // Enacts Exit Behavior
    private void press_Exit()
    {
        Debug.Log("Start has been pressed");
    }

    // Runs ones to bind button behaviors to the buttons.
    void Start() 
    {
        Start_Button.onClick.AddListener(press_Start);
        Start_Button.onClick.AddListener(press_Tutorial);
        Start_Button.onClick.AddListener(press_Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
