using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteObject : MonoBehaviour
{
    private bool canBePressed;
    public KeyCode keyToPress;
    private bool hasStarted;
    private SpriteRenderer sr;
    public float moveSpeed;
    public Vector3 activatorPosition;

    public Vector3 movementDirection;
    private Conductor _conductor;

    private float lastSongPos;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        hasStarted = true;  // should be false
        _conductor = GameObject.FindWithTag("Conductor").GetComponent<Conductor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                HitNote();
            }
        }

        if (hasStarted)
        {
            Move();
        }
    }

    void HitNote()
    {
        gameObject.SetActive(false);
        // Calculate how far from the optimal position the note was
        var dist = Mathf.Abs((transform.position - activatorPosition).magnitude);
        GameObject.FindWithTag("HitEffectDisplayer").GetComponent<HitEffectDisplayer>().Hit(dist);
    }

    void Move()
    {
        var pitch = _conductor.pitch;
        transform.position += movementDirection * (Time.deltaTime * moveSpeed * pitch);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = true;
            activatorPosition = other.transform.position;
        } 
        if (other.CompareTag("NoteDeletionPoint"))
        {
            // In case this point is touched -> note has gone too far and has been missed
            HitNote();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;
        }
    }

    public void OnBeat()
    {
        Debug.Log("ah yes beat");
        lastSongPos = _conductor.songPosition;
    }

    public void OnBar()
    {
        Debug.Log("oh shit a bar");
    }
}
