using Xamarin.Forms;

namespace MagicTheGatheringApp.CustomViewCells
{
  class DataViewCell : ViewCell
  {
    public DataViewCell()
    {
      var clusterLabel = new Label();
      var nameLabel = new Label();
      var typeLabel = new Label();
      var xLabel = new Label();
      var yLabel = new Label();
      var vLayout = new StackLayout();
      var vLayout2 = new StackLayout();
      var hLayout = new StackLayout();

      nameLabel.SetBinding(Label.TextProperty, new Binding("name"));
      clusterLabel.SetBinding(Label.TextProperty, new Binding("clusterNo"));
      typeLabel.SetBinding(Label.TextProperty, new Binding("type"));
      xLabel.SetBinding(Label.TextProperty, new Binding(App.dataX));
      yLabel.SetBinding(Label.TextProperty, new Binding(App.dataY));

      hLayout.Orientation = StackOrientation.Horizontal;
      hLayout.HorizontalOptions = LayoutOptions.Fill;
      vLayout.HorizontalOptions = LayoutOptions.End;
      vLayout2.HorizontalOptions = LayoutOptions.End;
      clusterLabel.FontSize = 12;
      typeLabel.FontSize = 12;
      xLabel.FontSize = 12;
      yLabel.FontSize = 12;
      nameLabel.FontSize = 24;

      vLayout.Children.Add(typeLabel);
      vLayout.Children.Add(clusterLabel);
      vLayout2.Children.Add(xLabel);
      vLayout2.Children.Add(yLabel);
      hLayout.Children.Add(nameLabel);
      hLayout.Children.Add(vLayout);
      hLayout.Children.Add(vLayout2);

      View = hLayout;
    }
  }
}