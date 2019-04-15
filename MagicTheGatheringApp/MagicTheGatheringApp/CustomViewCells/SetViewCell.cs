using Xamarin.Forms;

namespace MagicTheGatheringApp.CustomViewCells
{
  public class SetViewCell : ViewCell
  {
    public SetViewCell()
    {
      var icon = new Image();
      var nameLabel = new Label();
      var hLayout = new StackLayout();

      nameLabel.SetBinding(Label.TextProperty, new Binding("name"));
      //icon.SetBinding(Image.SourceProperty, new Binding("image"));

      hLayout.Orientation = StackOrientation.Horizontal;
      hLayout.HorizontalOptions = LayoutOptions.Fill;
      icon.HorizontalOptions = LayoutOptions.End;
      nameLabel.FontSize = 24;

      hLayout.Children.Add(nameLabel);
      hLayout.Children.Add(icon);

      View = hLayout;
    }
  }
}
