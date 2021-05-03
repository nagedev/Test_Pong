using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    ///DI composition root
    public sealed class CompositionRoot : MonoBehaviour
    {
        private void Awake()
        {
            //loading scriptable
            GameSettings gameSettings = Resources.Load<GameSettings>("GameSettings");
            
            //instantiating level
            LevelMono levelMono = Instantiate(gameSettings.LevelMono);
            
            //creating logic
            SavedProgress savedProgress = new SavedProgress();
            InputProvider inputProvider = new InputProvider();
            LevelController levelController = new LevelController(levelMono, savedProgress);
            MultiplayerPhoton multiplayer = levelMono.gameObject.AddComponent<MultiplayerPhoton>();
            multiplayer.Inject(levelController);
            LevelObjectsFactory objectsFactory = new LevelObjectsFactory(inputProvider, 
                gameSettings, 
                levelController, 
                levelMono,
                multiplayer);
            levelMono.UIController.Inject(gameSettings, savedProgress);

            //starting level
            LevelCreator levelCreator = levelMono.gameObject.AddComponent<LevelCreator>();
            levelCreator.Inject(levelMono, objectsFactory, levelController);
        }
    }
}
