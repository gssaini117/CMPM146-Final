using System;
using UnityEngine;

public class BTRepeater : Decorator
{
   private int currentNode = 0;
   public BTRepeater(BehaviorTree t, BTNode c) : base(t, c)
   {
   }

   public override Result Execute() {
      Debug.Log("Child returned " + Child.Execute());
      return Result.Running;
   }
}