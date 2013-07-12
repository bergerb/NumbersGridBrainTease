using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberGridBrainTease
{
    class NumberGridBrainTease
    {
        public const int NUM_DIRECTIONS = 6;
        public const int NUM_COMPARE = 4;

        static void Main(string[] args)
        {
            string[,] grid = new string[20, 20];

            // source grid
            string source = @"08 49 81 52 22 24 32 67 24 21 78 16 86 19 04 88 04 20 20 01
02 49 49 70 31 47 98 26 55 36 17 39 56 80 52 36 42 69 73 70
22 99 31 95 16 32 81 20 58 23 53 05 00 81 08 68 16 36 35 54
97 40 73 23 71 60 28 68 05 09 28 42 48 68 83 87 73 41 29 71
38 17 55 04 51 99 64 02 66 75 22 96 35 05 97 57 38 72 78 83
15 81 79 60 67 03 23 62 73 00 75 35 71 94 35 62 25 30 31 51
00 18 14 11 63 45 67 12 99 76 31 31 89 47 99 20 39 23 90 54
40 57 29 42 89 02 10 20 26 44 67 47 07 69 16 72 11 88 01 69
00 60 93 69 41 44 26 95 97 20 15 55 05 28 07 03 24 34 74 16
75 87 71 24 92 75 38 63 17 45 94 58 44 73 97 46 94 62 31 92
04 17 40 68 36 33 40 94 78 35 03 88 44 92 57 33 72 99 49 33
05 40 67 56 54 53 67 39 78 14 80 24 37 13 32 67 18 69 71 48
07 98 53 01 22 78 59 63 96 00 04 00 44 86 16 46 08 82 48 61
78 43 88 32 40 36 54 08 83 61 62 17 60 52 26 55 46 67 86 43
52 69 30 56 40 84 70 40 14 33 16 54 21 17 26 12 29 59 81 52
12 48 03 71 28 20 66 91 88 97 14 24 58 77 79 32 32 85 16 01
50 04 49 37 66 35 18 66 34 34 09 36 51 04 33 63 40 74 23 89
77 56 13 02 33 17 38 49 89 31 53 29 54 89 27 93 62 04 57 19
91 62 36 36 13 12 64 94 63 33 56 85 17 55 98 53 76 36 05 67
08 00 65 91 80 50 70 21 72 95 92 57 58 40 66 69 36 16 54 48";
            Console.Write(source);
            // Converted into array
            grid = buildGrid(source);
            // Will store calculated values
            gridResult[,] overallResults = new gridResult[20, 20];
           
            // Loop through everything
            for (int row = 0; row < 20; row++)
            {
                for (int col = 0; col < 20; col++)
                {
                    overallResults[row, col] = findHighestValue(grid, row, col);
                }
            }

            // Loop through to find the largest overall
            gridResult result = null;
            foreach (gridResult current in overallResults)
            {
                if (result == null)
                    result = current;
                else
                {
                    if (current.largestAmount() > result.largestAmount())
                    {
                        result = current;
                    }
                }
            }

            Console.Write(Environment.NewLine + Environment.NewLine + "The Largest Amount is: {0} at row: {1}, col: {2}", result.largestAmount().ToString(), (result.row+1).ToString(), (result.col+1).ToString());
            Console.Write(Environment.NewLine + "The Numbers: {0}", result.viewResultsData);
            Console.Write(Environment.NewLine + Environment.NewLine);
            Console.Write("Press Enter To Continue.");
            string lineread = Console.ReadLine();
        }

        /// <summary>
        /// Search for the highest number of n elements on grid in all directions
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        static gridResult findHighestValue(string[,] grid, int row, int col)
        {
            int[] results = new int[NUM_DIRECTIONS];
            string[] resultsData = new string[NUM_DIRECTIONS];
            directions d = new directions();
            int[][] directions = d.getDirections();

            for (int count = 0; count < NUM_DIRECTIONS; count++)
            {
                int[] currentresults = new int[NUM_COMPARE];
                for (int subcount = 0; subcount < NUM_COMPARE; subcount++)
                {
                    try
                    {
                        int new_row = row + (subcount * (directions[count][0]));
                        int new_col = col + (subcount * (directions[count][1]));
                        currentresults[subcount] = int.Parse(grid[new_row, new_col]);
                    }
                    catch (Exception ex)
                    {
                        currentresults[subcount] = 0;
                    }
                    if (resultsData[count] != null)
                        resultsData[count] += ",";
                    resultsData[count] += currentresults[subcount];
                }
                results[count] = currentresults[0] * currentresults[1] * currentresults[2] * currentresults[3];
            }
            gridResult r = new gridResult(results, resultsData);
            r.row = row;
            r.col = col;
            return r;
        }

        /// <summary>
        /// Disassemble string to 2d array
        /// </summary>
        /// <param name="source">grid</param>
        /// <returns>string[,]</returns>
        static string[,] buildGrid(string source)
        {
            string[,] fillGrid = new string[20, 20];
            int row_num = 0;
            // Get Each Line
            foreach (string line in source.Replace("\r\n", "\r").Split(Environment.NewLine.ToCharArray()))
            {
                // Break items apart
                int col_count = 0;
                foreach (string item in line.TrimStart(' ').Split(' '))
                {
                    fillGrid[row_num, col_count] = item;
                    col_count++;
                }
                row_num++;
            }
            return fillGrid;
        }

        /// <summary>
        /// Stores information about a calculated position
        /// </summary>
        class gridResult
        {
            int[] _Results = new int[NUM_DIRECTIONS];
            int _LargestAmount;
            int _row;
            int _col;
            string[] _resultsData = new string[NUM_DIRECTIONS];
            /// <summary>
            /// Load needed values
            /// </summary>
            /// <param name="results"></param>
            /// <param name="resultsData"></param>
            public gridResult(int[] results, string[] resultsData)
            {
                _Results = results;
                _resultsData = resultsData;
                getLargestAmount();
            }
            /// <summary>
            /// Return string value largest dataset
            /// </summary>
            public string viewResultsData
            {
                get
                {
                    // Find what set has the largest number 
                    int largestValues = 0;
                    for (int i = 0; i < NUM_DIRECTIONS; i++)
                    {
                        if (_Results[i] == _LargestAmount)
                        {
                            largestValues = i;
                            break;
                        }
                    }
                    return _resultsData[largestValues];
                }
            }
            /// <summary>
            /// Raw results data
            /// </summary>
            public string[] resultsData
            {
                get { return _resultsData; }
            }
            /// <summary>
            /// Row Location
            /// </summary>
            public int row
            {
                set { this._row = value; }
                get { return this._row; }
            }
            /// <summary>
            /// Column Location
            /// </summary>
            public int col
            {
                set { this._col = value; }
                get { return this._col; }
            }
            /// <summary>
            /// Pre-calculated Largest Amount
            /// </summary>
            /// <returns>integer</returns>
            public int largestAmount()
            {
                return _LargestAmount;
            }
            /// <summary>
            /// Calculate and fills the largest amount
            /// </summary>
            private void getLargestAmount()
            {
                int[] tempResults = new int[NUM_DIRECTIONS];

                for (int i = 0; i < NUM_DIRECTIONS; i++)
                {
                    tempResults[i] = _Results[i];
                }

                Array.Sort(tempResults);

                _LargestAmount = tempResults[tempResults.Count() - 1];
            }
        }

        /// <summary>
        /// Extraction directions for looking for possible locations
        /// </summary>
        class directions
        {
            int[][] iDirections;
            /// <summary>
            /// Pre-filled directions
            /// </summary>
            public directions()
            {
                int[][] directions = {
                                     new int[] {1,1}, // SE
                                     new int[] {1,-1}, // NE
                                     new int[] {-1,1},  // SW
                                     new int[] {-1,-1}, // NW
                                     new int[] {1,0}, // E
                                     new int[] {0,1},  // S
                                     //new int[] {-1,0}, // W // Dupe -> E
                                     //new int[] {0,-1}, // N  // Dupe -> S
                                 };
                iDirections = directions;
            }
            /// <summary>
            /// Return pre-filled directions
            /// </summary>
            /// <returns>Array of arrays</returns>
            public int[][] getDirections()
            {
                return iDirections;
            }
        }
    }
}
