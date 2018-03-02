using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour {
    public GameObject player, playerctrl;
    Text STATEUI;
    private void Start()
    {
        STATEUI = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {
        player = GameObject.Find("Player");
        playerctrl = GameObject.Find("PlayerController");

        STATEUI.text =
            "PLAYER CTRL" + "\n" +
            "Speed :" +
            "x" + playerctrl.GetComponent<PlayerControl>().speedx.ToString() +
            "Y" + playerctrl.GetComponent<PlayerControl>().speedy.ToString() + "\n" +
            "PLayerHP :" +
            playerctrl.GetComponent<PlayerControl>().playerhp.ToString() + "\n" +
            "shieldHP :" +
            playerctrl.GetComponent<PlayerControl>().shieldhp.ToString() + "\n" +
            "BulletVolume :" +
            playerctrl.GetComponent<PlayerControl>().bulletvolume.ToString() + "\n" +
            "Bulletinterval :" +
            playerctrl.GetComponent<PlayerControl>().bulletinterval.ToString() + "\n" +
            "Trajectoryvalue :" +
            playerctrl.GetComponent<PlayerControl>().trajectoryvalue.ToString() + "\n" +
            "rotationspeed :" +
            playerctrl.GetComponent<PlayerControl>().rotationspeed.ToString() + "\n" + "\n" +

            "PLAYER" + "\n" +
            "x" + playerctrl.GetComponent<PlayerControl>().speedx.ToString() +
            "Y" + playerctrl.GetComponent<PlayerControl>().speedy.ToString() + "\n" +
            "PLayerHP :" +
            player.GetComponent<Player>().playerhp.ToString() + "\n" +
            "shieldHP :" +
            player.GetComponent<Player>().shieldhp.ToString() + "\n" +
            "BulletVolume :" +
            player.GetComponent<Player>().bulletvolume.ToString() + "\n" +
            "Bulletinterval :" +
            player.GetComponent<Player>().bulletinterval.ToString() + "\n" +
            "Trajectoryvalue :" +
            player.GetComponent<Player>().trajectoryvalue.ToString() + "\n" +
            "rotationspeed :" +
            player.GetComponent<Player>().rotationspeed.ToString() + "\n" + "\n" + "Debugmode";
    }
}
