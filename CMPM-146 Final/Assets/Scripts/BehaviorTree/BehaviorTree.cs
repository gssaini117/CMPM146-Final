using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    private Node mRoot;
    private bool StartedBehavior;
    private Coroutine behavior;

    public Dictionary<string, object> Blackboard {get; set; }
    public Node Root {get {return mRoot;}}
    // Start is called before the first frame update
    void Start()
    {
        Blackboard = new Dictionary<string, object>();
        Blackboard.Add("WorldBounds", new Rect(0,0,40,40));

        // initial behavior is stopped
        StartedBehavior = false;


        // modify this to create the Behavior Tree
        mRoot = new Repeater(this, new Sequencer(this, new Node[] { new RandomWalk(this)}));
    }

    // Update is called once per frame
    void Update()
    {
        if(!StartedBehavior) {
            StartCoroutine(RunBehavior());
            StartedBehavior = true;
        }
    }

    private IEnumerator RunBehavior() 
    {
        Node.Result result = Root.Execute();
        while(result == Node.Result.Running) 
        {
            //Debug.Log("Root result: " + result);
            yield return null;
            result = Root.Execute();
        }

        Debug.Log("Behavior has finished with: " + result);
    }
}


