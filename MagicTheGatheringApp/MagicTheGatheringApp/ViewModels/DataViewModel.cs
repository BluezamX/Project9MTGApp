using MagicTheGatheringApp.Managers;
using MagicTheGatheringApp.Models.MTG;
using Syncfusion.SfChart.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MagicTheGatheringApp.ViewModels
{
  class DataViewModel
  {
    private Card ogCard;
    private readonly MTGSet[] sets;

    public ObservableCollection<ChartDataPoint> Data { get; set; }

    public DataViewModel()
    {
      Data = new ObservableCollection<ChartDataPoint>();
      ogCard = App.clusterCard;
      sets = new MTGSet[] { DatabaseManager.GetSet(ogCard.set) };
      App.clusters = ClusterObjectManager.Cluster(ogCard, sets);

      // Scatterplot
      List<Tuple<float, float>> points = wantedData(App.clusters);
      foreach (var point in points)
      {
        Data.Add(new ChartDataPoint(point.Item1, point.Item2));
      }

      ClusterManager.Begin(App.clusters, new string[] { "power", "toughness" }, 3, 6);
    }

    private List<Tuple<float, float>> wantedData(List<ClusterObject> values)
    {
      List<Tuple<float, float>> response = new List<Tuple<float, float>>();

      foreach (ClusterObject value in values)
      {
        response.Add(new Tuple<float, float>
          (
          value.GetValue(App.dataX),
          value.GetValue(App.dataY)
          ));
      }
      return response;
    }
  }
}
