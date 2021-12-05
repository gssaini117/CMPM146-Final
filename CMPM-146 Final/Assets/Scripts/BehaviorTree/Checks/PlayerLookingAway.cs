using System;
using UnityEngine;

// This leaf node
public class PlayerLookingAway : Node
{

   public PlayerLookingAway(BehaviorTree t) : base(t)
   {
   }

   public override Result Execute()
   {
      // do the check here?
      Vector3 directionToObject = Tree.agent.transform.position - Camera.main.transform.position;
      if(Vector3.Angle(Camera.main.transform.forward, directionToObject) > Camera.main.fieldOfView) {
         return Result.Success;
      }
      return Result.Failure;
   }
}
