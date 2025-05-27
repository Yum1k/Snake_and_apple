
using System.Runtime.CompilerServices;
using Snake_and_Apple;

class Program()
{
    static void Main()
    {
        char[,] map =
            {
                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#'},
                {'#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#'}
            };
        // Начальная позиция головы змейки
        int snakeX = 6, snakeY = 6;
        // Тело змейки
        var snakeBody = new List<(int x, int y)> { (snakeX, snakeY) };
        int score = 0;
        int appleX = -1, appleY = -1;

        bool isPaused = false;

        ConsoleKey lastDirection = ConsoleKey.RightArrow;
        Move move = new Move();
        Apple apple = new Apple();
        Collision collision = new Collision();

        // Размещаем первое яблоко
        apple.PlaceApple(map, snakeBody, out appleX, out appleY);

        while (true)
        {
            // Очистка консоли
            Console.Clear();

            // Отрисовка карты и тела змейки
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    bool isSnake = false;
                    foreach (var part in snakeBody)
                    {
                        if (part.x == i && part.y == j)
                        {
                            Console.Write('*');
                            isSnake = true;
                            break;
                        }
                    }

                    if (!isSnake)
                    {
                        Console.Write(map[i, j]);
                    }
                }
                Console.WriteLine();
            }

            // Отображение счёта
            Console.SetCursorPosition(0, map.GetLength(0) + 1);
            Console.WriteLine($"Счет: {score}");

            // Проверяем клавиши
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                // Переключение паузы
                if (key == ConsoleKey.P)
                {
                    isPaused = !isPaused;

                    // Очищаем строку под счётом
                    Console.SetCursorPosition(0, map.GetLength(0) + 2);
                    Console.Write(new string(' ', Console.WindowWidth));

                    if (isPaused)
                    {
                        Console.SetCursorPosition(0, map.GetLength(0) + 2);
                        Console.WriteLine("Пауза. Нажмите английскую клавишу \"P\", чтобы продолжить...");
                    }
                }

                // Обновляем направление, если не пауза
                if (!isPaused && new[] {ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow}.Contains(key))
                {
                    lastDirection = key;
                }
            }

            // Если пауза — пропускаем шаг
            if(isPaused)
            {
                continue;
            }


            // Двигаем голову
            (int newHeadX, int newHeadY) = move.MoveSnake(lastDirection, map, snakeBody[0].x, snakeBody[0].y);

            // Проверяем столкновение со стеной или своим телом
            if (collision.CheckCollision(map, snakeBody, newHeadX, newHeadY))
            {
                Console.WriteLine("\nСтолкновение! Игра окончена.");
                Console.ReadKey();
                break;
            }

            // Добавляем новую голову
            snakeBody.Insert(0, (newHeadX, newHeadY));


            // Проверяем, съела ли змейка яблоко
            if (newHeadX == appleX && newHeadY == appleY)
            {
                score++;
                map[appleX, appleY] = ' '; // Очищаем ячейку старого яблока
                apple.PlaceApple(map, snakeBody, out appleX, out appleY); // Ставим новое
            }
            else
            {
                // Убираем хвост, если яблоко не съедено

                // Последний элемент змейки
                var tail = snakeBody[snakeBody.Count - 1];
                map[tail.x, tail.y] = ' ';
                snakeBody.RemoveAt(snakeBody.Count - 1);
            }

            // Задержка между шагами
            Thread.Sleep(100);
        }
    }
}


