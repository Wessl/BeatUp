using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ReadAllSongs : MonoBehaviour
{
    private string destinationPath;
    private string fileContents;

    public bool debug;
    // Song properties
    private float songBpm;
    private string songTitle;
    private float offset;

    private string[] notes;
    public bool hasFinishedReading;
    public string songFilePath;
    public GameObject songPanelParent;
    public GameObject songPanelPrefab;

    public GameObject songSelectionInputObj;
    private List<GameObject> songGUIObjects;


    // Start is called before the first frame update
    void Start()
    {
        // Go into the file folder where songs are kept. 
        songGUIObjects = new List<GameObject>();
        FindMusicFoldersAndPopulateGUIList();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindMusicFoldersAndPopulateGUIList()
    {
        
        var songFiles = Directory.EnumerateDirectories(songFilePath);
        int index = 0;
        foreach (string currentFile in songFiles)
        {
            string fileName = currentFile.Substring(songFilePath.Length);
            var pos = songPanelParent.transform.position;
            var songGUIPicker = Instantiate(songPanelPrefab, pos, Quaternion.identity);
            songGUIPicker.transform.SetParent(songPanelParent.transform);
            var rectTransform = songGUIPicker.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2 (150, -50);
            rectTransform.anchoredPosition = new Vector2(-20, index*60);
            songGUIPicker.GetComponentInChildren<TextMeshProUGUI>().text = fileName;
            index++;
            
            // Put into list of all songs
            songGUIObjects.Add(songGUIPicker);
        }
        // Once this is done, enable user input
        songSelectionInputObj.SetActive(true);
        
    }

    public List<GameObject> SongGUIObjects => songGUIObjects;
}

