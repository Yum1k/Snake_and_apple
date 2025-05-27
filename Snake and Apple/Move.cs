using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_and_Apple
{
    internal class Move
    {
        public (int newX, int newY) MoveSnake(ConsoleKey charKey, char[,] map, int snakeX, int snakeY)
        {
            switch(charKey)
            {
                case ConsoleKey.UpArrow:
                    if (map[snakeX - 1, snakeY] != '!') snakeX--;
                    break;
                case ConsoleKey.DownArrow:
                    if (map[snakeX + 1, snakeY] != '!') snakeX++;
                    break;
                case ConsoleKey.LeftArrow:
                    if (map[snakeX, snakeY - 1] != '!') snakeY--;
                    break;
                case ConsoleKey.RightArrow:
                    if (map[snakeX, snakeY + 1] != '!') snakeY++;
                    break;
            }

            return (snakeX, snakeY);
        }
    }
}
