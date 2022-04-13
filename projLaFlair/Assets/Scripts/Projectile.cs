using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ParticleSystem[] ps;
    private int lifeCycle = 0;
    public GameObject gooBall;
    public AudioSource aSource;
    public AudioClip gooAnimClip;
    public AudioClip gooExplosionClip;
    public bool cantPlayExplosionClip = false;
    private void Start()
    {
        //makes sure the particle systems in the projectile dont play on the goo ball that the player holds and the initial launched projectiles.
        foreach (ParticleSystem psys in ps)
        {
            psys.Stop();
        }
    }
    void Update()
    {
        //deletes goo ball
        if (lifeCycle >= 1500) Destroy(gameObject);
        lifeCycle++;
        //print(lifeCycle);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //allows for pseudo explosion of particles on collision with anything, makes ball itself invisible.
        gooBall.SetActive(false);
        if(!cantPlayExplosionClip)
        {
            aSource.PlayOneShot(gooExplosionClip);
        }
        foreach(ParticleSystem psys in ps)
        {
            psys.Play();
        }
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }

    
}
