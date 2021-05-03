using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Pong
{
    public sealed class Ball : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float initialForceMagnitude;
        
        private LevelController _levelController;
        private LevelMono _levelMono;
        private GameSettings _gameSettings;
        
        public BallType BallType { get; private set; }

        private void OnValidate()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Inject(LevelController levelController, LevelMono levelMono, GameSettings gameSettings)
        {
            _levelController = levelController;
            _levelMono = levelMono;
            _gameSettings = gameSettings;

            ReconfigureBall(BallType.Slow);
        }

        private void Awake()
        {
            Launch();
        }

        private void Launch()
        {
            rb.velocity = Vector2.zero;
            
            var forceDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-10f, 10f)).normalized;
            rb.AddForce(forceDir * initialForceMagnitude, ForceMode2D.Impulse);
        }

        private void Update()
        {
            if (transform.position.y > _levelMono.VertBallLimit.position.y
                || transform.position.y < -_levelMono.VertBallLimit.position.y)
            {
                _levelController.BallOutside();
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Bat"))
                return;
            
            _levelController.AddScore();
        }

        public void ReconfigureBall(BallType newType, bool launch = true)
        {
            initialForceMagnitude = _gameSettings.GetBallSettings(newType).InitialSpeed;
            transform.localScale = Vector3.one * _gameSettings.GetBallSettings(newType).Size;

            if (launch)
            {
                transform.position = Vector3.zero;
                Launch();
            }

            BallType = newType;
        }
    }

    public enum BallType
    {
        Slow = 0,
        Fast = 1,
    }
}