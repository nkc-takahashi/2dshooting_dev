
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace InariSystem.MajiManji
{
    public class PlayerControl : MonoBehaviour
    {

        //public
        public GameObject Playerprefab;
        public string playerSpawnscene;

        public float
            SpeedX,
            SpeedY, //移動スピード
            BulletAmount, //弾数初期値
            BulletInterval, //発砲する間隔
            TrajectoryAmount, //弾道のブレ幅
            RotationSpeed, //マウスの追うスピード
            ReloadInterval, //リロードに掛かる時間
            Health, //playerのHP
            Shield; //shieldのHP

        public Vector3 playerpos;

        

        public PlayerMode modechange;
        public bool mouseinputswitch, reroadswitch, shieldswitch, invisibleswitch;


        public BulletType bullettype;
        public GameObject hpsort, shieldhpsort, speedsort;
        public GameObject[] Bullettype;
        public GameObject player, spowner;
        public GameObject Maxpos, Minpos;

        int i;

        void Start()
        {
            spowner = GameObject.Find("PlayerSpawnPos");
        }


        void Update()
        {
            ModeChange();
            PlayerDieIsIt();
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
                var obj = Instantiate(Playerprefab);
                obj.name = Playerprefab.name;
            }
        }

        public void Statesort()
        {
            Health += hpsort.GetComponent<StateModifyView>().Amount;
            Shield += shieldhpsort.GetComponent<StateModifyView>().Amount;
            SpeedX += speedsort.GetComponent<StateModifyView>().Amount;
            SpeedY += speedsort.GetComponent<StateModifyView>().Amount;
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene("2DShooting");
        }
    }

    /// <summary>
    /// 攻撃タイプ
    /// </summary>
    public enum BulletTypes
    {
        BulletA,
        BulletB,
        BulletC,
        BulletD
    }
    
    /// <summary>
    /// プレイヤー状態
    /// </summary>
    public enum PlayerMode
    {
        Normal,
        Invisible,
        Debugmode,
        Default
    }
}