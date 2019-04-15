
using MagicTheGatheringApp.CustomViewCells;
using MagicTheGatheringApp.Managers;
using MagicTheGatheringApp.Models.MTG;
using MagicTheGatheringApp.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicTheGatheringApp.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SetPage : ContentPage
  {
    public string type;

    public SetPage(string type)
    {
      this.type = type;
      InitializeComponent();

      Title = "Pick a Set";
      ToolbarItems.Add(new ToolbarItem("Search", "", () =>
      {
        App.navPage.PushAsync(new SearchPage());
      }));
      SetViewModel viewModel = new SetViewModel(type);
      BindingContext = viewModel;

      MyListView.RowHeight = 60;
      MyListView.ItemTemplate = new DataTemplate(typeof(SetViewCell));
      //MyListView.ItemsSource = items;
    }

    async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      if (e.Item == null)
        return;

      try
      {
        if (!DatabaseManager.CardTableFilled((MTGSet)e.Item) && !ConnectionManager.CheckConnection())
        {
          await DisplayAlert("No Internet", "You do not have downloaded this cardlist, and no available internet connection.", "OK");
        }
        else
        {
          MTGSet set = (MTGSet)e.Item;
          List<Card> cards = new List<Card>();
          if (DatabaseManager.CardTableFilled(set))
          {
            cards = DatabaseManager.GetCards(set);
          }
          else
          {
            var list = await ApiManager.GetCards(set.code);
            DatabaseManager.AddCards(list);
            DatabaseManager.SetDownloaded(set);
          }

          CardListPage cl = new CardListPage((MTGSet)e.Item);
          await Navigation.PushAsync(cl);
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex);
        await DisplayAlert("Error", ex.Message, "OK");
      }
      //Deselect Item
      ((ListView)sender).SelectedItem = null;
    }
  }
}