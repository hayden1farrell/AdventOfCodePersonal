using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] data = System.IO.File.ReadAllLines("input.txt");
            string[] numbers = data[0].Split(",");

            string[] perfectInput = CleanInput(data);
            Dictionary<int, string[,]> boards = To2D(perfectInput);
            Solve(numbers, boards);
            Part2(numbers, boards);
        }

        private static void Part2(string[] numbers, Dictionary<int, string[,]> boards)
        {
            int turnCount = 0;
            int winningBoard = 0;
            int currentNumber = 0;
            List<int> validNumbers = new List<int>();
            List<int> deleted = new List<int>();
            int totalCount = boards.Count;
            bool complete = false;
            while(complete == false){
                currentNumber = Convert.ToInt32(numbers[turnCount]);

                for(int i = 0; i <= turnCount; i++){
                    validNumbers.Add(Convert.ToInt32(numbers[i]));
                }

                for (int i = 0; i < totalCount; i++)
                {
                    if(deleted.Contains(i) == false){
                        if(CheckIfWon(numbers, boards[i], turnCount, validNumbers)){
                            Console.WriteLine(boards.Count);
                            if(boards.Count == 1){
                                complete = true;
                                break;
                            }
                            boards.Remove(i);
                            deleted.Add(i);
                        }
                    }
                }
                turnCount++;
            }

            foreach (var key in boards)  
            {  
                winningBoard = key.Key; 
            }  

            int sum =0;
            string[,] board = boards[winningBoard];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if(validNumbers.Contains(Convert.ToInt32(board[i,j])) == false){
                        sum += Convert.ToInt32(board[i,j]);
                    }
                }
            }
            Console.WriteLine($"2:board number: {winningBoard} Number: {currentNumber} Sum: {sum} Total: {sum * currentNumber} ");
        }

        static void Solve(string[] numbers, Dictionary<int, string[,]> boards){
            bool winners = false;
            int turnCount = 0;
            int winningBoard = 0;
            int currentNumber = 0;
            List<int> validNumbers = new List<int>();
            while(winners == false){
                currentNumber = Convert.ToInt32(numbers[turnCount]);

                for(int i = 0; i <= turnCount; i++){
                    validNumbers.Add(Convert.ToInt32(numbers[i]));
                }

                for (int i = 0; i < boards.Count; i++)
                {
                    if(CheckIfWon(numbers, boards[i], turnCount, validNumbers)){
                        winningBoard = i;
                        winners = true;
                        break;
                    }
                }

                turnCount++;
            }
            int sum =0;
            string[,] board = boards[winningBoard];
            for (int i = 0; i < boards[winningBoard].GetLength(0); i++)
            {
                for (int j = 0; j < boards[winningBoard].GetLength(1); j++)
                {
                    if(validNumbers.Contains(Convert.ToInt32(board[i,j])) == false){
                        sum += Convert.ToInt32(board[i,j]);
                    }
                }
            }
            Console.WriteLine($"Number: {currentNumber} Total: {sum * currentNumber}");
        }
        static bool CheckIfWon(string[] numbers, string[,] board, int turnCount, List<int> validNumbers){
            bool won = true;

            for(int y= 0; y < board.GetLength(1);y++){  // row check
                won = true;
                for(int i =0; i < 5; i++){
                    if(validNumbers.Contains(Convert.ToInt32(board[y,i])) == false){
                        won = false;
                    }
                }
                if(won == true){
                    return won;
                }
            }
            
            for(int x= 0; x < board.GetLength(0);x++){  
                won = true;
                for(int i =0; i < 5; i++){
                    if(validNumbers.Contains(Convert.ToInt32(board[i,x])) == false){
                        won = false;
                    }
                }
                if(won == true){
                    return won;
                }
            }


            return won;
        }
        static Dictionary<int, string[,]> To2D(string[] data){
            Dictionary<int, string[,]> boards = new Dictionary<int, string[,]>();
            int board = 0;
            int y =0;
            string[,] currentBoard = new string[5,5];

            for(int i = 0; i < data.Length; i++){
                
                string[] currentLine = data[i].Split(",");
                for(int x =0; x < 5; x++){
                    currentBoard[y,x] = currentLine[x];
                }
                y++;
                try{    // for when it hits the end
                    if(data[i+1] == string.Empty){    // after the current board has been complete
                        boards.Add(board, currentBoard);
                        currentBoard = new string[5,5];
                        board++;
                        i++; y =0;
                    }
                }catch{
                    boards.Add(board, currentBoard);
                }
            }
            return boards;
        }
        static void DisplayBoards(Dictionary<int, string[,]> boards){
            foreach (KeyValuePair<int, string[,]> ele1 in boards)
            {
                Display(ele1.Value);
            }
        }
        static void Display(string[,] a){
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write(a[i,j] + " ");
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine("---");
        }
        static string[] CleanInput(string[] data){
            string[] cleanedInput = new string[data.Length - 2];

            for(int i = 2; i < data.Length; i++){
                string cleanedString = Regex.Replace(data[i], @"\s", ",");
                if(cleanedString != ""){
                    if(cleanedString[0] == ','){
                        cleanedString = cleanedString.Remove(0, 1);
                    }
                    for(int w = 0; w < cleanedString.Length - 1; w++){
                        if(cleanedString[w] == ',' && cleanedString[w + 1] == ','){
                            cleanedString = cleanedString.Remove(w+1, 1);
                        }
                    }
                }
                cleanedInput[i-2] = cleanedString;
            }
            return cleanedInput;
        }
    }
}
