using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System;

namespace Projeto1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D par, pon;
        public Texture2D pla,bol; 
        private char[,] level;
        public int tile = 32;
        private player Player;
        public int diamonds;
        public List <Point> Boulders;
        private string[] lvls = { "lvl.txt","lvl2.txt" };
        private int lvlcount = 0;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            LoadLvl(lvls[lvlcount]);
            _graphics.PreferredBackBufferWidth = tile * level.GetLength(1);
            _graphics.PreferredBackBufferHeight = tile * (level.GetLength(0));
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            par = Content.Load<Texture2D>("CrateDark_Brown");
            pon = Content.Load<Texture2D>("EndPoint_Beige");
            pla = Content.Load<Texture2D>("Wall_Gray");
            bol = Content.Load<Texture2D>("WallRound_Gray");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.R)) Initialize();


            if (diamonds == 0) {
                lvlcount++;
                Initialize();
            }
            // TODO: Add your update logic here
            Player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            Rectangle position = new Rectangle(0, 0, tile, tile);

            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    position.X = j * tile;
                    position.Y = i * tile;
                    switch (level[i, j])
                    {
                        case 'X':
                            _spriteBatch.Draw(par, position, Color.White);
                            break;
                        case 'Y':
                            _spriteBatch.Draw(pon, position, Color.White);
                            break;
                        case ' ':
                            _spriteBatch.Draw(pla, position, Color.Black);
                            break;
                    }
                } }
            Player.DrawP(_spriteBatch);

            foreach (Point b in Boulders)
            {
                position.X = b.X*tile;
                position.Y = b.Y * tile;
                _spriteBatch.Draw(bol, position, Color.Blue);

            }



            // TODO: Add your drawing code here
            _spriteBatch.End();
                base.Draw(gameTime);
            }

            void LoadLvl(string lvl)
            {
            Boulders = new List<Point>();
            diamonds = 0;
                string[] linhas = File.ReadAllLines(path: $"Content/{lvl}");
                int a = linhas.Length;
                int b = linhas[0].Length;
                level = new char[a, b];

                for (int i = 0; i < a; i++)
                {
                for (int j = 0; j < b; j++)
                {
                    if (linhas[i][j] == 'W')
                    {
                        Player = new player(this, j, i);
                        level[i, j] = ' ';
                    }
                    else if (linhas[i][j] == 'B') {
                        Boulders.Add(new Point(j, i));
                        level[i, j] = ' ';
                            }
                    else
                    {
                        if (linhas[i][j] == 'Y')
                            diamonds++;
                        level[i, j] = linhas[i][j];
                    } }
                }
                Console.WriteLine(level.GetLength(0));

            }

        public void point(Point p)
        {
            if (level[p.Y, p.X] == 'Y')
            {
                diamonds--;
                level[p.Y, p.X] = 'f';
                Player.picareta++;
            }
        }
        public void mpoint(Point p)
        {
            if (level[p.Y, p.X] == 'X') Player.picareta--;
                level[p.Y, p.X] = 'f';
        }

        public bool HaveWall(Point p)
        {
            return (level[p.Y, p.X] == 'X');
        }

        public bool HaveDirt(Point p)
        {
            return (level[p.Y, p.X] == ' '||HasBou(p));
        }
        public bool HasBou(Point p)
        {
            foreach(Point b in Boulders)
            {
                if(b==p)return true;
            }
            return false;
        }
        public void PInitialize()
        {
            Initialize();
        }
    }
    } 
