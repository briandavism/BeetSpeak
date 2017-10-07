using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MusicalRepresentation
{
    public class Row : MonoBehaviour
    {
        public Miss missPrefab;
        public Image staffLineImage;
        public GameObject noteHolder;
        private List<Note> _notes;
        private Color _color;
        private readonly List<Miss> _missesList = new List<Miss>();
        private int _goodHits = 0;
        private Measure _measure;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _measure = this.transform.GetComponentInParent<Measure>();
            _rectTransform = (RectTransform) this.transform;

            _notes = new List<Note>(gameObject.GetComponentsInChildren<Note>());
        }

        public void Init(Color color)
        {
            _color = color;
            staffLineImage.color = color;
            foreach (var note in _notes)
            {
                note.Init(_color, this);
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
            _missesList.Clear();
            _notes.ForEach(x => x.Reset());
        }

        //TODO: what about if you hit one note multiple times? (probably not going to happen but eh...)
        public bool IsRowCorrect()
        {
            return (_goodHits == _notes.Count) && (_missesList.Count == 0);
        }

        //TODO: probably binary search this stuff
        public void RecordHit(float time)
        {
            if (_notes.Any(note => note.WasHit(time)))
            {
                _goodHits++;
                return;
            }

            var miss = Object.Instantiate<Miss>(missPrefab, _rectTransform);
            miss.transform.position = GetVectorLocationForTime(time);
            miss.Init(time, _color);
            _missesList.Add(miss);
        }

        public void SortNotes()
        {
            _notes.Sort();
        }

        //TODO: make this smarter by taking advantage of sorted list
        public bool IsRowValid()
        {
            //SortNotes();
            //var currentLatest = 0f;

            for (var i = 0; i < _notes.Count; i++)
            {
                for (var j = 0; j < _notes.Count; j++)
                {
                    if ((j != i) && MusicCore.DoNotesOverlap(_notes[i], _notes[j]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private Vector3 GetVectorLocationForTime(float time)
        {
            var location = transform.position;
            //Debug.Log("GetVectorLocationForTime: " + time + " between " + _rectTransform.rect.xMin + " and " +
            //          _rectTransform.rect.xMax);
            location.x += Mathf.Lerp(_rectTransform.rect.xMin, _rectTransform.rect.xMax, time);
            return location;
        }
    }
}