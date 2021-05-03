using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pong
{
    ///Not many object types at all, so one factory to create any of them
    public sealed class LevelObjectsFactory
    {
        private readonly GameSettings _gameSettings;
        private readonly IInputProvider _inputProvider;
        private readonly LevelController _levelController;
        private readonly LevelMono _levelMono;
        private readonly IMultiplayer _multiplayer;
        
        public LevelObjectsFactory(IInputProvider inputProvider, 
            GameSettings gameSettings, 
            LevelController levelController, 
            LevelMono levelMono,
            IMultiplayer multiplayer)
        {
            _inputProvider = inputProvider;
            _gameSettings = gameSettings;
            _levelController = levelController;
            _levelMono = levelMono;
            _multiplayer = multiplayer;
        }

        public Bat CreateBat()
        {
            var bat = Object.Instantiate(_gameSettings.BatPrefab, _levelMono.Parent.transform);
            bat.Inject(_inputProvider, _gameSettings.LevelMono);
            
            _multiplayer.RegisterSyncedObject(bat.gameObject);
            return bat;
        }

        public Ball CreateBall()
        {
            Ball prefab = _gameSettings.BallPrefab;
            var ball = Object.Instantiate(prefab, _levelMono.Parent.transform);
            ball.Inject(_levelController, _levelMono, _gameSettings);

            _multiplayer.RegisterSyncedObject(ball.gameObject);
            return ball;
        }
    }
}