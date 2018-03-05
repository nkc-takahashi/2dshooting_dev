using UnityEngine;
using UnityEngine.UI;

namespace InariSystem.MajiManji
{
    public class WaveControl : MonoBehaviour
    {
        [SerializeField]
        private CountType _countType;
        public Text counttext, CountTimeText, wavetext;
        public GameObject waveclear;
        public float countdown, starttime = 10, cooldown = 10;
        public int WaveCount, GameClearBorder;
        public float s, m, h, counttime, cooltime, round;
        string sst, mst, hst;
        public bool hours, countswitch, startcount;

        private void Start()
        {
            switch (_countType)
            {
                case CountType.Type1:
                    countswitch = true;
                    break;
                case CountType.Type2:
                    countswitch = true;
                    counttime = countdown;
                    break;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            SetType();

        }

        private void SetType()
        {
            switch (_countType)
            {
                case CountType.Type1:
                    CountUp();
                    break;
                case CountType.Type2:
                    CountDown();
                    break;
            }
        }


        private void CountUp()
        {
            if (countswitch) counttime += Time.deltaTime;
            s = Mathf.Floor(counttime);
            if (s < 10) sst = "0" + s;
            else sst = s.ToString();
            if (m < 10) mst = "0" + m;
            else mst = m.ToString();
            if (h < 10) hst = "0" + h;
            else hst = h.ToString();
            if (s > 60)
            {
                counttime = 0;
                m += 1;
                if (m > 60)
                {
                    hours = true;
                    h += 1;
                }
            }

            if (hours)
            {
                CountTimeText.text = (hst + ":" + mst + ":" + sst);
            }
            else
            {
                CountTimeText.text = (mst + ":" + sst);
            }
        }

        private void CountDown()
        {
            if (startcount)
            {
                if (countswitch) // True = Countdown False = Cooldown
                {
                    if (counttime <= 0)
                    {
                        if (GameClearBorder == WaveCount)
                        {
                            wavetext.text = "GAME CLEAR";
                            Debug.Log("GAMECLEAR");
                        }
                        else
                        {
                            counttime = 0;
                            wavetext.text = "WAVE " + WaveCount + " CLEAR";
                        }

                        counttime = 0;
                        waveclear.GetComponent<Animator>().Play("waveclear");
                    }
                    else
                    {
                        counttime -= Time.deltaTime;
                    }
                }
                else
                {
                    if (counttime <= 0)
                    {
                        counttime = 0;
                        wavetext.text = "WAVE " + WaveCount + " START";
                        waveclear.GetComponent<Animator>().Play("wavestart");
                    }
                    else
                    {
                        counttime -= Time.deltaTime;
                    }
                }

                m = Mathf.Floor(counttime / 60);
                s = Mathf.Floor(counttime) - Mathf.Floor(m * 60);
            }
            else
            {
                if (starttime <= 0)
                {
                    starttime = 0;
                    waveclear.GetComponent<Animator>().Play("wavestart");
                }
                else
                {
                    starttime -= Time.deltaTime;
                }

                m = Mathf.Floor(starttime / 60);
                s = Mathf.Floor(starttime) - Mathf.Floor(m * 60);
            }

            if (s < 10) sst = "0" + s;
            else sst = s.ToString();
            if (m < 10) mst = "0" + m;
            else mst = m.ToString();
            CountTimeText.text = (mst + ":" + sst);
        }

        public void Countswitch()
        {
            if (startcount)
            {
                if (countswitch) //クールタイムスタート
                {
                    if (GameClearBorder == WaveCount)
                    {
                        counttext.text = "GAME CLEAR";
                        Debug.Log("GAMECLEAR");
                    }
                    else
                    {
                        WaveCount += 1;
                        countswitch = false;
                        counttext.text = "COOL TIME";
                        counttime = cooldown;
                    }
                }
                else //クールタイム終了
                {
                    Debug.Log("ENDCOOL");
                    countswitch = true;
                    counttext.text = WaveCount + " WAVE";
                    counttime = countdown;
                }
            }
            else
            {
                startcount = true;
                counttext.text = "START COUNT";
            }
        }
        
        public enum CountType
        {
            Type1,
            Type2
        }
    }
}