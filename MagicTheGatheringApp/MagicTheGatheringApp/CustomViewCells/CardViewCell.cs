using Xamarin.Forms;

namespace MagicTheGatheringApp.CustomViewCells
{
  class CardViewCell : ViewCell
  {
    public CardViewCell()
    {
      var manaLabel = new Label();
      var nameLabel = new Label();
      var typeLabel = new Label();
      var vLayout = new StackLayout();
      var hLayout = new StackLayout();

      nameLabel.SetBinding(Label.TextProperty, new Binding("name"));
      manaLabel.SetBinding(Label.TextProperty, new Binding("manacost"));
      typeLabel.SetBinding(Label.TextProperty, new Binding("type"));

      hLayout.Orientation = StackOrientation.Horizontal;
      hLayout.HorizontalOptions = LayoutOptions.Fill;
      vLayout.HorizontalOptions = LayoutOptions.End;
      manaLabel.FontSize = 12;
      typeLabel.FontSize = 12;
      nameLabel.FontSize = 24;

      vLayout.Children.Add(typeLabel);
      vLayout.Children.Add(manaLabel);
      hLayout.Children.Add(nameLabel);
      hLayout.Children.Add(vLayout);

      View = hLayout;
    }
  }
}
