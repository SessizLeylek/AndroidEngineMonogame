using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class GraphicalEntity : Entity
    {
        public List<SpriteProperties> spriteAnimation;
        public EntityText text;
        public Vector2 position;
        public float verticalAlign = 1; //between 0 and 2; 0 is aligned to top, 2 is aligned to bottom, 1 is aligned to center
        public float layer; //between 0 and 1. 0 is back, 1 is front.

        public GraphicalEntity(GameManager _gameManager) : base(_gameManager)
        {

        }

    }
}
