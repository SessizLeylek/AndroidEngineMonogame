using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{

    public class TouchInputTap : TouchInputState
    {
        public Vector2 TapPosition { get; }
        public int NumberOfTaps { get; }

        /// <summary>
        /// Stores the information of recent tap
        /// </summary>
        public TouchInputTap (Vector2 tapPosition, int numberOfTaps)
        {
            TapPosition = tapPosition;
            NumberOfTaps = numberOfTaps;
        }
    }
}
