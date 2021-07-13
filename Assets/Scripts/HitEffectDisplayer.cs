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

    public GameManager gm;

    private float perfectThreshold;
    private float goodThreshold;
    private float hitThreshold;
    
    [SerializeField] Animator animator;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        perfectThreshold = gm.perfectThreshold;
        goodThreshold = gm.goodThreshold;
        hitThreshold = gm.hitThreshold;
    }

    public void Hit(float dist)
    {
        
        if (dist <= perfectThreshold)
        {
            _sr.sprite = perfect;
            animator.Play("hiteffect", -1, 0.0f);
        } else if (dist <= goodThreshold)
        {
            _sr.sprite = good;
            animator.Play("hiteffect", -1, 0.0f);
        } else if (dist <= hitThreshold)
        {
            _sr.sprite = hit;
            animator.Play("hiteffect", -1, 0.0f);
        }
        else
        {
            _sr.sprite = miss;
        }
        
    }
}
