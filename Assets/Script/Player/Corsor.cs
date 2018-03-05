using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corsor : MonoBehaviour {
    public GameObject player,corsor0;
    public bool mouseswitch;
    public float dis;
    // Use this for initialization
    void Update () {
        MouseCorsorSwitch();
    }

    // Update is called once per frame
    void MouseCorsorSwitch()
    {
        if (player.GetComponent<Player>().mouseinputswitch)
        {
            Cursor.visible = true; corsor0.SetActive(true);
            dis =  -10+Vector3.Distance(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            if (dis <= 0.6) mouseswitch = true;
            else mouseswitch = false;   
        }
        else
        {
            Cursor.visible = false; corsor0.SetActive(false);
        }
        if (mouseswitch)
        {

        }
        else
        {

        }
    }
}
