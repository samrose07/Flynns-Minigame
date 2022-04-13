using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	/*public FirstPersonAIO FPAio;
	public Text dialText;
	public Text cText;
	private Queue<string> sentences;

	// Use this for initialization
	void Start()
	{
		sentences = new Queue<string>();
	}
	public void StartDialogue (Dialogue dialogue)
	{
		Debug.Log("Starting conversation with " + dialogue.name);
		FPAio.enableCameraMovement = false;
		FPAio.lockAndHideCursor = false;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		FPAio.playerCanMove = false;

		//dialText.text = dialogue.sentences[0];
		//dialText.text = sentences.Peek().ToString();
		//sentences.Clear();
		
		/*foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		/*string sentence = sentences.Dequeue();
		Debug.Log(sentence);
	}

	public void EndDialogue()
	{
		Debug.Log("End of Conversation");
		FPAio.enableCameraMovement = true;
		FPAio.lockAndHideCursor = true;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		FPAio.playerCanMove = true;
	}*/
}

