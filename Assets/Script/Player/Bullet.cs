using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {
    public enum BulletType
    {
        TypeA,          //ますぐ
        TypeB,           //追尾弾
        TypeC
    }
    public BulletType bullettype;
    public float bulletspeed, homingrotatespeed, homingtiming, destroytiming , distanceangle, distance,angle;
    public GameObject player, enemy;
    public GameObject[] enemybox;
    public Vector2 targetenemypoint, targetenemy, enemypos,bulletpos, bulleteye;

    //TypeB
    public float hoomingbullet, enemyupdateinterval; float updateinterval;




    ////public bool mouserotateswitch;
    public float DestroyTiming,valuez;
    Rigidbody2D rd;

    public bool rockon;



    // Use this for initialization
    void Start()
    {
        rd = GetComponent<Rigidbody2D>(); rd.velocity = (transform.up * bulletspeed);
        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");
    }


    // Update is called once per frame
    void Update()
    {
        bullettypeset();
    }


    void bullettypeset()
    {
        switch (bullettype)
        {
            case BulletType.TypeA:
                BulletTypeA();
                break;
            case BulletType.TypeB:
                BulletTypeB();
                break;
            case BulletType.TypeC:
                BulletTypeC();
                break;
        }
    }


    void BulletTypeA()
    {
        rd.velocity = (transform.up * bulletspeed);
        ByeBye();
    }


    void BulletTypeB()
    {
        enemypos = enemy.transform.position; bulletpos = this.transform.position; bulleteye = this.transform.up;
        if (Vector3.Distance(enemypos, bulletpos) <= distance && Vector3.Angle((enemypos - bulletpos).normalized, bulleteye) <= angle) rockon = true;
        else rockon = false;
        if (rockon)
        {
            targetenemy = enemy.transform.position;
            Vector2 targetenemypoint = (Vector2)transform.position - targetenemy;
            targetenemypoint.Normalize();
            valuez = Vector3.Cross(targetenemypoint, transform.up).z;
            if (valuez > 0) rd.angularVelocity = homingrotatespeed;
            else if (valuez < 0) rd.angularVelocity = -homingrotatespeed;
            else homingrotatespeed = 0;
            rd.angularVelocity = homingrotatespeed * valuez;
            rd.velocity = transform.up * bulletspeed;
        }
        else
        {
            rd.angularVelocity = 0;
            rd.velocity = (transform.up * bulletspeed);
        }
    }
    void BulletTypeC()
    {
        if (enemy == null)
        {
            ByeBye();
        }
        else
        {
            enemypos = enemy.transform.position;
            if (homingtiming <= 0)
            {
                updateinterval -= Time.deltaTime;
                destroytiming -= Time.deltaTime;
                Vector2 targetenemypoint = (Vector2)transform.position - targetenemy;
                targetenemypoint.Normalize();
                valuez = Vector3.Cross(targetenemypoint, transform.up).z;
                if (valuez > 0) rd.angularVelocity = homingrotatespeed;
                else if (valuez < 0) rd.angularVelocity = -homingrotatespeed;
                else homingrotatespeed = 0;
                rd.angularVelocity = homingrotatespeed * valuez;
                rd.velocity = transform.up * bulletspeed;
                if (updateinterval < 0)
                {
                    targetenemy = enemy.transform.position;
                    updateinterval = enemyupdateinterval;
                }
            }
            else
            {
                homingtiming -= Time.deltaTime;
                rd.velocity = (transform.up * bulletspeed);
            }
            if (destroytiming < 0) Destroy(gameObject);
        }
        ByeBye();
    }
    void BulletTypeD()
    {
        enemypos = enemy.transform.position; bulletpos = this.transform.position; bulleteye = this.transform.forward;
        if (Vector3.Distance(enemypos, bulletpos) <= distance && Vector3.Angle((enemypos - bulletpos).normalized, bulleteye) <= angle)
        {
        }
        else
        {
            rd.velocity = (transform.up * bulletspeed);
        }
    }


    void ByeBye()
    {
        DestroyTiming -= Time.deltaTime;
        if (DestroyTiming <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"|| collision.gameObject.tag == "BulletTypeEnemy")
        {
            Destroy(gameObject);
        }
    }
}
