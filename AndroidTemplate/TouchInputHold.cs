using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class TouchInputHold : TouchInputState
    {
        public Vector2 HoldPosition { get; }
        public float HoldDuration { get; }

        /// <summary>
        /// Stores the information of current hold
        /// </summary>
        public TouchInputHold(Vector2 holdPosition, float holdDuration)
        {
            HoldPosition = holdPosition;
            HoldDuration = holdDuration;
        }
    }
}
