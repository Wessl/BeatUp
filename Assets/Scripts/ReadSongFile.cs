using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using SimpleFileBrowser;

public class ReadSongFile : MonoBehaviour
{
    private string destinationPath;
    private string fileContents;

    public bool debug;
    // Song properties
    private float songBpm;
    private string songTitle;
    private float offset;

    private string[] notes;
    private StringBuilder note_sb = new StringBuilder();
    public bool hasFinishedReading;
    [SerializeField] Conductor _conductor;
    [SerializeField] Spawner _spawner;
    public string[] musicFileExtensionTypes;
    // song is set in ConvertFilesToAudioClip()
    private AudioClip song; 
    
    [Tooltip("This path needs to be the same as ReadAllSongs' songFilePath in song select scene")]
    public string songFileParentPath = "Assets/Songs";
    
    [Tooltip("Should be a consistent .txt for all song data files, example: \"song.txt\" ")]
    public string songStepFileDataName = "/song.txt";

    

    // Start is called before the first frame update
    void Start()
    {
        destinationPath = (songFileParentPath + PlayerPrefs.GetString("ChosenSong"));
        
        // Initialize fileContents string
        fileContents = String.Empty;
        
        ReadFileContents();
    }

    // Uses streamreader to read all contents of the .sm file into a string
    void ReadFileContents()
    {
        // Read the contents of the file containing the stepfile metadata (dest path of parent folder + .txt file)
        StreamReader reader = new StreamReader(destinationPath + songStepFileDataName);
        fileContents = reader.ReadToEnd();
        // Now parse contents
        ParseFileContents();
        reader.Close();
    }

    // Scuffed method for reading my scuffed custom song content file format
    void ParseFileContents()
    {
        // Probably inefficient, idk
        bool firstLine = true;
        string[] fileContentLines = fileContents.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        for (int i = 0; i < fileContentLines.Length; i++)
        {
            var line = fileContentLines[i];
            if (line.Contains("#TITLE"))
            {
                var startIndex = line.IndexOf(':'); 
                // Get title and put into songTitle string. We want inbetween : and ; thus the +1 and -1.
                for (int j = startIndex + 1; j < line.Length - 1; j++)
                {
                    songTitle += line[j];
                }

                var results = AssetDatabase.FindAssets(songTitle);
                foreach (string result in results)
                {
                    var resultFileString = AssetDatabase.GUIDToAssetPath(result);
                    
                    // Check if any of the allowed file extensions are contained within 
                    string fileExtension = musicFileExtensionTypes.FirstOrDefault(resultFileString.Contains);
                    if(musicFileExtensionTypes.Contains(fileExtension))
                    {
                        String resultWithExtension = resultFileString;
                        Debug.Log("result and extension: " + resultWithExtension);
                        StartCoroutine(ConvertFilesToAudioClip(resultFileString));
                    }
                }

            } else if (line.Contains("#BPM"))
            {
                // Get the bpm of the file and put into bpm float variable
                var startIndex = line.IndexOf(':');
                var bpmString = "";
                for (int j = startIndex + 1; j < line.Length - 1; j++)
                {
                    bpmString += line[j];
                }
                songBpm = float.Parse(bpmString);
            } else if (line.Contains("#OFFSET"))
            {
                // Get the offset of the file and put into bpm float variable
                var startIndex = line.IndexOf(':');
                var offsetString = "";
                for (int j = startIndex + 1; j < line.Length - 1; j++)
                {
                    offsetString += line[j];
                }
                offset = float.Parse(offsetString, CultureInfo.InvariantCulture.NumberFormat);
            }
            else if (line.Contains("NOTES"))
            {
                // Start reading notes, all else is unnecessary
                i++;
                line = fileContentLines[i];
                while (line[0] != ';')
                {
                    note_sb.Append(line+' ');
                    i++;
                    line = fileContentLines[i];
                }
            }
        }
        notes = note_sb.ToString().Split(new[] { " " }, StringSplitOptions.None);

        if (debug)
        {
            Debug.Log("Title of the song: " + songTitle);
            Debug.Log("Song bpm: " + songBpm);
            Debug.Log("Offset: " + offset);
            foreach (var note in notes)
            {
                Debug.Log(note);
            }
            Debug.Log("Song has been read, it's time to begin.");
        }
        // Shit has been read, it's time
        _conductor.gameObject.SetActive(true);
        _spawner.gameObject.SetActive(true);
        _spawner.StartSpawning();

    }
    
    // For some reason you have to do this in order for Unity to understand that an audio file is actually audio
    private IEnumerator ConvertFilesToAudioClip(string songFile)
    {
        if (debug) {Debug.Log("Attempting to convert file:" + songFile + ", to audio clip");}
        if (songFile.Contains("meta"))
        {
            // Don't even try to mess with .meta files, they're useless
            yield break;
        }
        else
        {
            string url = string.Format("file:///{0}", songFile);
            WWW www = new WWW(url);
            yield return www;
            song = (www.GetAudioClip(false,false));
            if (debug) {Debug.Log("Song was successfully read from WWW");}
        }
        hasFinishedReading = true;
    }

    // Getters
    public string[] Notes => notes;
    public float BPM => songBpm;
    public string Title => songTitle;
    public float Offset => offset;
    public AudioClip Song => song;
}

