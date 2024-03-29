using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomElevatorTimer
{
	public class WeightedRandomizer
	{
		private readonly List<WeightedValue> weightedValues;
		private readonly Random random;

		private double totalWeight;

		public WeightedRandomizer(IEnumerable<WeightedValue> values, Random random)
		{
			weightedValues = values.OrderByDescending(x=>x.GetWeight()).ToList();
			this.random = random;

			totalWeight = weightedValues.Sum(x => x.GetWeight());
		}

		public double GetValue()
		{
			double currentWeight = 0;
			double randomWeight = random.NextDouble() * totalWeight;

			WeightedValue currentWeightedValue = weightedValues.First();

			foreach (WeightedValue w in weightedValues)
			{
				currentWeightedValue = w;
				currentWeight += w.GetWeight();

				if (currentWeight > randomWeight)
				{
					break;
				}
			}

			return currentWeightedValue.GetValue();
		}
	}
}
