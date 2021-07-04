using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    public float beatTempo;
    public float beatOffset;
    public bool hasStarted;

    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        beatTempo = (beatTempo / 60f);
        transform.position += new Vector3(0f, beatOffset, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            
        }
        else
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
            
        }
    }
}
