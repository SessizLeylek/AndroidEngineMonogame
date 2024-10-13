using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class EntityText
    {
        public string fontName;
        public string text;
        public double initialTime;
        public float transitionDuration;
        public Vector2 deltaPosition;
        public float rotation;
        public Color color;
        public float sizeMultiplier;

        /// <summary>
        /// Properties of the text to draw on a graphical entity
        /// </summary>
        /// <param name="fontName">Name of the font defined in game manager</param>
        /// <param name="text">Text to be written</param>
        /// <param name="previousTime">(Animation only) Start time of the animation in seconds</param>
        /// <param name="transitionDuration">(Animation only) Duration of the animation in seconds</param>
        /// <param name="deltaPosition">Position of the text related to the graphical entity</param>
        /// <param name="rotation">Rotation of the text</param>
        /// <param name="color">Color of the text</param>
        /// <param name="sizeMultiplier">Size multiplier will be multiplied with font size</param>
        public EntityText(string fontName, string text, string previousText, double previousTime, float transitionDuration, Vector2 deltaPosition, float rotation, Color color, float sizeMultiplier)
        {
            this.fontName = fontName;
            this.text = text;
            this.previousText = previousText;
            this.initialTime = previousTime;
            this.transitionDuration = transitionDuration;
            this.deltaPosition = deltaPosition;
            this.rotation = rotation;
            this.color = color;
            this.sizeMultiplier = sizeMultiplier;
        }
    }
}
