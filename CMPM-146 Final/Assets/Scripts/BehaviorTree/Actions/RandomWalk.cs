using System;
using UnityEngine;

// This leaf node
public class RandomWalk : Node
{
   protected Vector3 NextDestination {get; set;}
   public float speed = 5.0f;

   public RandomWalk(BehaviorTree t) : base(t)
   {
      NextDestination = new Vector3(0,4,0);
      FindNextDestination();
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
      if(Tree.gameObject.transform.position == NextDestination)
      {
         if(!FindNextDestination()) {
            return Result.Failure;
         } else {
            return Result.Success;
         }
      } else {
         Tree.gameObject.transform.position = 
            Vector3.MoveTowards(Tree.gameObject.transform.position, 
               NextDestination, Time.deltaTime * speed);
         
         return Result.Running;
      }
   }
}
