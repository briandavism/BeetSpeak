using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace MusicalRepresentation
{
    public class Measure : MonoBehaviour
    {
        public Row rowPrefab;
        public Transform rowHolder;

        private Row _redRow;
        private Row _orangeRow;
        private Row _yellowRow;
        private Row _greenRow;
        private Row _blueRow;
        private Row _purpleRow;

        private List<Row> _rowList;
        private System.DateTime _start;
        private RectTransform _rectTransform;
        private bool _isReset = true;

        public void Init(MeasureConfig config)
        {
            _rectTransform = (RectTransform) this.transform;
            _rowList = new List<Row>();

            var instrumentList = InstrumentHelper.GetInstrumentList();
            foreach (var instrument in instrumentList)
            {
                List<NoteConfig> noteList;
                if (!config.notes.TryGetValue(instrument, out noteList))
                {
                    noteList = new List<NoteConfig>();
                }
                var row = Instantiate(rowPrefab, rowHolder);
                row.Init(InstrumentHelper.GetColorForInstrument(instrument), noteList);
                _rowList.Add(row);
                SetRowForInstrument(instrument,row);
            }
        }

        public void Draw(float relativeTime)
        {
        }

        public Vector3 GetLocationForTime(float time)
        {
            var pos = transform.position;
            var xloc = Mathf.Lerp(_rectTransform.rect.xMin, _rectTransform.rect.xMax, time);
            pos.x += xloc;
            return pos;
        }

        public void ResetMeasure()
        {
            if (!_isReset)
            {
                _rowList.ForEach(x => x.Reset());
                _isReset = true;
            }
        }

        public bool IsMeasureCorrect()
        {
            return _rowList.TrueForAll(x => x.IsRowCorrect());
        }

        public void RecordHit(Instrument instrument, float relativeTime)
        {
            _isReset = false;
            GetRowForInstrument(instrument).RecordHit(relativeTime);
        }

        public Row GetRowForInstrument(Instrument instrument)
        {
            switch (instrument)
            {
                case Instrument.Red:
                    return _redRow;
                case Instrument.Orange:
                    return _orangeRow;
                case Instrument.Yellow:
                    return _yellowRow;
                case Instrument.Green:
                    return _greenRow;
                case Instrument.Blue:
                    return _blueRow;
                case Instrument.Purple:
                    return _purpleRow;
                default:
                    Debug.LogError("No row found for: " + instrument.ToString());
                    return null;
            }
        }

        public void SetRowForInstrument(Instrument instrument, Row row)
        {
            row.gameObject.name = instrument.ToString() + "Row";
            switch (instrument)
            {
                case Instrument.Red:
                    _redRow = row;
                    break;
                case Instrument.Orange:
                    _orangeRow = row;
                    break;
                case Instrument.Yellow:
                    _yellowRow = row;
                    break;
                case Instrument.Green:
                    _greenRow = row;
                    break;
                case Instrument.Blue:
                    _blueRow = row;
                    break;
                case Instrument.Purple:
                    _purpleRow = row;
                    break;
                default:
                    Debug.LogError("No row found for: " + instrument.ToString());
                    break;
            }
        }
    }
}