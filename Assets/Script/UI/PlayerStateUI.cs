using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace InariSystem.MajiManji
{
    public class PlayerStateUI : MonoBehaviour
    {
        public GameObject Player, PlayerControl;
        
        private Text _stateLabel;

        private Player _player;
        private PlayerControl _playerControl;

        private void Start()
        {
            _stateLabel = GetComponent<Text>();

            _player = Player.GetComponent<Player>();
            _playerControl = PlayerControl.GetComponent<PlayerControl>();
        }

        // Update is called once per frame
        private void Update()
        {
            var builder = new StringBuilder();

            builder.AppendLine("Player Control");
            builder.AppendLine($"Speed X:{_playerControl.SpeedX} Y:{_playerControl.SpeedY}");
            builder.AppendLine($"PlayerHP: {_playerControl.Health}");
            builder.AppendLine($"ShieldHP: {_playerControl.Shield}");
            builder.AppendLine($"BulletAmount: {_playerControl.BulletAmount}");
            builder.AppendLine($"BulletInterval: {_playerControl.BulletInterval}");
            builder.AppendLine($"TrajectoryAmount: {_playerControl.TrajectoryAmount}");
            builder.AppendLine($"RotationSpeed: {_playerControl.RotationSpeed}");
            builder.AppendLine();

            builder.AppendLine("Player");
            builder.AppendLine($"Pos:{_player.Position}");
            builder.AppendLine($"PlayerHP: {_player.Health}");
            builder.AppendLine($"ShieldHP: {_player.Shield}");
            builder.AppendLine($"BulletAmount: {_player.BulletAmount}");
            builder.AppendLine($"BulletInterval: {_player.BulletInterval}");
            builder.AppendLine($"TrajectoryAmount: {_player.TrajectoryAmount}");
            builder.AppendLine($"RotationSped: {_player.RotationSpeed}");
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine("DebugMode");

            _stateLabel.text = builder.ToString();
        }
    }
}