using MagicTheGatheringApp.Managers;
using MagicTheGatheringApp.Models.MTG;
using MagicTheGatheringApp.Pages;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MagicTheGatheringApp
{
  public partial class App : Application
  {
    public static NavigationPage navPage;
    public static string nameOfSet;
    public static Command searchCommand = new Command(async () =>
      {
        await navPage.PushAsync(new SearchPage());
      });

    public static Card clusterCard = new Card();
    public static string dataY = "toughness";
    public static string dataX = "power";
    public static  List<ClusterObject> clusters;

    public App()
    {
      try
      {
        InitializeComponent();
        DatabaseManager.Create();
        navPage = new NavigationPage(new MainPage());

        MainPage = navPage;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex);
      }
    }

    protected override void OnStart()
    {
      // Handle when your app starts
    }

    protected override void OnSleep()
    {
      // Handle when your app sleeps
    }

    protected override void OnResume()
    {
      // Handle when your app resumes
    }
  }
}
