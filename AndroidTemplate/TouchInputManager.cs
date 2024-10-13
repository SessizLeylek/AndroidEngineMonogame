using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidTemplate
{
    public class TouchInputManager
    {
        private Vector2 screenSize;
        private int minSwipeRange = 0;              //Swipes less than this value are ignored

        private bool isUserTouching = false;        //Is user is touching right now
        private bool previouslyTouched = false;     //Did user touched in the previous frame
        private double initalTouchTime;             //When was user touched the screen
        private double currentTime;                 //Current time
        private bool passedHoldThreshold;           //Is user touching longer than 225 ms
        private Vector2 firstTouchPosition;         //Where did user touch first
        private Vector2 currentTouchPosition;       //Where user is touching right now
        private bool leftTheInitialArea = false;    //Did user moved
        private bool recentFingerUp;                //Did user stop touching recently
        private int numberOfTaps = 0;               //How many times did user tap recently
        private TouchInputState currentState;       //Input State in the current frame

        public TouchInputManager(Vector2 _screenSize)
        {
            screenSize = _screenSize;
            minSwipeRange = (int)(_screenSize.X / 15);
        }

        public void InputUpdate(GameTime gameTime)
        {
            currentTime = gameTime.TotalGameTime.TotalSeconds;
            recentFingerUp = false;
            passedHoldThreshold = currentTime - initalTouchTime > 0.225f;

            TouchCollection touchLocations = TouchPanel.GetState();

            previouslyTouched = isUserTouching;
            if (touchLocations.Count > 0)
            {
                if (isUserTouching)
                {
                    //Continue touching
                    currentTouchPosition = touchLocations[0].Position;

                    if (!leftTheInitialArea)
                        leftTheInitialArea = Vector2.Distance(firstTouchPosition, currentTouchPosition) > minSwipeRange;
                }
                else
                {
                    //Touch for the first time
                    isUserTouching = true;
                    leftTheInitialArea = false;
                    initalTouchTime = currentTime;
                    firstTouchPosition = touchLocations[0].Position;
                    currentTouchPosition = touchLocations[0].Position;
                }
            }
            else
            {
                if (isUserTouching)
                {
                    //Stop touching
                    isUserTouching = false;
                    if (!passedHoldThreshold)
                    {
                        recentFingerUp = true;
                        numberOfTaps++;
                    }
                }
                else
                {
                    //Continue not touching
                    if (passedHoldThreshold)
                        numberOfTaps = 0;
                }
            }

            UpdateInputState();
        }

        public bool IsUserTouching()
        {
            return isUserTouching;
        }

        private void UpdateInputState()
        {
            if (isUserTouching && passedHoldThreshold)
            {
                if (leftTheInitialArea)
                    //  DRAG
                    currentState = new TouchInputDrag(firstTouchPosition, currentTouchPosition, (float)(currentTime - initalTouchTime));
                else
                    //  HOLD
                    currentState = new TouchInputHold(currentTouchPosition, (float)(currentTime - initalTouchTime));
            }
            else if (recentFingerUp)
            {
                if (leftTheInitialArea)
                    //  SWIPE
                    currentState = new TouchInputSwipe(firstTouchPosition, currentTouchPosition);
                else
                    //TAP
                    currentState = new TouchInputTap(firstTouchPosition, numberOfTaps);
            }
            else
                //  NOTHING
                currentState = null;
        }

        /// <summary>
        /// Returns the current state of touch input
        /// </summary>
        /// <returns>Drag, Hold, Swipe or Tap</returns>
        public TouchInputState GetState()
        {
            return currentState;
        }

        /// <summary>
        /// Returns how many times the user touched inside the region
        /// </summary>
        /// <param name="regionCorner0">A corner of the region (in real coordinates)</param>
        /// <param name="regionCorner1">Opposite corner of the region (in real coordinates)</param>
        /// <returns>Times touched</returns>
        public int TimesTouchedInsideRegion(Vector2 regionCorner0, Vector2 regionCorner1)
        {
            if (currentState is TouchInputTap)
            {
                TouchInputTap _state = (TouchInputTap)GetState();
                if (StaticOperations.IsVector2Inside(regionCorner0, regionCorner1, _state.TapPosition))
                    return 0;
                else
                    return _state.NumberOfTaps;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns how many times the user touched inside a circle with given values
        /// </summary>
        /// <param name="circleCenter">Center of the circle (in real coordinates)</param>
        /// <param name="circleRadius">Radius of the circle (in real coordinates)</param>
        /// <returns>Times touched</returns>
        public int TimesTouchedInsideCircle(Vector2 circleCenter, float circleRadius)
        {
            if (currentState is TouchInputTap)
            {
                TouchInputTap _state = (TouchInputTap)GetState();
                if (Vector2.Distance(circleCenter, _state.TapPosition) < circleRadius)
                    return 0;
                else
                    return _state.NumberOfTaps;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns how long the user is touching inside a circle given values
        /// </summary>
        /// <param name="circleCenter">Center of the circle (in real coordinates)</param>
        /// <param name="circleRadius">Radius of the circle (in real coordinates)</param>
        /// <returns>Duration of touching in seconds</returns>
        public float DurationHoldingInsideRegion(Vector2 circleCenter, float circleRadius)
        {
            if (currentState is TouchInputHold)
            {
                TouchInputHold _state = (TouchInputHold)GetState();
                if (Vector2.Distance(circleCenter, _state.HoldPosition) < circleRadius)
                    return 0;
                else
                    return _state.HoldDuration;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Returns how long the user is touching inside the region
        /// </summary>
        /// <param name="regionCorner0">A corner of the region (in real coordinates)</param>
        /// <param name="regionCorner1">Opposite corner of the region (in real coordinates)</param>
        /// <returns>Duration of touching in seconds</returns>
        public float DurationHoldingInsideRegion(Vector2 regionCorner0, Vector2 regionCorner1)
        {
            if (currentState is TouchInputHold)
            {
                TouchInputHold _state = (TouchInputHold)GetState();
                if (StaticOperations.IsVector2Inside(regionCorner0, regionCorner1, _state.HoldPosition))
                    return 0;
                else
                    return _state.HoldDuration;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Did user started touching in this frame
        /// </summary>
        public bool DidUserStartedTouching()
        {
            return !previouslyTouched && isUserTouching;
        }

        /// <summary>
        /// Did user stopped touching in this frame
        /// </summary>
        public bool DidUserStoppedTouching()
        {
            return previouslyTouched && !isUserTouching;
        }

        /// <summary>
        /// Returns current touch position in real coordinates
        /// </summary>
        public Vector2 TouchPosition()
        {
            return isUserTouching ? currentTouchPosition : Vector2.Zero;
        }
    }
}
