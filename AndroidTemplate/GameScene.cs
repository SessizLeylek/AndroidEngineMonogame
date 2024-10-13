using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class GameScene
    {
        public string[] textures = { };
        public string[] sounds = { };

        public GameManager gameManager;
        public Color backgroundColor = Color.Turquoise;
        public List<Entity> entities = new List<Entity>();

        public GameScene(GameManager _gameManager)
        {
            gameManager = _gameManager;
        }

        public virtual void InitializeScene()
        {
            gameManager.canvasColor = backgroundColor;
            gameManager.ReloadContent(textures, sounds);

            foreach (Entity entity in entities)
            {
                entity.OnCreate();
            }
        }

        public virtual void DestroyScene()
        {
            foreach (Entity entity in entities)
            {
                entity.Destroy();
            }
        }
    }
}
