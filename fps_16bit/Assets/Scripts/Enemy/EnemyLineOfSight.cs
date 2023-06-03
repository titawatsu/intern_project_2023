#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fps_16bit;

namespace fps_16bit
{
	#if UNITY_EDITOR
	[CustomEditor(typeof(EnemyLineOfSight))]
	public class LineOfSightEditor : Editor
	{
		void OnSceneGUI()
		{
			EnemyLineOfSight fow = (EnemyLineOfSight)target;
			Handles.color = Color.white;
			Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
			Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
			Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

			Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
			Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

			Handles.color = Color.red;
			foreach (Transform visibleTarget in fow.visibleTargets)
			{
				Handles.DrawLine(fow.transform.position, visibleTarget.position);
			}
		}
	}
	#endif

	public class EnemyLineOfSight : MonoBehaviour
	{

		public float viewRadius;
		[Range(0, 360)]
		public float viewAngle;

		public LayerMask targetMask;
		public LayerMask occlusionMask;

		public bool playerVisible = false;

		public List<Transform> visibleTargets = new List<Transform>();

		private void Start()
		{
			StartCoroutine("FindTargetsWithDelay", .1f);

		}

		IEnumerator FindTargetsWithDelay(float delay)
		{
			while (true)
			{
				yield return new WaitForSeconds(delay);
				FindVisibleTargets();
			}
		}

		void FindVisibleTargets()
		{
			visibleTargets.Clear();

			playerVisible = false;

			Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

			for (int i = 0; i < targetsInViewRadius.Length; i++)
			{
				Transform target = targetsInViewRadius[i].transform;
				Vector3 dirToTarget = (target.position - transform.position).normalized;
				if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
				{
					float dstToTarget = Vector3.Distance(transform.position, target.position);

					if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, occlusionMask))
					{
						visibleTargets.Add(target);
					}
				}
			}

			foreach (Transform target in visibleTargets)
			{
				if (target.gameObject.GetComponent<Player>())
				{
					playerVisible = true;
					break;
				}
			}

		}


		public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
		{
			if (!angleIsGlobal)
			{
				angleInDegrees += transform.eulerAngles.y;
			}
			return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
		}

	}
}