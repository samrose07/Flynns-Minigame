using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooAudioScript : MonoBehaviour
{
    public GameManager gm;
    public AudioSource aSource;
    public AudioClip gooAnimClip;
    public void PlayGooAnimClip()
    {
        if(gm.canPlayGooBlebs) aSource.PlayOneShot(gooAnimClip);

    }
}
