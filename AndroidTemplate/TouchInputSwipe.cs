using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class TouchInputSwipe : TouchInputState
    {
        public Vector2 SwipeStartPosition { get; }
        public Vector2 SwipeEndPosition { get; }

        /// <summary>
        /// Stores the information of recent swipe
        /// </summary>
        public TouchInputSwipe(Vector2 swipeStartPosition, Vector2 swipeEndPosition)
        {
            SwipeStartPosition = swipeStartPosition;
            SwipeEndPosition = swipeEndPosition;
        }
    }
}
