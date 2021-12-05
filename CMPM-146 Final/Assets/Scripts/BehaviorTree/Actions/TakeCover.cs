using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


// This leaf node
public class TakeCover : Node
{
   protected Vector3 NextDestination {get; set;}
   private Vector3 Player = new Vector3(0,4,0);
   private Collider[] Colliders = new Collider[10]; // more is less performant, but more options
   public float HideSensitivity = 0;
   public TakeCover(BehaviorTree t) : base(t)
   {
      NextDestination = Tree.gameObject.transform.position;
      FindCover();
      Tree.agent.destination = NextDestination;
   }

   public bool FindCover()
   {
      int hits = Physics.OverlapSphereNonAlloc(Tree.agent.transform.position, 
      Tree.lineOfSightChecker.Collider.radius, Colliders, Tree.HidableLayers);
      for(int i = 0; i < hits; i++)
      {
         if(NavMesh.SamplePosition(Colliders[i].transform.position, out NavMeshHit hit, 2f, Tree.agent.areaMask))
         {
            if(!NavMesh.FindClosestEdge(hit.position, out hit, Tree.agent.areaMask))
            {
               Debug.LogError($"Unable to find edge close to {hit.position}");
            }
            if(Vector3.Dot(hit.normal, (Player - hit.position).normalized) < HideSensitivity)
            {
               Tree.agent.SetDestination(hit.position);
               return true;
            }
            else
            {
               if(NavMesh.SamplePosition(Colliders[i].transform.position - (Player - hit.position).normalized * 2, out NavMeshHit hit2, 2f, Tree.agent.areaMask))
               {
                  if(!NavMesh.FindClosestEdge(hit2.position, out hit2, Tree.agent.areaMask))
                  {
                     Debug.LogError($"Unable to find edge close to {hit2.position}");
                  }
                  if(Vector3.Dot(hit2.normal, (Player - hit2.position).normalized) < HideSensitivity)
                  {
                     NextDestination = hit2.position;
                     return true;
                  }
               }
            }
         }
      }
      return false;
   }

   public override Result Execute()
   {
      if(Tree.agent.hasPath == false)
      {
         if(!FindCover()) {
            return Result.Failure;
         } else {
            
            return Result.Success;
         }
      } else {
         Tree.agent.destination = NextDestination;
         return Result.Running;
      }

   }
}
