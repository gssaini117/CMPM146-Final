using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{

    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, objects.Length);
        float rotate = 90f * Random.Range(0, objects.Length);
        Instantiate(objects[rand], transform.position, transform.rotation * Quaternion.Euler(0f, rotate, 0f));
        Debug.Log(rand);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
