using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardcoded_Bot : MonoBehaviour
{
    // Public Variables
    public GameObject Calling_Bot;  // The bot that's calling the script
    public GameObject Level;        // The game Level

    // Private Variables
    private int state = 0;          // 0 = Idle
                                    // 1 = Getting Cover
                                    // 2 = Attacking
                                    // 3 = Waiting

    // Locates the next waypoint cover closest to the player
    GameObject find_Cover() 
    {
        Debug.Log("find_Cover has been called");
        
        // Referencing the obstacles List
        Transform Obstacle_List = Level.transform.Find("Obstacles");
        Debug.Log(Obstacle_List == null);

        // Searching for the next closest cover towards the player direction
        GameObject Target_Cover = null;
        float path_Length = Mathf.Infinity;

        Debug.Log("Searching though children");
        int count = 0;
        foreach(Transform child in Obstacle_List)
        {
            // Getting waypoints of obstacle
            Transform Waypoints = child.transform.Find("Waypoints");

            // Debug
            count++;
            Debug.Log("Round number: " + count);
            Debug.Log("Obstacle Name: " + child.name);
            Debug.Log("Child Count: " + Waypoints.childCount);
        }

        // Returning the cover
        return Target_Cover;
    }

    // Locates the cover closest to the bot
    GameObject find_Cover_Emergency()
    {
        return null;   
    }

    // Paths to the waypoint closest to the bot
    void move_To_Cover(GameObject cover)
    {

    }

    // Start is called before render
    void Start()
    {
        find_Cover();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
