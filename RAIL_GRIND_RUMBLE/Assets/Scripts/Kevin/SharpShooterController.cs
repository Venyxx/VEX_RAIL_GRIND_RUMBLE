using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class SharpShooterController : MonoBehaviour
{
	public LayerMask HidableLayers;
	public EnemyLineOfSightChecker LineOfSightChecker;
	public UnityEngine.AI.NavMeshAgent Agent;
	[Range(-1, 1)]
	[Tooltip("Lower is a better hiding spot")]
	public float HideSensitivity = 0;
	[Range(1, 20)]
	public float MinPlayerDistance = 5;

	private Coroutine MovementCoroutine;
	private Collider[] Colliders = new Collider[10];

	private void Awake()
	{
		Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		LineOfSightChecker.OnGainSight += HandleGainSight;
		LineOfSightChecker.OnLoseSight += HandleLoseSight;
	}
	private void HandleGainSight(Transform Target)
	{
		if (MovementCoroutine != null)
		{
			StopCoroutine(MovementCoroutine);
		}
		MovementCoroutine = StartCoroutine(Hide(Target));
	}

	private void HandleLoseSight(Transform Target)
	{
		if (MovementCoroutine != null)
		{
			StopCoroutine(MovementCoroutine);
		}
	}
	private IEnumerator Hide(Transform Target)
	{
		while (true)
		{
			for (int i = 0; i < Colliders.Length; i++)
            {
				Colliders[i] = null;
            }

			int hits = Physics.OverlapSphereNonAlloc(Agent.transform.position, LineOfSightChecker.Collider.radius, Colliders, HidableLayers);

			int hitReduction = 0;
			for (int i = 0; i < hits; i++)
            {
				if (Vector3.Distance(Colliders[i].transform.position, Target.position) < MinPlayerDistance)
                {
					Colliders[i] = null;
					hitReduction++;
                }
            }
			hits -= hitReduction;

			System.Array.Sort(Colliders, ColliderArraySortComparer);
			
			for (int i = 0; i < hits; i++)
			{
				if (UnityEngine.AI.NavMesh.SamplePosition(Colliders[i].transform.position, out UnityEngine.AI.NavMeshHit hit, 15f, Agent.areaMask))
				{
					if (!UnityEngine.AI.NavMesh.FindClosestEdge(hit.position, out hit, Agent.areaMask))
					{
						Debug.LogError($"Unable to find edge close to {hit.position}");
					}

					if (Vector3.Dot(hit.normal, (Target.position - hit.position).normalized) < HideSensitivity)
					{
						Agent.SetDestination(hit.position);
						break;
					}
					else
					{
						if (UnityEngine.AI.NavMesh.SamplePosition(Colliders[i].transform.position - (Target.position - hit.position).normalized * 2, out UnityEngine.AI.NavMeshHit hit2, 15f, Agent.areaMask))
						{
							if (!UnityEngine.AI.NavMesh.FindClosestEdge(hit2.position, out hit2, Agent.areaMask))
							{
								Debug.LogError($"Unable to find edge close to {hit2.position}");
							}

							if (Vector3.Dot(hit2.normal, (Target.position - hit2.position).normalized) < HideSensitivity)
							{
								Agent.SetDestination(hit2.position);
								break;
							}
						}

					}

				}
				else
				{
					Debug.LogError($"Unable to find NavMesh near object {Colliders[i].name} at {Colliders[i].transform.position}");
				}

			}
			yield return null;
		}
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
			return Vector3.Distance(Agent.transform.position, A.transform.position).CompareTo(Vector3.Distance(Agent.transform.position, B.transform.position));
        }
    }
}