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

	// Use this for initialization
	void Start()
	{
		_rowList = new List<Row>();

		foreach (Instrument instrument in System.Enum.GetValues(typeof(Instrument)))
		{
			var row = GetRowForInstrument(instrument);
			if (row != null)
			{
				_rowList.Add(row);
			}
		}
	}
	
	// Update is called once per frame
	void Update()
	{
	
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
}

