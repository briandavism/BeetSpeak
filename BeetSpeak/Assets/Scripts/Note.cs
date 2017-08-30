using UnityEngine;
using System.Collections;

public class Note : System.IComparable
{
	public float hitTime;
	public float visualLength;

	private const float LEAD_TIME = .001f;
	private const float TRAIL_TIME = .002f;

	private bool _wasHit = false;

	public void Reset()
	{
		_wasHit = false;
	}

	public bool WasHit(float time)
	{
		if (!_wasHit)
		{
			_wasHit = ((hitTime - LEAD_TIME) < time) && (time < (hitTime + TRAIL_TIME));
			return _wasHit;
		}
		else
		{
			return false;
		}
	}

	public int CompareTo(object other)
	{
		var otherNote = (Note)other;
		return hitTime.CompareTo(otherNote.hitTime);
	}

	public static bool DoNotesOverlap(Note a, Note b)
	{
		return (a.hitTime + TRAIL_TIME) >= (b.hitTime - LEAD_TIME) && (b.hitTime + TRAIL_TIME) >= (a.hitTime - LEAD_TIME);
	}
}