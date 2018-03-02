
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {

    //public
    public GameObject Playerprefab;
    public string playerSpawnscene;

    public float
        speedx, speedy,         //移動スピード
        bulletvolume,           //弾数初期値
        bulletinterval,         //発砲する間隔
        trajectoryvalue,        //弾道のブレ幅
        rotationspeed,          //マウスの追うスピード
        reroadinterval,         //リロードに掛かる時間
        playerhp,               //playerのHP
        shieldhp;               //shieldのHP

    public Vector3 playerpos;
    public enum PlayerMode  
    {
        Normal, Invisible, Debugmode, Default
    }
    public PlayerMode modechange;
    public bool mouseinputswitch, reroadswitch, shieldswitch, invisibleswitch;

    public enum BulletTypectrl
    {
        BulletA, BulletB, BulletC, BulletD
    }
    public BulletTypectrl bullettype;
    public GameObject hpsort, shieldhpsort, speedsort;
    public GameObject[] Bullettype;
    public GameObject player, spowner; public GameObject Maxpos,Minpos;

    int i;

    void Start () {
        spowner = GameObject.Find("PlayerSpawnPos");
    }


    void Update()
    {
        ModeChange(); PlayerDieIsIt();
    }
    void ModeChange()
    {
        switch (modechange)
        {
            case PlayerMode.Normal:
                invisibleswitch = false;
                break;
            case PlayerMode.Invisible:
                invisibleswitch = true;
                reroadswitch = true;
                break;
            case PlayerMode.Debugmode:
                invisibleswitch = false;
                break;
            case PlayerMode.Default:
                break;
        }
    }
    void PlayerDieIsIt()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            PlayerSpawn();
        }
        else
        {
            playerpos = player.transform.position;
        }
    }

    void PlayerSpawn()
    {
        if (SceneManager.GetActiveScene().name == playerSpawnscene)
        {
            GameObject obj = Instantiate(Playerprefab) as GameObject;
            obj.name = Playerprefab.name;
        }
    }

    public void Statesort()
    {
        playerhp += hpsort.GetComponent<StateSort>().value;
        shieldhp += shieldhpsort.GetComponent<StateSort>().value;
        speedx += speedsort.GetComponent<StateSort>().value;
        speedy += speedsort.GetComponent<StateSort>().value;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("2DShooting");
    }
}
