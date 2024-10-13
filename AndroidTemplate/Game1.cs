using Android.App;
using Android.Content;
using Android.Widget;
using Java.IO;
using Java.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AndroidTemplate
{
    public class GameManager : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SceneManager _sceneManager;
        public SoundManager _soundManager = new();
        public TouchInputManager _inputManager;
        public InputDialog _inputDialog;
        public GameOutputConsole _outputConsole = new();
        public ProgressSaver _progressSaver = new();

        private bool gamePaused = false;
        private float timeScaler = 1;

        private List<Entity> _newEntities = new List<Entity>();
        private List<Entity> _entitiesToBeDestroyed = new List<Entity>();
        private List<Entity> _entities = new List<Entity>();

        private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, SpriteFont> _fonts = new Dictionary<string, SpriteFont>();

        private List<GraphicalEntity> _graEntities = new List<GraphicalEntity>();
        private List<GraphicalEntity> _newGraEntities = new List<GraphicalEntity>();
        private List<GraphicalEntity> _graEntitiesToBeDestroyed = new List<GraphicalEntity>();

        private float canvasMultiplier = 1;
        private Vector2 canvasCenterDelta;
        public Color canvasColor;

        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _inputManager = new(new Vector2(Window.ClientBounds.Width, Window.ClientBounds.Height));

            _graphics.IsFullScreen = true;
            canvasMultiplier = Window.ClientBounds.Width / 360f;
            canvasCenterDelta = new Vector2(Window.ClientBounds.Width * 0.5f, Window.ClientBounds.Height * 0.5f);

            _outputConsole.ConsolePrint(_progressSaver.filePath);
            _sceneManager = new(this);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            _fonts.Add("ConsoleFont", Content.Load<SpriteFont>("Fonts/ConsoleFont"));
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            _inputManager.InputUpdate(gameTime);

            //_inputDialog.ShowInputDialog("testing", _outputConsole.PrintSavedValue);
            if (GamePad.GetState(0).IsButtonDown(Buttons.Back))
                gamePaused = !gamePaused;

            #region ENTITY_UPDATE
            foreach (GraphicalEntity newEn in _newGraEntities)
            {
                _graEntities.Add(newEn);
            }
            _newGraEntities.Clear();

            foreach (GraphicalEntity oldEn in _graEntitiesToBeDestroyed)
            {
                _graEntities.Remove(oldEn);
            }
            _graEntitiesToBeDestroyed.Clear();

            foreach (Entity en in _entities)
            {
                if(!gamePaused || en.ignorePause)
                    en.Update(gameTime, timeScaler);
            }

            foreach(Entity newEn in _newEntities)
            {
                _entities.Add(newEn);
            }
            _newEntities.Clear();

            foreach (Entity oldEn in _entitiesToBeDestroyed)
            {
                _entities.Remove(oldEn);
            }
            _entitiesToBeDestroyed.Clear();
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(canvasColor);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);

            _spriteBatch.DrawString(_fonts["ConsoleFont"], _outputConsole.GetConsoleOutput(), Vector2.One * 8, Vector3.Dot(canvasColor.ToVector3(), Vector3.One) < 1.5f ? Color.White : Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            // Rendering the graphical entities
            foreach (GraphicalEntity grapEn in _graEntities)
            {
                if (grapEn.spriteAnimation.Count == 1)
                {
                    //Drawing the sprite directly
                    SpriteProperties spriteProp = grapEn.spriteAnimation[0];
                    _spriteBatch.Draw(_textures[spriteProp.textureName], (spriteProp.position + grapEn.position) * canvasMultiplier + canvasCenterDelta * (Vector2.UnitY * grapEn.verticalAlign), spriteProp.boundaries, spriteProp.color, spriteProp.rotation, spriteProp.origin * canvasMultiplier, spriteProp.scale, spriteProp.spriteEffect, grapEn.layer);
                }
                else if (grapEn.spriteAnimation.Count > 1)
                {
                    //Transition between two frames
                    SpriteProperties spritePropPrev = grapEn.spriteAnimation[0];
                    SpriteProperties spritePropNext = grapEn.spriteAnimation[1];

                    if (spritePropNext.targetTime == -1)
                        spritePropPrev.targetTime = gameTime.TotalGameTime.TotalSeconds + spritePropPrev.frameDuration;

                    //Setting the tween multplier. goes from 1 to 0
                    float tweenMultiplier = (float)(spritePropNext.targetTime - gameTime.TotalGameTime.TotalSeconds) / spritePropNext.frameDuration;  //1 at the start, 0 at the end
                    switch (spritePropPrev.easeMethod)
                    {
                        case SpriteProperties.EaseMethod.Instant:
                            tweenMultiplier = 0;
                            break;
                        case SpriteProperties.EaseMethod.In:
                            tweenMultiplier *= tweenMultiplier;
                            break;
                        case SpriteProperties.EaseMethod.Out:
                            tweenMultiplier *= 2 - tweenMultiplier;
                            break;
                    }

                    //Ease lambda functions
                    var EaseFloat = (float float0, float float1) => float0 * tweenMultiplier + float1 * (1 - tweenMultiplier);
                    var EaseVector2 = (Vector2 vector0, Vector2 vector1) => Vector2.Lerp(vector1, vector0, tweenMultiplier);
                    var EaseColor = (Color color0, Color color1) => Color.Lerp(color1, color0, tweenMultiplier);

                    //Drawing the sprite
                    _spriteBatch.Draw(_textures[spritePropPrev.textureName], (EaseVector2(spritePropPrev.position, spritePropNext.position) + grapEn.position) * canvasMultiplier + canvasCenterDelta * (Vector2.UnitY * grapEn.verticalAlign),
                        spritePropPrev.boundaries,EaseColor(spritePropPrev.color, spritePropNext.color), EaseFloat(spritePropPrev.rotation, spritePropNext.rotation),
                        EaseVector2(spritePropPrev.origin, spritePropNext.origin) * canvasMultiplier, EaseVector2(spritePropPrev.scale, spritePropNext.scale), spritePropPrev.spriteEffect, grapEn.layer);

                }

                //Drawing the text
                float lerpValue = (float)Math.Min(1, ((gameTime.TotalGameTime.TotalSeconds - grapEn.text.initialTime) / grapEn.text.transitionDuration));
                _spriteBatch.DrawString(_fonts[grapEn.text.fontName], grapEn.text.text.Substring(0, (int)(grapEn.text.text.Length * lerpValue)), (grapEn.position + grapEn.text.deltaPosition) * canvasMultiplier + canvasCenterDelta, grapEn.text.color, grapEn.text.rotation, Vector2.Zero, 1, SpriteEffects.None, 0);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ReloadContent(string[] textures, string[] sounds)
        {
            _textures.Clear();
            _soundManager._sounds.Clear();

            foreach(string tex in textures )
            {
                _textures.Add(tex, Content.Load<Texture2D>("Content/Textures/" + tex));
            }
            foreach (string snd in sounds)
            {
                _soundManager._sounds.Add(snd, Content.Load<SoundEffect>("Content/Sounds/" + snd));
            }
        }

        public void CreateEntity(Entity newEntity)
        {
            _newEntities.Add(newEntity);
            if (newEntity is GraphicalEntity)
                _newGraEntities.Add(newEntity as GraphicalEntity);
        }

        public void DestroyEntity(Entity entityToBeDestroyed)
        {
            _entitiesToBeDestroyed.Add(entityToBeDestroyed);
            if (entityToBeDestroyed is GraphicalEntity)
                _graEntitiesToBeDestroyed.Add(entityToBeDestroyed as GraphicalEntity);
        }

        /// <summary>
        /// Returns real coordinates of given in game coordinates
        /// </summary>
        /// <param name="inGameCoordinates">In Game Coordinates of Canvas with width 360 unit</param>
        /// <param name="verticalAlign">Alignment; 0 is anchored to top, 2 is anchored to bottom</param>
        /// <returns>Real Coordinates (with Screen width and height)</returns>
        public Vector2 ReturnRealCoordinates(Vector2 inGameCoordinates, float verticalAlign = 1)
        {
            return inGameCoordinates * canvasMultiplier + canvasCenterDelta * (Vector2.UnitY * verticalAlign);
        }
    }
}