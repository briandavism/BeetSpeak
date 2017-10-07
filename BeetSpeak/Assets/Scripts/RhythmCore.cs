using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class RhythmCore : UnitySingleton<RhythmCore>
{
	private float _secondsSinceStart;

	public float LEAD_TIME = .001f;
	public float TRAIL_TIME = .002f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		_secondsSinceStart += Time.unscaledDeltaTime;
	}

	public float GetAbsoluteTime()
	{
		return _secondsSinceStart;
	}
	
	public static bool DoNotesOverlap(Note a, Note b)
	{
		return (a.hitTime + RhythmCore.Instance.TRAIL_TIME) >= (b.hitTime - RhythmCore.Instance.LEAD_TIME) &&
		       (b.hitTime + RhythmCore.Instance.TRAIL_TIME) >= (a.hitTime - RhythmCore.Instance.LEAD_TIME);
	}
}

