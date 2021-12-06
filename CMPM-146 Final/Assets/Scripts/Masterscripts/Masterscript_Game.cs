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
    public GameObject Bot_Prefab;           // Bot Enemy Prefab

    // Private Adjustment Variables
    private int MAX_HEALTH = 10;            // Max number of hits the player can take before failing.
    private int HIT_DIST = 2;               // Amount of studs away from the player to be considered 'hit'.
    private int SPAWN_OFFSET = 2;           // Spawn offset from borders of map.
    private int BOT_COUNT = 10;              // Number of bots present.

    // Private Fluid Variables
    private bool Game_Over = false;         // Checks if the game is currently playing.
    private int Current_Health = 3;         // Player Current Health.
    private int Max_Time = 60;              // Time until game is over?
    private int Num_Bots = 0;               // Current number of bots alive.

    // ==================================================================
    // Functions
    // ==================================================================
    // 'Respawns' the bot
    private void spawn_Bot() {
        float offset = Random.Range(0, SPAWN_OFFSET + 1);
        int direction = Random.Range(0, 8);
        float half_dist = 27f; // Hardcoded
        float final_dist = half_dist - offset;

        Vector3 pos = new Vector3(0, 0, 0);    // Spawn Position

        switch(direction)
        {
            case 0: // Top left
                pos = new Vector3(final_dist, 1f, final_dist);
                break;
            case 1: // Top
                pos = new Vector3(-final_dist, 1f, final_dist);
                break;
            case 2: // Top Right
                pos = new Vector3(final_dist, 1f, -final_dist);
                break;
            case 3: // Mid Left
                pos = new Vector3(-final_dist, 1f, -final_dist);
                break;
            case 4: // Mid Right
                pos = new Vector3(0f, 1f, final_dist);
                break;
            case 5: // Bot Left
                pos = new Vector3(0f, 1f, -final_dist);
                break;
            case 6: // Bot Mid
                pos = new Vector3(final_dist, 1f, 0f);
                break;
            case 7: // Bot Right
                pos = new Vector3(-final_dist, 1f, 0f);
                break;
        }
        Debug.Log(pos);
        Instantiate(Bot_Prefab, pos, Quaternion.identity, Bot_List.transform);
        Num_Bots++;
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
                Destroy(Bot.gameObject);
                Num_Bots--;
                
                // Reducing Health
                Current_Health -= 1;
            }
        }
    }
    

    // ==================================================================
    // Update / Start Loop
    // ==================================================================
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Game_Over) { return; } // Only runs once the game has started
        
        // Making sure bots are available
        if (Num_Bots < BOT_COUNT) 
        {
            spawn_Bot();
        }

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
