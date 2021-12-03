using System;
using System.Collections.Generic;

public class BTSelector : BTComposite
{
   private int currentNode = 0;
   public BTSelector(BehaviorTree t, BTNode [] c) : base(t, c)
   {
   }

   public override Result Execute() {
      if(currentNode < Children.Count) {
         Result result = Children[currentNode].Execute();

         if(result == Result.Running) {
            return Result.Running;
         } else if( result == Result.Success) {
            return Result.Success;
         } else if( result == Result.Failure) {
            currentNode++;
            if(currentNode < Children.Count) {
               return Result.Running;
            } else {
               return Result.Failure;
            }
         }
      }

      return Result.Failure;
   }
}