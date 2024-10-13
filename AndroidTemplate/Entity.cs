using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class Entity
    {
        public GameManager gameManager;
        public bool ignorePause = false;

        public Entity(GameManager _gameManager)
        {
            gameManager = _gameManager;
        }

        public virtual void Update(GameTime gameTime, float timeScaler)
        {

        }

        public virtual void OnCreate()
        {
            gameManager.CreateEntity(this);
        }

        public virtual void Destroy() 
        {
            gameManager.DestroyEntity(this);
        }
    }
}
