using System;
using UnityEngine;

// This leaf node
public class RandomWalk : Node
{
   protected Vector3 NextDestination {get; set;}

   public RandomWalk(BehaviorTree t) : base(t)
   {
      NextDestination = new Vector3(0,4,0);
      FindNextDestination();
      Tree.agent.destination = NextDestination;
   }

   public bool FindNextDestination()
   {
      object o;
      bool found = false;
      found = Tree.Blackboard.TryGetValue("WorldBounds", out o);
      if(found) {
         Rect bounds = (Rect)o;
         float x = UnityEngine.Random.value * bounds.width - bounds.width/2;
         float z= UnityEngine.Random.value * bounds.height - bounds.height/2;
         NextDestination = new Vector3(x,NextDestination.y, z);
      }

      return found;
   }

   public override Result Execute()
   {
      // if we've arrived at the point, then find the next destination
      if(Tree.agent.hasPath == false)
      {
         if(!FindNextDestination()) {
            return Result.Failure;
         } else {
            Tree.agent.destination = NextDestination;
            return Result.Success;
         }
      } else {
         
         return Result.Running;
      }
   }
}
