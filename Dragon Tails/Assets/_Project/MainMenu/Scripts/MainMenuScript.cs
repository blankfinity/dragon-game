using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

    public LevelLoaderScript loader;

    GameObject OptionsPanel;

    // Use this for initialization
    void Start() {
        if(loader == null)
            loader = new LevelLoaderScript();
    }

    // Update is called once per frame
    void Update() {

    }

    public void Play(){
        loader.LoadLevel("demo");
    }

    public void Options()
    {

    }

    public void Exit(){
        Application.Quit();
    }
}
