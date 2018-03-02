using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    public GameObject[] State;
    public float point;
    public Text pointtext;

    void Update()
    {
        pointtext.text = point.ToString();
    }
    

}
