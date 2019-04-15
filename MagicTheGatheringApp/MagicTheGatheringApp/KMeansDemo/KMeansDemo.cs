using System;

namespace MagicTheGatheringApp.KMeansDemo
{
  class KMeansDemo
  {
    static void Main(string[] args)
    {
      System.Diagnostics.Debug.WriteLine("Start Demo");
      double[][] rawData = new double[20][];
      rawData[0] = new double[] { 65.0, 220.0 };
      rawData[1] = new double[] { 73.0, 160.0 };
      rawData[2] = new double[] { 59.0, 110.0 };
      rawData[3] = new double[] { 61.0, 120.0 };
      rawData[4] = new double[] { 75.0, 150.0 };
      rawData[5] = new double[] { 67.0, 240.0 };
      rawData[6] = new double[] { 68.0, 230.0 };
      rawData[7] = new double[] { 70.0, 220.0 };
      rawData[8] = new double[] { 62.0, 130.0 };
      rawData[9] = new double[] { 66.0, 210.0 };
      rawData[10] = new double[] { 77.0, 190.0 };
      rawData[11] = new double[] { 75.0, 180.0 };
      rawData[12] = new double[] { 74.0, 170.0 };
      rawData[13] = new double[] { 70.0, 210.0 };
      rawData[14] = new double[] { 61.0, 110.0 };
      rawData[15] = new double[] { 58.0, 100.0 };
      rawData[16] = new double[] { 66.0, 230.0 };
      rawData[17] = new double[] { 59.0, 120.0 };
      rawData[18] = new double[] { 68.0, 210.0 };
      rawData[19] = new double[] { 61.0, 130.0 };

      System.Diagnostics.Debug.WriteLine("Raw Data:");
      System.Diagnostics.Debug.WriteLine("Height // Weight\n");
      ShowData(rawData, 1, true, true);

      int numClusters = 3;
      System.Diagnostics.Debug.WriteLine("Amount of clusters: " + numClusters);

      int[] clustering = Clustering(rawData, numClusters);
      System.Diagnostics.Debug.WriteLine("Clustering complete.");

      System.Diagnostics.Debug.WriteLine("Final clustering in internal form:");
      ShowVector(clustering, true);

      System.Diagnostics.Debug.WriteLine("Amount of clusters: " + numClusters);
      ShowClustered(rawData, clustering, numClusters, 1);

      System.Diagnostics.Debug.WriteLine("End of program.");
      Console.ReadLine();
    }

    static int[] Clustering(double[][] rawData, int numClusters)
    {
      double[][] data = Normalized(rawData);
      bool changed = true;
      bool succes = true;

      int[] clustering = InitClustering(data.Length, numClusters, 0);
      double[][] means = Allocate(numClusters, data[0].Length);

      int maxCount = data.Length * 10;
      int count = 0;

      while (changed == true && succes == true && count < maxCount)
      {
        count++;
        succes = UpdateMeans(data, clustering, means);
        changed = UpdateClustering(data, clustering, means);
      }
      return clustering;
    }

    static double[][] Normalized(double[][] rawData)
    {
      double[][] result = new double[rawData.Length][];
      for (int i = 0; i < rawData.Length; i++)
      {
        result[i] = new double[rawData[i].Length];
        Array.Copy(rawData[i], result[i], rawData[i].Length);
      }

      for (int j = 0; j < result[0].Length; j++)
      {
        double colSum = 0.0;
        for (int i = 0; i < result.Length; i++)
        {
          colSum += result[i][j];
        }
        double mean = colSum / result.Length;

        double sum = 0.0;
        for (int i = 0; i < result.Length; i++)
          sum += (result[i][j] - mean) * (result[i][j] - mean);

        double sd = sum / result.Length;
        for (int i = 0; i < result.Length; i++)
          result[i][j] = (result[i][j] - mean) / sd;
      }
      return result;
    }

    static int[] InitClustering(int numTuples, int numClusters, int seed)
    {
      Random rng = new Random(seed);
      int[] clustering = new int[numTuples];
      for (int i = 0; i < numClusters; i++)
        clustering[i] = 1;

      for (int i = numClusters; i < clustering.Length; i++)
        clustering[i] = rng.Next(0, numClusters);
      return clustering;
    }

