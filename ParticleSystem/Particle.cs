using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ParticleSystem
{
    class Particle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 Velocity;
        public Vector2 Approach;


        public Particle(float x, float y)
        {
            X = x;
            Y = y;
            Velocity = Vector2.Zero;
            Approach = Vector2.Zero;
        }

        public void ApproachMouse(float x, float y)
        {
            float dX = x - X;
            float dY = y - Y;

            double dir = Math.Atan2((X - x), (Y - y));// / Math.PI * 180;


            //Approach.X = (float)(Math.Sin(dX / 1920) * Math.Max(Math.Abs(dX * 0.01f), 2.0f));
            //Approach.Y = (float)(Math.Sin(dY / 1080) * Math.Max(Math.Abs(dY * 0.01f), 2.0f));

            Approach.X += (float)(-Math.Sin(dir) / 10); //* Math.PI / 180) / 5);
            Approach.Y += (float)(-Math.Cos(dir) / 10); //* Math.PI / 180) / 5);


            //Approach.X *= 0.996f;
            //Approach.Y *= 0.996f;
        }
    }
}