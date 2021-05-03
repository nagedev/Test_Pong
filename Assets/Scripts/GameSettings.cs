using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Pong/" + nameof(GameSettings))]
    public sealed class GameSettings : ScriptableObject
    {
        [Header("Prefabs")] 
        [SerializeField] private LevelMono levelMono;
        [SerializeField] private Bat batPrefab;
        [SerializeField] private Ball ballPrefab;
        [Header("Visual")] 
        [SerializeField] private Material ballMat;
        [Header("Params")] 
        [SerializeField] private List<BallsSettings> ballsSettings;

        //prefabs
        public LevelMono LevelMono => levelMono;
        public Bat BatPrefab => batPrefab;
        public Ball BallPrefab => ballPrefab;
        
        //visual
        public Material BallMat => ballMat;
        
        //params
        public BallsSettings GetBallSettings(BallType type) => ballsSettings[(int) type];
    }

    [Serializable]
    public sealed class BallsSettings
    {
        [SerializeField] private float _initialSpeed;
        [SerializeField] private float _size;

        public float InitialSpeed => _initialSpeed;
        public float Size => _size;
    }
}