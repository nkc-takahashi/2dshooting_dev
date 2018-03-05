using UnityEngine;
using System.Collections;

namespace InariSystem.MajiManji
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private GameObject _muzzle;

        private GameObject _bullet;

        private GameObject _shield;

        [SerializeField]
        private PlayerControl _playerControl;

        private string _modeChange;

        [SerializeField]
        private GameObject[] _bulletType;

        public float Health, Shield;

        [SerializeField]
        private GameObject _shieldObject;

        [System.NonSerialized]
        public bool _freeRotation, noreloadswitch, shieldswitch, invisibleswitch, recoveryswitch;

        //private
        [System.NonSerialized]
        public float
            SpeedX,
            SpeedY, //移動スピード
            BulletAmount, //弾数初期値
            BulletInterval, /*発砲する間隔*/
            Interval,
            TrajectoryAmount, //弾道のブレ幅
            Remaining, //変動する弾数
            RotationSpeed, //マウスの追うスピード
            ReloadInterval, //
            RecoverHealthInterval = 5, /* */
            RecoverInterval;

        public GameObject Maxpos, Minpos;

        public Vector2 maxpos, minpos;
        Quaternion bulletRough;
        
        private Rigidbody2D _rigidbody;

        [System.NonSerialized]
        public float remainingbulletvalue;

        [System.NonSerialized]
        public Vector2 Position;

        [System.NonSerialized]
        public BulletType bullettype;

        private void Start()
        {
            _playerControl = GameObject.Find("PlayerController").GetComponent<PlayerControl>();
            Interval = BulletInterval;
            BulletInterval = _playerControl.BulletInterval;
            InitializeStatus();
            remainingbulletvalue = BulletAmount;
            _muzzle = transform.Find("muzzle").gameObject;
            _modeChange = _playerControl.modechange.ToString();
            bullettype = _playerControl.bullettype;
        }

        private void InitializeStatus()
        {
            Health = _playerControl.Health;
            SpeedX = _playerControl.SpeedX;
            SpeedY = _playerControl.SpeedY;
            BulletInterval = _playerControl.BulletInterval;
            BulletAmount = _playerControl.BulletAmount;
            RotationSpeed = _playerControl.RotationSpeed;
            TrajectoryAmount = _playerControl.TrajectoryAmount;
        }

        private void Update()
        {
            ModeChange();
            Actionrange();
        }

        private void ModeChange()
        {
            switch (_modeChange)
            {
                case "Normal":
                    BulletSetType();
                    Move();
                    Rotate();
                    Bullet();
                    Guard();
                    Actionrange();
                    invisibleswitch = false;
                    noreloadswitch = false;
                    break;
                case "Invisible":
                    BulletSetType();
                    Move();
                    Rotate();
                    Bullet();
                    Guard();
                    Actionrange();
                    invisibleswitch = true;
                    noreloadswitch = true;
                    break;
                //case "Debugmode":
                //    BulletSetType(); Move(); Rotate(); Bullet(); shield();  Playerctrlset(); Actionrange();
                //    invisibleswitch = false;recoveryswitch = true; recovery();
                //    break;
            }
        }


        // --- 移動制御 ---
        private void Move()
        {
            var speed = new Vector2(SpeedX, SpeedY);
            Position = transform.position; //現在位置
            if (Input.GetKey(KeyCode.A) || Input.GetKey("left"))
            {
                Position.x -= speed.x;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey("right"))
            {
                Position.x += speed.x;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey("up"))
            {
                Position.y += speed.y;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey("down"))
            {
                Position.y -= speed.y;
            }

            transform.position = Position;
            Clamp();
        }

        private void Clamp()
        {
            Position.x = Mathf.Clamp(Position.x, minpos.x, maxpos.x);
            Position.y = Mathf.Clamp(Position.y, minpos.y, maxpos.y);
            transform.position = new Vector2(Position.x, Position.y);
        }


        // --- マウス方向を向く --- 
        private void Rotate()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                _freeRotation = !_freeRotation;
            }

            var destination = _freeRotation
                ? Input.mousePosition - Camera.main.WorldToScreenPoint(transform.localPosition)
                : Vector3.up;

            var rotation = Quaternion.LookRotation(Vector3.forward, destination);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
        }


        // --- 発砲 ---
        private void Bullet()
        {
            Interval -= Time.deltaTime;
            bulletRough = transform.rotation;
            bulletRough.z += Random.Range(-TrajectoryAmount / 100, TrajectoryAmount / 100);
            if (remainingbulletvalue > 0)
            {
                if (Interval <= 0 && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
                {
                    Interval = BulletInterval;
                    remainingbulletvalue -= 1;
                    Instantiate(_bullet, _muzzle.transform.position, bulletRough);
                    Debug.Log("発砲");
                }
                else
                {
                    Interval = 0;
                }
            }

            if (Input.GetKey(KeyCode.R))
            {
                StartCoroutine(Reload());
            }
        }

        private IEnumerator Reload()
        {
            yield return new WaitForSeconds(ReloadInterval);
            remainingbulletvalue = BulletAmount;
            yield return null;
        }


        // --- 自動ヒール --- 
        private void Recovery()
        {
            if (RecoverInterval < 0 && _playerControl.Health != Health)
            {
                RecoverInterval = 6;
            }

            if (RecoverInterval == 6 && _playerControl.Health != Health)
            {
                Health += 1;
            }
            else if (RecoverInterval <= 5)
            {
                RecoverInterval -= Time.deltaTime;
            }
        }

        // --- シールド ---
        private void Guard()
        {
            if (Input.GetKey(KeyCode.Q) && shieldswitch == false)
            {
                shieldswitch = true;
                Instantiate(_shieldObject, transform.position, bulletRough, transform);
                _shield = transform.Find("shield(Clone)").gameObject;
            }

            if (_shield != null)
            {
                Shield = transform.Find("shield(Clone)").gameObject.GetComponent<Shield>().shieldhp;
            }
        }


        // --- バレットタイプセット ---
        private void BulletSetType()
        {
            switch (bullettype)
            {
                case BulletType.TypeA: _bullet = _bulletType[0]; break;

                case BulletType.TypeB: _bullet = _bulletType[1]; break;

                case BulletType.TypeC: _bullet = _bulletType[2]; break;

                default: break;
            }
        }

        [SerializeField]
        private Transform _maxPos;
        [SerializeField]
        private Transform _minPos;

        private void Actionrange()
        {
            maxpos = _maxPos.position;
            minpos = _minPos.position;
        }

        // --- 衝突判定 ---
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (shieldswitch == false && invisibleswitch == false)
                if (collision.CompareTag("BulletTypeEnemy"))
                {
                    Health -= 1;
                    RecoverInterval = 5;
                    if (Health < 0)
                    {
                        Destroy(gameObject);
                    }
                }
        }
    }
}