    static double[][] Allocate(int numClusters, int numColumns)
    {
      double[][] result = new double[numClusters][];
      for (int k = 0; k < numClusters; k++)
        result[k] = new double[numColumns];
      return result;
    }

    static bool UpdateMeans(double[][] data, int[] clustering, double[][] means)
    {
      int numClusters = means.Length;
      int[] clusterCounts = new int[numClusters];
      for (int i = 0; i < data.Length; i++)
      {
        int cluster = clustering[i];
        clusterCounts[cluster]++;
      }

      for (int k = 0; k < numClusters; k++)
      {
        if (clusterCounts[k] == 0)
          return false;
      }

      for (int k = 0; k < means.Length; k++)
        for (int j = 0; j < means[k].Length; j++)
          means[k][j] = 0.0;

      for (int i = 0; i < data.Length; i++)
      {
        int cluster = clustering[i];
        for (int j = 0; j < data[i].Length; j++)
          means[cluster][j] += data[i][j];
      }

      for (int k = 0; k < means.Length; k++)
        for (int j = 0; j < means[k].Length; j++)
          means[k][j] /= clusterCounts[k];

      return true;
    }

    static bool UpdateClustering(double[][] data, int[] clustering, double[][] means)
    {
      int numClusters = means.Length;
      bool changed = false;

      int[] newClustering = new int[clustering.Length];
      Array.Copy(clustering, newClustering, clustering.Length);

      double[] distances = new double[numClusters];

      for (int i = 0; i < data.Length; i++)
      {
        for (int k = 0; k < numClusters; k++)
          distances[k] = Distance(data[i], means[k]);

        int newClusterId = MinIndex(distances);
        if (newClusterId != newClustering[i])
        {
          changed = true;
          newClustering[i] = newClusterId;
        }

        if (!changed)
          return changed;

        int[] clusterCounts = new int[numClusters];
        for (int j = 0; j < data.Length; j++)
        {
          int cluster = newClustering[j];
          clusterCounts[cluster]++;
        }

        for (int k = 0; k < numClusters; k++)
        {
          if (clusterCounts[k] == 0)
            return false;
        }

        Array.Copy(newClustering, clustering, newClustering.Length);
      }
      return changed;
    }

    static double Distance(double[] tuple, double[] mean)
    {
      double sumSquaredDiffs = 0.0;
      for (int i = 0; i < tuple.Length; i++)
        sumSquaredDiffs += Math.Pow((tuple[i] - mean[i]), 2);
      return Math.Sqrt(sumSquaredDiffs);
    }

    static int MinIndex(double[] distances)
    {
      int indexOfMin = 0;
      double smallDist = distances[0];
      for (int k = 0; k < distances.Length; k++)
      {
        if (distances[k] < smallDist)
        {
          smallDist = distances[k];
          indexOfMin = k;
        }
      }

      return indexOfMin;
    }

    static void ShowData(double[][] data, int decimals, bool indices, bool newLine)
    {
      for (int i = 0; i < data.Length; ++i)
      {
        if (indices) Console.Write(i.ToString().PadLeft(3) + " ");
        for (int j = 0; j < data[i].Length; ++j)
        {
          if (data[i][j] >= 0.0) Console.Write(" ");
          Console.Write(data[i][j].ToString("F" + decimals) + " ");
        }
        System.Diagnostics.Debug.WriteLine("");
      }
      if (newLine) System.Diagnostics.Debug.WriteLine("");
    }

    static void ShowVector(int[] vector, bool newLine)
    {
      for (int i = 0; i < vector.Length; ++i)
        Console.Write(vector[i] + " ");
      if (newLine) System.Diagnostics.Debug.WriteLine("\n");
    }

    static void ShowClustered(double[][] data, int[] clustering, int numClusters, int decimals)
    {
      for (int k = 0; k < numClusters; ++k)
      {
        System.Diagnostics.Debug.WriteLine("===================");
        for (int i = 0; i < data.Length; ++i)
        {
          int clusterID = clustering[i];
          if (clusterID != k) continue;
          Console.Write(i.ToString().PadLeft(3) + " ");
          for (int j = 0; j < data[i].Length; ++j)
          {
            if (data[i][j] >= 0.0) Console.Write(" ");
            Console.Write(data[i][j].ToString("F" + decimals) + " ");
          }
          System.Diagnostics.Debug.WriteLine("");
        }
        System.Diagnostics.Debug.WriteLine("===================");
      }
    }
  }
}