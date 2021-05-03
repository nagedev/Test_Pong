using System;

namespace Pong
{
    ///Controlling level logic
    public sealed class LevelController
    {
        private readonly LevelMono _levelMono;
        private readonly ISavedProgress _savedProgress;
        
        private Bat _topBat;
        private Bat _bottomBat;
        private Ball _ball;
        private int _score;
        private int _scoreBest;

        public LevelController(LevelMono levelMono, ISavedProgress savedProgress)
        {
            _levelMono = levelMono;
            _savedProgress = savedProgress;
            
            _scoreBest = _savedProgress.ScoreBest;

            UpdateDisplayedScore();
        }

        public void AddScore()
        {
            _score += 1;
            
            UpdateDisplayedScore();
        }

        private void ResetScore()
        {
            if (_score > _scoreBest)
                _scoreBest = _score;
            _score += 0;
            
            _savedProgress.ScoreBest = _scoreBest;

            UpdateDisplayedScore();
        }

        private void UpdateDisplayedScore()
        {
            _levelMono.UIController.TextMeshScore.text = _score.ToString();
            _levelMono.UIController.TextMeshScoreBest.text = _scoreBest.ToString();
        }

        public void BallOutside()
        {
            ResetScore();
            
            BallType ballType = (BallType) UnityEngine.Random.Range(0, 2);
            GetBall().ReconfigureBall(ballType);
        }
        
        public void RegisterBat(Bat bat, BatPlace batPlace)
        {
            if (batPlace == BatPlace.Top)
                _topBat = bat;
            else
                _bottomBat = bat;
        }

        public void RegisterBall(Ball ball)
        {
            _ball = ball;
        }
        
        public Bat GetBat(BatPlace place) => place == BatPlace.Top ? _topBat : _bottomBat;
        public Ball GetBall() => _ball;
    }
}