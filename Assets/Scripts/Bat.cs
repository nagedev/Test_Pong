using Photon.Pun;
using UnityEngine;

namespace Pong
{
    public sealed class Bat : MonoBehaviour
    {
        private IInputProvider _inputProvider;
        private LevelMono _gameSettings;
        private IBatControl _batControl;
        
        public void Inject(IInputProvider inputProvider, LevelMono gameSettings)
        {
            _inputProvider = inputProvider;
            _gameSettings = gameSettings;

            SetBatControl(BatControlType.Player);
        }

        public void SetBatControl(BatControlType batControlType)
        {
            switch (batControlType)
            {
                case BatControlType.Player:
                    _batControl = new BatControlPlayer(this, _inputProvider, _gameSettings);
                    break;
                case BatControlType.Other:
                    _batControl = new BatControlOther(this);
                    break;
            }
        }
        
        private void Update()
        {
            _batControl.Update();
        }
    }

    public enum BatControlType
    {
        Player = 0,
        Other = 1,
    }
}