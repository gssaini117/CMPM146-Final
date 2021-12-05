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
      Debug.Log("Camera position: " + Camera.main.transform.position);
      Debug.Log("enemy Pos: " + Tree.agent.transform.position);
      Vector3 directionToObject = Tree.agent.transform.position - Camera.main.transform.position;
      Debug.Log("direction: " + directionToObject);
      Debug.Log("camera angle: " + Camera.main.transform.forward);
      Debug.Log("angle: " + Vector3.Angle(Camera.main.transform.forward, directionToObject));
      if(Vector3.Angle(Camera.main.transform.forward, directionToObject) < Camera.main.fieldOfView) {
         Debug.Log("player looking at enemy");
         return Result.Success;
      }
      return Result.Failure;
   }
}
