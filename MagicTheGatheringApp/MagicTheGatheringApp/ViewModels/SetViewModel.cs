using MagicTheGatheringApp.Managers;
using MagicTheGatheringApp.Models.MTG;
using MagicTheGatheringApp.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MagicTheGatheringApp.ViewModels
{
  public class SetViewModel : INotifyPropertyChanged
  {
    public ObservableCollection<MTGSet> items { get; set; }
    public ObservableCollection<MTGSet> Items
    {
      get => items;
      set
      {
        if (value == items) return;
        items = value;
        OnPropertyChanged(nameof(Items));
      }
    }

    private List<MTGSet> sets { get; set; }

    public ListView myListView;
    public ListView MyListView
    {
      get => myListView;
      set
      {
        if (value == myListView) return;
        myListView = value;
        OnPropertyChanged(nameof(MyListView));
      }
    }

    public ICommand updateCommand { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public SetViewModel(string setName)
    {
      updateCommand = new Command(async () =>
      {
        try
        {
          var list = await ApiManager.GetSets();
          DatabaseManager.AddSets(list);
          FillSets(setName);
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine(ex);
          await App.navPage.DisplayAlert("Error", ex.Message, "OK");
        }
      });

      FillSets(setName);
    }

    private void FillSets(string setName)
    {
      Items = new ObservableCollection<MTGSet>();
      if (DatabaseManager.SetTableFilled())
      {
        sets = DatabaseManager.GetSets(new List<string> { setName });

        foreach (MTGSet set in sets)
        {
          items.Add(set);
        }
      }
    }
  }
}
