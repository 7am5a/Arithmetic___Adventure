using System;

namespace Arithmetic___Adventure.Core
{
    /// <summary>
    /// Głowna klasa programu
    /// </summary>
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
}
