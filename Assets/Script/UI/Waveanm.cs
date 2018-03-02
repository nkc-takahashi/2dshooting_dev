using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveanm : MonoBehaviour {
    public GameObject wavectrl;
    bool waveanmswitch;

    void Wavestartanm()
    {
            wavectrl.GetComponent<WaveControl>().Countswitch();
    }
    void Waveclearanm()
    {
        if (wavectrl.GetComponent<WaveControl>().gameclearnum == wavectrl.GetComponent<WaveControl>().wavenum)
        {
            Time.timeScale = 0f;
        }
        else
        {
            wavectrl.GetComponent<WaveControl>().Countswitch();
        }
    }
}
