using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
    /// <summary>
    /// Część danych ogólnych programu
    /// </summary>
   public static class Data
    {
        /// <summary>
        /// Ustawienie stałej szerokości okna
        /// </summary>
        public static int ScreenWid { get; set; } = 1280;
        /// <summary>
        /// Ustawienie stałej wysokości okna
        /// </summary>
        public static int ScreenHei { get; set; } = 720;
        /// <summary>
        /// Zmienna do wyjscia z programu
        /// </summary>
        public static bool Exit { get; set; } = false;
        /// <summary>
        /// Wykorzystane scenny w grze
        /// </summary>
        public enum Scenes { Menu, Game1, Summary, Game2, Game3, HowToPlay, Story }
        /// <summary>
        /// Ustawienie pierwszej, domyślnej sceny - Menu
        /// </summary>
        public static Scenes CurrentState { get; set; } = Scenes.Menu;
    }
}
