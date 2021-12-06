using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Master Script manages player stats and game state transition.

public class Masterscript_Game : MonoBehaviour
{
    // ==================================================================
    // Variables
    // ==================================================================
    // Public Variables
    public Canvas Game_Screen;              // In-Game Ui

    public GameObject Player;               // Player Object
    public GameObject Bot_List;             // Empty List of Bots
    public GameObject Level;                // Game Level

    // Private Adjustment Variables
    private int MAX_HEALTH = 10;             // Max number of hits the player can take before failing.
    private int HIT_DIST = 2;               // Amount of studs away from the player to be considered 'hit'.
    private int SPAWN_OFFSET = 2;           // Spawn offset from borders of map.

    // Private Fluid Variables
    private bool Game_Over = false;         // Checks if the game is currently playing.
    private int Current_Health = 3;         // Player Current Health.
    private int Max_Time = 60;              // Time until game is over?


    // ==================================================================
    // Functions
    // ==================================================================
    // 'Respawns' the bot
    private void respawn_Bot(Transform Bot) {
        float offset = Random.Range(0, SPAWN_OFFSET + 1);
        int direction = Random.Range(0, 9);
        float half_dist = 20f; // Hardcoded
        offset = half_dist - offset;

        switch(direction)
        {
            case 0: // Top left
                Bot.position = new Vector3(offset, -13.2f, offset);
                break;
            case 1: // Top
                Bot.position = new Vector3(-offset, -13.2f, offset);
                break;
            case 2: // Top Right
                Bot.position = new Vector3(offset, -13.2f, -offset);
                break;
            case 3: // Mid Left
                Bot.position = new Vector3(-offset, -13.2f, -offset);
                break;
            case 4: // Mid Right
                Bot.position = new Vector3(0f, -13.2f, offset);
                break;
            case 5: // Bot Left
                Bot.position = new Vector3(0f, -13.2f, -offset);
                break;
            case 6: // Bot Mid
                Bot.position = new Vector3(offset, -13.2f, 0f);
                break;
            case 7: // Bot Right
                Bot.position = new Vector3(-offset, -13.2f, 0f);
                break;
        }
    }
    
    // Checks to see if there are any bots within the 'hit' vacinity,
    // then 'respawns' the bot once its hit.
    private void check_Hit() {
        foreach (Transform Bot in Bot_List.transform) {
            // Distance from bot to player
            float distance = Vector3.Distance(Player.transform.position, Bot.position);

            // Check Hit
            if (distance < HIT_DIST)
            {
                Debug.Log("BOT HAS HIT THE PLAYER");
                // Reducing Health
                Current_Health -= 1;
                respawn_Bot(Bot);
            }
        }
    }
    

    // ==================================================================
    // Update / Start Loop
    // ==================================================================
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform Bot in Bot_List.transform) {
            respawn_Bot(Bot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Game_Over) { return; } // Only runs once the game has started
        
        // Performing Checks
        check_Hit();

        // Updating UI
        Game_Screen.transform.Find("TMP_Health").GetComponent<TMPro.TextMeshProUGUI>().text = 
            "Health: " + Current_Health;

        // Checking for Game Over
        if (Current_Health <= 0) {
            Game_Over = true;
        }
    }
}
