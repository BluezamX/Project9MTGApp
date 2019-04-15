using MagicTheGatheringApp.CustomViewCells;
using MagicTheGatheringApp.Managers;
using MagicTheGatheringApp.Models.MTG;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicTheGatheringApp.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class CardListPage : ContentPage
  {
    public ObservableCollection<Card> items { get; set; }
    private MTGSet set;
    private List<Card> cards { get; set; }
    private ICommand searchCommand = App.searchCommand;

    public CardListPage(MTGSet set)
    {
      this.set = set;
      InitializeComponent();
      StartUp();
    }

    private void StartUp()
    {
      cards = DatabaseManager.GetCards(set);

      items = new ObservableCollection<Card>();

      MyListView.RowHeight = 60;
      Title = "Pick a Card";
      ToolbarItems.Add(new ToolbarItem("Search", "", () =>
      {
        App.navPage.PushAsync(new SearchPage());
      }));
      MyListView.ItemTemplate = new DataTemplate(typeof(CardViewCell));
      foreach (Card card in cards)
      {
        items.Add(card);
      }
      MyListView.ItemsSource = items;
    }

    async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
    {
      if (e.Item == null)
        return;

      CardPage ci = new CardPage((Card)e.Item);
      ci.WidthRequest = Application.Current.MainPage.Width;
      ci.HeightRequest = Application.Current.MainPage.Width * 1.4;
      await App.navPage.PushAsync(ci);

      //Deselect Item
      ((ListView)sender).SelectedItem = null;
    }
  }
}