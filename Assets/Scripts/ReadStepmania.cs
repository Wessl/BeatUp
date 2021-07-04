using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using SimpleFileBrowser;

public class ReadStepmania : MonoBehaviour
{
    private string destinationPath;

    private string fileContents;

    public bool debug;
    // Song properties
    private Dictionary<float, float> songBPMs =
        new Dictionary<float, float>();

    private string[] notes;
    private StringBuilder note_sb = new StringBuilder();
    public bool hasFinishedReading;
    
    // Start is called before the first frame update
    void Start()
    {
        // Set filters
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Stepmania files", ".sm"));
        // Default filter
        FileBrowser.SetDefaultFilter( ".sm" );
        // Initialize fileContents string
        fileContents = String.Empty;
    }
    
    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.FilesAndFolders, false, null, null, "Load Files and Folders", "Load" );

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log( FileBrowser.Success );

        if( FileBrowser.Success )
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for( int i = 0; i < FileBrowser.Result.Length; i++ )
                Debug.Log( FileBrowser.Result[i] );

            // Read the bytes of the first file via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            // byte[] bytes = FileBrowserHelpers.ReadBytesFromFile( FileBrowser.Result[0] );

            // Or, copy the first file to persistentDataPath
            destinationPath = Path.Combine( Application.persistentDataPath, FileBrowserHelpers.GetFilename( FileBrowser.Result[0] ) );
            FileBrowserHelpers.CopyFile( FileBrowser.Result[0], destinationPath );
            // Now ready to read the file contents inside the .sm file
            ReadFileContents();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ShowLoadDialogCoroutine());
        }
    }

    // Uses streamreader to read all contents of the .sm file into a string
    void ReadFileContents()
    {
        StreamReader reader = new StreamReader(destinationPath);
        fileContents = reader.ReadToEnd();
        // Now parse contents
        ParseFileContents();
        reader.Close();
    }

    void ParseFileContents()
    {
        // Probably inefficient, idk
        bool firstLine = true;
        string[] fileContentLines = fileContents.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
        for (int i = 0; i < fileContentLines.Length; i++)
        {
            string line = fileContentLines[i];
            if (line.Contains("#BPMS"))
            {
                while (!line.Contains(";"))
                {
                    
                    var bpmStartIndex = line.IndexOf('=');
                    var bpmChangeStartTimeIndex = 0;
                    // If this is the first time we are recording a bpm start time, use colon. All others are prefixed by a comma. 
                    if (firstLine)
                    {
                        bpmChangeStartTimeIndex = line.IndexOf(':');
                        firstLine = false;
                    }
                    else
                    {
                        bpmChangeStartTimeIndex = line.IndexOf(',');
                    }
                    
                    string bpmChangeStartTime = "";
                    string songBPM = "";
                    for (int j = bpmChangeStartTimeIndex + 1; j < bpmStartIndex; j++)
                    {
                        bpmChangeStartTime += line[j];
                    }
                    for (int j = bpmStartIndex + 1; j < line.Length; j++)
                    {
                        songBPM += line[j];
                    }
                    songBPMs.Add(float.Parse(bpmChangeStartTime, CultureInfo.InvariantCulture), float.Parse(songBPM, CultureInfo.InvariantCulture));
                    // go to the next line whilst recording bpm
                    i++;
                    line = fileContentLines[i];
                }
            } else if(line.Contains("#NOTES"))
            {
                while (line[0] !=';') 
                {
                    if (line[0] == ' ')
                    {
                        // Ignore, not started yet
                    }
                    else
                    {
                        note_sb.Append(line+'\n');
                    }
                    i++;
                    line = fileContentLines[i];
                }

            }
        }
        notes = note_sb.ToString().Split(new[] { "\n" }, StringSplitOptions.None);

        if (debug)
        {
            foreach(var pair in songBPMs)
            {
                //Debug.Log($"Time: {pair.Key}: BPM={pair.Value}");
            }

            foreach (var note in notes)
            {
                Debug.Log(note);
            }
        }

        hasFinishedReading = true;

    }

    // Getters
    public string[] Notes => notes;
    public Dictionary<float, float> SongBpMs => songBPMs;
}
