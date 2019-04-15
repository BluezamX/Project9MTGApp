using MagicTheGatheringApp.Models.MTG;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicTheGatheringApp.Managers
{
  public class ClusterManager
  {
    public static void Begin(List<ClusterObject> data, string[] types, int clusterCount, int iterations)
    {
      System.Diagnostics.Debug.WriteLine("Begin clustering");

      System.Diagnostics.Debug.WriteLine("Raw Data:");
      System.Diagnostics.Debug.WriteLine("\n");

      //float[][] data = ProcessRawData(rawData, types);

      ShowData(data);

      System.Diagnostics.Debug.WriteLine("Amount of clusters: " + iterations);

      KMeansResults result = Clustering(data, clusterCount, iterations);

      System.Diagnostics.Debug.WriteLine("Operations complete.");
    }

    private static KMeansResults Clustering(List<ClusterObject> rawData, int clusterCount, int iterations)
    {
      double[][] data = ProcessData(rawData);

      bool hasChanges = true;
      int currentIteration = 0;
      double totalDistance = 0;
      int numData = data.Length;
      int numAttributes = 8;

      // Start with random initial Cluster assignment
      int[] clustering = InitializeClustering(numData, clusterCount);

      // Create Cluster means and centroids
      double[][] means = CreateMatrix(clusterCount, numAttributes);
      int[] centroidIdx = new int[clusterCount];
      int[] clusterItemCount = new int[clusterCount];

      // Perform clustering
      while (hasChanges && currentIteration < iterations)
      {
        clusterItemCount = new int[clusterCount];
        totalDistance = CalcClusteringInfo(data, clustering, ref means, ref centroidIdx, clusterCount, ref clusterItemCount);

        hasChanges = AssignClustering(data, clustering, centroidIdx, clusterCount);
        currentIteration++;
      }

      // Creation of final clusters
      ClusterObject[][] clusters = new ClusterObject[clusterCount][];
      for (int k = 0; k < clusters.Length; k++)
        clusters[k] = new ClusterObject[clusterItemCount[k]];

      int[] clustersCurIdx = new int[clusterCount];
      for (int i = 0; i < clustering.Length; i++)
      {
        clusters[clustering[i]][clustersCurIdx[clustering[i]]] = rawData[i];
        clustersCurIdx[clustering[i]]++;
      }

      return new KMeansResults(clusters, means, centroidIdx, totalDistance);
    }

    private static double[][] ProcessData(List<ClusterObject> rawData)
    {
      double[][] result = new double[rawData.Count][];

      double[] values = new double[8];
      for (int i = 0; i < rawData.Count; i++)
      {
        values = new double[] { rawData[i].name, rawData[i].cost, rawData[i].type, rawData[i].effect, rawData[i].keyword, rawData[i].power, rawData[i].toughness, rawData[i].loyalty };
        result[i] = values;
      }

      return result;
    }

    private static int[] InitializeClustering(int numData, int clusterCount)
    {
      var rng = new Random();
      var clustering = new int[numData];

      for (int i = 0; i < numData; i++)
        clustering[i] = rng.Next(0, clusterCount);

      return clustering;
    }

    private static double[][] CreateMatrix(int rows, int columns)
    {
      var matrix = new double[rows][];

      for (int i = 0; i < matrix.Length; i++)
        matrix[i] = new double[columns];

      return matrix;
    }

    private static double CalcClusteringInfo(double[][] data, int[] clustering, ref double[][] means, ref int[] centroidIdx, int clusterCount, ref int[] clusterItemCount)
    {
      // Reset all cluster means
      foreach (var mean in means)
        for (int i = 0; i < mean.Length; i++)
          mean[i] = 0;

      // Calculate the cluster means
      for (int i = 0; i < data.Length; i++)
      {
        // Sum the means
        var row = data[i];
        var clusterIdx = clustering[i];
        clusterItemCount[clusterIdx]++;
        for (int j = 0; j < row.Length; j++)
          means[clusterIdx][j] += row[j];
      }

      // Divide to get the average
      for (int k = 0; k < means.Length; k++)
      {
        for (int l = 0; l < means[k].Length; l++)
        {
          int itemCount = clusterItemCount[k];
          means[k][l] /= itemCount > 0 ? itemCount : 1;
        }
      }

      double totalDistance = 0;
      // Calculate the centroids
      double[] minDistances = new double[clusterCount].Select(x => double.MaxValue).ToArray();

      for (int i = 0; i < data.Length; i++)
      {
        var clusterIdx = clustering[i];
        // What cluster is I assigned to
        var distance = CalcDistance(data[i], means[clusterIdx]);
        totalDistance += distance;
        if (distance < minDistances[clusterIdx])
        {
          minDistances[clusterIdx] = distance;
          centroidIdx[clusterIdx] = i;
        }
      }

      return totalDistance;
    }

    private static bool AssignClustering(double[][] data, int[] clustering, int[] centroidIdx, int clusterCount)
    {
      bool changed = false;

      for (int i = 0; i < data.Length; i++)
      {
        double minDistance = double.MaxValue;
        int minClusterIndex = -1;

        for (int k = 0; k < clusterCount; k++)
        {
          double distance = CalcDistance(data[i], data[centroidIdx[k]]);
          if (distance < minDistance)
          {
            minDistance = distance;
            minClusterIndex = k;
          }
        }

        // Re-arrange datapoint clustering if needed
        if (minClusterIndex != -1 && clustering[i] != minClusterIndex)
        {
          changed = true;
          clustering[i] = minClusterIndex;
        }
      }

      return changed;
    }

    private static double CalcDistance(double[] point, double[] centroid)
    {
      double sum = 0;
      for (int i = 0; i < point.Length; i++)
        sum += Math.Pow(centroid[i] - point[i], 2);

      return Math.Sqrt(sum);
    }

    private static void ShowData(List<ClusterObject> data)
    {
      for (int i = 0; i < data.Count; ++i)
      {
        System.Diagnostics.Debug.WriteLine(data[i].cardName + " : " + data[i].name + " : " + data[i].cost + " : " + data[i].type + " : " + data[i].effect + " : " + data[i].keyword + " : " + data[i].power + " : " + data[i].toughness + " : " + data[i].loyalty + " : " + data[i].average);
      }
    }
  }

  public class KMeansResults
  {
    public ClusterObject[][] clusters { get; private set; }
    public double[][] means { get; private set; }
    public int[] centroids { get; private set; }
    public double totalDistance { get; private set; }

    public KMeansResults(ClusterObject[][] cl, double[][] m, int[] ce, double t)
    {
      clusters = cl;
      means = m;
      centroids = ce;
      totalDistance = t;
    }
  }
}
