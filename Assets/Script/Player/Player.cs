using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [HideInInspector]
    string modechange;


    public GameObject[] Bullettype;

    public float playerhp,shieldhp;

    [System.NonSerialized] public bool mouseinputswitch, noreloadswitch, shieldswitch, invisibleswitch, recoveryswitch;
    //private
    [System.NonSerialized]
    public float
    speedx, speedy,         //移動スピード
    bulletvolume,           //弾数初期値
    bulletinterval,         /*発砲する間隔*/ interval,
    trajectoryvalue,        //弾道のブレ幅
    remaining,              //変動する弾数
    rotationspeed,          //マウスの追うスピード
    reloadinterval,         //
    recoverhpinterval = 5,      /* */ recoverinterval;


    GameObject playerctrl, bullet, muzzle,shieldobj;
    public GameObject shield;
    public GameObject Maxpos, Minpos;

    public Vector2 maxpos, minpos;
    Quaternion bulletRough;Rigidbody2D rd;
    [System.NonSerialized] public float remainingbulletvalue;
    [System.NonSerialized] public Vector2 Position;
    [System.NonSerialized] public string bullettype;

    void Start()
    {
        playerctrl = GameObject.Find("PlayerController");
        interval = bulletinterval;
        bulletinterval = playerctrl.GetComponent<PlayerControl>().bulletinterval;
        Playerctrlset();
        remainingbulletvalue = bulletvolume;
        muzzle = transform.Find("muzzle").gameObject;
        modechange = playerctrl.GetComponent<PlayerControl>().modechange.ToString(); bullettype = playerctrl.GetComponent<PlayerControl>().bullettype.ToString();
    }

    void Playerctrlset()
    {
        playerhp = playerctrl.GetComponent<PlayerControl>().playerhp;
        speedx = playerctrl.GetComponent<PlayerControl>().speedx; speedy = playerctrl.GetComponent<PlayerControl>().speedy;
        bulletinterval = playerctrl.GetComponent<PlayerControl>().bulletinterval;
        bulletvolume = playerctrl.GetComponent<PlayerControl>().bulletvolume;
        rotationspeed = playerctrl.GetComponent<PlayerControl>().rotationspeed;
        trajectoryvalue = playerctrl.GetComponent<PlayerControl>().trajectoryvalue;
    }

    void Update()
    {
        ModeChange(); Actionrange();
    }

    void ModeChange()
    {
        switch (modechange)
        {
            case "Normal":
                BulletSetType(); Move();Rotate();Bullet();Shield();  Actionrange();
                invisibleswitch = false;noreloadswitch = false;
                break;
            case "Invisible":
                BulletSetType(); Move(); Rotate(); Bullet(); Shield();  Actionrange();
                invisibleswitch = true;noreloadswitch = true;
                break;
            //case "Debugmode":
            //    BulletSetType(); Move(); Rotate(); Bullet(); shield();  Playerctrlset(); Actionrange();
            //    invisibleswitch = false;recoveryswitch = true; recovery();
            //    break;
        }
    }


    // --- 移動制御 ---
    void Move()
    {
        Vector2 SPEED = new Vector2(speedx, speedy);
        Position = transform.position;  //現在位置
        if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
        {
            Position.x -= SPEED.x;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
        {
            Position.x += SPEED.x;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
        {
            Position.y += SPEED.y;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
        {
            Position.y -= SPEED.y;
        }
        transform.position = Position;
        Clamp();
    }
    void Clamp()
    {
        Position.x = Mathf.Clamp(Position.x, minpos.x, maxpos.x);
        Position.y = Mathf.Clamp(Position.y, minpos.y, maxpos.y);
        transform.position = new Vector2(Position.x, Position.y);
    }


    // --- マウス方向を向く --- 
    void Rotate()
    {
        if (mouseinputswitch == true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                mouseinputswitch = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                mouseinputswitch = true;
            }
        }
        if (mouseinputswitch)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.localPosition);
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Input.mousePosition - pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationspeed);
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationspeed);
        }
    }


    // --- 発砲 ---
    void Bullet()
    {
        interval -= Time.deltaTime;
        bulletRough = transform.rotation;
        bulletRough.z += Random.Range(-trajectoryvalue / 100, trajectoryvalue / 100);
        if (remainingbulletvalue > 0)
        {
            if (interval <= 0 && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
            {
                interval = bulletinterval;
                remainingbulletvalue -= 1;
                Instantiate(bullet, muzzle.transform.position, bulletRough);
                Debug.Log("発砲");
            }
            else
            {
                interval = 0;
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine("reload");
        }
    }
    IEnumerator reload()
    {
        yield return new WaitForSeconds(reloadinterval);
        remainingbulletvalue = bulletvolume;
        yield return null;
    }


    // --- 自動ヒール --- 
    void recovery()
    {
        if(recoverinterval < 0 && playerctrl.GetComponent<PlayerControl>().playerhp != playerhp)
        {
            recoverinterval = 6;
        }
       if(recoverinterval == 6 && playerctrl.GetComponent<PlayerControl>().playerhp != playerhp)
        {
            playerhp += 1;
        }
        else if(recoverinterval <= 5)
        {
            recoverinterval -=Time.deltaTime;
        }
    }

    // --- シールド ---
    void Shield()
    {
        if (Input.GetKey(KeyCode.Q) && shieldswitch == false)
        {
            shieldswitch = true;
            Instantiate(shield, transform.position, bulletRough, transform);
            shieldobj = transform.Find("shield(Clone)").gameObject;
        }
        if (shieldobj != null)
        {
            shieldhp = transform.Find("shield(Clone)").gameObject.GetComponent<Shield>().shieldhp;
        }
    }


    // --- バレットタイプセット ---
    void BulletSetType()
    {
        switch (bullettype)
        {
            case "BulletA":
                bullet = Bullettype[0];
                break;

            case "BulletB":
                bullet = Bullettype[1];
                break;

            case "BulletC":
                bullet = Bullettype[2];
                break;

            case "BulletD":

                break;
        }
    }

    void Actionrange()
    {
        Maxpos = GameObject.Find("maxpos");
        Minpos = GameObject.Find("minpos");
        if (Maxpos != null && Minpos != null)
        {
            maxpos = Maxpos.GetComponent<Transform>().position;
            minpos = Minpos.GetComponent<Transform>().position;
        }
    }

    // --- 衝突判定 ---
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BulletTypeEnemy" && shieldswitch == false && invisibleswitch == false)
        {
            playerhp -= 1;
            recoverinterval = 5;
            if (playerhp < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
