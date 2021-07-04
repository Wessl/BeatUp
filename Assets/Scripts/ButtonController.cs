using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite defaultImage;
    public Sprite pressedImage;
    private Samurai _samurai;
    public KeyCode keyToPress;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        _samurai = GameObject.FindWithTag("Samurai").GetComponent<Samurai>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            sr.sprite = pressedImage;
            PlayAnim();
        }

        if (Input.GetKeyUp(keyToPress))
        {
            sr.sprite = defaultImage;
        }
    }

    void PlayAnim()
    {
        switch (keyToPress)
        {
            case KeyCode.LeftArrow:
                _samurai.StrikeLeft();
                break;
            case KeyCode.RightArrow:
                _samurai.StrikeRight();
                break;
            case KeyCode.UpArrow:
                _samurai.StrikeUp();
                break;
            case KeyCode.DownArrow:
                _samurai.StrikeDown();
                break;
            default:
                Debug.Log("I don't recognize this keycode! ");
                break;     
        }
    }
}
