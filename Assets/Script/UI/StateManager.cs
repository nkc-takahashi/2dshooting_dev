using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InariSystem.MajiManji
{
	public class StateManager : MonoBehaviour 
	{
		public GameObject[] State;
		public float Point;
		public Text PointLabel;

		private void Update()
		{
			PointLabel.text = Point.ToString();
		}
	}
}

