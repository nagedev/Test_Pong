using UnityEngine;

namespace Pong
{
    [CreateAssetMenu(fileName = nameof(GameSettings), menuName = "Pong/" + nameof(GameSettings))]
    public sealed class GameSettings : ScriptableObject
    {
        [Header("Prefabs")] 
        [SerializeField] private LevelMono levelMono;
        [SerializeField] private Bat batPrefab;
        [SerializeField] private Ball ballSlowPrefab;
        [SerializeField] private Ball ballFastPrefab;
        [Header("Visual")] 
        [SerializeField] private Material ballMat;

        //prefabs
        public LevelMono LevelMono => levelMono;
        public Bat BatPrefab => batPrefab;
        public Ball BallSlowPrefab => ballSlowPrefab;
        public Ball BallFastPrefab => ballFastPrefab;
        
        //visual
        public Material BallMat => ballMat;
    }
}