using System;
using System.Collections.Generic;

public class WeightedRandom
{
    private Random random;
    private List<int> probabilities;

    public WeightedRandom(List<int> weights)
    {
        random = new Random();
        probabilities = new List<int>();

        int totalWeight = 0;
        foreach (int weight in weights)
        {
            totalWeight += weight;
            probabilities.Add(totalWeight);
        }
    }

    public int GetRandomIndex()
    {
        int randomNumber = random.Next(probabilities[probabilities.Count - 1]);

        for (int i = 0; i < probabilities.Count; i++)
        {
            if (randomNumber < probabilities[i])
            {
                return i;
            }
        }

        return 0; // Default value, should not happen
    }
}
