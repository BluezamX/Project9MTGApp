using System.Collections.Generic;
using Xamarin.Forms;

namespace MagicTheGatheringApp.Pages
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
      Title = "Pick a set type";
      Startup();
    }

    private void Startup()
    {
      var btnList = new List<ButtonObject> {
        new ButtonObject("Expansion"),
        new ButtonObject("Core"),
        new ButtonObject("Draft"),
        new ButtonObject("Commander"),
        new ButtonObject("Deck"),
        new ButtonObject("Promo"),
        new ButtonObject("Silver Border")
      };
      buttonList.Children.Clear();
      foreach (var item in btnList)
      {
        var btn = new Button()
        {
          Text = item.text
        };
        btn.Clicked += delegate
        {
          App.navPage.PushAsync(new SetPage(btn.Text));
        };
        buttonList.Children.Add(btn);
      }
    }
  }

  public class ButtonObject
  {
    public string text;

    public ButtonObject(string title)
    {
      text = title;
    }
  }
}
