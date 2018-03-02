using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public GameObject player,enemy;
    public GameObject[] enemys;
    public enum UIType
    {
        TypeA, TypeB
    }
    public UIType uitype;
    public Image bulletright,bulletleft;
    [ColorHtmlProperty]
    public Color ManyColor;
    [ColorHtmlProperty]
    public Color lowColor;
    public Text bulletAtxt, bulletBtxt;
    public Text weapon, enemyHP,playerHP,shiledHP;
    public Text playerstate;

    public GameObject pouseui;

    //private
    float bulletvolume;
    float bullettime,Weapontime;   //playerがdie状態の時に使用
    string[] keys = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "-", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"," ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };

    void Start()
    {
        
    }

    void Update ()
    {
        setType(); player = GameObject.Find("Player"); Pouse();
    }
    void setType()
    {
        player = GameObject.Find("Player");
        switch (uitype)
        {
            case UIType.TypeA:
                BulletUI(); EnemyHP(); Weapon(); PlayerHP();
                break;
            case UIType.TypeB:
                break;
        }
    }


    //----------UIType.TypeA----------//
    void BulletUI()
    {
        if (player == null) {
            bullettime -= Time.deltaTime;
            if (bullettime <= 0)
            {
                bullettime = 0.1f;
                float f = Random.Range(10, 101);
                bulletAtxt.text = f.ToString();
            }
        }
        else
        {
            if (player.GetComponent<Player>().noreloadswitch == true)
            {

            }
            else
            {
                bulletvolume = player.GetComponent<Player>().remainingbulletvalue;
                if (bulletvolume >= 50)
                {
                    bulletright.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 25); bulletleft.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 25);
                }
                else if(bulletvolume < 0)
                {
                    bulletright.GetComponent<RectTransform>().sizeDelta = new Vector2(bulletvolume * 0, 25); bulletleft.GetComponent<RectTransform>().sizeDelta = new Vector2(bulletvolume * 0, 25);
                }
                else
                {
                    bulletright.GetComponent<RectTransform>().sizeDelta = new Vector2(bulletvolume * 10, 25); bulletleft.GetComponent<RectTransform>().sizeDelta = new Vector2(bulletvolume * 10, 25);
                }

                if (player.GetComponent<Player>().invisibleswitch == true)
                {
                    bulletAtxt.GetComponent<Text>().text = "Unlimited"; bulletBtxt.GetComponent<Text>().text = "Unlimited";
                }
                else
                {
                    bulletAtxt.GetComponent<Text>().text = bulletvolume.ToString(); bulletBtxt.GetComponent<Text>().text = bulletvolume.ToString();
                }

                if (bulletvolume <= 20)
                {
                    bulletright.GetComponent<Image>().color = lowColor; bulletleft.GetComponent<Image>().color = lowColor;
                }
                else
                {
                    bulletright.GetComponent<Image>().color = ManyColor;bulletleft.GetComponent<Image>().color = ManyColor;
                }
            }
        }
    }
    void Weapon()
    {if (player == null)
        {
            Weapontime -= Time.deltaTime;
            if(Weapontime <= 0)
            {
                Weapontime = 0.1f;
                int i = keys.Length;
                weapon.text = keys[Random.Range(0, i)] + keys[Random.Range(0, i)] + keys[Random.Range(0, i)] + keys[Random.Range(0, i)] + keys[Random.Range(0, i)] + keys[Random.Range(0, i)] + keys[Random.Range(0, i)];
            }
        }
        else
        {
            //weapon.text = player.GetComponent<PlayerControl>().bullet.name;
        }
    }
    void EnemyHP()
    {
        enemyHP.text = enemy.GetComponent<EnemyMove>().HP.ToString();
    }
    void PlayerHP()
    {
        if (player.GetComponent<Player>().invisibleswitch == true)
        {
            playerHP.text = "Invisible";
        }
        else
        {
            playerHP.text = player.GetComponent<Player>().playerhp.ToString();
        }
    }

    void Dietiming()
    {

    }

    void Pouse()
    {
        if(Time.timeScale != 1)
        {
            pouseui.SetActive(true);
        }
        else
        {
            pouseui.SetActive(false);
        }
        if (Input.GetKeyDown("o"))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale -=0.1f;
            }
        }
        if (Input.GetKeyDown("p"))
        {
            if (Time.timeScale <= 1)
            {
                Time.timeScale += 0.1f;
            }
        }
    }

    void EnemyUI()
    {
        //gameObject[]enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }
}
