using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class SpriteProperties
    {
        public enum EaseMethod { Instant, Linear, In, Out };

        public string textureName;
        public Vector2 position;
        public float rotation;
        public Vector2 scale;
        public Rectangle boundaries;
        public Vector2 origin;
        public Color color;
        public SpriteEffects spriteEffect;
        public float frameDuration;
        public EaseMethod easeMethod;
        public double targetTime = -1;

        /// <summary>
        /// Stores the properties of a sprite
        /// </summary>
        /// <param name="textureName">Name of the texture</param>
        /// <param name="position">Position of the sprite where width of a canvas is 360</param>
        /// <param name="rotation">Rotation of the sprite in angles</param>
        /// <param name="scale">Scale of the sprite</param>
        /// <param name="boundaries">Upper left corner and the sizes of the sprite</param>
        /// <param name="origin">Origin point of the sprite. (0,0) upper left, (1, 1) bottom right</param>
        /// <param name="color">Color of the sprite, white is default</param>
        /// <param name="spriteEffect">Flipping of the sprite; horizontal, vertical or none</param>
        /// <param name="frameDuration">(Animations only) How long this sprite will be shown, in seconds</param>
        /// <param name="easeMethod">(Animations only) How the transition will be</param>
        public SpriteProperties(string textureName, Vector2 position, float rotation, Vector2 scale, Rectangle boundaries, Vector2 origin, Color color, SpriteEffects spriteEffect, float frameDuration, EaseMethod easeMethod)
        {
            this.textureName = textureName;
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
            this.boundaries = boundaries;
            this.origin = origin;
            this.color = color;
            this.spriteEffect = spriteEffect;
            this.frameDuration = frameDuration;
            this.easeMethod = easeMethod;
        }
    }
}
