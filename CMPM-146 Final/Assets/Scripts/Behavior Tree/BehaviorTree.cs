using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    private BTNode mRoot;
    private bool StartedBehavior;
    private Coroutine behavior;

    public Dictionary<string, object> Blackboard {get; set; }
    public BTNode Root {get {return mRoot;}}
    // Start is called before the first frame update
    void Start()
    {
        Blackboard = new Dictionary<string, object>();
        Blackboard.Add("Worldbounds", new Rect(0,0,5,5));

        // initial behavior is stopped
        StartedBehavior = false;

        mRoot = new BTNode(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(!StartedBehavior) {
            StartCoroutine(RunBehavior());
            StartedBehavior = true;
        }
    }

    private IEnumerator RunBehavior() {
        BTNode.Result result = Root.Execute();
        while(result == BTNode.Result.Running) {
            Debug.Log("Root result: " + result);
            yield return null;
            result = Root.Execute();
        }

        Debug.Log("Behavior has finished with: " + result);
    }
}
