using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Public Adjustment Variables
    public int MAX_HEALTH = 3;             // Max number of hits the player can take before failing.
    public int HIT_DIST = 2;               // Amount of studs away from the player to be considered 'hit'.
    public int SPAWN_OFFSET = 2;           // Spawn offset from borders of map.
    public int BOT_COUNT = 5;              // Number of bots present.

    // Private Fluid Variables
    private bool Game_Over = false;         // Checks if the game is currently playing.
    private int Kill_Count = 0;             // Number of kills the player has.

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
        Instantiate(Bot_Prefab, pos, Quaternion.identity, Bot_List.transform);
    }

    // Checks to see if there are any bots within the 'hit' vacinity,
    // then 'respawns' the bot once its hit.

    private void check_Hit() {
        foreach (Transform Bot in Bot_List.transform) {
            // Distance from bot to player
            float distance = Vector3.Distance(Player.transform.position, Bot.transform.position);

            // Deletes the bot
            if (!Bot.gameObject.GetComponent<BehaviorTree>()) {
                if (distance > HIT_DIST) {
                    Kill_Count++;
                }
                Destroy(Bot.gameObject);
            }

            // Check Hit
            if (distance < HIT_DIST && Bot.gameObject.GetComponent<BehaviorTree>())
            {
                Destroy(Bot.gameObject.GetComponent<BehaviorTree>());
                MAX_HEALTH -= 1;
            }
        }
    }


    // ==================================================================
    // Update / Start Loop
    // ==================================================================
    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
        if (Game_Over) { 
            SceneManager.LoadScene("Scene_End");
        } // Only runs once the game has started

        // Making sure bots are available
        if (Bot_List.transform.childCount < BOT_COUNT) 
        {
            spawn_Bot();
        }

        // Performing Checks
        check_Hit();

        // Updating UI
        Game_Screen.transform.Find("TMP_Health").GetComponent<TMPro.TextMeshProUGUI>().text = 
            "Health: " + MAX_HEALTH;

        Game_Screen.transform.Find("TMP_Kills").GetComponent<TMPro.TextMeshProUGUI>().text = 
            "Kills: " + Kill_Count;

        // Checking for Game Over
        if (MAX_HEALTH <= 0) {
            Game_Over = true;
        }
    }
}
