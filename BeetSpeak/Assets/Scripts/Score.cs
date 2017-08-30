using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    public List<Measure> measures;
    public float measureLength;

    private float _scoreLength;

    // Use this for initialization
    void Start()
    {
        _scoreLength == ((float)measures.Count) * measureLength;
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    public bool IsScoreCorrect()
    {
        return measures.TrueForAll(x => x.IsMeasureCorrect());
    }

    public void RecordHit(Instrument instrument, float absoluteTime)
    {
        var timeInScore = absoluteTime % _scoreLength;
        var measureNumber = Mathf.FloorToInt( timeInScore / measureLength);
        var timeInMeasure = timeInScore % measureLength;
        measures[measureNumber].RecordHit(instrument, timeInMeasure);
    }
}

