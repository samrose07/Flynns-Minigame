//All new changes are marked as NEW CHANGE and old comments for the most part have been deleted.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ability : MonoBehaviour
{
    //sound vars
    public AudioSource playerSource;
    public AudioClip playerTookDamage;
    public AudioClip enemyTookDamage;
    public AudioClip playerDead;
    public AudioClip gravityClip;
    public AudioClip gooBlebsClip;
    public AudioClip[] wrenchWooshes;
    public bool canPlayDeath = false;
    //end sound vars

    //GUI vars
    public Text essenceCountText;
    public int  essenceCount = 0;
    //end GUI vars
    
    //for wrench
    public Animator animator;
    public Animator handimator;
    public Collider wrenchCollider;
    //public Collider enemyCollider;
    public Wrench   wrenchScript;
    public int      wrenchDamage;
    //end wrench

    //for grav flip
    public bool gravityEnabled = false;
    public Transform      playerBody;
    public PlayerMovement playerMovement;
    public Quaternion     targetRotation;
    public bool           canRotate = false;
    private bool          canClick = true;
    public float          rotateCounter = 0;
    public float          turnSpeed;
    //end grav flip
    //for fire
    public bool gooEnabled = false;
    public GameObject firePrefab;
    public Transform  startPos;
    public float      projectileSpeed;
    public Vector3    gravityVector;
    public int        fireDamage;
    public Animator   gooImator;
    //end fire

    //for Extract
    public GameObject extractor;
    public Animator extractimator;
    //end Extract
    //other player variables
    public int playerHealth;
    public DeathScreen deathScreen;
    public PlayerMovement pm;
    public MouseLook mouseLook;
    //end other variables

    //state machine
    public enum State
    {
        GravFlip,
        Fire,
        Extract,
        Wrench
    }

    public State state;
    private void Awake()
    {
        targetRotation = playerMovement.transform.rotation;
        //state initially set to wrench
        state = State.Wrench;
    }
    private void Update()
    {
        //CHANGE: Display currency allocation
        essenceCountText.text = "Essence Collected: " + essenceCount;
        //checks to see if a new ability is selected.
        SwitchAbility();
        //CHANGE: handles death if health goes to or below zero
        if(playerHealth <= 0)
        {
            if(!canPlayDeath)
            {
                PlayPlayerDiedClip();
                canPlayDeath = true;
            }
            if (!deathScreen.deathActivated) deathScreen.GameOver();
            mouseLook.enableCameraMovement = false;
            mouseLook.lockAndHideCursor = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            pm.playerCanMove = false;
        }
        //CHANGE IN STATE MACHINE: just updated for goo animations (:
        switch(state)
        {
            case State.Wrench:
                /* Wrench: calls Hit() in the wrench class to check collions which
                 * gets communicated to the enemy class.
                 */
                handimator.SetBool("HandRaise", false);
                gooImator.SetBool("goo", false);
                animator.SetBool("RaiseWrench", true);
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    animator.Play("wrenchhit");
                    PlayWooshClip();
                    wrenchScript.Hit();
                    // animator.SetBool("HitWrench", true);
                }
                

                //animator.SetBool("HitWrench", false);
                break;

            case State.GravFlip:
                //flips gravity on right click
                handimator.SetBool("HandRaise", true);
                gooImator.SetBool("goo", false);
                if (Input.GetKeyDown(KeyCode.Mouse1) && canClick)
                {                   
                    playerMovement.gravity *= -1;
                    playerMovement.jumpHeight *= -1;
                    if (playerMovement.isGrounded)
                    {
                        playerMovement.vel.y *= -1;
                        canRotate = false;
                        canClick = true;
                        
                    }
                    PlayGravClip();

                    canRotate = true;
                    targetRotation *= Quaternion.AngleAxis(180, Vector3.left);
                }
                if (canRotate) StartRotate();
                break;

            case State.Fire:
                //state allows the goo to be fired.
                handimator.SetBool("HandRaise", false);
                gooImator.SetBool("goo", true);
                if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Fire();
                }
                
                
                break;
                //CHANGE: Extract state created and used.
            case State.Extract:
                //state allows essence to be extracted for points
                extractimator.SetBool("RaiseExtractor", true);
                break;
                
        }
        
    }
    private void SwitchAbility()
    {
        //new changes: advocates for the goo and extractor animation change.
        if (Input.GetKeyDown(KeyCode.Alpha1)) state = State.Wrench;
        if (!Input.GetKeyDown(KeyCode.Alpha1)) animator.SetBool("RaiseWrench", false);
        if (Input.GetKeyDown(KeyCode.Alpha2) && gravityEnabled) state = State.GravFlip;
        if (Input.GetKeyDown(KeyCode.Alpha3) && gooEnabled) state = State.Fire;
        if (Input.GetKeyDown(KeyCode.Alpha4)) state = State.Extract;
        if (!Input.GetKeyDown(KeyCode.Alpha4)) extractimator.SetBool("RaiseExtractor", false);
    }

    private void Hit()
    {
        
        //TODO but wont because it is done
        
    }
    private void StartRotate()
    {
        //this sets the rotation of the player when gravity is inverted. Took a lot
        //of trial and error, but essentially checks to see if the player can rotate
        //then sets whether or not they can in the update function,
        //then rotates the player!
        print("I am here");
        playerBody.transform.rotation = Quaternion.Lerp(playerBody.transform.rotation, targetRotation, 10 * turnSpeed * Time.deltaTime);
        if (rotateCounter == 250)
        {
            canClick = true;
            canRotate = false;
            rotateCounter = 0;
        }
        else
        {
            canClick = false;
            rotateCounter++;
        }
    }

    private void Fire()
    {
        //projectile system for the goo ball to travel
        //towards where player is looking and make noise.
        Vector3 place = new Vector3(startPos.transform.position.x,startPos.transform.position.y,startPos.transform.position.z);
        GameObject fireball = Instantiate(firePrefab,place,Quaternion.identity) as GameObject;
        fireball.transform.position = startPos.position;
        fireball.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        AudioSource projSource = fireball.GetComponent<AudioSource>();
        projSource.PlayOneShot(gooBlebsClip);
    }

    //all scripts below are able to be accessed by other scripts, depending on what 
    //audio needs to be played at any given time.

    public void PlayWooshClip()
    {
        int randomClip = Random.Range(0, wrenchWooshes.Length);
        playerSource.PlayOneShot(wrenchWooshes[randomClip]);
    }

    public void PlayGravClip()
    {
        playerSource.PlayOneShot(gravityClip);
    }

    public void PlayDeathClip()
    {

    }
    public void PlayPlayerHitClip()
    {
        playerSource.PlayOneShot(playerTookDamage);
    }

    public void PlayEnemyHitClip()
    {
        playerSource.PlayOneShot(enemyTookDamage);
    }

    public void PlayPlayerDiedClip()
    {
        playerSource.PlayOneShot(playerDead);
    }
}

/*
 * This class is the player's base class in the minigame. It handles the state machine
 * of switching abilities and using them, as well as playing audio depending on what
 * happens and what accesses the given functions.
 */