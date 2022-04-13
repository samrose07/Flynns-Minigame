using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {
	[Header("Set in Inspector")]
	public bool canOpen;
	public bool isLockedOnStart;
	public bool locked;
	public Vector3 openDirection;
	public Vector3 closeDirection;
	public int openSpeed;
	public int closeSpeed;
	public bool open;
	public bool close;
	public bool isOpenAlready;
	[TextArea]
	public string Note = "ALWAYS OPEN DOORS TO THE RIGHT. UNLESS YOU WANT TO DO MORE CODING (:";
	// Use this for initialization
	void Start () {
		locked = isLockedOnStart;
		if(locked)
		{
			isOpenAlready = false;
			close = false;
		}
	}

	public void UnlockDoor()
    {
		if(locked) locked = false;
		return;
    }
	

	public void OpenDoor()
    {
		if (!locked && !isOpenAlready) open = true;
		if (isOpenAlready) close = true;
		
    }

	private void InScriptOpenDoor()
	{
		transform.position += openDirection * Time.deltaTime * openSpeed;
		if (transform.position.x == openDirection.x) open = false;
	}
	// Update is called once per frame
	void Update () {
		float vecX = openDirection.x;
		float vecZ = openDirection.z;
		if(vecX > 0 || vecX < 0)
		{			
			if (vecZ == 0)
			{
				if (open)
				{
					if (transform.position.x != openDirection.x)
					{
						transform.position += openDirection * Time.deltaTime * openSpeed;
					}
					if (transform.position.x >= openDirection.x)
					{
						open = false;
						isOpenAlready = true;
					}
				}
				if (close)
				{
					if (transform.position.x != closeDirection.x)
					{
						transform.position -= closeDirection * Time.deltaTime * (openSpeed + closeDirection.x);
					}
					if (transform.position.x <= closeDirection.x)
					{
						close = false;
						isOpenAlready = false;
					}
				}
			}
			
		}
		if (vecZ < 0)
		{
			if (open)
			{
				if (transform.position.z != openDirection.z)
				{
					transform.position += openDirection * Time.deltaTime * openSpeed;
				}
					if (transform.position.z <= openDirection.z)
					{
						open = false;
						isOpenAlready = true;
					}
			}
			if (close)
				{
					//print("I GOT TO CLOSE");
					if (transform.position.z != closeDirection.z)
					{
						transform.position += closeDirection * Time.deltaTime * closeSpeed;
					}
					if (transform.position.z >= closeDirection.z)
					{
						close = false;
						isOpenAlready = false;
					}
				}
			
		}
		if (locked) return;
		

		
	}
}
