using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void LoadMainScene()
    {
        SceneManager.LoadScene("main");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
