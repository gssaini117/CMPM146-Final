using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Classes
public class Node
{
    public enum Result { Running, Failure, Success};

    public BehaviorTree Tree {get; set;}

    public Node(BehaviorTree t) {
        Tree = t;
    }

    public virtual Result Execute() {
        return Result.Failure;
    }
}

public class Composite : Node
{
   public List<Node> Children {get; set; }

   public Composite(BehaviorTree t, Node [] nodes) : base(t)
   {
      Children = new List<Node>(nodes);
   }
}

public class Decorator : Node
{
   public Node Child {get; set; }
   public Decorator(BehaviorTree t, Node c) : base(t)
   {
      Child = c;
   }
}

// Composite Nodes
public class Selector : Composite
{
   private int currentNode = 0;
   public Selector(BehaviorTree t, Node [] c) : base(t, c)
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
            }
         }
      }

      return Result.Failure;
   }
}

public class Sequencer : Composite
{
   private int currentNode = 0;
   public Sequencer(BehaviorTree t, Node [] c) : base(t, c)
   {
   }

   public override Result Execute() {
      if(currentNode < Children.Count) {
         Result result = Children[currentNode].Execute();

         if(result == Result.Running) {
            return Result.Running;
         } else if( result == Result.Failure) {
            currentNode = 0;
            return Result.Failure;
         } else {
            Debug.Log("Completed: " +Children[currentNode]);
            currentNode++;
            if(currentNode < Children.Count) {
               return Result.Running;
            } else {
               currentNode = 0;
               return Result.Success;
            }
         }
      }

      return Result.Success;
   }
}

// Decorator Nodes
public class Repeater : Decorator
{
   public Repeater(BehaviorTree t, Node c) : base(t, c)
   {
   }

   public override Result Execute() 
   {
      //Debug.Log("Child returned " + Child.Execute());
      Child.Execute();
      return Result.Running;
   }
}

public class RepeatUntilSuccess : Decorator
{
   public RepeatUntilSuccess(BehaviorTree t, Node c) : base(t, c)
   {
   }

   public override Result Execute()
   {
      Result result = Child.Execute();
      if(result == Result.Success)
      {
         return Result.Success;
      }
      return Result.Running;
   }
}

public class RepeatUntilFailure : Decorator
{
   public RepeatUntilFailure(BehaviorTree t, Node c) : base(t, c)
   {
   }

   public override Result Execute()
   {
      Result result = Child.Execute();
      if(result == Result.Failure)
      {
         return Result.Success;
      }
      return Result.Running;
   }
}
