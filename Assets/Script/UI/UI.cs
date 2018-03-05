using UnityEngine;
using UnityEngine.UI;

namespace InariSystem.MajiManji
{
    public enum UITypes
    {
        TypeA,
        TypeB
    }
    
    public class UI : MonoBehaviour
    {

        private Player _player;
        
        public GameObject Enemy;
        public GameObject[] Enemies;

        public UITypes UIType;
        public Image BulletRight, BulletLeft;

        [ColorHtmlProperty]
        public Color ManyColor;

        [ColorHtmlProperty]
        public Color lowColor;

        public Text bulletAtxt, bulletBtxt;
        public Text weapon, enemyHP, playerHP, shiledHP;
        public Text playerstate;

        public GameObject PauseUI;

        private float _bulletAmount;
        private float _bulletTime, _weaponTime; //playerがdie状態の時に使用

        private string[] keys =
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u",
            "v", "w", "x", "y", "z", "-", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", " ", " ", " ", " ", " ",
            " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "
        };

        private void Update()
        {
            _player = GameObject.Find("Player").GetComponent<Player>();
            
            setType();
            Pause();
        }

        private void setType()
        {
            switch (UIType)
            {
                case UITypes.TypeA:
                    BulletUI();
                    EnemyHP();
                    Weapon();
                    PlayerHP();
                    break;
            }
        }


        //----------UIType.TypeA----------//
        private void BulletUI()
        {
            if (_player == null)
            {
                _bulletTime -= Time.deltaTime;
                if (_bulletTime <= 0)
                {
                    _bulletTime = 0.1f;
                    float f = Random.Range(10, 101);
                    bulletAtxt.text = f.ToString();
                }
            }
            else
            {
                if (_player.noreloadswitch == true)
                {

                }
                else
                {
                    _bulletAmount = _player.remainingbulletvalue;
                    if (_bulletAmount >= 50)
                    {
                        BulletRight.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 25);
                        BulletLeft.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 25);
                    }
                    else if (_bulletAmount < 0)
                    {
                        BulletRight.GetComponent<RectTransform>().sizeDelta = new Vector2(_bulletAmount * 0, 25);
                        BulletLeft.GetComponent<RectTransform>().sizeDelta = new Vector2(_bulletAmount * 0, 25);
                    }
                    else
                    {
                        BulletRight.GetComponent<RectTransform>().sizeDelta = new Vector2(_bulletAmount * 10, 25);
                        BulletLeft.GetComponent<RectTransform>().sizeDelta = new Vector2(_bulletAmount * 10, 25);
                    }

                    if (_player.invisibleswitch)
                    {
                        bulletAtxt.GetComponent<Text>().text = "Unlimited";
                        bulletBtxt.GetComponent<Text>().text = "Unlimited";
                    }
                    else
                    {
                        bulletAtxt.GetComponent<Text>().text = _bulletAmount.ToString();
                        bulletBtxt.GetComponent<Text>().text = _bulletAmount.ToString();
                    }

                    if (_bulletAmount <= 20)
                    {
                        BulletRight.GetComponent<Image>().color = lowColor;
                        BulletLeft.GetComponent<Image>().color = lowColor;
                    }
                    else
                    {
                        BulletRight.GetComponent<Image>().color = ManyColor;
                        BulletLeft.GetComponent<Image>().color = ManyColor;
                    }
                }
            }
        }

        private void Weapon()
        {
            if (_player == null)
            {
                _weaponTime -= Time.deltaTime;
                if (_weaponTime <= 0)
                {
                    _weaponTime = 0.1f;
                    int i = keys.Length;
                    weapon.text = keys[Random.Range(0, i)] + keys[Random.Range(0, i)] + keys[Random.Range(0, i)] +
                                  keys[Random.Range(0, i)] + keys[Random.Range(0, i)] + keys[Random.Range(0, i)] +
                                  keys[Random.Range(0, i)];
                }
            }
            else
            {
                //weapon.text = player.GetComponent<PlayerControl>().bullet.name;
            }
        }

        private void EnemyHP()
        {
            enemyHP.text = Enemy.GetComponent<EnemyMove>().HP.ToString();
        }

        private void PlayerHP()
        {
            if (_player.invisibleswitch == true)
            {
                playerHP.text = "Invisible";
            }
            else
            {
                playerHP.text = _player.Health.ToString();
            }
        }

        private void Pause()
        {
            if (Time.timeScale != 1)
            {
                PauseUI.SetActive(true);
            }
            else
            {
                PauseUI.SetActive(false);
            }

            if (Input.GetKeyDown("o"))
            {
                if (Time.timeScale > 0)
                {
                    Time.timeScale -= 0.1f;
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
    }
}