using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.Rendering.VirtualTexturing;

public class Spawner : MonoBehaviour
{
    public ReadSongFile songFile;
    public Conductor conductor;
    [Tooltip("Should be same as the distance ")]
    public float noteStartTimeOffset;
    
    float lastbeat; //this is the ‘moving reference point’

    private float bpm; 
    private float crotchet;
    private List<String> notes;
    private int noteIndex;

    public Vector3[] spawnPositions;
    public GameObject[] notePrefabs;

    public void StartSpawning()
    {
        lastbeat = 0;
        bpm = songFile.BPM;
        crotchet = 60 / bpm;
        notes = songFile.Notes.ToList();
        noteIndex = 0;
    }

    void Update()
    {
        if (conductor.songPosition > lastbeat + crotchet)
        {
            lastbeat += crotchet;
            HandleNextNotes();
        }
    }

    public void HandleNextNotes()
    {
        string note = notes[noteIndex];
        if (noteIndex < (notes.Count-1))
        {
            noteIndex++;
        }
        for (int i = 0; i < note.Length; i++)
        {
            if (note[i].Equals('1'))
            {
                SpawnNote(i);
            }
        }
        
    }

    void SpawnNote(int spawnIndex)
    {
        Instantiate(notePrefabs[spawnIndex], spawnPositions[spawnIndex], notePrefabs[spawnIndex].transform.rotation);
    }
}
