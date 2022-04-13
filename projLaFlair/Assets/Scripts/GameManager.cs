using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    /*created game manager. in the future will have more to do.
     * currently allows for streamlines communication between ability, hit, and other classes.
     * the enemies only need to reference this class to get the ability, even tho they reference
     * ability too. Ability only needs to reference this class to get information about the enemy.
     * originally thought to add list of dead enemies for essence extraction, bailed on that.
     */
    public Ability abilityClassAttachedToGameObject;
    public bool setHit = false;
    public List<Enemy> enemyList = new List<Enemy>();
    public bool canPlayGooBlebs = false;
    //public Enemy[] enemyArray;
    
        //adds essence to the current count
    public void AddEssence()
    {
        abilityClassAttachedToGameObject.essenceCount++;
        
    }
    //global allowing for hits from enemy and player.
    public void SetTheHit()
    {
        setHit = true;
    }
    public void PlayAbilityClip()
    {
        abilityClassAttachedToGameObject.PlayEnemyHitClip();
    }
    //below is list graveyard. barren class right now but was a breath of fresh air.
    /*public void AddDeadEnemyToList(Enemy enemy)
    {
        enemyList.Add(enemy);
    }
    public void RemoveEnemyFromList(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    public int GetCurrentEnemyIndexFromList(Enemy enemy)
    {
        int index;

        index = enemyList.IndexOf(enemy);

        return index;
    }

    public void ExtractionMethod()
    {
        //int indexOfEnemyToExtract = GetCurrentEnemyIndexFromList(enemyList.IndexOf);
    }*/
}
