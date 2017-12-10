using System.Collections.Generic;
using Configs;
using UnityEngine;
using UnityEngine.UI;

namespace MusicalRepresentation
{
    public class Score : MonoBehaviour
    {
        public Image timeMarker;
        public Transform gridLayout;
        public Measure measurePrefab;

        private List<Measure> _measures;
        private float _scoreLength;

        private void Awake()
        {
            MusicCore.Instance.RegisterScore(this);
        }

        public void Init(SongConfig config)
        {
            for(int i = 0; i<gridLayout.childCount; i++)
            {
                Destroy(gridLayout.GetChild(i).gameObject);
            }
            
            _measures = new List<Measure>();
            
            foreach (var measure in config.measures)
            {
                var measureObj = Instantiate(measurePrefab, gridLayout);
                measureObj.Init(measure);
                _measures.Add(measureObj);
            }
            
            _scoreLength = ((float) _measures.Count) * MusicCore.Instance.measureLength;
        }

        void Update()
        {
            if (_measures != null && (_measures.Count > 0))
            {    
                var absoluteTime = RhythmCore.Instance.GetAbsoluteTime();
                var currentMeasure = GetMeasureAtTime(absoluteTime);
                timeMarker.transform.position = currentMeasure.GetLocationForTime(GetRelativeTime(absoluteTime));

                var nextMeasure = GetMeasureAtTime(absoluteTime + MusicCore.Instance.measureLength);
                nextMeasure.ResetMeasure();
            }
        }

        public void Draw(float absoluteTime)
        {
            GetMeasureAtTime(absoluteTime).Draw(GetRelativeTime(absoluteTime));
        }

        public bool IsScoreCorrect()
        {
            return _measures.TrueForAll(x => x.IsMeasureCorrect());
        }

        public void RecordHit(Instrument instrument, float absoluteTime)
        {
            GetMeasureAtTime(absoluteTime).RecordHit(instrument, GetRelativeTime(absoluteTime));
        }

        private float GetRelativeTime(float absoluteTime)
        {
            var timeInMeasure = absoluteTime % MusicCore.Instance.measureLength;
            return timeInMeasure / MusicCore.Instance.measureLength;
        }

        private Measure GetMeasureAtTime(float absoluteTime)
        {
            var timeInScore = absoluteTime % _scoreLength;
            var measureNumber = Mathf.FloorToInt(timeInScore / MusicCore.Instance.measureLength);
            return _measures[measureNumber];
        }
    }
}