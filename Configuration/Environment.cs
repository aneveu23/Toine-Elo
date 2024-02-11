using System;

namespace ToineElo.Configuration
{
    public class Environment
    {
        public double InitialMean { get; }
        public double InitialStDev { get; }
        public double Beta { get; }
        public double DynamicFactor { get; }
        public double DrawProbability { get; }

        public Environment(double initialMean = 25.0, double initialStDev = 25.0 / 3, double beta = 25.0 / 6, double dynamicsFactor = 25.0 / 300, double drawProbability = 0.1)
        {
            InitialMean = initialMean;
            InitialStDev = initialStDev;
            Beta = beta;
            DynamicFactor = dynamicsFactor;
            DrawProbability = drawProbability;
        }
    }
}
