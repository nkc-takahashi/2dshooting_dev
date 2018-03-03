using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ColorManager : MonoBehaviour {
    public Color[] Color;
    public int i;
    GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        camera = GameObject.Find("Main Camera");
        camera.GetComponent<Camera>().backgroundColor = Color[i];
        DontDestroyOnLoad(gameObject);
    }
}
