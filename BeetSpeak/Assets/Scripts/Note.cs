using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour, System.IComparable
{
    public float hitTime;
    public float visualLength;
    public Image noteVisual;

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
            _wasHit = ((hitTime - RhythmCore.Instance.LEAD_TIME) < time) &&
                      (time < (hitTime + RhythmCore.Instance.TRAIL_TIME));
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
        var otherNote = (Note) other;
        return hitTime.CompareTo(otherNote.hitTime);
    }
}