using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Row : MonoBehaviour
{
    public List<Note> notes;
    public Miss missPrefab;
    public Image staffLineImage;
    private Color _color;
    private List<Miss> _missesList = new List<Miss>();
    private int _goodHits = 0;
    private Measure _measure;
    private RectTransform _rectTransform;

    public void Init(Color color)
    {
        _color = color;
        staffLineImage.color = color;
        foreach (var note in notes)
        {
            note.Init(_color, this);
        }
    }

    private void Start()
    {
        _measure = this.transform.GetComponentInParent<Measure>();
        _rectTransform = (RectTransform) this.transform;
        foreach (var note in notes)
        {
            note.transform.position = GetVectorLocationForTime(note.hitTime);
        }
    }

    public void Draw(float relativeTime)
    {
    }

    public void Reset()
    {
        _goodHits = 0;
        _missesList.ForEach(x => Object.Destroy(x.gameObject));
        notes.ForEach(x => x.Reset());
    }

    //TODO: what about if you hit one note multiple times? (probably not going to happen but eh...)
    public bool IsRowCorrect()
    {
        return (_goodHits == notes.Count) && (_missesList.Count == 0);
    }

    //TODO: probably binary search this stuff
    public void RecordHit(float time)
    {
        if (notes.Any(note => note.WasHit(time)))
        {
            _goodHits++;
            return;
        }

        var miss = Object.Instantiate(missPrefab, _rectTransform);
        miss.transform.position = GetVectorLocationForTime(time);
    }

    public void SortNotes()
    {
        notes.Sort();
    }

    //TODO: make this smarter by taking advantage of sorted list
    public bool IsRowValid()
    {
        //SortNotes();
        //var currentLatest = 0f;

        for (int i = 0; i < notes.Count; i++)
        {
            for (int j = 0; j < notes.Count; j++)
            {
                if (j != i)
                {
                    if (Note.DoNotesOverlap(notes[i], notes[j]))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private float GetRelativeLocationForTime(float time)
    {
        return Mathf.Lerp(_rectTransform.rect.xMin, _rectTransform.rect.xMax, time);
    }

    private Vector3 GetVectorLocationForTime(float time)
    {
        var location = transform.position;
        location.x = GetRelativeLocationForTime(time);
        return location;
    }
}