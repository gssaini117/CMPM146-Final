using System;
using UnityEngine;

// This leaf node
public class RandomWalk : Node
{
   public bool running = false;
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
      if(running == false)
      {
         running = true;
         if(!FindNextDestination()) {
            return Result.Failure;
         }
         Tree.agent.destination = NextDestination;
         return Result.Running;
      } else{
         if(Tree.agent.hasPath == false) {
            running = false;
            return Result.Success;
         }
         return Result.Running;
      } 
   }
}
