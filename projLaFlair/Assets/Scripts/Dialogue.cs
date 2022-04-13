using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue {
	public string name;

	[TextArea(3,10)]
	public string[] sentences;
    [TextArea(3, 10)]
    public string[] choices;
	
}


/* To add options and outcomes of the optional dialogues:
 * Make all sentences available in the queue so we can go to them later. Each sentence will have
 * more options to choose from i guess.
 * In here, make a function called options that takes a parameter of the option chosen.
 * then, set the current sentence to the new option. Or cycle through using a for-loop
 * Should create an exit dialogue button
 * should create a function to get back to a baseline.
 */