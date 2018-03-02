using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    public float shieldhp;
    [ColorHtmlProperty] public Color changecolor;

    //private
    float changeAlphaa;
    GameObject player;

    void Start()
    {
        changeAlphaa = changecolor.a / shieldhp - 1;
        player = transform.root.gameObject;
    }

    // Update is called once per frame
    void Update () {
        transform.GetComponent<SpriteRenderer>().color = changecolor;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletTypeEnemy")
        {
            shieldhp -= 1;
            changeAlphaa = changecolor.a * shieldhp;
            if (shieldhp <= 0)
            {
                player.GetComponent<Player>().shieldswitch = false;
                Debug.Log("ぶぼぼぼぉ...");
                Destroy(gameObject);
            }
        }
    }
}
