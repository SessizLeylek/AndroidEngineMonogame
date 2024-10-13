using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class SceneManager
    {
        public Dictionary<string, GameScene> scenes = new Dictionary<string, GameScene>();
        private GameScene activeScene;
        private GameManager gameManager;

        /// <summary>
        /// Manages the scenes
        /// </summary>
        public SceneManager(GameManager _gameManager) 
        {
            gameManager = _gameManager;
            activeScene = new InitializerScene(_gameManager);
            activeScene.InitializeScene();
        }

        /// <summary>
        /// Terminates the currently active scene and initializes the new one
        /// </summary>
        /// <param name="_sceneName">Name of the scene</param>
        public void LoadScene(string _sceneName)
        {
            activeScene.DestroyScene();
            activeScene = scenes[_sceneName];
            activeScene.InitializeScene();
        }
    }
}
