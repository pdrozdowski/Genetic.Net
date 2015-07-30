﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Genetic.Problems
{
    /*
    * I would like to have an evolution algorithm solving traveling salesman problem.
    * As problem is very hard to solve with numerous cities it will be limited to 10 for sake of the test.
    * 
    * Chromosome is a permutation of cities named A,B,C...
    * Length of genome will be fixed to number of cities.
    * Fitness will be calculated as total lenght of route through the cities, with favoring smallest value.
    * Mutation rate should be 2% and single-cut crossing should be applied.
    * For selection I would like to use 'roulette wheel' strategy.
    * I would like population to be not bigger than 50 chromosomes per step.
    * I would like to have 10 simultaneous population running, with a migration rate of 1 chromosomes happening witch 5% chance.
    * 
    * It is expected that algorithm will be able to find better solution than the greedy algorithm.
    * Timeouts after 1000 steps.
    */
    [TestClass]
    public class TravelingSalesmanProblem
    {
        [TestMethod]
        public void SolveTravelingSalesmanProblem()
        {
            var citiesCoordinates = new List<Location>()
            {
                new Location('A', 0, 0),
                new Location('B', 10, 100),
                new Location('C', 20, 20),
                new Location('D', 60, 20),
                new Location('E', 30, 10),
                new Location('F', 50, 70),
                new Location('G', 70, 30),
                new Location('H', 30, 30),
                new Location('I', 50, 80)
            };

            var random = new RandomNumberGenerators.Default();

            var genePool = new Chromosomes.GenePool<char>();
            genePool.AddRange(new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I' });

            var chromosomeFactory = new Chromosomes.PermutationChromosomeFactory<int>(random, genePool, 9); // chromosome as a permutation of 1-9 numbers
            var fitnessCalc = new TravelingSalesmanProblemFitnessCalculator(citiesCoordinates);
            var mutationStrategy = new MutationStrategies.SingleGeneMutation(random, 0.02); // mutation rate fixed to 2%
            var crossoverStrategy = new CrossoverStrategies.DoubleCut(random);
            var selectionStrategy = new SelectionStrategies.RouletteWheel(random);
            var migrationStrategy = new MigrationStrategies.Random(random, 0.05); // random migration with chance of occurence 5%
            var populationStrategy = new PopulationStrategies.MultiPopulation(10, 50, migrationStrategy); // 10 population with fixed size of 50

            var evolution = new Evolution(random,
                chromosomeFactory,
                populationStrategy,
                mutationStrategy,
                crossoverStrategy,
                selectionStrategy);

            do
            {
                evolution.Step();
                if (evolution.TopFitness == 1.0)
                {
                    return;
                }
            }
            while (evolution.Generation < 1000);
            Assert.Fail();
        }
    }

    class Location
    {
        public Location(char id, double x, double y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
        }

        public char Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    class TravelingSalesmanProblemFitnessCalculator : IFitnessCalculator<Chromosomes.IPermutationChromosome<char>>
    {
        private List<Location> citiesCoordinates;

        public TravelingSalesmanProblemFitnessCalculator(IEnumerable<Location> citiesCoordinates)
        {
            this.citiesCoordinates = new List<Location>(citiesCoordinates);
        }

        public double Calculate(Chromosomes.IPermutationChromosome<char> chromosome)
        {
            if(chromosome.Length != citiesCoordinates.Count)
            {
                throw new ArgumentOutOfRangeException("chromosome");
            }

            // calc euclid distance across all cities on route, order defined by chromosome

            return 0;
        }
    }
}
