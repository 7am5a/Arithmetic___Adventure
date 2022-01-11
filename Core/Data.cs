using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
   public static class Data
    {
        //ustawienie rozmiaru okna
        public static int ScreenWid { get; set; } = 1280;
        public static int ScreenHei { get; set; } = 720;
        //
        public static bool Exit { get; set; } = false;

        public enum Scenes { Menu, Game1, Game2, Game3, Score}
        public static Scenes CurrentState { get; set; } = Scenes.Menu;
    }
}
