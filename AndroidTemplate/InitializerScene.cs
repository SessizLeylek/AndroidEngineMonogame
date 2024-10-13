using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class InitializerScene : GameScene
    {
        public InitializerScene(GameManager _gameManager) : base(_gameManager)
        {
            textures = new string[0];
            sounds = new string[0];
            backgroundColor = Color.DarkBlue;
        }
    }
}
