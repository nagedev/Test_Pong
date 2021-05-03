using UnityEngine;

namespace Pong
{
    public sealed class LevelCreator : MonoBehaviour
    {
        private LevelObjectsFactory _objectsFactory;
        private LevelMono _levelMono;
        private LevelController _levelController;

        public void Inject(LevelMono levelMono, 
            LevelObjectsFactory objectsFactory, 
            LevelController levelController)
        {
            _objectsFactory = objectsFactory;
            _levelMono = levelMono;
            _levelController = levelController;
            
            Init();
        }

        private void Init()
        {
            SpawnBats();
            SpawnBall();
        }
        
        private void SpawnBats()
        {
            SpawnBat(BatPlace.Top);
            SpawnBat(BatPlace.Bottom);
        }

        private void SpawnBat(BatPlace place)
        {
            var bat = _objectsFactory.CreateBat();
            _levelController.RegisterBat(bat, place);

            switch (place)
            {
                case BatPlace.Top:
                    bat.transform.position = _levelMono.BatPlaceTop.position;
                    break;
                default:
                    bat.transform.position = _levelMono.BatPlaceBottom.position;
                    break;
            }
        }

        private void SpawnBall()
        {
            var ball = _objectsFactory.CreateBall();
            ball.transform.position = _levelMono.BallPlace.position;
            
            _levelController.RegisterBall(ball);
        }
    }

    public enum BatPlace
    {
        Top = 0,
        Bottom = 1,
    }
}