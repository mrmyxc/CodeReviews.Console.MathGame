﻿namespace csa_maths_game
{
    internal class Game
    {
        IOperation? Operation { get; set; }
        Random Rng { get; set; }
        int NumberOfRounds { get; set; } = 5;

        public Game(Random rand)
        {
            Rng = rand;
        }

        public void Play(IOperation op)
        {
            Operation = op;
            bool correctAnswer = true;
            TimeSpan lastTimeTaken;
            TimeSpan totalTimeTaken = TimeSpan.FromSeconds(0);
            int score = 0;
            int roundsPlayed= 0;

            while (roundsPlayed < NumberOfRounds)
            {
                (correctAnswer, lastTimeTaken) = PlayRound();
                if (correctAnswer) score++;
                roundsPlayed++;
                totalTimeTaken += lastTimeTaken;
            }

            HighScore.Add(new(score, Operation.GetTextString()));
            ShowGameEndScreen(score, totalTimeTaken);
        }        
        
        public void PlayRandom(List<IOperation> operations)
        {
            bool correctAnswer = true;
            TimeSpan lastTimeTaken;
            TimeSpan totalTimeTaken = TimeSpan.FromSeconds(0);
            int score = -1;
            int roundsPlayed = 0;

            while (roundsPlayed < NumberOfRounds)
            {
                Operation = operations[Rng.Next(0, operations.Count)];
                (correctAnswer, lastTimeTaken) = PlayRound();
                if (correctAnswer) score++;
                roundsPlayed++;
                totalTimeTaken += lastTimeTaken;
            }

            HighScore.Add(new GameScore(score, "Random"));
            ShowGameEndScreen(score, totalTimeTaken);
        }

        public void ShowGameEndScreen(int score, TimeSpan totalTimeTaken)
        {
            Console.WriteLine("Score: {0}", score);
            Console.WriteLine("Time taken: {0}", totalTimeTaken.TotalSeconds);
            Console.WriteLine("Avg time taken per question: {0}s", totalTimeTaken.TotalSeconds / (score + 1));
            Console.WriteLine("Press any key to go back to main menu");
            Console.ReadKey();
        }

        private (bool, TimeSpan) PlayRound()
        {
            (int opA, int opB, int result) = GetNextQuestion();
            DisplayQuestion(opA, opB);
            DateTime start = DateTime.Now;
            int userAnswer = GetUserResponse();
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            bool correct = CheckResult(result, userAnswer);
            if (correct)
            {
                Console.WriteLine("Correct answer");
                Console.WriteLine("Press any key to play to next round");
            }
            else
            {
                Console.WriteLine("Incorrect. The answer was {0}", result);
                Console.WriteLine("Press any key to view time stats");
            }

            Console.ReadKey();

            return (correct, span);
        }

        private int GetUserResponse()
        {
            int response;
            bool success = false;
            do
            {
                success = Int32.TryParse(Console.ReadLine(), out response);
            }
            while (!success);

            return response;
        }

        private (int, int, int) GetNextQuestion()
        {
            if (Operation is null)
            {
                return (0, 0, 0);
            }

            int operandA = Rng.Next(0, 100);
            int operandB = Rng.Next(0, 100);
            
            while (!Operation.IsValidOperands(operandA, operandB))
            {
                operandA = Rng.Next(0, 100);
                operandB = Rng.Next(0, 100);
            }

            return (operandA, operandB, Operation.Execute(operandA, operandB));
        }

        private void DisplayQuestion(int opA, int opB)
        {
            if (Operation is null)
            {
                return;
            }

            Console.Clear();
            Console.WriteLine("{0} game", Operation.GetTextString());
            Console.WriteLine("{0} {1} {2} = ?", opA, Operation.GetSymbolChar(), opB);
        }

        private bool CheckResult(int correctResult, int userResult)
        {
            return correctResult == userResult;
        }

        public void SetNumberOfRounds()
        {
            int rounds = 0; 
            while (rounds <= 0)
            {
                Console.Clear();
                Console.WriteLine("Enter number of rounds to play");
                rounds = GetUserResponse();
                if (rounds <= 0)
                {
                    Console.WriteLine("Invalid number of rounds set. Must be a positive and non-zero integer");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
                else
                {
                    NumberOfRounds = rounds;
                    Console.WriteLine("Rounds set to {0}", NumberOfRounds);
                }
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
