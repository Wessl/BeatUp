using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectDisplayer : MonoBehaviour
{
    private SpriteRenderer _sr;
    public Sprite perfect;
    public Sprite good;
    public Sprite hit;
    public Sprite miss;

    public float perfectThreshhold;
    public float goodThreshhold;
    public float hitThreshhold;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    public void Hit(float dist)
    {
        if (dist <= perfectThreshhold)
        {
            _sr.sprite = perfect;
        } else if (dist <= goodThreshhold)
        {
            _sr.sprite = good;
        } else if (dist <= hitThreshhold)
        {
            _sr.sprite = hit;
        }
        else
        {
            _sr.sprite = miss;
        }
    }
}
