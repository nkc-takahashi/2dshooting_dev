using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InariSystem.MajiManji
{
	public class WaveAnimation : MonoBehaviour
	{
		[SerializeField]
		private WaveControl _waveControl;
		
		private bool _waveAnimatinoSwitch;
		
		private void Wavestartanm()
		{
			_waveControl.Countswitch();
		}
		
		private void Waveclearanm()
		{
			if (_waveControl.GameClearBorder == _waveControl.WaveCount)
				Time.timeScale = 0f;
			else
				_waveControl.Countswitch();
		}
	}
}

