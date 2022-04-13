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
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //statemachine
    public enum State
    {
        Idle,
        Scream,
        CloseDistance,
        Attack,
        Damage,
        Die,
        Search
    }
    //variables
    public GameManager gameManager;
    public Transform target;
    public Transform targetHitArea;
    public float viewConeDegrees;
    public float distanceToCheck;
    public State state;
    private Vector3 targetRotation;
    private Vector3 lastKnownTargetPosition;
    private int countBeforeSearch = 0;
    private int countAnim = 0;
    public Animator animator;
    public NavMeshAgent agent;
    public Material enemyMat;
    private int damageCount = 0;
    public int health = 100;
    public int damage;
    public EnemyHit enemyHit;
    private bool canExtract = true;
    private bool canSubtract = false;

    void Start()
    {
        //initial state is idling
        //agent allows for navigational data to happen
        //makes sure to reset the albedo of the enemy to indicate it not being hit. Placeholder albedo for now.
        state = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        enemyMat.SetColor("_Color", Color.white);
    }

    void Update()
    {
        
        //print(countAnim);
        //state machine switch
        switch (state)
        {
            /*Animations are changed with setBools in each given state.
             * 
             * idle: checks the field of view and if the player breaches the radius, the state called scream is initiated.
             */
            case State.Idle:
                CheckHealth();
                animator.SetBool("IntoAttack", false);
                //CheckFieldOfView();
                bool inView = CheckFieldOfView();
                if (inView)
                {
                    animator.SetBool("IntoAttack", true);
                    state = State.Scream;
                }
                break;
                /*Scream: purely to play the scream animation and to make sure the animation plays without moving the enemy
                 * itself. without this, the enemy floats around while screaming.
                 * The countAnim is to make sure the enemy switches states on time.
                 * after the animation is done, state is set to closeDistance
                 */
            case State.Scream:
                CheckHealth();
                if (countAnim >= 100)
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsName("Screamer"))
                    {
                        //print("in screamer");
                    }
                    else
                    {
                        //print("not in screamer");
                        state = State.CloseDistance;
                    }
                }
                else countAnim++;
                
                
                break;

                /* CloseDistance: follows the player to "close the gap" and enter hit range.
                 * the lastknowntargetposition was made for the search state which 
                 * i couldn't get working correctly just yet.
                 * checks field of fiew again, if the player manages to leave the FOV then idle is called.
                 * Allows the enemy to reset if the player flips their gravity or otherwise gets away.
                 * 
                 * closeToTarget checks to see if enemy is in hit range, then switches states accordingly.
                 * other calculations include: facing towards player, moving towards target with navigation.
                 */
            case State.CloseDistance:
                CheckHealth();
                animator.SetBool("IntoAttack", true);
                animator.SetBool("ReachedTarget", false);
                lastKnownTargetPosition = target.position;
                inView = CheckFieldOfView();
                //print("In view: " + inView);
                if (!inView) state = State.Idle;
                bool closeToTarget = CheckDistanceOf(target);
                if (closeToTarget) state = State.Attack;
                targetRotation = new Vector3(target.position.x, transform.position.y, target.position.z);
                transform.LookAt(targetRotation);
                agent.destination = target.position;
                break;

                /*Attack: pretty basic right now, starts attack animation.
                 * also checks the distance and if it needs to close the gap again,
                 * then goes back into CloseDistance.
                 */
            case State.Attack:
                CheckHealth();
                animator.SetBool("IntoAttack", false);
                animator.SetBool("ReachedTarget", true);
                enemyHit.Hit(damage);
                closeToTarget = CheckDistanceOf(target);
                if (!closeToTarget) state = State.CloseDistance;
                break;

                /*Damage: this state is set by the wrench class.
                 * if the wrench confirms a hit then change color to indicate as such.
                 * Further changes will include sound effects and damage numbers.
                 * The damageCount is used for the same reason as the scream,
                 * allowing the red color to appear on screen for longer than a frame.
                 * 
                 * after that, set to close distance again which acts as a pseudo-flinch,
                 * meaning more feedback occurs. CloseDistance will then set to Attack again if
                 * still in range.
                 */
            case State.Damage:
                CheckHealth();
                // print("enemy health: " + health);
                damageCount++;
                enemyMat.SetColor("_Color", Color.red);
                if(damageCount >= 30)
                {
                    enemyMat.SetColor("_Color", Color.white);
                    damageCount = 0;
                    state = health <= 0 ? State.Die : State.CloseDistance;
                    //state = State.CloseDistance;
                    
                }
                
                break;
                //Die: TODO
            case State.Die:
                //CHANGE: makes sure to set color to white when dead so as to not portray damage to other hiveminded enemies.
                enemyMat.SetColor("_Color", Color.white);
                CheckHealth();
                animator.SetBool("IsDead", true);
                //thought of adding enemy to dead list for extraction purposes, bailed. This is the shadow of that.
                //gameManager.AddDeadEnemyToList(this);
                break;
                //Search: TOFIX
            case State.Search:
                //print("I got here and the LKPP is: " + lastKnownTargetPosition);
                //still haven't done anything here.
                break;
        }
        //print(state);
    }

    //method returns a bool depending on the position between tf (target) position and enemy position.
    private bool CheckDistanceOf(Transform tf)
    {
        float distance = Vector3.Distance(transform.position, tf.position);
        return distance < distanceToCheck ? true : false;
    }

    //method returns a bool after creating a field of view. Measured in angles.
    private bool CheckFieldOfView()
    {
        Vector3 targetDirection = target.position - transform.position;
        float viewAngle = Vector3.Angle(targetDirection, transform.forward);

        if (viewAngle < viewConeDegrees)
        {
            return true;
        }
        return false;
    }

    //CHANGE: allows to take away damage to the instance this script is attached to. previously
    //used wrench.cs to do so, but that didn't allow for multiple prefabs without
    //tedious coding.
    public void OnCollisionEnter(Collision collision)
    {
        print("collidedWithsomething" + collision.collider.tag);
        if(collision.collider.tag == "Wrench" && gameManager.setHit)
        {
           
            gameManager.setHit = false;
            canSubtract = false;
            state = State.Damage;
            gameManager.PlayAbilityClip();
        }
        //if goo collides, take away enough damage to allow for only one hit of the wrench to kill
        if(collision.collider.tag == "Projectile")
        {
            print("got to projectile collider");
            health -= 70;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        //once the collision is over, then take away health from the enemy
        //print("exited collision");
        if (collision.collider.tag == "Wrench" && !canSubtract)
        {
            health = SubtractHealth(health);
            
        }

    }
    
    //CHANGE: adds essence via game manager, destroys enemy upon extraction
    public void Extract()
    {
        if (canExtract) print("extracted");
        gameManager.AddEssence();
        canExtract = false;
        Destroy(gameObject);
    }
    //just takes away health from enemy
    private int SubtractHealth(int currentHealth)
    {      
        return currentHealth - 30;
    }
    //checks health in each state to make sure it isn't dead.
    private void CheckHealth()
    {
        if (health <= 0) state = State.Die;
    }
}

/*
 * This is like the ability class but for the enemy.
 * The enemies have their own state machine that differs
 * from the player, so that is created here. 
 */