using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InariSystem.MajiManji
{
	public class StateManager : MonoBehaviour
	{
		[SerializeField]
		private Text _pointLabel;

		[SerializeField]
		private int _point = 100;
		public int Point
		{
			get { return _point; }
			private set
			{
				_point = value;
				_pointLabel.text = $"POINT: {_point:000}";
			}
		}

		private void Start()
		{
			Point = _point;
		}

		public void Increment()
		{
			Point++;
		}

		public void Decrement()
		{
			Point = Mathf.Max(0, Point - 1);
		}
	}
}

