using System;
using System.Text.RegularExpressions;

namespace Cheesboard
{
    class Program
    {
        static string input = "";
        static string firstFigureCoordinates = "";
        static string secondFigureCoordinates = "";
        static string thirdFigureCoordinates = "";
        static string nameWhiteFigure = "";
        static string nameBlackFigure = "";

        static bool IsValidChessCoordinateFormat()
        {
            string patter = @"^(ладья|конь|слон|ферзь|король)$";
            return Regex.IsMatch(input, patter);
        }

        static void EnteringString()
        {
            Console.WriteLine("Введите название фигуры(ладья, конь, слон, ферзь, король): ");
            input = Console.ReadLine().ToLower();
        }

        static void ConvertChessCoordinatesToArray(string str, out int[] shape)
        {
            shape = new int[2];
            switch (str[0])
            {
                case 'a': shape[0] = 0; break;
                case 'b': shape[0] = 1; break;
                case 'c': shape[0] = 2; break;
                case 'd': shape[0] = 3; break;
                case 'e': shape[0] = 4; break;
                case 'f': shape[0] = 5; break;
                case 'g': shape[0] = 6; break;
                case 'h': shape[0] = 7; break;
            }

            shape[1] = Convert.ToInt32(str[1].ToString()) - 1;
        }

        static bool IsQueenTargetingFigure(string coordinatesFigure, string coordinatesCell)
        {
            int[] queenPosition = new int[2];

            int[] CellPosition = new int[2];

            ConvertChessCoordinatesToArray(coordinatesFigure, out queenPosition);

            ConvertChessCoordinatesToArray(coordinatesCell, out CellPosition);

            int diagonal1Constant = queenPosition[1] - queenPosition[0];
            int diagonal2Constant = queenPosition[1] + queenPosition[0];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i - j) == diagonal1Constant || (i + j) == diagonal2Constant)
                    {
                        if (i == CellPosition[1] && j == CellPosition[0])
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        static bool IsKnightTargetingFigure(string coordinatesFigure, string coordinatesCell)
        {
            int[] knightPosition = new int[2];

            int[] CellPosition = new int[2];

            ConvertChessCoordinatesToArray(coordinatesFigure, out knightPosition);

            ConvertChessCoordinatesToArray(coordinatesCell, out CellPosition);

            int[] xMoves = { 2, 1, -1, -2, -2, -1, 1, 2 };
            int[] yMoves = { 1, 2, 2, 1, -1, -2, -2, -1 };

            for (int i = 0; i < 8; i++)
            {
                int potentialPositionX = knightPosition[1] + xMoves[i];
                int potentialPositionY = knightPosition[0] + yMoves[i];

                if (potentialPositionX == CellPosition[1] && potentialPositionY == CellPosition[0])
                {
                    return true;
                }
            }
            return false;
        }

        static bool IsKingTargetingFigure(string coordinatesFigure, string coordinatesCell)
        {
            int[] kingPosition = new int[2];

            int[] CellPosition = new int[2];

            ConvertChessCoordinatesToArray(coordinatesFigure, out kingPosition);

            ConvertChessCoordinatesToArray(coordinatesCell, out CellPosition);

            for (int i = kingPosition[1] - 1; i <= kingPosition[1] + 1; i++)
            {
                for (int j = kingPosition[0] - 1; j <= kingPosition[0] + 1; j++)
                {
                    if (i == CellPosition[1] && j == CellPosition[0])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        static bool ChoosingFigure(string figure, string coordinatesFigure, string coordinatesCell)
        {
            switch (figure)
            {
                case "ладья": if (firstFigureCoordinates[0] == secondFigureCoordinates[0] || firstFigureCoordinates[1] == secondFigureCoordinates[1]) return true; break;
                case "конь": if (IsKnightTargetingFigure(coordinatesFigure, coordinatesCell)) return true; break;
                case "слон": if (IsQueenTargetingFigure(coordinatesFigure, coordinatesCell)) return true; break;
                case "ферзь": if (IsQueenTargetingFigure(coordinatesFigure, coordinatesCell) || firstFigureCoordinates[0] == secondFigureCoordinates[0] || firstFigureCoordinates[1] == secondFigureCoordinates[1]) return true; break;
                case "король": if (IsKingTargetingFigure(coordinatesFigure, coordinatesCell)) return true; break;
            }
            return false;
        }

        static void CanFigureMove()
        {
            if (ChoosingFigure(input, firstFigureCoordinates, secondFigureCoordinates))
            {
                Console.WriteLine($"{input} находящийся на клетке {firstFigureCoordinates} угрозает клетке {secondFigureCoordinates}");
            }
            else
            {
                Console.WriteLine($"{input} находящийся на клетке {firstFigureCoordinates} не угрозает клетке {secondFigureCoordinates}");
            }
        }

        static bool CheckFigureMoveValidity()
        {
            if (!(IsValidChessCoordinateFormat()))
            {
                Console.WriteLine("Вы вели строку неправильного формата \n" +
                                  "Формат:a1 a2");
                return true;
            }
            else if (firstFigureCoordinates == secondFigureCoordinates && firstFigureCoordinates == thirdFigureCoordinates && secondFigureCoordinates == thirdFigureCoordinates)
            {
                Console.WriteLine("Вы вели одинаковые позиции фигур \n" +
                                  "Введите разные позиции фигур");
                return true;
            }
            return false;
        }

        static string AssignACoordinate()
        {
            char letter = (char)('a' + new Random().Next(0, 8)); // Генерируем число от 0 до 7 и прибавляем к 'a' для получения буквы от 'a' до 'h'

            // Генерируем случайное число для координаты
            int number = new Random().Next(1, 9); // Генерируем число от 1 до 8

            // Формируем строку с координатами
            string coordinate = $"{letter}{number}";
            return coordinate;
        }

        static void RandomCoordinates()
        {
            firstFigureCoordinates = AssignACoordinate();
            secondFigureCoordinates = AssignACoordinate();
        }

        static void Main()
        {
            do
            {
                EnteringString();
                RandomCoordinates();

            } while (CheckFigureMoveValidity());

            CanFigureMove();

        }
    }
}