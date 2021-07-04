using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : MonoBehaviour
{
    public Color colorStart = Color.white;
    public Color colorEnd = Color.clear;
    public static float colortimer = 0.0f;
    public float colorduration = 0.4F;
    public Conductor conductor;
    
    float lastbeat; //this is the ‘moving reference point’

    float bpm = 175;

    private float crotchet;

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.clear;
        lastbeat = 0;
        crotchet = 60 / bpm;
    }

    void Update(){

        if (conductor.songPosition > lastbeat + crotchet) {
            Flash();
            lastbeat += crotchet;
        }
        colortimer += Time.deltaTime;
        GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, colortimer/colorduration);

    }

    public static void Flash(){
        colortimer = 0;
    }
}
