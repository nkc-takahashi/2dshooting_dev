using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InariSystem.MajiManji
{
    public class StateModifyView : MonoBehaviour
    {
        [SerializeField]
        private float _amount;

        public float Amount => _amount;
        
        [SerializeField]
        private float _maxValue = 10;

        
        [Header("UI Parts")]
        [SerializeField]
        private Text _amountLabel;
        
        [SerializeField]
        private RectTransform _gaugeTransform;
        
        [SerializeField]
        private Vector2 _cellSize;
        
        
        
        [Header("Buttons")]
        [SerializeField]
        private Button _plus;
        
        [SerializeField]
        private Button _minus;

        private StateManager _stateManager;
        
        // Use this for initialization
        private void Start()
        {
            _stateManager = GameObject.FindObjectOfType<StateManager>();
            
            SetEvent();
        }

        private void Update()
        {
            InValidate();
        }
        
        private void InValidate()
        {
            var newDelta = CalculateSize(_amount);
            _gaugeTransform.sizeDelta = newDelta;

            _amountLabel.text = _maxValue <= _amount ? $"+{_amount * 10}M" : $"+{_amount * 10}";
        }

        private Vector2 CalculateSize(float value)
        {
            return new Vector2(_cellSize.x + value * 10, _cellSize.y);
        }

        private void SetEvent()
        {
            _plus.onClick.AddListener(OnClickPlus);
            _minus.onClick.AddListener(OnClickMinus);
        }
        
        private void OnClickPlus()
        {
            if (!(_stateManager.Point > 0)) return;

            if (!(_amount < _maxValue)) return;

            _amount += 1;
            _stateManager.Decrement();
        }

        private void OnClickMinus()
        {
            if (!(_amount > 0)) return;
            
            _amount--;
            _stateManager.Increment();
        }
    }
}