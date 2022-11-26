using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyWave
{
	public string WaveName = "Wave";
	public Transform PositionMarker; // the screen stops scrolling at this position marker until the wave is complete
	public List<GameObject> EnemyList = new List<GameObject> ();
	public List<GameObject> CustomerList = new List<GameObject>();

	public bool waveComplete()
	{
		if (EnemyList.Count == 0) {
			return true;
		} else {
			return false;
		}
	}

	public bool deliveriesMade()
	{
		if (CustomerList.Count == 0)
		{
			return true;
		} else
		{
			return false;
		}
	}

	public void RemoveCustomerFromList(GameObject g)
	{
		if (CustomerList.Contains(g)) { CustomerList.Remove(g); }
	}

	public void RemoveEnemyFromWave(GameObject g)
	{
		if (EnemyList.Contains (g))
			EnemyList.Remove (g);
	}

}