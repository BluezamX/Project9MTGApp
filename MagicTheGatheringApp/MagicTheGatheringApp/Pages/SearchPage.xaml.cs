using MagicTheGatheringApp.CustomViewCells;
using MagicTheGatheringApp.Managers;
using MagicTheGatheringApp.Models.MTG;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicTheGatheringApp.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class SearchPage : ContentPage
  {
    private string name;
    private string types;
    private string text;

    public SearchPage()
    {
      InitializeComponent();
      searchResult.RowHeight = 60;
      Title = "Search";
      searchResult.ItemTemplate = new DataTemplate(typeof(CardViewCell));
    }

    private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
      List<Card> cards = new List<Card>();
      name = searchBar.Text;
      types = cardType.Text;
      text = cardText.Text;
      if (name == null)
      {
        name = "";
      }
      if (types == null)
      {
        types = "";
      }
      if (text == null)
      {
        text = "";
      }

      foreach (var set in DatabaseManager.GetSets())
      {
        if (set.downloaded)
        {
          if(name != "")
            cards.AddRange(DatabaseManager.GetCards(set).Where(x => x.name.Contains(name)));
          if (types != "")
            cards.AddRange(DatabaseManager.GetCards(set).Where(x => x.type.Contains(types)));
          if (text != "")
            cards.AddRange(DatabaseManager.GetCards(set).Where(x => x.text.Contains(text)));
        }
      }
      searchResult.ItemsSource = cards;
    }

    async void SearchResult_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      if (e.Item == null)
        return;

      CardPage ci = new CardPage((Card)e.Item);
      ci.WidthRequest = Application.Current.MainPage.Width;
      ci.HeightRequest = Application.Current.MainPage.Width * 1.4;
      await Navigation.PushModalAsync(ci);

      //Deselect Item
      ((ListView)sender).SelectedItem = null;
    }
  }
}