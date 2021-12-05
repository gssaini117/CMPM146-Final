using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviorTree : MonoBehaviour
{
    private Node mRoot;
    private bool StartedBehavior;
    private Coroutine behavior;
    public NavMeshAgent agent;
    public LineOfSightChecker lineOfSightChecker;
    public LayerMask HidableLayers;
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

        Node[] takeCover = new Node[2];
        takeCover[0] = new PlayerLookingTowards(this);
        takeCover[1] = new TakeCover(this);
        Node[] selector = new Node[2];
        selector[0] = new Sequencer(this, takeCover);
        selector[1] = new WalkToPlayer(this);

        mRoot = new Repeater(this, new Selector(this, selector));
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


