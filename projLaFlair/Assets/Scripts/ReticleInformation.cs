/*
 * Samuel Rose
 * Seeking Bachelor's Degree in Games, Interactive Media, and Mobile Design
 * This code was written for the purpose of an individual game project
 * under the guise of education in the respective major at
 * BOISE STATE UNIVERSITY.
 * Any unauthorized use of code is not cool man.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReticleInformation : MonoBehaviour {

	/*
	 * This class allows for information about an object in game to be
	 * presented to the player, such as the name meaning it can be interacted with.
	 * eventually, these objects will be in game and interactable, allowing for the immersive sim
	 * aspect of it.
	 */
	private bool connect = false;
	public Text txt;
	public Text dialText;
	public GameObject choiceBtns;
	public GameObject[] objs;
	private bool DisplayInfo;
	private bool isText = false;
	public Ability ability;
	public GameManager gameManager;
	private Enemy enemy;
	private string enemyName;
	//look at objects only in layer 8

	// Use this for initialization
	void Start () {
		choiceBtns.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		int layerMask = 1 << 8;
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		
		if (Physics.Raycast(transform.position, fwd, out hit, 5.0f, layerMask))
		{
			Debug.DrawLine(transform.position, hit.point, Color.red);
				txt.text = hit.collider.gameObject.name;
			//CHANGE: if collider is named enemy, set it to horror just cuz. still allows for individuality between instances.
				if(hit.collider.gameObject.name == "Enemy")
				{
					txt.text = "horror";
				//CHANGE: Switch case gets the component of enemy for whatever enemy the player is looking at. This needed to happen
				//because i didn't create the ability class around all instances, instead focusing my testing on just one.
				//This took too long to figure out and i am not proud of that time spent but it's done now.
					switch(hit.collider.gameObject.name)
					{
					case "Enemy":
						enemy = hit.collider.gameObject.GetComponent<Enemy>();
						//used for later switch statement.
						enemyName = "Enemy";
						break;					
					}
				}
				

			StartInteract(hit.collider.gameObject);
			connect = true;
		}
		else {
			txt.text = "";
			dialText.text = "";
			connect = false;
			}
	}
	void StartInteract(GameObject go)
	{
		
		//DialogueTrigger dial = go.GetComponent<DialogueTrigger>();
		if(Input.GetKeyDown(KeyCode.E))
		{
			//dial.TriggerDialogue();
			//these are for the big project itself, don't worry about em yet (:
			switch (go.name)
			{
				case "Door":
					DoorScript ds = go.GetComponent<DoorScript>();
					ds.UnlockDoor();
					ds.OpenDoor();
					break;
				case "Harold Flynn":
					HaroldFlynn hf = go.GetComponent<HaroldFlynn>();
					hf.StartTheInteraction();
					break;
				case "Justine Floyd":

					break;
				case "Jack Flynn":

					break;
				case "Hanna Cho":

					break;
				case "Teresa Johnson":

					break;
				
			}
			//this had to be in an if statement because it isn't a static variable.
			//if name is enemy, and state is die, allow for extraction of essence.
			if(go.name == enemyName)
			{
				if (enemy.state == Enemy.State.Die)
				{

					if(ability.state == Ability.State.Extract)
					{
						enemy.Extract();
					}
					
				}
				
			}

		}
		//this is for later implementation. dont worry about it (:
		if (Input.GetKeyDown(KeyCode.Q))
		{
			//dial.ED();
			switch (go.name)
			{
				case "Harold Flynn":
					HaroldFlynn hf = go.GetComponent<HaroldFlynn>();
					hf.EndTheInteraction();
					break;
				case "Justine Floyd":

					break;
				case "Jack Flynn":

					break;
				case "Hanna Cho":

					break;
				case "Teresa Johnson":

					break;
			}
		}
	}
}

/*
 * Some code in the switch statements are not used. They are
 * a relic of an idea long past, and I was too invested to 
 * get rid of them. Just know it was really cool and I want to 
 * continue it at some point!
 */
