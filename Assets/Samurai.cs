using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai : MonoBehaviour
{
    [SerializeField] Animator animator;

    void Start()
    {

    }
    
    public void StrikeLeft()
    {
        // Activate animation that plays
        animator.Play("samurai_left");
    }
    public void StrikeRight()
    {
        // Activate animation that plays
        animator.Play("samurai_right");
    }
    public void StrikeUp()
    {
        // Activate animation that plays
        animator.Play("samurai_up");
    }
    public void StrikeDown()
    {
        // Activate animation that plays
        animator.Play("samurai_down");
    }
}
