using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Conductor conductor;
    public Spawner spawner;
    public void ClickPlay()
    {
        conductor.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        spawner.StartSpawning();
    }
}
