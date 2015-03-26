using System;

namespace ParticleSystem
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            ResChooser chooser = new ResChooser();

            if (chooser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                using (Game1 game = new Game1(chooser.Width, chooser.Height, chooser.Amount, chooser.reduceVel))
                {
                    game.Run();
                }
            }
        }
    }
#endif
}

