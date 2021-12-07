using System;
using UnityEngine;

// This leaf node
public class KillZone : Node
{

    public KillZone(BehaviorTree t) : base(t)
    {
    }

    public override Result Execute()
    {
        // do the check here?
        Vector3 playerPos = new Vector3(0, 2, 0);
        if (Vector3.Distance(Tree.agent.transform.position, playerPos) <= 6)
        {
            return Result.Success;
        }
        return Result.Failure;
    }
}
