using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masterscript : MonoBehaviour
{
    // Public Variables
    public Canvas Main_Screen;          // Main Menu Ui
    public Canvas Game_Screen;          // In-Game Ui
    public Canvas Over_Screen;          // Game Over Ui

    // Private Variables
    private bool Game_Over = true;           // Checks if the game is currently playing.
    private int Max_Health = 3;             // Max number of hits the player can take before failing.
    private int Max_Time = 60;              // Time until game is over?

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
