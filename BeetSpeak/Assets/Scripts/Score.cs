using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Image timeMarker;
    public float measureLength;

    public List<Measure> measures;
    private float _scoreLength;

    // Use this for initialization
    private void Start()
    {
        measures = new List<Measure>(this.GetComponentsInChildren<Measure>());
        _scoreLength = ((float) measures.Count) * measureLength;
    }

    // Update is called once per frame
    void Update()
    {
        var absoluteTime = RhythmCore.Instance.GetAbsoluteTime();
        var currentMeasure = GetCurrentMeasure(absoluteTime);
        timeMarker.transform.position = currentMeasure.GetLocationForTime(GetRelativeTime(absoluteTime));
    }

    public void Draw(float absoluteTime)
    {
        GetCurrentMeasure(absoluteTime).Draw(GetRelativeTime(absoluteTime));
    }

    public bool IsScoreCorrect()
    {
        return measures.TrueForAll(x => x.IsMeasureCorrect());
    }

    public void RecordHit(Instrument instrument, float absoluteTime)
    {
        GetCurrentMeasure(absoluteTime).RecordHit(instrument, GetRelativeTime(absoluteTime));
    }

    private float GetRelativeTime(float absoluteTime)
    {
        var timeInMeasure = absoluteTime % measureLength;
        return timeInMeasure / measureLength;
    }

    private Measure GetCurrentMeasure(float absoluteTime)
    {
        var timeInScore = absoluteTime % _scoreLength;
        var measureNumber = Mathf.FloorToInt(timeInScore / measureLength);
        return measures[measureNumber];
    }
}