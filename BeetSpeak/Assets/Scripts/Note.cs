using UnityEngine;
using System.Collections;

public class Note : System.IComparable
{
	public float hitTime;
	public float length;

	private const float leadTime = .001f;
	private const float trailTime = .002f;

	public bool WasHit(int time)
	{
		return ((hitTime - leadTime) < time) && (time < (hitTime + trailTime));
	}

	public int CompareTo(object other)
	{
		return -1;
	}
}