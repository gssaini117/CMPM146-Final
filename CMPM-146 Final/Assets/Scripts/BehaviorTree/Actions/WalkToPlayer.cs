using System;
using UnityEngine;

// This leaf node
public class WalkToPlayer : Node
{
   protected Vector3 NextDestination {get; set;}

   public WalkToPlayer(BehaviorTree t) : base(t)
   {
      NextDestination = new Vector3(0,2,0);
   }


   public override Result Execute()
   {
      Tree.agent.destination = NextDestination;
      return Result.Success;
      /* uninterruptable
      if(Tree.agent.remainingDistance == 0) {
         return Result.Success;
      }
      return Result.Running;*/
   }
}
