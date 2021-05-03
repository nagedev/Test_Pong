using TMPro;
using UnityEngine;

namespace Pong
{
    ///For storing references
    public sealed class LevelMono : MonoBehaviour
    {
        [Header("Params")]
        [SerializeField] private Transform sideMoveBorder;
        [SerializeField] private Transform vertBallLimit;
        [SerializeField] private Transform batPlaceTop;
        [SerializeField] private Transform batPlaceBottom;
        [SerializeField] private Transform ballPlace;

        [Header("UI")]
        [SerializeField] private UIController uiController;

        [Header("Refs")] 
        [SerializeField] private Transform parent;

        //params
        public Transform SideMoveBorder => sideMoveBorder;
        public Transform VertBallLimit => vertBallLimit;
        public Transform BatPlaceTop => batPlaceTop;
        public Transform BatPlaceBottom => batPlaceBottom;
        public Transform BallPlace => ballPlace;

        //ui
        public UIController UIController => uiController;
        
        //refs
        public Transform Parent => parent;
    }
}