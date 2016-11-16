using UnityEngine;
using System.Collections;

public class Distance : MonoBehaviour {

	public delegate void CombatAreaEvent(bool isLeaving);
	public static event CombatAreaEvent OnLeavingCombatArea;
	public static event CombatAreaEvent OnEnteringCombatArea; 

	[SerializeField]Transform midPoint;

	const float MAX_DISTANCE = 10f;
	float dist;

	void Update()
	{
		if(midPoint != null){
			dist = Vector3.Distance(midPoint.position, transform.position);
		}

		CheckDist();
	}

	void CheckDist()
	{
		if(dist > MAX_DISTANCE)
		{
			if(OnLeavingCombatArea != null)
			{
				OnLeavingCombatArea(true);

			}
		}
		if(dist < MAX_DISTANCE)
		{
			if(OnEnteringCombatArea != null)
			{
				OnEnteringCombatArea(false);
			}
		}
	}
}
