using System;
using UnityEngine;

// This leaf node
public class PlayerLookingTowards : Node
{

   public PlayerLookingTowards(BehaviorTree t) : base(t)
   {
   }

   public override Result Execute()
   {
      // do the check here?
      Vector3 directionToObject = Tree.agent.transform.position - Camera.main.transform.position;
      if(Vector3.Angle(Camera.main.transform.forward, directionToObject) < Camera.main.fieldOfView + 30) {
         //Debug.Log("player looking at enemy");
         return Result.Success;
      }
      return Result.Failure;
   }
}
