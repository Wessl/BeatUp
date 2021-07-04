using System.Collections;
using System.Data.SqlTypes;
using UnityEngine;
public class NoteDisplayer : MonoBehaviour
{
    public float timePassed;
    public GameObject notePrefab;
    public float noteMoveSpeed;
    private float timePassedSinceLastSnap = 0;
    //private float bpm;

    public ReadStepmania stepmaniaFileReadInfo;
    private bool hasSpawnedNotes;
    // Start is called before the first frame update
    void Start()
    {
        hasSpawnedNotes = false;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (stepmaniaFileReadInfo.hasFinishedReading)
        {
            SpawnNotes();
        }
    }

    void SpawnNotes()
    {
        // disaster used to rest here
    }
}
