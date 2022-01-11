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
        public int gameLevel;
        public string UPgeneratedEquation;

        public int GenereEQ(int _gameLevel)
        {
            
            gameLevel = _gameLevel;

            Random randomNumberGenerator = new Random();
            int num1;
            string num1String;

            int num2;
            string num2String;

            int symbol = 0;
            string symbolString = "&";

            string generatedEquation;
            int equationAnswer = 0;

            //generowanie losowych cyfr elementów działania matematycznego
            num1 = randomNumberGenerator.Next(1, 11);
            num2 = randomNumberGenerator.Next(1, 11);

            //losowanie znaków +, -, * w zależności od posiomu rozgrywki
            if (gameLevel == 1)
            {
                symbol = 0;
            }
            else if (gameLevel == 2)
            {
                symbol = randomNumberGenerator.Next(0, 2);
            }
            else if (gameLevel == 3)
            {
                symbol = randomNumberGenerator.Next(0, 3);
            }

            //zabezpieczenie przed dodawaniem zbyt dużych liczb
            for (int i = 1; i > 0;)
            {
                if (symbol == 0 && num1 + num2 >= 21)
                {
                    num2 = randomNumberGenerator.Next(0, 10);
                }
                else
                {
                    i = 0;
                }
            }

            //zabezpieczneie przed wylosowaniem działania (odejmowanie) dającego ujemny wynik
            for (int i = 1; i > 0;)
            {
                if (symbol == 1 && num1 < num2)
                {
                    num2 = randomNumberGenerator.Next(0, 10);
                }
                else
                {
                    i = 0;
                }
            }

            //zmniejszenie zakresu czynników mnożenia
            if(symbol == 2)
            {
                num1 = randomNumberGenerator.Next(1, 11);
                num2 = randomNumberGenerator.Next(1, 11);
            }

            //zmniejszenie losowanych wartości do działań z mnożeniem
            for (int i = 1; i > 0;)
            {
                if (symbol == 2 && num1 + num2 > 11)
                {
                    num2 = randomNumberGenerator.Next(0, 20);
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
            UPgeneratedEquation = generatedEquation;
            //potrzebne dane to:
            /*
             * equationAnswer do wartości na kuli
             * generatedEquation do wypisania na cegle
             */
            return equationAnswer;
        }

        public string StringEQ()
        {
            return UPgeneratedEquation;
        }

        internal override void LoadContent(ContentManager Content)
        {

            
        }

        internal override void Update(GameTime gameTime)
        {
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
        }


    }
}
