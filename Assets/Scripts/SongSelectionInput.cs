using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectionInput : MonoBehaviour
{
    public KeyCode[] navigateUp;
    public KeyCode[] navigateDown;
    public KeyCode[] navigateLeft;
    public KeyCode[] navigateRight;

    public GameObject selectionOutline;
    public GameObject[] columnOptions;
    public List<GameObject> songs;
    private int columnIndex;
    private int songIndex;

    private bool isBrowsingSongs;
    public ReadAllSongs songsRead;
    public GameObject songPanelParent;
    public GameObject canvas;
    public GameObject currentSelection;



    // Start is called before the first frame update
    void Start()
    {
        selectionOutline.transform.position = columnOptions[0].transform.position;
        isBrowsingSongs = false;
        songs = songsRead.SongGUIObjects;
        songIndex = 0;
        currentSelection = GameObject.FindWithTag("SongsPanel");
        Debug.Log(songs.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBrowsingSongs)
        {
            if (navigateLeft.Any(Input.GetKeyDown))
            {
                isBrowsingSongs = false;
                var selectionOutlineTransform = selectionOutline.GetComponent<RectTransform>();
                var menuTransform = columnOptions[columnIndex].GetComponent<RectTransform>();
                selectionOutline.transform.SetParent(canvas.transform);
                selectionOutlineTransform.anchorMin = menuTransform.anchorMin;
                selectionOutlineTransform.anchorMax = menuTransform.anchorMax;
                selectionOutlineTransform.anchoredPosition = menuTransform.anchoredPosition;
                selectionOutlineTransform.sizeDelta = menuTransform.sizeDelta;
            }

            if (navigateDown.Any(Input.GetKeyDown))
            {
                // Try to move selection down
                if (songIndex > 0)
                {
                    songIndex--;
                    songPanelParent.transform.position += new Vector3(0, 100, 0);
                    selectionOutline.transform.position = songs[songIndex].transform.position;
                }
            }
            if (navigateUp.Any(Input.GetKeyDown))
            {
                // Try to move selection down
                if (songIndex < songs.Count - 1)
                {
                    songIndex++;
                    songPanelParent.transform.position += new Vector3(0, -100, 0);
                    selectionOutline.transform.position = songs[songIndex].transform.position;
                }
            }

            if (navigateRight.Any(Input.GetKeyDown))
            {
                // Load gameplayscene with appropriate song
                string songString = songs[songIndex].GetComponentInChildren<TextMeshProUGUI>().text;
                Debug.Log("LOADING " + songString);
                LoadSong(songString);
            }
        }
        else
        {
            if (navigateDown.Any(Input.GetKeyDown))
            {
                // Try to move selection down
                if (columnIndex < columnOptions.Length - 1)
                {
                    columnIndex++;
                    selectionOutline.transform.position = columnOptions[columnIndex].transform.position;
                    currentSelection = columnOptions[columnIndex];
                }
            }

            if (navigateUp.Any(Input.GetKeyDown))
            {
                // Try to move selection up
                if (columnIndex > 0)
                {
                    columnIndex--;
                    selectionOutline.transform.position = columnOptions[columnIndex].transform.position;
                    currentSelection = columnOptions[columnIndex];
                }
            }

            if (navigateRight.Any(Input.GetKeyDown) )
            {
                if (currentSelection.CompareTag("SongsPanel"))
                {
                    isBrowsingSongs = true;
                    var selectionOutlineTransform = selectionOutline.GetComponent<RectTransform>();
                    Debug.Log(songs[0]);
                    var songTransform = songs[songIndex].GetComponent<RectTransform>();
                    selectionOutline.transform.SetParent(songPanelParent.transform);
                    selectionOutlineTransform.anchorMin = songTransform.anchorMin;
                    selectionOutlineTransform.anchorMax = songTransform.anchorMax;
                    selectionOutlineTransform.anchoredPosition = songTransform.anchoredPosition;
                    selectionOutlineTransform.sizeDelta = songTransform.sizeDelta;
                }
                else if (currentSelection.CompareTag("SettingsPanel"))
                {
                    isBrowsingSongs = false;
                }
                
            }
        }
        
    }

    void LoadSong(string song)
    {
        PlayerPrefs.SetString("ChosenSong", song);
        SceneManager.LoadScene(1);
    }
}
