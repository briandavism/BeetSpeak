using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmVisualizer : MonoBehaviour {

    public Slider slider; //Spawn and name the slider
    public float sliderPosition = 0; //initialize the variable to check on slider position
    public float measureLength = 2; //Set the number of seconds it takes for the slider to complete

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var deltaTime = Time.deltaTime; //the time since it last incremented the slider ie. time since last frame
        var sliderProgress = deltaTime / measureLength; //var to show how much it should increment the slider this frame

        sliderPosition = (sliderPosition + sliderProgress) % 1; //not sure what the %1 does however this adds the progress ammount to the existing bar and sets the total that the bar should be at the end of the frame

        slider.value = sliderPosition; //this sets the new position
	}
}
