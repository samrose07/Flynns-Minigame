using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    private bool setHit = false;
    public Material enemyMat;
    public Enemy enemy;
    public GameManager gameManager;
    /* The purpose of this script is to collect collider information from the wrench
     * and the enemies. Each enemy created will have the enemy script attached
     * which allows it to take damage, search for the player,
     * etc. 
     */
    public void Hit()
    {

        //change, set the hit from the game manager.

        //IN FACT, FULL STOP, THIS CLASS DOESN'T DO ANYTHING ANYMORE LMAO
        gameManager.SetTheHit();
    }
    private void OnCollisionEnter(Collision col)
    {
        /* this method only constructs collision data if the 
         * tag of the collider is marked as an enemy.
         * The state of the hit enemy is then set
         * to take damage.
         * Then setHit is returned to the false value to allow
         * another calculation to be made upon
         * another wrench swing.
         */
        if(setHit)
        {
            if (col.collider.tag == "Enemy")
            {
                //print("hit enemy");
                
                //enemy.health -= 30;
               // enemy.state = Enemy.State.Damage;
                //enemyMat.SetColor("_Color", Color.red);
              
            }
           setHit = false; 
        }

        
    }
}
