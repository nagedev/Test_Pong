using UnityEngine;

namespace Pong
{
    public sealed class SavedProgress : ISavedProgress
    {
        private int _scoreBest;
        public int ScoreBest
        {
            get => _scoreBest;
            set
            {
                _scoreBest = value;
                Save();
            }
        }

        private float _ballColor;
        public float BallColor
        {
            get => _ballColor;
            set
            {
                _ballColor = value;
                Save();
            }
        }

        private const string ScoreBestId = "ScoreBest";
        private const string BallColorId = "BallColor";

        public SavedProgress()
        {
            Load();
        }
        
        private void Load()
        {
            _scoreBest = PlayerPrefs.GetInt(ScoreBestId);
            _ballColor = PlayerPrefs.GetFloat(BallColorId);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(ScoreBestId, ScoreBest);
            PlayerPrefs.SetFloat(BallColorId, BallColor);

            PlayerPrefs.Save();
        }
    }
}