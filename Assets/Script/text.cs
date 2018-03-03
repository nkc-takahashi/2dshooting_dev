using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class text : MonoBehaviour {
    public Color textcolor;
    public bool Alphaswitch;
    public float outtime;
    public float intime;
    float fadetime;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        plzanykey();
    }
    void plzanykey()
    {
        if (Alphaswitch)
        {
            gameObject.GetComponent<Text>().color = new Color(1, 1, 1, Mathf.Lerp(0, 1, fadetime /intime));
        }
        else
        {
            gameObject.GetComponent<Text>().color = new Color(1, 1, 1, Mathf.Lerp(1,0, fadetime/outtime));
        }
        if (gameObject.GetComponent<Text>().color.a == 1)
        {
            Alphaswitch = false;fadetime = 0;
        }
        else if(gameObject.GetComponent<Text>().color.a==0)
        {
            Alphaswitch = true;fadetime = 0;
        }
        fadetime += Time.deltaTime;
    }
}
