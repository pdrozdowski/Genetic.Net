﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic.CrossoverStrategies
{
    public class SingleCut : ICrossoverStrategy
    {
        private RandomNumberGenerators.Default random;

        public SingleCut(RandomNumberGenerators.Default random)
        {
            // TODO: Complete member initialization
            this.random = random;
        }

        public List<IChromosome> Cross(IChromosome chromosomeA, IChromosome chromosomeB)
        {
            throw new NotImplementedException();
        }
    }
}
