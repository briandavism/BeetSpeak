using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Row : MonoBehaviour
{
	public List<Note> notes;

	private int goodHits = 0;
	private int misses = 0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Reset()
	{
		goodHits = 0;
		misses = 0;
		notes.ForEach(x => x.Reset());
	}

	//TODO: what about if you hit one note multiple times? (probably not going to happen but eh...)
	public bool IsRowCorrect()
	{
		return (goodHits == notes.Count) && (misses == 0);
	}

	//TODO: probably binary search this stuff
	public void RecordHit(float time)
	{
		foreach(var note in notes)
		{
			if (note.WasHit(time))
			{
				goodHits++;
				return;
			}
		}

		misses++;
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

