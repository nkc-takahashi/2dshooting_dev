using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateSort : MonoBehaviour {
    public GameObject statemanager;

    public string valuename;
    public Text valuetext,valueptext;
    public Image bar;
    public Image limitbar;
    public float maxvalue;
    public float value;
    public float sizedelta_width, sizedelta_height;//1メモリの横幅,メモリの縦幅
    public bool limiterbreak;
    public float hp, shiled, speed;
    int i;

    // Use this for initialization
    void Start () {
        valuename = gameObject.name;
	}
    void Update()
    {
        Bar();
    }

    void Bar()
    {
        if (limiterbreak)
        {
            bar.GetComponent<RectTransform>().sizeDelta = new Vector2(sizedelta_width/2 + (value * sizedelta_width*2), sizedelta_height);
        }
        else
        {
            bar.GetComponent<RectTransform>().sizeDelta = new Vector2(sizedelta_width    + (value * 10), sizedelta_height);
            if (maxvalue <= value)
            {
                valuetext.text = "+" + (value * 10).ToString()+"M";
            }
            else
            {
                //valuetext.text = "NOW HELTH";
                valuetext.text = "+"+ (value * 10).ToString();
            }
        }
    }

    void StateValue()
    {
    }


    public void OnClickPlus()
    {
        int i;
        if (statemanager.GetComponent<StateManager>().point > 0)
        {
            if (maxvalue > value)
            {
                value += 1; statemanager.GetComponent<StateManager>().point -= 1;
            }
            else
            {
                if (limiterbreak == true)
                {
                    value += 1; statemanager.GetComponent<StateManager>().point -= 1;
                }
            }
        }
    }
    public void OnClickMinus()
    {
        if (0 < value)
        {
            value -= 1; statemanager.GetComponent<StateManager>().point += 1;
        }
    }
}
