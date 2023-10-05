using System;
using System.Collections.Generic;

public class WeightedRandom
{
    private Random _random;
    private List<int> _probabilities;

    public WeightedRandom(List<int> weights)
    {
        _random = new Random();
        _probabilities = new List<int>();

        int totalWeight = 0;
        foreach (int weight in weights)
        {
            totalWeight += weight;
            _probabilities.Add(totalWeight);
        }
    }

    public int GetRandomIndex()
    {
        int randomNumber = _random.Next(_probabilities[_probabilities.Count - 1]);

        for (int i = 0; i < _probabilities.Count; i++)
        {
            if (randomNumber < _probabilities[i])
            {
                return i;
            }
        }

        return 0; // Default value, should not happen
    }
}