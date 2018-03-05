using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour {
    public GameObject player, target, motherbase,BulletTypeEnemy, muzzle,Bullet_Box;
    public enum MoveType
    {
        TypeA, TypeB, TypeC, TypeD
    }
    public MoveType movetype;

    public GameObject[] pos;
    //[HideInInspector]
    public float MoveSpeed,HP;
    public ParticleSystem deathEffect;
    [ColorHtmlProperty] public Color DamageColor;

    Vector3 MovingPosition, playerpos;
    public float rotationspeed, bulletInterval;


    public Vector3 v3;
    //private
    float interval;
    int i;
    public int bullet;

    // Use this for initialization
    void Start () {
        muzzle = transform.Find("muzzle").gameObject;
    }
	
	// Update is called once per frame
	void Update() {
        player = GameObject.FindWithTag("Player");
        SetMoveType(); PlayerUpdate();
    }

    void SetMoveType()
    {
        switch (movetype)
        {
            case MoveType.TypeA:
                MoveTypeA(); Bullet(); Rotate();
                break;
            case MoveType.TypeB:
                MoveTypeB(); Bullet(); Rotate();
                break;
            case MoveType.TypeC:
                Bullet(); Rotate();
                break;
            case MoveType.TypeD:

                break;
        }
    }


    void MoveTypeA()
    {
        if (MovingPosition == transform.position)
        {
            if (i >= pos.Length)
            {
                i = 0; 
            }
            else
            {
                i += 1;
            }
        }
        MovingPosition = pos[i].GetComponent<Transform>().position;
        transform.position = Vector3.MoveTowards(gameObject.transform.position, MovingPosition, MoveSpeed * Time.deltaTime);
    }


    void MoveTypeB()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
    }
    //void MoveTypeB()
    //{
    //    transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, MoveSpeed * Time.deltaTime);
    //}




    void DamageColorChange()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
    }


    void Rotate()
    {
        if (target == null)
        {
            if (player == null)
            {
                Quaternion rotation = Quaternion.LookRotation(v3, Vector3.down);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationspeed);
            }
            else
            {
                Vector3 pos = transform.localPosition;
                Vector3 playerpos = player.GetComponent<Transform>().position;
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, playerpos - pos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationspeed);
            }
        }
        else
        {
            Vector3 pos = transform.localPosition;
            Vector3 targetpos = player.GetComponent<Transform>().position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, targetpos - pos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationspeed);
        }
    }


    void Bullet()
    {
        interval -= Time.deltaTime;
        bullet = Random.Range(0, 10);
        if (player == null)
        {

        }
        else
        {
            if (interval <= 0 && bullet == 1)
            {
                interval = bulletInterval;
                Instantiate(BulletTypeEnemy, muzzle.transform.position, transform.rotation,Bullet_Box.transform);
            }
        }
    }


    void PlayerUpdate()
    {
        if (player == null)
        {
            player = GameObject.Find("Bullet");
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            HP -= 1;
            gameObject.GetComponent<SpriteRenderer>().color = DamageColor;
            Invoke("DamageColorChange", 0.1f);
            if (HP <= 0)
            {
                //Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
