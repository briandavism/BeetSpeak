using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Row : MonoBehaviour
{
	public List<Note> notes;
	private Color _color; 

	private int _goodHits = 0;
	private int _misses = 0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (var note in notes)
		{
			
		}
	}

	public void Reset()
	{
		_goodHits = 0;
		_misses = 0;
		notes.ForEach(x => x.Reset());
	}

	//TODO: what about if you hit one note multiple times? (probably not going to happen but eh...)
	public bool IsRowCorrect()
	{
		return (_goodHits == notes.Count) && (_misses == 0);
	}

	//TODO: probably binary search this stuff
	public void RecordHit(float time)
	{
		foreach(var note in notes)
		{
			if (note.WasHit(time))
			{
				_goodHits++;
				return;
			}
		}

		_misses++;
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

		for(int i = 0; i < notes.Count; i++)
		{
			for(int j = 0; j< notes.Count; j++)
			{
				if(j != i)
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
}

