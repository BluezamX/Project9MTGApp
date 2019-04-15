using MagicTheGatheringApp.Models.MTG;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace MagicTheGatheringApp.Managers
{
  public static class DatabaseManager
  {
    public static string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "database.db3");

    public static void Create()
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        //db.DropTable<MTGSet>();
        //db.DropTable<Card>();
        try
        {
          SetTableFilled();
          /*
          foreach (var set in GetSets())
          {
            foreach (var card in GetCards(set))
            {
              System.Diagnostics.Debug.WriteLine(set.name + " : " + card.name);
            }
          }
          //*/
        }
        catch
        {
          db.CreateTable<MTGSet>();
          db.CreateTable<Card>();
        }
      }
    }

    public static bool SetAdded(MTGSet set)
    {
      return set.downloaded;
    }

    public static bool SetTableFilled()
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        return db.Table<MTGSet>().ToList().Count > 0;
      }
    }

    public static bool CardTableFilled(MTGSet set)
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        //List<Card> cards = db.Table<Card>().ToList();
        return db.Table<Card>().Where(x => x.set == set.code).ToList().Count > 0;
      }
    }

    public static void AddSets(List<MTGSet> sets)
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        foreach (MTGSet set in sets)
        {
          db.InsertOrReplace(set);
        }
      }
    }

    public static void SetDownloaded(MTGSet set)
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        set.downloaded = true;
        db.Update(set);
      }
    }

    public static List<MTGSet> GetDownloaded()
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        return db.Table<MTGSet>().Where(s => s.downloaded).ToList();
      }
    }

    public static void AddCards(List<Card> cards)
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        foreach (Card card in cards)
        {
          db.InsertOrReplace(card);
        }
      }
    }

    public static List<MTGSet> GetSets()
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        return db.Table<MTGSet>().ToList();
      }
    }

    public static List<MTGSet> GetSets(List<string> type)
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        return db.Table<MTGSet>().Where(x => type.Contains(x.type)).ToList();
      }
    }

    public static MTGSet GetSet(string code)
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        return db.Table<MTGSet>().Where(x => x.code == code).ToArray()[0];
      }
    }

    public static List<Card> GetCards(MTGSet set)
    {
      using (SQLiteConnection db = new SQLiteConnection(dbPath))
      {
        string code = set.code;
        return db.Table<Card>().Where(x => x.set == code).ToList();
      }
    }
  }
}
