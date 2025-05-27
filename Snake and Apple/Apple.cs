using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_and_Apple
{
    internal class Apple
    {
        static Random random = new Random();

        public void PlaceApple(char[,] map, List<(int x, int y)> snakeBody, out int appleX, out int appleY)
        {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);

            do
            {
                appleX = random.Next(1, rows - 1);
                appleY = random.Next(1, cols - 1);
            }
            while (map[appleX, appleY] == '#' || snakeBody.Contains((appleX, appleY)));

            map[appleX, appleY] = '@';
        }
    }
}
