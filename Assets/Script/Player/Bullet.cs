using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InariSystem.MajiManji
{
    public enum BulletType
    {
        TypeA, //ますぐ
        TypeB, //追尾弾
        TypeC
    }
    
    public class Bullet : MonoBehaviour
    {
        public BulletType BulletType;
        public float Speed, RotationSpeed, HomingTiming, LifeTime, LimitAngle, Distance, Angle;
        public GameObject player, enemy;
        public GameObject[] enemybox;
        public Vector2 targetenemypoint, targetenemy, enemypos, bulletpos, bulleteye;

        //TypeB
        public float hoomingbullet, enemyupdateinterval;
        float updateinterval;

        ////public bool mouserotateswitch;
        public float DestroyTiming, valuez;
        private Rigidbody2D _rigidbody;

        public bool rockon;



        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = transform.up * Speed;
            
            player = GameObject.Find("Player");
            enemy = GameObject.Find("Enemy");
        }


        // Update is called once per frame
        private void Update()
        {
            BulletBehaviour();
        }


        private void BulletBehaviour()
        {
            switch (BulletType)
            {
                case BulletType.TypeA: BulletTypeA(); break;
                case BulletType.TypeB: BulletTypeB(); break;
                case BulletType.TypeC: BulletTypeC(); break;
            }
        }


        private void BulletTypeA()
        {
            _rigidbody.velocity = (transform.up * Speed);
            Despawn();
        }


        private void BulletTypeB()
        {
            enemypos = enemy.transform.position;
            bulletpos = transform.position;
            bulleteye = transform.up;
            if (Vector3.Distance(enemypos, bulletpos) <= Distance &&
                Vector3.Angle((enemypos - bulletpos).normalized, bulleteye) <= Angle) rockon = true;
            else rockon = false;
            if (rockon)
            {
                targetenemy = enemy.transform.position;
                var targetPosition = (Vector2) transform.position - targetenemy;
                targetPosition.Normalize();
                valuez = Vector3.Cross(targetPosition, transform.up).z;
                if (valuez > 0) _rigidbody.angularVelocity = RotationSpeed;
                else if (valuez < 0) _rigidbody.angularVelocity = -RotationSpeed;
                else RotationSpeed = 0;
                _rigidbody.angularVelocity = RotationSpeed * valuez;
                _rigidbody.velocity = transform.up * Speed;
            }
            else
            {
                _rigidbody.angularVelocity = 0;
                _rigidbody.velocity = (transform.up * Speed);
            }
        }

        private void BulletTypeC()
        {
            if (enemy == null)
            {
                Despawn();
            }
            else
            {
                enemypos = enemy.transform.position;
                if (HomingTiming <= 0)
                {
                    updateinterval -= Time.deltaTime;
                    LifeTime -= Time.deltaTime;
                    var targetPosition = (Vector2) transform.position - targetenemy;
                    targetPosition.Normalize();
                    valuez = Vector3.Cross(targetPosition, transform.up).z;
                    if (valuez > 0) _rigidbody.angularVelocity = RotationSpeed;
                    else if (valuez < 0) _rigidbody.angularVelocity = -RotationSpeed;
                    else RotationSpeed = 0;
                    _rigidbody.angularVelocity = RotationSpeed * valuez;
                    _rigidbody.velocity = transform.up * Speed;
                    if (updateinterval < 0)
                    {
                        targetenemy = enemy.transform.position;
                        updateinterval = enemyupdateinterval;
                    }
                }
                else
                {
                    HomingTiming -= Time.deltaTime;
                    _rigidbody.velocity = (transform.up * Speed);
                }

                if (LifeTime < 0) Destroy(gameObject);
            }

            Despawn();
        }

        private void BulletTypeD()
        {
            enemypos = enemy.transform.position;
            bulletpos = transform.position;
            bulleteye = transform.forward;
            if (Vector3.Distance(enemypos, bulletpos) <= Distance &&
                Vector3.Angle((enemypos - bulletpos).normalized, bulleteye) <= Angle)
            {
            }
            else
            {
                _rigidbody.velocity = (transform.up * Speed);
            }
        }


        private void Despawn()
        {
            DestroyTiming -= Time.deltaTime;
            if (DestroyTiming <= 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") || other.CompareTag("BulletTypeEnemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}