using System;
using System.Threading;

namespace FindingGem;

class Program
{
    static void Main(string[] args)
    {
        //? if we put something empty it will go back to the question
        string? playChoice;
        //do for menu
        do
        {
            Console.Clear();
            Console.WriteLine("Welcome to Finding Gem!");
            Console.WriteLine("In this game you have 7 lives to find the gem!");
            Console.WriteLine();
            //do for when you want to try or not, only executed once
            do
            {
                Console.WriteLine("Would you like to try? Y/N: ");
                //!= for Y and N to become true
                playChoice = Console.ReadLine()?.Trim().ToUpper();
                //prints this if wrong word
                if (playChoice != "Y" && playChoice != "N")
                {
                    Console.WriteLine("Please write Y and N only.");
                }
                //choices != for y and n to become true
                //if we use == and use other words it will
                //continue which we don't want it in our game
            } while (playChoice != "Y" && playChoice != "N");
            
            //if N it ends the game then we return to print this
            //if we use break; it won't print and jump on another loop
            if (playChoice == "N")
            {
                Console.Clear();
                Console.WriteLine("See ya later!");
                return;
            }
            //if Y then it prints a loading animation
            Console.Clear();
            //used nested loop to print cycle and dot
            for (int cycle = 0; cycle < 3; cycle++)
            {
                for (int dot = 1; dot <= 3; dot++)
                {
                    Console.Clear();
                    Console.WriteLine($"Loading {new string('.', dot)}");
                    Thread.Sleep(500);
                }
            }
            //after the loading animation it prints this
            //then opens the game
            Console.Clear();
            Console.WriteLine("Ready!");
            Thread.Sleep(500);
            MainGame();
            //another do while for replayability
            do
            {
                Console.WriteLine("Do you want to play again? Y/N: ");
                playChoice = Console.ReadLine()?.Trim().ToUpper();
            } while (playChoice != "Y" && playChoice != "N");
            //if Y then we go back to menu
            //if N then it prints this
        } while (playChoice == "Y");
        Console.Clear();
        Console.WriteLine("Thank you for playing!");
        
        //our very own game
        static void MainGame()
        {
            //char named board with 5x5 also called our CreateBoard here
            char[,] board = CreateBoard(5, 5);
            //declare random so the gem location will be random
            Random rand = new Random();
            //gem location
            int gemRow = rand.Next(0, 5);
            int gemCol = rand.Next(0, 5);
            //bool is false so it won't until we found the gem
            bool foundGem = false;
            //total lives
            int lives = 7;
            
            //Main game loop: continues until gem is found or lives run out
            //if put it to true the game ends abruptly
            while (!foundGem && lives > 0)
            {
                //clears unnecessary mess
                Console.Clear();
                
                //prints the board
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Console.Write($"{board[i, j]} ");
                    }
                    Console.WriteLine();
                }
                
                //get row input
                //this is where user puts the num row
                int row;
                Console.Write("Enter the number of rows (1-5): ");
                if (!int.TryParse(Console.ReadLine(), out row) || row < 1 || row > 5)
                {
                    Console.WriteLine("Invalid input.");
                }
                
                //accurate guess
                row--;

                //get column input
                //this is where user put the num column
                int col;
                Console.Write("Enter the number of columns (1-5): ");
                if (!int.TryParse(Console.ReadLine(), out col) || col < 1 || col > 5)
                {
                    Console.WriteLine("Invalid input.");
                }
                
                //col accurate guess
                col--;
                
                //prevents guessing again
                if (board[row, col] != '.')
                {
                    Console.WriteLine("That spot has been found!");
                    Console.ReadKey(true);
                    continue;
                }
                
                //if we put the input it shows this
                //if we got it correct it shows G if not then X
                //if wrong guess minus lives
                if (row == gemRow && col == gemCol)
                {
                    foundGem = true;
                    Console.WriteLine("Congrats! You found the gem!");
                    board[row, col] = 'G';
                }
                else
                {
                    board[row, col] = 'X';
                    lives--;
                    Console.WriteLine($"You guessed it wrong! {lives} remaining.");
                }
                
            }
            
            //calls location treasure to pinpoint the location of the gem
            if (lives == 0)
            {
                LocationGem(foundGem, gemRow, gemCol);
            }
            
            //call the final board
            FinalBoard(board);
        }
        //we make a method called create board
        //this is where our board was created then call it on our MainGame
        static char[,] CreateBoard(int rows, int cols)
        {
            char[,] board = new char[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    board[i, j] = '.';
                }
            }

            return board;
        }
        
        //I created another method for location gem
        //if we lose it will pinpoint the location of gem
        static void LocationGem(bool foundGem, int gemRow, int gemCol)
        {
            if (!foundGem)
            {
                Console.WriteLine($"You lost! The gem was at {gemRow + 1} - {gemCol + 1}.");
            }
        }
        
        //I made another method which called final board
        //It reveals the overall mistake or where the gem is if you found it
        static void FinalBoard(char[,] board)
        {
            Console.WriteLine("Final Board: ");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write($"{board[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
