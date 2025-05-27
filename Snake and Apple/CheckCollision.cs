using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_and_Apple
{
    internal class Collision
    {

        public bool CheckCollision(char[,] map, List<(int x, int y)> snakeBody, int x, int y)
        {
            // Проверка на столкновение со своим телом
            foreach (var segment in snakeBody)
            {
                if (segment.x == x && segment.y == y)
                {
                    return true;
                }
            }

            // Проверка на выход за границы или столкновение со стеной
            if (x < 0 || x >= map.GetLength(0) ||
                y < 0 || y >= map.GetLength(1) ||
                map[x, y] == '#')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
