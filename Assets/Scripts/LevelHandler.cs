using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public bool secColorsActive = false;

    public string[] levelOneColors = { "blue", "red", "yellow"};
    public string[] levelTwoColors = { "blue", "red", "yellow", "purple", "orange", "green"};

    //Primary Colors
    public Color blue = new Color(0.3101f, 0.2572f, 0.9245f);
    public Color red = new Color(0.9254f, 0.2780f, 0.2588f);
    public Color yellow = new Color(0.9254f, 0.9142f, 0.2588f);

    //Secondary Colors
    public Color purple = new Color(0.6865f, 0.1448f, 0.8301f);
    public Color orange = new Color(1.0f, 0.5026f, 0.0f);
    public Color green = new Color(0.2259f, 0.7169f, 0.2063f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
