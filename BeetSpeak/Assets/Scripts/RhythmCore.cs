using UnityEngine;

public class RhythmCore : UnitySingleton<RhythmCore>
{
	private float _secondsSinceStart;

	// Use this for initialization
	void Start ()
	{
		_secondsSinceStart = 0;
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
}

