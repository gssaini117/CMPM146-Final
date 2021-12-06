using System;
using UnityEngine;

// This leaf node
public class RunToPlayer : Node
{
    protected Vector3 NextDestination { get; set; }

    public RunToPlayer(BehaviorTree t) : base(t)
    {
        NextDestination = new Vector3(0, 2, 0);
    }

    public override Result Execute()
    {
        Tree.agent.speed = 5f;
        Tree.agent.destination = NextDestination;
        // uninterruptable
        if(Tree.agent.remainingDistance == 0) {
           return Result.Success;
        }
        return Result.Running;
    }
}
