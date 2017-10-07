using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Measure : MonoBehaviour
{
    public Row redRow;
    public Row orangeRow;
    public Row yellowRow;
    public Row greenRow;
    public Row blueRow;
    public Row purpleRow;

    private List<Row> _rowList;
    private System.DateTime _start;
    private Score _score;
    private RectTransform _rectTransform;

    // Use this for initialization
    void Start()
    {
        _score = this.transform.GetComponentInParent<Score>();
        _rectTransform = (RectTransform) this.transform;

        _rowList = new List<Row>();

        foreach (Instrument instrument in System.Enum.GetValues(typeof(Instrument)))
        {
            var row = GetRowForInstrument(instrument);
            if (row != null)
            {
                _rowList.Add(row);
                row.Init(GetColorForInstrument(instrument));
            }
        }
    }

    public void Draw(float relativeTime)
    {
        
    }
    
    public Vector3 GetLocationForTime(float time)
    {
        var pos = this.transform.position;
        var xloc = Mathf.Lerp(_rectTransform.rect.xMin, _rectTransform.rect.xMax, time);
        pos.x += xloc;
        return pos;
    }

    public void ResetMeasure()
    {
        _rowList.ForEach(x => x.Reset());
    }

    public bool IsMeasureCorrect()
    {
        return _rowList.TrueForAll(x => x.IsRowCorrect());
    }

    public void RecordHit(Instrument instrument, float relativeTime)
    {
        GetRowForInstrument(instrument).RecordHit(relativeTime);
    }

    public Row GetRowForInstrument(Instrument instrument)
    {
        switch (instrument)
        {
            case Instrument.Red:
                return redRow;
            case Instrument.Orange:
                return orangeRow;
            case Instrument.Yellow:
                return yellowRow;
            case Instrument.Green:
                return greenRow;
            case Instrument.Blue:
                return blueRow;
            case Instrument.Purple:
                return purpleRow;
            default:
                Debug.LogError("No row found for: " + instrument.ToString());
                return null;
        }
    }

    public Color GetColorForInstrument(Instrument instrument)
    {
        switch (instrument)
        {
            case Instrument.Red:
                return Color.red;
            case Instrument.Orange:
                return new Color32(255, 69, 0, 255);
            case Instrument.Yellow:
                return Color.yellow;
            case Instrument.Green:
                return Color.green;
            case Instrument.Blue:
                return Color.blue;
            case Instrument.Purple:
                return new Color32(128, 0, 128, 255);
            default:
                Debug.LogError("No color found for: " + instrument.ToString());
                return Color.magenta;
        }
    }
}