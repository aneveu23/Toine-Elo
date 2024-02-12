using System;
using Microsoft.ML.Probabilistic.Distributions;
using Microsoft.ML.Probabilistic.Models;

namespace ToineElo.Configuration
{
    public class Player
    {
        public Gaussian Skill { get; set; }

        public Player(double Mean, double StDev)
        {
            Skill = new Gaussian(Mean, StDev);
        }
    }

    public class ToineEloEnvironment
    {
        public double InitialMean { get; }
        public double InitialStDev { get; }
        public double Beta { get; }
        public double DynamicFactor { get; }
        public double DrawProbability { get; }

        public ToineEloEnvironment(double initialMean = 25.0, double initialStDev = 25.0 / 3, double beta = 25.0 / 6, double dynamicsFactor = 25.0 / 300, double drawProbability = 0.1)
        {
            InitialMean = initialMean;
            InitialStDev = initialStDev;
            Beta = beta;
            DynamicFactor = dynamicsFactor;
            DrawProbability = drawProbability;
        }
        public Player CreatePlayer()
        {
            return new Player(InitialMean, InitialStDev);
        }

        public void Rate1vs1(Player player1, Player player2, bool player1Wins)
        {
            double beta = Beta;

            Variable<double> player1Skill = Variable.GaussianFromMeanAndVariance(player1.Skill.GetMean(), player1.Skill.GetVariance()).Named("player1Skill");
            Variable<double> player2Skill = Variable.GaussianFromMeanAndVariance(player2.Skill.GetMean(), player2.Skill.GetVariance()).Named("player2Skill");

            Variable<double> player1Performance = Variable.GaussianFromMeanAndVariance(player1Skill, beta * beta).Named("player1Performance");
            Variable<double> player2Performance = Variable.GaussianFromMeanAndVariance(player2Skill, beta * beta).Named("player2Performance");

            Variable<bool> player1WinsVar = (player1Performance > player2Performance).Named("player1Wins");

            player1WinsVar.ObservedValue = player1Wins;

            InferenceEngine engine = new InferenceEngine();

            Gaussian updatedPlayer1Skill = engine.Infer<Gaussian>(player1Skill);
            Gaussian updatedPlayer2Skill = engine.Infer<Gaussian>(player2Skill);

            player1.Skill = updatedPlayer1Skill;
            player2.Skill = updatedPlayer2Skill;
        }
    }
}
