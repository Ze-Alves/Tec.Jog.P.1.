using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Projeto1
{
    class player
    {
        private Point pos;
        private Game1 game;
        private bool move = true;
        bool BouldFall = false;
       public  int picareta = 0;

        public player(Game1 gam,int x,int y)
        {
            game = gam;
            pos.X = x;
            pos.Y = y;
        }
        public void MoveBuld()
        {

            if (BouldFall)
            {
                for (int i = 0; i < game.Boulders.Count; i++)
                {
                    Point c = game.Boulders[i];
                    c.Y++;
                    if (!game.HaveWall(c) && !game.HaveDirt(c)) game.Boulders[i] = c;
                }
            }
            if(move)
            foreach(Point b in game.Boulders)
            {
                if (pos == b) game.PInitialize();
            }
        }
        public void Update(GameTime g)
        {
            KeyboardState ks = Keyboard.GetState();
            Point laspos = pos;
            BouldFall = false;

            if (move)
            {
                move=false;

                if (ks.IsKeyDown(Keys.A))
                {
                BouldFall = true;
                    pos.X--;
                    Console.WriteLine(picareta);
                }
                else if (ks.IsKeyDown(Keys.D))
                {
                BouldFall = true;
                    pos.X++;
                }
                else if (ks.IsKeyDown(Keys.W))
                {
                BouldFall = true;
                    pos.Y--;
                }
                else if (ks.IsKeyDown(Keys.S))
                {
                BouldFall = true;
                    pos.Y++;
                }
                else
                {
                    move = true;
                }

                if (game.HaveWall(pos) && picareta == 0) { pos = laspos;}
                else if (game.HasBou(pos))
                {
                    int delx = pos.X - laspos.X;
                    int dely = pos.Y - laspos.Y;
                    Point MoveBoul = new Point(pos.X + delx, pos.Y + dely);
                    if (!game.HaveDirt(MoveBoul) && !game.HaveWall(MoveBoul))
                    {
                        for (int i = 0; i < game.Boulders.Count; i++)
                        {
                            if (game.Boulders[i] == pos)
                            {
                                game.Boulders[i] = MoveBoul;
                                BouldFall = false;
                            }
                        }
                    }
                    else pos = laspos;
                }

            }
            else if (ks.IsKeyUp(Keys.A) && ks.IsKeyUp(Keys.D) && ks.IsKeyUp(Keys.W) && ks.IsKeyUp(Keys.S)) move = true;
            game.point(pos);
            game.mpoint(pos);

            MoveBuld();
        }

        public void DrawP(SpriteBatch c)
        {
            Rectangle posi = new Rectangle(pos.X*32,pos.Y*32,game.tile,game.tile);
            c.Draw(game.pla,posi,Color.White);
        }
    }
}
