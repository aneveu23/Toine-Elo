using System;
using Microsoft.ML.Probabilistic.Models;
using Microsoft.ML.Probabilistic.Distributions;

namespace ToineElo.Objects
{
    public class Player
    {
        public Gaussian Skill { get; set; }

        public Player(double initialMean, double initialStDev)
        {
            Skill = new Gaussian(initialMean, initialStDev);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(25.0, 8.333);
            Console.WriteLine($"Player's Skill Mean: {player.Skill.GetMean()}");
            Console.WriteLine($"Player's Skill Standard Deviation: {player.Skill.GetVariance()}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static Gaussian UpdatePlayerSkill(Gaussian playerSkill, Gaussian opponentSkill, bool outcome)
        {
            Variable<double> playerSkillVar = Variable.Random(playerSkill);
            Variable<double> opponentSkillVar = Variable.Random(opponentSkill);

            Variable<bool> playerWins = playerSkillVar > opponentSkillVar;

            playerWins.ObservedValue = outcome;

            InferenceEngine engine = new InferenceEngine();
            Gaussian updatedPlayerSkill = engine.Infer<Gaussian>(playerSkillVar);

            return updatedPlayerSkill;
        }
    }
}