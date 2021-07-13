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
    private AudioSource audioSource;
    public bool playHitSounds = true;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        percentageHit = (float) notesHit / totalNotes;
        // Sets the percentHit text to a percentage-formatted string of the most recent percentage of hit notes
        percentHitText.text = percentageHit.ToString("p");
    }

    void SongActive()
    {
        totalNotes = songFile.Notes.Length;
    }

    public void HitNote(float hitDist)
    {
        
        Debug.Log(hitDist);
        if (hitDist <= perfectThreshold)
        {
            notesHit += 1;
            HitSound();
        } else if (hitDist <= goodThreshold)
        {
            notesHit += (2f / 3f);
            HitSound();
        } else if (hitDist <= hitThreshold)
        {
            notesHit += (1f / 3f);
            HitSound();
        }
        
        totalNotes++;
    }

    private void HitSound()
    {
        if (playHitSounds)
        {
            audioSource.Play();
        }
    }

    public float NotesHit
    {
        get => notesHit;
        set => notesHit = value;
    }
}
