using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Enemy enemy;
    private bool setHit = false;
    public Ability ability;
    public DeathScreen deathScreen;
    private int damageToDo;
    public void Hit(int damage)
    {
        //set hit allows for permission to hit to happen.
        setHit = true;
        damageToDo = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        print("triggered by or with " + other.tag);
        if (other.tag == "Player")
        {
            print("playerHealth is: " + ability.playerHealth);
            //CHANGE: made sure the collision wouldn't harm the player if the enemy wasn't strictly in attack mode.
            if (ability.playerHealth <= 0 || enemy.state != Enemy.State.Attack)
            {


                return;
            }
            else
            {
                //subtracts health set in inspector
                ability.PlayPlayerHitClip();
                ability.playerHealth -= damageToDo;
                print("playerHealth is now: " + ability.playerHealth);
            }


        }
        setHit = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("collided with: " + collision.collider.tag);
        
    }
}
