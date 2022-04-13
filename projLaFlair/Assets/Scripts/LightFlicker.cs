using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public int number;
    public int numberToChoose;
    public int flickRate;
    public int min;
    public GameObject light;
    public Light light2;
    //public Material material;
    public Color color;


    // Update is called once per frame
    void Update()
    {
        //choose a number from a range that has been input in the inspector.
        number = Random.Range(min, flickRate);
        //if the number matches, flicker the light.
        if (numberToChoose == number) Flicker();
        
    }

    //check to see if light is active, if it is, set inactive (to simulate turning off). Do the opposite if the opposite is true.
    //this allows for the eerie feeling brought about by lights that flicker.
    void Flicker()
    {
        //CHANGE switched form gameobject to light object to keep at least a little light
        if (light2.intensity >= 2.7)
        {
           // light.SetActive(false);
            light2.intensity = 0.66f;
            //colour *= 0.1f; //  4X brighter
            
        }
        else
        {
            
            //light.SetActive(true);
            light2.intensity = 2.7f;
            
        }
        //material.SetColor("_EmissionColor", color);
    }
}
