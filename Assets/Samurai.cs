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
        animator.Play("samurai_left", -1, 0.0f);
    }
    public void StrikeRight()
    {
        animator.Play("samurai_right", -1, 0.0f);
    }
    public void StrikeUp()
    {
        animator.Play("samurai_up", -1, 0.0f);
    }
    public void StrikeDown()
    {
        animator.Play("samurai_down", -1, 0.0f);
    }
}
