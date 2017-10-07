using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour, System.IComparable
{
	public float hitTime;
	public float visualLength;
	public Image noteVisual;

	private const float LEAD_TIME = .001f;
	private const float TRAIL_TIME = .002f;
	
	private Color _color = Color.magenta;
	private bool _wasHit = false;
	private Row _row;
	
	public void Init(Color color, Row row)
	{
		_row = row;
		_color = color;
	}

	public void Reset()
	{
		_wasHit = false;
		noteVisual.color = Color.white;
	}

	public bool WasHit(float time)
	{
		if (!_wasHit)
		{
			_wasHit = ((hitTime - LEAD_TIME) < time) && (time < (hitTime + TRAIL_TIME));
			noteVisual.color = _color;
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