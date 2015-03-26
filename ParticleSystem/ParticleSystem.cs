using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ParticleSystem
{
    class ParticleSystem
    {
        List<Particle> particleList;
        Texture2D pix;
        Random randy;
        Color particleColor;
        bool appr = false;
        MouseState oldState;

        Color[,] colors2D;

        public int Amount { get; set; }
        int MaxAmount, oldScroll;

        bool reduceVel = false;

        public ParticleSystem(int amount, float x, float y, bool reduceVel ,Texture2D pix, Color particleColor, Random randy)
        {
            particleList = new List<Particle>();
            this.pix = pix;
            this.particleColor = particleColor;
            this.randy = randy;
            this.Amount = amount;
            MaxAmount = Amount;
            oldScroll = 0;

            this.reduceVel = reduceVel;

            //Color[] colors = new Color[back.Width * back.Height];
            //back.GetData<Color>(colors);

            //colors2D = new Color[back.Width, back.Height];
            //for (int i = 0; i < back.Width; i++)
            //    for (int j = 0; j < back.Height; j++)
            //        colors2D[i, j] = colors[i + j * back.Width];

            oldState = Mouse.GetState();

            for (int i = 0; i < Amount; i++)
            {
                //particleList.Add(new Particle(randy.Next(1281), randy.Next(721)));
                particleList.Add(new Particle(x, y));
            }
        }

        public void Update(MouseState mouse, GraphicsDeviceManager graphics)
        {

            if (Amount + ((mouse.ScrollWheelValue - oldScroll) / 3) <= MaxAmount)
                Amount += (mouse.ScrollWheelValue - oldScroll) / 3;

            oldScroll = mouse.ScrollWheelValue;

                foreach (Particle p in particleList)
                {

                    if (mouse.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                    {
                        appr = !appr;
                    }
                    if (mouse.RightButton == ButtonState.Pressed)
                    {
                        p.Velocity.Y += 1;
                    }

                    oldState = mouse;

                    if (appr)
                    {
                        p.ApproachMouse(mouse.X, mouse.Y);

                        if (reduceVel)
                        {
                            p.Approach.X *= 0.996f;
                            p.Approach.Y *= 0.996f;
                        }
                    }
                    else
                    {
                        p.Approach = Vector2.Zero;

                        p.Velocity.X += (float)(randy.Next(-1, 2) * 0.1f);
                        p.Velocity.Y += (float)(randy.Next(-1, 2) * 0.1f);
                    }


                    p.X += p.Velocity.X + p.Approach.X;
                    p.Y += p.Velocity.Y + p.Approach.Y;

                    if (p.X > graphics.PreferredBackBufferWidth)
                    {
                        p.X = graphics.PreferredBackBufferWidth;
                        p.Velocity.X *= -1.0f;
                        p.Approach.X *= -1.0f;
                    }

                    if (p.X < 0)
                    {
                        p.X = 0;
                        p.Velocity.X *= -1.0f;
                        p.Approach.X *= -1.0f;
                    }

                    if (p.Y > graphics.PreferredBackBufferHeight)
                    {
                        p.Y = graphics.PreferredBackBufferHeight;
                        p.Velocity.Y *= -0.8f;
                        p.Approach.Y *= -0.8f;
                    }

                    if (p.Y < 0)
                    {
                        p.Y = 0;
                        p.Velocity.Y *= -1.0f;
                        p.Approach.Y *= -1.0f;
                    }

                }
        }

        public void Draw(SpriteBatch spr)
        {
            for(int i = 0; i < Amount; i++)
            {
                spr.Draw(pix, new Vector2(particleList[i].X, particleList[i].Y), particleColor);
            }
        }
    }
}
