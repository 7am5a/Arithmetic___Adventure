using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arithmetic___Adventure.Core
{
    class EquationGenerator : Component
    {
        public int gameLevel = 1;
        

        internal override void LoadContent(ContentManager Content)
        {

            Random randomNumberGenerator = new Random();
            int num1;
            string num1String;

            int num2;
            string num2String;

            int symbol = 0;
            string symbolString = "+";

            string generatedEquation;
            int equationAnswer;

            //generowanie losowych cyfr elementów działania matematycznego
            num1 = randomNumberGenerator.Next(1, 10);
            num2 = randomNumberGenerator.Next(0, 10);                       

            //losowanie znaków +, -, * w zależności od posiomu rozgrywki
            if (gameLevel == 1)
            {
                symbol = 0;
            }
            else if(gameLevel == 2)
            {
                symbol = randomNumberGenerator.Next(0, 1);
            }
            else if(gameLevel == 3)
            {
                symbol = randomNumberGenerator.Next(0, 2);
            }

            //zabezpieczneie przed wylosowaniem działania (odejmowanie) dającego ujemny wynik
            for(int i = 1; i > 0; )
            {
                if(symbol == 1 && num1 < num2)
                {
                    num2 = randomNumberGenerator.Next(0, 10);
                }
                else
                {
                    i = 0;
                }
            }

            //zamiana zmiennej znaku na jej symbol, 0 dla +, 1 dla -, 2 dla *
            if (symbol == 0)
            {
                symbolString = "+";
            }
            else if (symbol == 1)
            {
                symbolString = "-";
            }
            else if (symbol == 2)
            {
                symbolString = "*";
            }

            num1String = num1.ToString();
            num2String = num2.ToString();

            //wykonanie działania i przygotowanie wyniku
            if (symbol == 0)
            {
                equationAnswer = num1 + num2;
            }
            else if (symbol == 1)
            {
                equationAnswer = num1 - num2;
            }
            else if (symbol == 2)
            {
                equationAnswer = num1 * num2;
            }

            generatedEquation = num1String + " " + symbolString + " " + num2String;
            //potrzebne dane to:
            /*
             * equationAnswer do wartości na kuli
             * generatedEquation do wypisania na cegle
             */
        }

        internal override void Update(GameTime gameTime)
        {
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
        }


    }
}
