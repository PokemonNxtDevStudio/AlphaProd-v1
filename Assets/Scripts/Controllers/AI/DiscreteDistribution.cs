using UnityEngine;
using System.Collections.Generic;


public class DiscreteDistribution
{
    private int[] _alias;
    private float[] _probability;

    /// <summary>
    /// Generates a <see cref="DiscreteDistribution"/> using the given weights.
    /// </summary>
    /// <param name="weights">Relative weights to assign to each output. Needn't sum to one. Negatives, NaNs, and infinities are treated as zero.</param>
    public DiscreteDistribution(IList<float> weights)
    {
        _alias = new int[weights.Count];
        _probability = new float[weights.Count];

        PopulateTables(weights);
    }

    /// <summary>
    /// Samples the <see cref="DiscreteDistribution"/> randomly.
    /// </summary>
    /// <returns>The index of the selected weight. In the range zero to n-1, where n is the number of weights.</returns>
    public int Sample()
    {
        int entry = Random.Range(0, _alias.Length);
        if (Random.value < _probability[entry])
        {
            return entry;
        }
        else
        {
            return _alias[entry];
        }
    }


    private void PopulateTables(IList<float> weights)
    {
        Stack<int> smallWorklist;
        Stack<int> largeWorklist;

        float[] scaledWeights = GenerateWorkLists(weights, out smallWorklist, out largeWorklist);

        while (smallWorklist.Count > 0 && largeWorklist.Count > 0)
        {
            int smallItem = smallWorklist.Pop();
            int largeItem = largeWorklist.Pop();
            float smallProb = scaledWeights[smallItem];
            float largeProb = scaledWeights[largeItem];

            _probability[smallItem] = smallProb;
            _alias[smallItem] = largeItem;

            largeProb = (smallProb + largeProb) - 1f;
            scaledWeights[largeItem] = largeProb;

            if (largeProb > 1f)
            {
                largeWorklist.Push(largeItem);
            }
            else
            {
                smallWorklist.Push(largeItem);
            }
        }

        while (largeWorklist.Count > 0)
        {
            int largeItem = largeWorklist.Pop();
            _probability[largeItem] = 1f;
            _alias[largeItem] = largeItem;
        }

        while (smallWorklist.Count > 0)
        {
            int smallItem = smallWorklist.Pop();
            _probability[smallItem] = 1f;
            _alias[smallItem] = smallItem;
        }
    }

    private float[] GenerateWorkLists(IList<float> weights, out Stack<int> smallWorklist, out Stack<int> largeWorklist)
    {
        int itemCount = weights.Count;
        smallWorklist = new Stack<int>(itemCount);
        largeWorklist = new Stack<int>(itemCount);

        float[] scaledWeights = new float[itemCount];

        float totalWeight = 0f;

        for (int i = 0; i < itemCount; i++)
        {
            float weight = weights[i];
            // Ignore values that don't make for sensible probabilities.
            if (!float.IsNaN(weight) && weight >= 0f && weight < float.PositiveInfinity)
            {
                totalWeight += weight;
                scaledWeights[i] = weight;
            }
        }

        float scale = itemCount / totalWeight;

        for (int i = 0; i < itemCount; i++)
        {
            scaledWeights[i] *= scale;
            if (scaledWeights[i] < 1f)
            {
                smallWorklist.Push(i);
            }
            else
            {
                largeWorklist.Push(i);
            }
        }

        return scaledWeights;
    }
}