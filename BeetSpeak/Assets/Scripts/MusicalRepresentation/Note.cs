using System.Collections;
using Configs;
using UnityEngine;
using UnityEngine.UI;

namespace MusicalRepresentation
{
    public class Note : MonoBehaviour, System.IComparable
    {
        private float _visualLength;
        public Image noteVisual;

        private Color _color = Color.magenta;
        private bool _wasHit = false;

        public float HitTime { get; private set; }

        public void Init(Color color, NoteConfig config, Row row)
        {
            _color = color;
            noteVisual.color = Color.black;
            HitTime = config.hitTime;
            _visualLength = config.visualLength;
            StartCoroutine(SetLocation(row));
        }

        private IEnumerator SetLocation(Row row)
        {
            yield return null;
            
            transform.position = row.GetVectorLocationForTime(HitTime);
        }

        public void Reset()
        {
            _wasHit = false;
            noteVisual.color = Color.black;
        }

        public bool WasHit(float time)
        {
            if (!_wasHit)
            {
                _wasHit = MusicCore.IsHit(this, time);

                if (_wasHit)
                {
                    noteVisual.color = _color;
                }

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
            return HitTime.CompareTo(otherNote.HitTime);
        }
    }
}