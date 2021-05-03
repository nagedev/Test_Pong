using UnityEngine;

namespace Pong
{
    public sealed class BatControlPlayer : IBatControl
    {
        private readonly Bat _bat;
        private IInputProvider _inputProvider;
        private LevelMono _gameSettings;
        
        public BatControlPlayer(Bat bat, IInputProvider inputProvider, LevelMono gameSettings)
        {
            _bat = bat;
            _inputProvider = inputProvider;
            _gameSettings = gameSettings;
        }
        
        public void Update()
        {
            if (!_inputProvider.IsTouched)
                return;

            var sidePosition = _gameSettings.SideMoveBorder.transform.position;

            Vector2 newPos = _inputProvider.TouchPosition;
            var lerpValue = newPos.x / Screen.width;
            newPos.x = Mathf.Lerp(-sidePosition.x, sidePosition.x, lerpValue);
            newPos.y = _bat.transform.position.y;
            
            _bat.transform.position = newPos;
        }
    }
}