using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //VARIABLES.
    public CharacterController controller;
    public float speed = 12f;
    public float sprint;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public bool playerCanMove = true;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Vector3 vel;
    public bool isGrounded;
    public Transform characterBody;

    
    //END VARIABLES

    void Update()
    {
        //if you can move (i.e. you're not in a dialogue or paused) then do it.
        //future prooding. When I add other things this will be more usefull.
        if (playerCanMove)
        {
            //check if on ground
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && vel.y < 0)
            {
                vel.y = -2f;
            }

            //set up movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            //move the controller based on speed and movement setup
            controller.Move(move * speed * Time.deltaTime);

            //jump
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                vel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            //end jump


            //start sprint statements
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                //print("sprint");
                speed *= sprint;
                //EMERGENT GAMEPLAY OPPORTUNITY
                //powerup that allows you to increase your speed nonlinearly to go zoom zoom
            }
            //when shift is release, no longer sprint
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed /= sprint;
            }
            //end sprint statements


            vel.y += gravity * Time.deltaTime;
            controller.Move(vel * Time.deltaTime);
            //print(isGrounded);
            //if you can't move, do nothing.
        }
        else return;
    }

    //the function to invert the player.     
}
