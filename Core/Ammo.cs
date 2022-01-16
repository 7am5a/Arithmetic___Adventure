using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
    /// <summary>
    /// Klasa do obsługi odpowiedzi na amunicji do trebusza
    /// </summary>
    class Ammo
    {
        /// <summary>
        /// wartość liczbowa odpowiedzi
        /// </summary>
        public int equationAnswer = 0;
        /// <summary>
        /// wartość liczbowa odpowiedzi przekonwertowana na stringa
        /// </summary>
        public string answerString = "a";

        /// <summary>
        /// zwracanie wartości stringa
        /// </summary>
        /// <returns>zwraca wartość stringa</returns>
        public string Bullet()
        {
            answerString = equationAnswer.ToString(); 
            return answerString;
        }

    }
}
