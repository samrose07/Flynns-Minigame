using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HaroldFlynn : MonoBehaviour {

	[TextArea(3, 10)]
	public string[] sentences;
	public Button[] cButtons;
	public GameObject choiceHolder;
	[TextArea(3, 10)]
	public string[] choices;
	public Text dialText;
	private bool canUpdate = false;
	public MouseLook mouseLook;
	public PlayerMovement pm;
	private int responseNeededB1;
	private int responseNeededB2;
	private int responseNeededB3;
	private int currSentence;
	// Use this for initialization
	void Start() {

}

	// Update is called once per frame
	public void StartTheInteraction()
	{
		mouseLook.enableCameraMovement = false;
		mouseLook.lockAndHideCursor = false;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		pm.playerCanMove = false;
		canUpdate = true;
		dialText.text = sentences[0];
	}
	public void EndTheInteraction()
	{
		canUpdate = false;
		mouseLook.enableCameraMovement = true;
		mouseLook.lockAndHideCursor = true;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		pm.playerCanMove = true;
		dialText.text = "";
		choiceHolder.SetActive(false);
	}
	void Update () {
		if(!canUpdate)
		{
			return;
		}
		Debug.Log("I have arrived");
		MakeYourChoice(currSentence);
	}


	/*this function is where you handle the choices.
	 * Adds an event listener to each button that takes parameters
	 * and passes them to the function of choice picked.
	 * each response needed is set in the actual cases so as to avoid clutter/confusion. 
	 * It is laid out as when you press the button I pass the number required 
	 * for the correct response.*/
	private void MakeYourChoice(int cs)
	{
		dialText.text = sentences[currSentence];
		choiceHolder.SetActive(true);
		cButtons[0].onClick.AddListener(delegate { ChoicePicked(responseNeededB1, 0); });
		cButtons[1].onClick.AddListener(delegate { ChoicePicked(responseNeededB2, 1); });
		cButtons[2].onClick.AddListener(delegate { ChoicePicked(responseNeededB3, 2); });
		switch (cs)
		{
			case 0:
				
				cButtons[0].GetComponentInChildren<Text>().text = choices[0].ToString();
				cButtons[1].GetComponentInChildren<Text>().text = choices[1].ToString();
				cButtons[2].GetComponentInChildren<Text>().text = choices[9].ToString();
				responseNeededB1 = 2;
				responseNeededB2 = 1;
				responseNeededB3 = 8;
				
				break;
			case 1:
				cButtons[0].GetComponentInChildren<Text>().text = choices[0].ToString();
				cButtons[1].GetComponentInChildren<Text>().text = choices[3].ToString();
				cButtons[2].GetComponentInChildren<Text>().text = choices[9].ToString();
				responseNeededB1 = 2;
				responseNeededB2 = 3;
				responseNeededB3 = 8;
				
				break;
			case 2:
				cButtons[0].GetComponentInChildren<Text>().text = choices[2].ToString();
				cButtons[1].GetComponentInChildren<Text>().text = choices[9].ToString();
				cButtons[2].GetComponentInChildren<Text>().text = "";
				responseNeededB1 = 4;
				responseNeededB2 = 8;
				responseNeededB3 = -1;
				break;
			case 3:

				break;
			case 4:

				break;
			case 8:
				EndTheInteraction();
				currSentence = 0;
				break;
		}
	}

	private void ChoicePicked(int responseNumber, int clickedButton)
	{
		Debug.Log("CHOICE ONE PICKED");
		if(responseNumber != -1)
		{
			currSentence = responseNumber;
		}
		else
		{
			return;
		}
		
	}
}
