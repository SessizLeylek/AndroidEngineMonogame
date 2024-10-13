using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AndroidTemplate
{
    public class TouchInputDrag : TouchInputState
    {
        public Vector2 InitialDragPosition { get; }
        public Vector2 CurrentDragPosition { get; }
        public float DragDuration { get; }

        /// <summary>
        /// Stores the information of current drag input
        /// </summary>
        public TouchInputDrag(Vector2 initialDragPosition, Vector2 currentDragPosition, float dragDuration)
        {
            InitialDragPosition = initialDragPosition;
            CurrentDragPosition = currentDragPosition;
            DragDuration = dragDuration;
        }
    }
}
