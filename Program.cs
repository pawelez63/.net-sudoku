using System;

namespace ConsoleApplication8
{
    class Program
    {
        public static void ShowBoard( int[,] board, int lineLenght)
        {
            for (int i = 0; i < lineLenght; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < lineLenght; j++)
                {
                    Console.Write(string.Format("{0} ", board[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        public static bool CanInsertNumber(int[,] board, int number, int row, int column, int rowLenght, int colLenght)
        {   if (board[row, column] != 0)
            {
                return false;
            }

            var rowChecked = !RowContainsNumber(board, number, row, rowLenght * colLenght);
            var columnChecked = !ColumnContainsNumber(board, number, column, rowLenght * colLenght);
            var fieldChecked = !FieldContainsNumber(board, number, row, column, rowLenght, colLenght);

            if (rowChecked && columnChecked && fieldChecked)
            {
                return true;
            }
            return false;
        }

        public static bool RowContainsNumber(int[,] board, int number, int row, int lineLenght)
        {
            for (int i = 0; i < lineLenght; i++)
            {
                if (board[row,i] == number)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ColumnContainsNumber(int[,] board, int number, int column, int lineLenght)
        {
            for (int i = 0; i < lineLenght; i++)
            {
                if (board[i, column] == number)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool FieldContainsNumber(int[,] board, int number, int row, int column, int rowLenght, int colLenght)
        {
            for (int i = 0; i < rowLenght; i++)
            {
                for (int j = 0; j < colLenght; j++)
                {
                    if (board[(row/rowLenght)*rowLenght+i,(column/colLenght)*colLenght+j] == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static int CountBlankFields(int[,] board)
        {
            var counter = 0;
            foreach (int number in board)
            {
                if (number == 0)
                {
                    counter++;
                }
            }
            return counter;
        }

        public static int[,] CheckingRows(int[,] board, int lineLenght, int rowLenght, int colLenght, bool hintMode)
        {
            int countNumberOfMatches;
            bool fieldToUpdateFound;
            bool triggerBreak = false;

            for (int number = 1; number < lineLenght + 1; number++) //checkWithAllNumbers
            {
                for (int rows = 0; rows < lineLenght; rows++)   //checkWithAllRows
                {
                    countNumberOfMatches = 0;
                    fieldToUpdateFound = true;

                    for (int i = 0; i < lineLenght; i++)
                    {
                        if (board[rows, i] == number)
                        {
                            fieldToUpdateFound = false;
                            break;
                        }
                        if (CanInsertNumber(board, number, rows, i, rowLenght, colLenght))
                        {
                            countNumberOfMatches++;
                        }
                    }

                    if (countNumberOfMatches == 1 && fieldToUpdateFound == true)
                    {
                        for (int i = 0; i < lineLenght; i++)
                        {
                            if (CanInsertNumber(board, number, rows, i, rowLenght, colLenght))
                            {
                                board[rows, i] = number;
                                if (hintMode) triggerBreak = true;
                                break;
                            }
                        }
                    }
                    if (triggerBreak) break;
                }
                if (triggerBreak) break;
            }

            return board;
        }

        public static int[,] CheckingColumns(int[,] board, int lineLenght, int rowLenght, int colLenght, bool hintMode)
        {
            int countNumberOfMatches;
            bool fieldToUpdateFound;
            bool triggerBreak = false;

            for (int number = 1; number < lineLenght + 1; number++) //checkWithAllNumbers
            {
                for (int columns = 0; columns < lineLenght; columns++)   //checkWithAllColumns
                {
                    countNumberOfMatches = 0;
                    fieldToUpdateFound = true;

                    for (int i = 0; i < lineLenght; i++)
                    {
                        if (board[i, columns] == number)
                        {
                            fieldToUpdateFound = false;
                            break;
                        }
                        if (CanInsertNumber(board, number, i, columns, rowLenght, colLenght))
                        {
                            countNumberOfMatches++;
                        }
                    }

                    if (countNumberOfMatches == 1 && fieldToUpdateFound == true)
                    {
                        for (int i = 0; i < lineLenght; i++)
                        {
                            if (CanInsertNumber(board, number, i, columns, rowLenght, colLenght))
                            {
                                board[i, columns] = number;
                                if (hintMode) triggerBreak = true;
                                break;
                            }
                        }
                    }
                    if (triggerBreak) break;
                }
                if (triggerBreak) break;
            }

            return board;
        }

        public static int[,] CheckingFields(int[,] board, int lineLenght, int rowLenght, int colLenght, bool hintMode)
        {
            int fieldsColumnOffset = 0;
            int fieldsRowOffset = 0;

            int countNumberOfMatches;
            bool fieldToUpdateFound;
            bool triggerBreak = false;

            if (Math.Min(colLenght, rowLenght) > 2)
            {
                for (int number = 1; number < lineLenght + 1; number++) //checkWithAllNumbers
                {
                    for (int fields = 0; fields < lineLenght; fields++)   //checkWithAllFields
                    {
                        fieldsColumnOffset = fields % colLenght;
                        fieldsRowOffset = fields / rowLenght;

                        countNumberOfMatches = 0;
                        fieldToUpdateFound = true;

                        for (int i = 0; i < rowLenght; i++)
                        {
                            for (int j = 0; j < colLenght; j++)
                            {
                                if (board[fieldsRowOffset * rowLenght + i, fieldsColumnOffset * colLenght + j] == number)
                                {
                                    fieldToUpdateFound = false;
                                    break;
                                }
                                if (CanInsertNumber(board, number, fieldsRowOffset * rowLenght + i, fieldsColumnOffset * colLenght + j, rowLenght, colLenght))
                                {
                                    countNumberOfMatches++;
                                }
                            }
                        }

                        if (countNumberOfMatches == 1 && fieldToUpdateFound == true)
                        {
                            for (int i = 0; i < rowLenght; i++)
                            {
                                for (int j = 0; j < colLenght; j++)
                                {
                                    if (CanInsertNumber(board, number, fieldsRowOffset * rowLenght + i, fieldsColumnOffset * colLenght + j, rowLenght, colLenght))
                                    {
                                        board[fieldsRowOffset * rowLenght + i, fieldsColumnOffset * colLenght + j] = number;
                                        if (hintMode) triggerBreak = true;
                                        break;
                                    }
                                }
                                if (triggerBreak) break;
                            }
                        }
                        if (triggerBreak) break;
                    }
                    if (triggerBreak) break;
                }
            }

            return board;
        }

        public static int[,] CheckWholeBoard(int[,] board, int lineLenght, int rowLenght, int colLenght)
        {
            int counterBlankFields;

            do
            {
                counterBlankFields = CountBlankFields(board);

                board = CheckingRows(board, lineLenght, rowLenght, colLenght, false);
                board = CheckingColumns(board, lineLenght, rowLenght, colLenght, false);
                board = CheckingFields(board, lineLenght, rowLenght, colLenght, false);

            }
            while (counterBlankFields != CountBlankFields(board));

            return board;
        }

        public static int[,] GetHint(int [,] board, int lineLenght, int rowLenght, int colLenght)
        {
            int counterBlankFields = CountBlankFields(board);

            board = CheckingRows(board, lineLenght, rowLenght, colLenght, true);
            if (CountBlankFields(board) < counterBlankFields) return board;

            board = CheckingColumns(board, lineLenght, rowLenght, colLenght, true);
            if (CountBlankFields(board) < counterBlankFields) return board;

            board = CheckingFields(board, lineLenght, rowLenght, colLenght, true);

            return board;
        }

        static void Main(string[] args)
        {
            int[,] board = new int[,]{
                            { 0,9,0,0,0,2,6,0,0},
                            { 6,0,0,0,0,0,0,9,1},
                            { 0,8,3,0,0,5,2,0,7},
                            { 7,0,0,0,6,3,0,0,2},
                            { 0,0,6,0,0,9,0,5,0},
                            { 8,1,0,0,0,0,0,0,0},
                            { 5,0,0,0,0,0,8,1,0},
                            { 0,0,0,0,0,0,0,0,3},
                            { 0,3,0,0,0,0,7,0,0}
                        };

            int rowLenght = 3;
            int colLenght = 3;
            int lineLenght = rowLenght * colLenght;
            
            ShowBoard(board, lineLenght);

            int counterBlankFields;

            do
            {
                counterBlankFields = CountBlankFields(board);
                System.Threading.Thread.Sleep(300);

                board = GetHint(board, lineLenght, rowLenght, colLenght);
                Console.Clear();
                ShowBoard(board, lineLenght);
            }
            while (CountBlankFields(board) < counterBlankFields);

            Console.WriteLine("Thanks for playing!");
            Console.ReadLine();

            //string inputKey;
            //
            //do
            //{
            //    inputKey = Console.ReadLine();

            //    if (inputKey == "1")
            //    {
            //        board = CheckWholeBoard(board, lineLenght, rowLenght, colLenght);
            //    }
            //    else
            //    {
            //        board = GetHint(board, lineLenght, rowLenght, colLenght);
            //    }

            //    ShowBoard(board, lineLenght);
            //}
            //while (inputKey != "3");

        }
    }
}

//2x2

//int[,] board = new int[,]
//{
//    {1,0,0,0 },
//    {0,4,2,0 },
//    {2,0,1,0 },
//    {4,0,0,0 }
//};

//3x3

//int[,] board = new int[,]{
//                { 0,9,0,0,0,2,6,0,0},
//                { 6,0,0,0,0,0,0,9,1},
//                { 0,8,3,0,0,5,2,0,7},
//                { 7,0,0,0,6,3,0,0,2},
//                { 0,0,6,0,0,9,0,5,0},
//                { 8,1,0,0,0,0,0,0,0},
//                { 5,0,0,0,0,0,8,1,0},
//                { 0,0,0,0,0,0,0,0,3},
//                { 0,3,0,0,0,0,7,0,0}
//            };

//int[,] board = new int[,]{
//                { 6,0,7,0,0,0,0,8,0},
//                { 0,2,3,0,0,4,0,9,7},
//                { 0,0,0,0,0,3,2,0,0},
//                { 5,0,8,0,7,2,9,0,0},
//                { 0,0,9,3,0,0,0,1,0},
//                { 0,0,0,0,0,0,0,0,2},
//                { 8,6,1,0,2,5,0,3,0},
//                { 0,0,0,0,0,0,0,0,0},
//                { 9,0,0,0,0,0,0,5,0}
//            };

//partial

//int[,] board = new int[,]{
//                { 0,0,3,8,0,0,0,9,0},
//                { 0,0,0,9,4,2,0,6,0},
//                { 0,0,0,6,0,0,1,0,0},
//                { 0,0,0,1,0,0,0,0,0},
//                { 0,0,0,0,0,4,3,1,0},
//                { 1,0,0,0,6,0,9,7,2},
//                { 0,9,6,7,0,0,0,0,0},
//                { 0,0,2,0,0,0,0,0,0},
//                { 7,0,1,0,2,3,0,8,0}
//            };