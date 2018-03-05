using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InariSystem.MajiManji
{
    public enum MoveType
    {
        TypeA,
        TypeB,
        TypeC,
        TypeD
    }

    public class EnemyMove : MonoBehaviour
    {
        public GameObject player;
        public GameObject BulletTypeEnemy;
        public GameObject muzzle;

        public MoveType movetype;

        public GameObject[] pos;

        //[HideInInspector]
        public float MoveSpeed, HP;

        [ColorHtmlProperty]
        public Color DamageColor;

        Vector3 MovingPosition, playerpos;
        public float rotationspeed, bulletInterval;


        public Vector3 v3;

        //private
        float interval;
        
        private int _destionationIndex;
        public int bullet;

        private SpriteRenderer _spriteRenderer;
        
        

        private void SetMoveType()
        {
            switch (movetype)
            {
                case MoveType.TypeA:
                    MoveTypeA();
                    Bullet();
                    Rotate();
                    break;
                case MoveType.TypeB:
                    MoveTypeB();
                    Bullet();
                    Rotate();
                    break;
                case MoveType.TypeC:

                    break;
                case MoveType.TypeD:

                    break;
            }
        }

        #region Unity Callback

        // Use this for initialization
        private void Start()
        {
            muzzle = transform.Find("muzzle").gameObject;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            player = GameObject.FindWithTag("Player");
            SetMoveType();
            PlayerUpdate();
        }
        
        private void OnTriggerEnter2D(Collider2D coll)
        {
            HP -= 1;
            if (!coll.gameObject.CompareTag("Player")) return;
            
            _spriteRenderer.color = DamageColor;
                
            DelayMethod(0.1f, DamageColorChange);
            if (HP < 0)
            {
                //Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        #endregion

        #region StateMachine

        private void MoveTypeA()
        {
            if (Vector3.Distance(MovingPosition, transform.position) < 0.1f)
                _destionationIndex = _destionationIndex == pos.Length - 1 ? 0 : _destionationIndex + 1;

            MovingPosition = pos[_destionationIndex].transform.position;
            
            var dest = Vector3.MoveTowards(transform.position, MovingPosition, MoveSpeed * Time.deltaTime);
            transform.position = dest;
        }

        private void MoveTypeB()
        {
            var moveDir = MoveSpeed * Time.deltaTime;
            var dest = Vector3.MoveTowards(transform.position, player.transform.position, moveDir);
            transform.position = dest;
        }

        #endregion
        
        private void Rotate()
        {
            if (player == null)
            {
                var rotation = Quaternion.LookRotation(v3, Vector3.down);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationspeed);
            }
            else
            {
                var pos = transform.localPosition;
                var playerpos = player.GetComponent<Transform>().position;
                var rotation = Quaternion.LookRotation(Vector3.forward, playerpos - pos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationspeed);
            }
        }

        private void DamageColorChange()
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        }


        private void Bullet()
        {
            interval -= Time.deltaTime;
            bullet = Random.Range(0, 10);
            if (player == null) return;
            if (!(interval <= 0) || bullet != 1) return;
            
            interval = bulletInterval;
            Instantiate(BulletTypeEnemy, muzzle.transform.position, transform.rotation);
        }


        private void PlayerUpdate()
        {
            if (player == null)
            {
                player = GameObject.Find("Player");
            }
        }

        #region Utility

        private void DelayMethod(float delayTime, Action action)
        {
            StartCoroutine(DelayMethodBody(delayTime, action));
        }

        private static IEnumerator DelayMethodBody(float delayTime, Action action)
        {
            yield return new WaitForSeconds(delayTime);

            action?.Invoke();
        }

        #endregion
    }
}
