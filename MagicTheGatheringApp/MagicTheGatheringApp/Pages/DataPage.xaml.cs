using MagicTheGatheringApp.CustomViewCells;
using MagicTheGatheringApp.Managers;
using MagicTheGatheringApp.Models.MTG;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicTheGatheringApp.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class DataPage : ContentPage
  {
    public ObservableCollection<Card> items;
    public ObservableCollection<Card> Items
    {
      get { return items; }
      set
      {
        if (value == items) return;
        items = value;
        OnPropertyChanged(nameof(Items));
      }
    }

    private MTGSet set = new MTGSet();

    public DataPage()
    {
      InitializeComponent();
      Startup();
    }

    private void Startup()
    {
      items = new ObservableCollection<Card>();
      set = DatabaseManager.GetSet(App.clusterCard.set);

      cardList.RowHeight = 60;
      cardList.ItemTemplate = new DataTemplate(typeof(DataViewCell));

      List<Card> cards = DatabaseManager.GetCards(set);
      KMeansResults result = ClusterManager.Begin(ClusterObjectManager.Cluster(App.clusterCard, set), (int)Math.Sqrt(cards.Count / 2), 5);

      int i = 1;

      foreach (ClusterObject[] l in result.clusters)
      {
        foreach (ClusterObject c in l)
        {
          if (c != null)
          {
            Card searchResult = cards.Where(x => x.name == c.cardName).First();
            searchResult.clusterNo += i;
            searchResult.power = "pow dif: " + c.power;
            searchResult.toughness = "tou dif: " + c.toughness;
            items.Add(searchResult);
          }
          else
            System.Diagnostics.Debug.WriteLine("\n\n\n\n!!!" + i + "!!!\n\n\n\n");
        }
        i++;
      }
      cardList.ItemsSource = items;

      pickerAxisLeft.ItemsSource = new List<string> { "name", "cost", "type", "keyword", "effect", "power", "toughness", "loyalty", "average" };
      pickerAxisBottom.ItemsSource = new List<string> { "name", "cost", "type", "keyword", "effect", "power", "toughness", "loyalty", "average" };
      //pickerSort.ItemsSource = new List<string> { "name", "cost", "type", "keyword", "effect", "power", "toughness", "loyalty", "average" };

      // Get Clusters
      typeLabel.Text = "Visualisation of " + App.clusterCard.name;
      setLabel.Text += set.name;
    }

    async void CardList_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      if (e.Item == null)
        return;

      Card card = (Card)e.Item;

      CardPage ci = new CardPage(DatabaseManager.GetCard(card.name));
      ci.WidthRequest = Application.Current.MainPage.Width;
      ci.HeightRequest = Application.Current.MainPage.Width * 1.2;
      await Navigation.PushModalAsync(ci);

      //Deselect Item
      ((ListView)sender).SelectedItem = null;
    }

    private void PickerAxisLeft_SelectedIndexChanged(object sender, EventArgs e)
    {
      App.dataY = e.ToString();
    }

    private void PickerAxisBottom_SelectedIndexChanged(object sender, EventArgs e)
    {
      App.dataX = e.ToString();
    }

    //TODO: Add?
    private void PickerSort_SelectedIndexChanged(object sender, EventArgs e)
    {
      /*
      items = new ObservableCollection<CardCluster>();
      //App.clusters = ClusterObject.OrderBy(App.clusters, pickerSort.SelectedItem.ToString());
      List<Card> cards = DatabaseManager.GetCards(set);
      foreach (var cluster in App.clusters)
      {
        List<Card> result = cards.Where(x => x.name == cluster.cardName).ToList();
        if (result.Count > 1)
          System.Diagnostics.Debug.WriteLine("\n\n\n\n\n" + result[0].name + " : " + result[1].name + "\n\n\n\n\n");
        else if (result.Count > 0)
          items.Add(result[0]);
      }
      cardList.ItemsSource = items;
      */
    }
  }
}