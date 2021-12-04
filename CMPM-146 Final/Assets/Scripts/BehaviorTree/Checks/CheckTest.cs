using System;
using UnityEngine;

// This leaf node
public class CheckTest : Node
{

   public CheckTest(BehaviorTree t) : base(t)
   {
   }

   public override Result Execute()
   {
      // do the check here?
      return base.Execute();
   }

}
