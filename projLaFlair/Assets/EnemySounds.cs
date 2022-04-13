using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class EnemySounds : MonoBehaviour
{
    public AudioSource enemySource;
    public AudioClip deathClip;
    public AudioClip idleClip;
    public AudioClip screamClip;
    public AudioClip footStep1;
    public AudioClip footStep2;
    public AudioClip enemyHitClip;
    // Start is called before the first frame update
    public void PlayDeath()
    {
        enemySource.PlayOneShot(deathClip);
    }
    public void PlayIdle()
    {
        enemySource.PlayOneShot(idleClip);
    }
    public void PlayScream()
    {
        enemySource.PlayOneShot(screamClip);
    }
    public void PlayFootStep1()
    {
        enemySource.PlayOneShot(footStep1);
    }

    public void PlayFootStep2()
    {
        enemySource.PlayOneShot(footStep2);
    }
    public void PlayHitClip()
    {
        enemySource.PlayOneShot(enemyHitClip);
    }
}
