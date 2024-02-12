using System;
using ToineElo.Configuration;
using Microsoft.ML.Probabilistic.Models;
using Microsoft.ML.Probabilistic.Distributions;

namespace ToineElo.Objects
{
    class Program
    {
        static void Main(string[] args)
        {
            ToineEloEnvironment environment = new ToineEloEnvironment();
            Player Jets = environment.CreatePlayer();
            Player Eagles = environment.CreatePlayer();
            Console.WriteLine($"Jets' Skill Mean: {Jets.Skill.GetMean()}");
            Console.WriteLine($"Jets' Skill Standard Deviation: {Jets.Skill.GetVariance()}");
            Console.WriteLine($"Eagles' Skill Mean: {Eagles.Skill.GetMean()}");
            Console.WriteLine($"Eagles' Skill Standard Deviation: {Eagles.Skill.GetVariance()}");
            environment.Rate1vs1(Jets, Eagles, true);
            Console.WriteLine($"Jets' Skill Mean: {Jets.Skill.GetMean()}");
            Console.WriteLine($"Jets' Skill Standard Deviation: {Jets.Skill.GetVariance()}");
            Console.WriteLine($"Eagles' Skill Mean: {Eagles.Skill.GetMean()}");
            Console.WriteLine($"Eagles' Skill Standard Deviation: {Eagles.Skill.GetVariance()}");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}