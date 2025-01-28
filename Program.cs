using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tennis
{
    internal class Program
    {
        static int player1Pos = 10;
        static int player2Pos = 10;
        static int player1Score = 0;
        static int player2Score = 0;
        static int ballXPos = 3;
        static double ballYPos = 11;
        static int xDirection = -1;
        static double yDirection = 0;
        static bool gameOver = false;
        static bool setStarted = false;
        static int setWinner = 1;
        static char rocket = '║';
        static char ball = 'O';
        static void Update()
        {
            Console.Clear();
            Console.SetCursorPosition(15, 0);
            Console.WriteLine($"{player1Score} | {player2Score}");
            for(int i = 2; i <= 34; i++)
            {
                Console.SetCursorPosition(i, 2);
                Console.Write("-");
            }
            for(int i = 2; i <= 20; i++)
            {
                Console.SetCursorPosition(17, i);
                Console.WriteLine("|");
            }
            for(int i = 2; i <= 34; i++)
            {
                Console.SetCursorPosition(i, 20);
                Console.WriteLine("-");
            }
            for (int i = 0; i < 3; i++) 
            {
                Console.SetCursorPosition(3, player1Pos + i);
                Console.WriteLine(rocket);
            }
            for(int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(33, player2Pos + i);
                Console.WriteLine(rocket);
            }
            Console.SetCursorPosition(ballXPos, Convert.ToInt32(ballYPos));
            Console.WriteLine(ball);

        }
        static void Input()
        {
            if (!setStarted)
            {
                ConsoleKeyInfo start = Console.ReadKey();
                //Player 1 inputs
                if (start.Key == ConsoleKey.W && player1Pos > 2)
                {
                    player1Pos--;
                }
                if (start.Key == ConsoleKey.S && player1Pos < 20)
                {
                    player1Pos++;
                }
                //Player 2 inputs
                if (start.Key == ConsoleKey.UpArrow && player2Pos > 2)
                {
                    player2Pos--;
                }
                if (start.Key == ConsoleKey.DownArrow && player2Pos < 20)
                {
                    player2Pos++;
                }
                if(setWinner == 1)
                {
                    if (start.Key == ConsoleKey.W) ballYPos--;
                    if (start.Key == ConsoleKey.S) ballYPos++;
                }
                if(setWinner == 2)
                {
                    if(start.Key == ConsoleKey.UpArrow) ballYPos--;
                    if (start.Key == ConsoleKey.DownArrow) ballYPos++;
                }
                if (setWinner == 2 && start.Key == ConsoleKey.Enter)
                {
                    ballXPos = 33;
                    xDirection = -1;
                    setStarted = true;
                }
                if(setWinner == 1 && start.Key == ConsoleKey.Enter)
                {
                    ballXPos = 3;
                    xDirection = 1;
                    setStarted = true;
                }
            }
            if (Console.KeyAvailable && setStarted)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                if(input.Key == ConsoleKey.W && player1Pos > 2)
                {
                    player1Pos--;
                }
                if(input.Key == ConsoleKey.S && player1Pos < 20)
                {
                    player1Pos++;
                }
                if(input.Key == ConsoleKey.UpArrow && player2Pos > 2)
                {
                    player2Pos--;
                }
                if(input.Key == ConsoleKey.DownArrow && player2Pos < 20)
                {
                    player2Pos++;
                }
            }
        }
        static void Logic()
        {
            if (setStarted)
            {
                ballXPos = ballXPos + xDirection;
                ballYPos = ballYPos + yDirection;
                //Checking for collision with player number 1
                if (ballXPos == 3 && Convert.ToInt32(ballYPos) == player1Pos)
                {
                    xDirection = 1;
                    yDirection = -0.2;
                }
                if (ballXPos == 3 && Convert.ToInt32(ballYPos) == player1Pos + 1)
                {
                    xDirection = 1;
                    yDirection = 0;
                }
                if (ballXPos == 3 && Convert.ToInt32(ballYPos) == player1Pos + 2)
                {
                    xDirection = 1;
                    yDirection = 0.2;
                }
                //Checking for collision with player number 2
                if (ballXPos == 33 && Convert.ToInt32(ballYPos) == player2Pos)
                {
                    xDirection = -1;
                    yDirection = -0.2;
                }
                if (ballXPos == 33 && Convert.ToInt32(ballYPos) == player2Pos + 1)
                {
                    xDirection = -1;
                    yDirection = 0;
                }
                if (ballXPos == 33 && Convert.ToInt32(ballYPos) == player2Pos + 2)
                {
                    xDirection = -1;
                    yDirection = 0.2;

                }
            }
            if(ballXPos < 2)
            {
                player2Score++;
                setWinner = 2;
                DefaultValues();
            }
            if(ballYPos <= 2 && ballXPos <= 17)
            {
                player2Score++;
                setWinner = 2;
                DefaultValues();
            }
            if(ballXPos > 34) 
            {
                player1Score++;
                setWinner = 1;
                DefaultValues();
            }
            if(ballYPos >= 20 && ballXPos >= 17)
            {
                player1Score++;
                setWinner = 1;
                DefaultValues();
            }
            if (player1Score == 11 || player2Score == 11) gameOver = false;
        }
        static void DefaultValues()
        {
            player1Pos = 10;
            player2Pos = 10;
            if (setWinner == 1)
            {
                ballXPos = 3;
                ballYPos = player1Pos + 1;
            }
            else
            {
                ballXPos = 33;
                ballYPos = player2Pos + 1;
            }
            yDirection = 0;
            setStarted = false;
        }
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            while (!gameOver)
            {
                Update();
                Input();
                Logic();
                Thread.Sleep(50);
            }
            Console.ReadKey();
        }
    }
}
