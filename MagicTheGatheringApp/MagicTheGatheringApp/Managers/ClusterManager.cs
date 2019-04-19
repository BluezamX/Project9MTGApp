using MagicTheGatheringApp.Models.MTG;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicTheGatheringApp.Managers
{
  public class ClusterManager
  {
    public static KMeansResults Begin(List<ClusterObject> data, int numCluster, int iterations)
    {
      System.Diagnostics.Debug.WriteLine("Begin clustering");

      System.Diagnostics.Debug.WriteLine("Raw Data:");
      System.Diagnostics.Debug.WriteLine("\n");

      //float[][] data = ProcessRawData(rawData, types);

      ShowData(data);
      System.Diagnostics.Debug.WriteLine("Amount of clusters: " + iterations);

      KMeansResults result = Clustering(data, numCluster, iterations);

      System.Diagnostics.Debug.WriteLine("Operations complete.");
      ShowKMeans(result);
      return result;
    }

    private static KMeansResults Clustering(List<ClusterObject> rawData, int numCluster, int iterations)
    {
      double[][] data = ProcessData(rawData);

      bool hasChanges = true;
      int currentIteration = 0;
      double totalDistance = 0;
      int numData = data.Length;
      int numAttributes = 8;

      // Start with random initial Cluster assignment
      int[] clustering = InitializeClustering(numData, numCluster);

      // Create Cluster means and centroids
      double[][] means = CreateMatrix(numCluster, numAttributes);
      int[] centroidIdx = new int[numCluster];
      int[] clusterItemCount = new int[numCluster];

      // Perform clustering
      while (hasChanges && currentIteration < iterations)
      {
        System.Diagnostics.Debug.WriteLine("\nIterate\n");
        clusterItemCount = new int[numCluster];
        totalDistance = CalcClusteringInfo(data, clustering, ref means, ref centroidIdx, numCluster, ref clusterItemCount);

        hasChanges = AssignClustering(data, clustering, centroidIdx, numCluster);
        currentIteration++;
      }

      // Creation of final clusters
      ClusterObject[][] clusters = new ClusterObject[numCluster][];
      for (int k = 0; k < clusters.Length; k++)
        clusters[k] = new ClusterObject[clusterItemCount[k]];

      int[] clustersCurIdx = new int[numCluster];
      for (int i = 0; i < clustering.Length; i++)
      {
        try
        {
          int clusterValue = clustering[i];
          int idxValue = clustersCurIdx[clusterValue];

          clusters[clusterValue][idxValue] = rawData[i];
          clustersCurIdx[clusterValue]++;
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine("" + i + ex);
        }
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
        if (data != null)
          System.Diagnostics.Debug.WriteLine(data[i].cardName + " : " + data[i].name + " : " + data[i].cost + " : " + data[i].type + " : " + data[i].effect + " : " + data[i].keyword + " : " + data[i].power + " : " + data[i].toughness + " : " + data[i].loyalty + " : " + data[i].average);
      }
    }

    private static void ShowKMeans(KMeansResults data)
    {
      for (int i = 0; i < data.clusters.Length; ++i)
      {
        System.Diagnostics.Debug.WriteLine("\n\nCluster" + i + ": ");
        for (int j = 0; j < data.clusters[i].Length; j++)
        {
          if (data.clusters[i][j] != null)
            System.Diagnostics.Debug.WriteLine(data.clusters[i][j].cardName + " : " + data.clusters[i][j].name + " : " + data.clusters[i][j].cost + " : " + data.clusters[i][j].type + " : " + data.clusters[i][j].effect + " : " + data.clusters[i][j].keyword + " : " + data.clusters[i][j].power + " : " + data.clusters[i][j].toughness + " : " + data.clusters[i][j].loyalty + " : " + data.clusters[i][j].average);
        }
      }
    }
  }

  public class KMeansResults
  {
    public ClusterObject[][] clusters { get; set; }
    public double[][] means { get; set; }
    public int[] centroids { get; set; }
    public double totalDistance { get; set; }

    public KMeansResults(ClusterObject[][] cl, double[][] m, int[] ce, double t)
    {
      try
      {
        clusters = cl.OrderBy(x => x.Average(y => y.power + y.toughness)).ToArray();
        means = m;
        centroids = ce;
        totalDistance = t;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine("\n\n\n"+ ex + "\n" + cl.Length + "\n\n\n");
        clusters = cl;
        means = m;
        centroids = ce;
        totalDistance = t;
      }
    }
  }
}
