using System;

namespace ConsoleApp13
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Переменные
            int[] xPosition = new int[50];
               xPosition[0]= 10;
            int[] YPosition = new int[50];
               YPosition[0]=10;
            int a;
            int appleX = 10;
            int appleY = 10;
            int appleEaten = 0;

            string User = "";



            bool isGameStatus = true;
            bool isStenka = false;
            bool isAppleEaten = false;
            bool Menu = true;

            Random random = new Random();

            Console.CursorVisible = false;




            #endregion

            do
            {

                // Экран приветсвтия
                ShowMenu(out User);
            


               switch (User)
               {
                  #region Case Инструкции
                case "1":
                    Console.Clear();
                    Console.SetCursorPosition(5, 5);
                    Console.WriteLine("Управление змейкой на стрелочки");
                    Console.SetCursorPosition(5, 6);
                    Console.WriteLine("Ваша цель - кушать яблоки");
                    Console.SetCursorPosition(5, 7);
                    Console.WriteLine("Если вы врежетесь в стенку,то вы умрете");
                    Console.SetCursorPosition(5, 8);
                    Console.WriteLine("В ИГРЕ НЕ РЕАЛИЗОВАНА СМЕРТЬ ОТ ХВОСТА И Проблемы с перезапуском игры!");
                        Console.SetCursorPosition(5, 9);
                        Console.WriteLine("Отнеситесь с любовью и пониманием!");
                        Console.SetCursorPosition(5, 10);
                    Console.WriteLine("Нажмите ENTER чтобы выйти в меню");
                    Console.ReadLine();
                    Console.Clear();
                    ShowMenu(out User);

                    break;
                #endregion

                  #region Case Игра
                case "2":
                    #region Установка игры
                    Console.Clear();


                        Console.WriteLine("Выберите сложность игры");
                        Console.WriteLine("1 - для пусичек");
                        Console.WriteLine("2 - нормальная сложность");
                        Console.WriteLine("3 - сущий ад");
                        a = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();


                        // Змейка появляется на экране
                        paintSnake(appleEaten, xPosition, YPosition, out xPosition, out YPosition);

                    // Яблочко появляется на экране
                    applePosition(random, out appleX, out appleY);
                    paintapple(appleX, appleY);

                    // Построим стены
                    Wall();


                    ConsoleKey consoleKey = Console.ReadKey().Key;

                    #endregion


                    do
                    {
                        #region Изменение направления
                        // Движение змейки
                        switch (consoleKey)
                        {

                            case ConsoleKey.LeftArrow:
                                Console.SetCursorPosition(xPosition[0], YPosition[0]);
                                Console.Write(" ");
                                xPosition[0]--;
                                break;

                            case ConsoleKey.RightArrow:
                                Console.SetCursorPosition(xPosition[0], YPosition[0]);
                                Console.Write(" ");
                                xPosition[0]++;
                                break;

                            case ConsoleKey.UpArrow:
                                Console.SetCursorPosition(xPosition[0], YPosition[0]);
                                Console.Write(" ");
                                YPosition[0]--;
                                break;

                            case ConsoleKey.DownArrow:
                                Console.SetCursorPosition(xPosition[0], YPosition[0]);
                                Console.Write(" ");
                                YPosition[0]++;
                                break;
                        }

                        #endregion

                        #region Играем в игру
                        //туловище змейки
                        paintSnake(appleEaten, xPosition, YPosition, out xPosition, out YPosition);



                        //змейка стукается головой об стенку
                        isStenka = HitWall(xPosition[0], YPosition[0]);
                        if (isStenka)
                        {
                            isGameStatus = false;
                            Console.SetCursorPosition(28, 20);
                            Console.WriteLine("Змейка врезалась и умерла!");

                         
                            //Показываем счет

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(15, 21);
                            Console.Write("Твой счет " + appleEaten * 100 + "!");
                            Console.SetCursorPosition(15, 22);
                            Console.WriteLine("Нажмите ENTER чтобы закрыть игру!");
                            appleEaten = 0;
                            Console.ReadLine();
                            Console.Clear();
                            Menu = false;
     
                        }


                        //яблоко скушано
                        isAppleEaten = SnakeEatApple(xPosition[0], YPosition[0], appleX, appleY);

                        //Место спавна яблока (рандом)
                        if (isAppleEaten)
                        {
                            applePosition(random, out appleX, out appleY);
                            paintapple(appleX, appleY);

                            //как много яблочек было съедено
                            //Змейка благодаря этому становится длинее
                            appleEaten++;
                        }






                        if (Console.KeyAvailable) consoleKey = Console.ReadKey().Key;

                            //Уменьшаем скорость игры
                            if (a == 1)
                            {
                                System.Threading.Thread.Sleep(150);
                            }
                            else if (a == 2)
                            {
                                System.Threading.Thread.Sleep(100);
                            }
                            else if (a == 3)
                            {
                                System.Threading.Thread.Sleep(25);
                            }
                            else
                            {
                                Console.SetCursorPosition(10, 10);
                                Console.WriteLine("Вы ввели что-то не так! Попробуйте заново! Перезапустите консоль!");
                                Console.ReadLine();
                                Console.Clear();                                                          
                            }


                            #endregion

                        } while (isGameStatus);
                    break;
                    #endregion

                  #region Case Выход
                    case "3":
                    Menu = false;
                    Console.Clear();
                    break;
                    #endregion

                  #region Case Неправельный ввод
                    default:
                    Console.WriteLine("Возможно вы не поняли, вам нужно нажать необходимуб клавишу! Попробуйте заново нажмите ENTER или перезапустите консоль!!!");
                    Console.ReadLine();
                    Console.Clear();
                    ShowMenu(out User);
                    break;
                        #endregion
                }
            } while (Menu);
        }



        #region Методы

        private static void ShowMenu(out string user)
        {
            string menu1 = " 1)Инструкции \n 2)Выбор сложности и игра\n 3)Выход";
            Console.WriteLine(menu1);
            user = Console.ReadLine();

        }// Меню приветствия
        private static bool HitWall(int xPosition, int yPosition)
        {
            if (xPosition == 1 || xPosition == 70 || yPosition == 1 || yPosition == 40)
            {
                return true;
            }
            else
            {
                return false;
            }
        }// Змейка стукается со стенкой

        private static void paintSnake(int appleEaten, int[] xPositionIN, int[] yPositionIN, out int[] xPositionOut, out int[] yPositionOut)
        {

            // Голова змейки
            Console.SetCursorPosition(xPositionIN[0], yPositionIN[0]);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine((char)2);


            // Туловище змейки
            for (int i = 1; i < appleEaten + 1; i++)
            {
                Console.SetCursorPosition(xPositionIN[i], yPositionIN[i]);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("O");
            }

            //Стирают последнюю часть змейки
            Console.SetCursorPosition(xPositionIN[appleEaten + 1], yPositionIN[appleEaten + 1]);
            Console.WriteLine(" ");

            //запись хвоста к туловищу
            for (int i = appleEaten + 1; i > 0; i--)
            {
                xPositionIN[i] = xPositionIN[i - 1];
                yPositionIN[i] = yPositionIN[i - 1];
            }


            // возвращаем новый массив
            xPositionOut = xPositionIN;
            yPositionOut = yPositionIN;
        }// Рисуем змейку
        private static void Wall()
        {
            for (int i = 1; i < 41; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(1, i);
                Console.Write("#");
                Console.SetCursorPosition(70, i);
                Console.Write("#");
            }
            for (int i = 1; i < 71; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(i, 1);
                Console.Write("#");
                Console.SetCursorPosition(i, 40);
                Console.Write("#");
            }
        }// Стена
        private static void applePosition(Random random, out int appleX, out int appleY)
        {
            appleX = random.Next(0 + 2, 70 - 2);
            appleY = random.Next(0 + 2, 40 - 2);
        }//Спавн яблока
        private static void paintapple(int appleX, int appleY)
        {
            Console.SetCursorPosition(appleX, appleY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)64);
        }//Прорисовка яблока
        private static bool SnakeEatApple(int xPosition, int yPosition, int appleX, int appleY)
        {
            if (xPosition == appleX && yPosition == appleY) return true; return false;
            
        }//Змейка ест яблоко

        #endregion
    }
}