using System;
using System.Collections.Generic;

namespace CommandPattern
{
    // Command
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    // Receiver
    public class Sportsperson
    {
        public string name { get; set; }
        public Sportsperson(string _name)
        {
            name = _name;
        }
    }
    // ConcreteCommand
    public class Running : ICommand
    {
        public Sportsperson player { get; set; }
        public Running(Sportsperson _player)
        {
            player = _player;
        }
        public void Execute()
        {
            Console.WriteLine("{0} runs\n", player.name);
        }

        public void Undo()
        {
            Console.WriteLine("{0} - remove runs from your training plan\n", player.name);
        }
    }
    // ConcreteCommand
    public class Jumping : ICommand
    {
        public Sportsperson player { get; set; }
        public Jumping(Sportsperson _player)
        {
            player = _player;
        }
        public void Execute()
        {
            Console.WriteLine("{0} jumps\n", player.name);
        }

        public void Undo()
        {
            Console.WriteLine("{0} - remove jumping from your training plan\n", player.name);
        }
    }
    // ConcreteCommand
    public class Gym : ICommand
    {
        public Sportsperson player { get; set; }
        public Gym(Sportsperson _player)
        {
            player = _player;
        }
        public void Execute()
        {
            Console.WriteLine("{0} lifts weights at the gym\n", player.name);
        }

        public void Undo()
        {
            Console.WriteLine("{0} - remove lifts weights at the gym from your training plan\n", player.name);
        }
    }
    // Invoker
    public class Trener
    {
        private readonly List<ICommand> traning = new List<ICommand>();
        public void AddTraning(ICommand _traning)
        {
            traning.Add(_traning);
        }

        public void Train()
        {
            // Apply traning in the order they were added.
            foreach (ICommand train in traning)
            {
                train.Execute();
            }
        }
        public void RemoveTrain(int index)
        {
            var train = traning[index];
            train.Undo();
            traning.Remove(train);
        }
    }
    class Program
    {
        static void WriteDiffrentColor(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }
        static void Main(string[] args)
        {
            Trener enduranceTraning = new Trener();
            Trener strengthTraining = new Trener();
            Sportsperson sabina = new Sportsperson("Sabina");
            Sportsperson pawel = new Sportsperson("Pawel");

            WriteDiffrentColor("'Sabina' traning");
            enduranceTraning.AddTraning(new Running(sabina));
            enduranceTraning.AddTraning(new Jumping(sabina));
            enduranceTraning.AddTraning(new Gym(sabina));
            enduranceTraning.Train();

            WriteDiffrentColor("'Pawel' traning");
            strengthTraining.AddTraning(new Gym(pawel));
            strengthTraining.AddTraning(new Running(pawel));
            strengthTraining.Train();

            WriteDiffrentColor("Remove the gym from a training plan 'Sabina'");
            enduranceTraning.RemoveTrain(2);
            enduranceTraning.Train();

            WriteDiffrentColor("Remove the gym from a training plan 'Pawel'");
            strengthTraining.RemoveTrain(0);
            strengthTraining.Train();
            Console.ReadKey();
        }
    }
}
