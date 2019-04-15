
using MagicTheGatheringApp.Managers;
using MagicTheGatheringApp.Models.MTG;
using Plugin.Clipboard;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MagicTheGatheringApp.Pages
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class CardPage : ContentPage
  {
    private Card card;
    private ICommand searchCommand = App.searchCommand;

    public CardPage(Card _card)
    {
      card = _card;
      InitializeComponent();
      StartUp();
    }

    public void StartUp()
    {
      ToolbarItems.Add(new ToolbarItem("Search", "", () =>
      {
        App.navPage.PushAsync(new SearchPage());
      }));

      cardImage.Source = card.imageUrl;
      cardImage.Aspect = Aspect.Fill;
      cardImage.WidthRequest = Application.Current.MainPage.Width;
      cardImage.HeightRequest = Application.Current.MainPage.Width * 1.4;

      cardText.Text = card.name + "\n"
        + card.manacost + "\n"
        + card.type + " - " + card.set + "\n\n"
        + card.text + "\n"
        + card.power + " / " + card.toughness + "\n"
        + card.loyalty + "\n\n"
        + "Artist: " + card.artist;

      cardButton.Clicked += delegate
      {
        string clipboardText = card.name + "\n";
        clipboardText += card.manacost + "\n";

        clipboardText += card.type + " - " + card.set + "\n" + "\n"
              + card.text + "\n";
        //+ card.lore + "\n";
        clipboardText += card.power + " / " + card.toughness + "\n";

        clipboardText += card.loyalty + "\n";

        clipboardText += "\nArtist: " + card.artist;

        CrossClipboard.Current.SetText(clipboardText);
      };

      clusterButton.Clicked += delegate
      {
        App.clusterCard = card;
        App.navPage.PushAsync(new DataPage());
      };

      if (ConnectionManager.CheckConnection())
      {
        cardText.IsVisible = false;
      }
      else
      {
        cardImage.IsVisible = false;
      }
    }
  }
}