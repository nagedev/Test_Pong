using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pong
{
    public sealed class UIController : MonoBehaviour
    {
        [Header("Windows")]
        [SerializeField] private GameObject gameplay;
        [SerializeField] private GameObject settings;
        
        [Header("Gameplay")]
        [SerializeField] private TextMeshProUGUI textMeshScore;
        [SerializeField] private TextMeshProUGUI textMeshScoreBest;
        [SerializeField] private Button toSettingsButton;

        [Header("Settings")] 
        [SerializeField] private Slider colorSlider;
        [SerializeField] private Image sliderBackground;
        [SerializeField] private Button toGameplayButton;
        
        public TextMeshProUGUI TextMeshScore => textMeshScore;
        public TextMeshProUGUI TextMeshScoreBest => textMeshScoreBest;

        private GameSettings _gameSettings;
        private ISavedProgress _savedProgress;

        private void Awake()
        {
            colorSlider.onValueChanged.AddListener(OnValueChanged);
            toSettingsButton.onClick.AddListener(ToSettings);
            toGameplayButton.onClick.AddListener(ToGameplay);

            GenerateSliderTexture();
        }

        private void GenerateSliderTexture()
        {
            const int textureWidth = 100;
            Texture2D tex = new Texture2D(textureWidth, 1);
            for (int i = 0; i < textureWidth; i++)
            {
                var newColor = Color.HSVToRGB((float) i / textureWidth, .9f, .9f);
                tex.SetPixel(i, 0, newColor);
            }
            tex.Apply();
            
            sliderBackground.sprite = Sprite.Create(tex, 
                new Rect(0.0f, 0.0f, tex.width, tex.height), 
                Vector2.one * .5f);
        }

        public void Inject(GameSettings gameSettings, ISavedProgress savedProgress)
        {
            _gameSettings = gameSettings;
            _savedProgress = savedProgress;

            colorSlider.value = _savedProgress.BallColor;
        }

        private void ToSettings() => OpenWindow(UIWindow.Settings);
        private void ToGameplay() => OpenWindow(UIWindow.Gameplay);

        public void OpenWindow(UIWindow window)
        {
            switch (window)
            {
                case UIWindow.Gameplay:
                    gameplay.SetActive(true);
                    settings.SetActive(false);
                    break;
                case UIWindow.Settings:
                    gameplay.SetActive(false);
                    settings.SetActive(true);
                    break;
            }
        }

        private void OnValueChanged(float newValue)
        {
            var newColor = Color.HSVToRGB(newValue, .9f, .9f);
            colorSlider.handleRect.GetComponent<Image>().color = newColor;
            _gameSettings.BallMat.color = newColor;

            _savedProgress.BallColor = newValue;
        }
    }

    public enum UIWindow
    {
        Gameplay = 0,
        Settings = 1,
    }
}