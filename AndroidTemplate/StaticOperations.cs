using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class StaticOperations
    {
        /// <summary>
        /// Checks if a given point is inside a region between two points
        /// </summary>
        /// <param name="regionCorner0">Any corner of the region</param>
        /// <param name="regionCorner1">Opposite corner of the region</param>
        /// <param name="subject">Point to check if it is inside</param>
        public static bool IsVector2Inside(Vector2 regionCorner0, Vector2 regionCorner1, Vector2 subject)
        {
            Vector2 checkVector = (subject - regionCorner0) * (subject - regionCorner1);
            return checkVector.X < 0 && checkVector.Y < 0;
        }

        /// <summary>
        /// Checks if a given point is inside a region between two points
        /// </summary>
        /// <param name="regionCorner0">Any corner of the region</param>
        /// <param name="regionCorner1">Opposite corner of the region</param>
        /// <param name="subject">Point to check if it is inside</param>
        public static bool IsVector3Inside(Vector3 regionCorner0, Vector3 regionCorner1, Vector3 subject)
        {
            Vector3 checkVector = (subject - regionCorner0) * (subject - regionCorner1);
            return checkVector.X < 0 && checkVector.Y < 0 && checkVector.Z < 0;
        }
    }
}
