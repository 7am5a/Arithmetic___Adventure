using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
    class Ammo
    {
        public int equationAnswer = 0;
        public string answerString = "a";

        public string Bullet()
        {
            answerString = equationAnswer.ToString(); 
            return answerString;
        }

    }
}
