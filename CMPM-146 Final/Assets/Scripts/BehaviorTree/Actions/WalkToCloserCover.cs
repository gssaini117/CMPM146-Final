using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


// This leaf node
public class WalkToCloserCover : Node
{
    protected Vector3 NextDestination { get; set; }
    private Vector3 Player = new Vector3(0, 2, 0);
    private Collider[] Colliders = new Collider[20]; // more is less performant, but more options
    public float HideSensitivity = -0.1f;
    public WalkToCloserCover(BehaviorTree t) : base(t)
    {
        NextDestination = Tree.gameObject.transform.position;
    }

    public bool FindCover()
    {
        for (int i = 0; i < Colliders.Length; i++)
        {
            Colliders[i] = null;
        }

        int hits = Physics.OverlapSphereNonAlloc(Tree.agent.transform.position,
        Tree.lineOfSightChecker.Collider.radius, Colliders, Tree.HidableLayers);

        int hitReduction = 0;
        for (int i = 0; i < hits; i++)
        {
            if (Vector3.Distance(Player, Colliders[i].transform.position) > Vector3.Distance(Player, Tree.agent.transform.position) - 5)
            {
                hitReduction++;
                Colliders[i] = null;
            }
        }
        hits -= hitReduction;

        System.Array.Sort(Colliders, ColliderArraySortComparer);

        for (int i = 0; i < hits; i++)
        {
            if (NavMesh.SamplePosition(Colliders[i].transform.position, out NavMeshHit hit, 2f, Tree.agent.areaMask))
            {
                if (!NavMesh.FindClosestEdge(hit.position, out hit, Tree.agent.areaMask))
                {
                    Debug.LogError($"Unable to find edge close to {hit.position}");
                }
                if (Vector3.Dot(hit.normal, (Player - hit.position).normalized) < HideSensitivity)
                {
                    NextDestination = hit.position;
                    return true;
                }
                else
                {
                    if (NavMesh.SamplePosition(Colliders[i].transform.position - (Player - hit.position).normalized * 2,
                           out NavMeshHit hit2, 2f, Tree.agent.areaMask))
                    {
                        if (!NavMesh.FindClosestEdge(hit2.position, out hit2, Tree.agent.areaMask))
                        {
                            Debug.LogError($"Unable to find edge close to {hit2.position}");
                        }
                        if (Vector3.Dot(hit2.normal, (Player - hit2.position).normalized) < HideSensitivity)
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

    private int ColliderArraySortComparer(Collider A, Collider B)
    {
        if (A == null && B != null)
        {
            return 1;
        }
        else if (A != null && B == null)
        {
            return -1;
        }
        else if (A == null && B == null)
        {
            return 0;
        }
        else
        {
            return Vector3.Distance(Tree.agent.transform.position, A.transform.position).CompareTo
                  (Vector3.Distance(Tree.agent.transform.position, B.transform.position));
        }
    }

    public override Result Execute()
    {
        if (!FindCover())
        {
            return Result.Failure;
        }
        else
        {
            Tree.agent.destination = NextDestination;
            return Result.Success;
        }
    }
}
