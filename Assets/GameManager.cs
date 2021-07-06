using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ReadSongFile songFile;
    public float percentageHit;
    private int totalNotes;
    private float notesHit;
    
    public float perfectThreshold;
    public float goodThreshold;
    public float hitThreshold;

    public TextMeshProUGUI percentHitText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        percentageHit = (float) notesHit / totalNotes;
        percentHitText.text = percentageHit * 100 + "%";
        Debug.Log(percentageHit + "%");
    }

    void SongActive()
    {
        totalNotes = songFile.Notes.Length;
    }

    public void HitNote(float hitDist)
    {
        if (hitDist <= perfectThreshold)
        {
            notesHit += 1;
        } else if (hitDist <= goodThreshold)
        {
            notesHit += (2f / 3f);
        } else if (hitDist <= hitThreshold)
        {
            notesHit += (1f / 3f);
        }

        totalNotes++;
    }

    public float NotesHit
    {
        get => notesHit;
        set => notesHit = value;
    }
}
