using MagicTheGatheringApp.Models.MTG;
using MagicTheGatheringApp.Models.Scryfall;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MagicTheGatheringApp.Managers
{
  public class ApiManager
  {
    private static readonly string baseUrl = "https://api.scryfall.com";
    private static readonly string setsUrl = "/sets";
    private static readonly string cardsUrl = "/cards/search?q=set%3A";

    public static void TimeRequest()
    {
      float time = (DateTime.Now - (DateTime)App.Current.Properties["last_request"]).Milliseconds;
      if (time >= 100)
        System.Threading.Thread.Sleep((int)time);
    }

    public static async Task<List<MTGSet>> GetSets()
    {
      using (var client = new HttpClient())
      {
        List<MTGSet> sets = new List<MTGSet>();

        var response = await client.GetAsync(baseUrl + setsUrl);

        if (response.IsSuccessStatusCode)
        {
          //System.Diagnostics.Debug.WriteLine(await response.Content.ReadAsStringAsync());
          var result = JsonConvert.DeserializeObject<ScryFallSetType>(await response.Content.ReadAsStringAsync());
          var list = result.data;
          sets.AddRange(ConversionManager.ConvertScryFallSet(list));

          if (result.has_more)
          {
            sets.AddRange(await GetSets());
          }
        }
        return sets;
      }
    }

    public static async Task<List<Card>> GetCards(string set)
    {
      using (var client = new HttpClient())
      {
        List<Card> cards = new List<Card>();
        var response = await client.GetAsync(baseUrl + cardsUrl + set);

        if (response.IsSuccessStatusCode)
        {
          var result = JsonConvert.DeserializeObject<ScryFallCardType>(await response.Content.ReadAsStringAsync());
          var list = result.data;
          cards.AddRange(ConversionManager.ConvertScryFallCard(list));

          if (result.has_more)
          {
            cards.AddRange(await RecursiveCards(result.next_page));
          }
        }
        return cards;
      }
    }

    public static async Task<List<Card>> RecursiveCards(string request)
    {
      using (var client = new HttpClient())
      {
        List<Card> cards = new List<Card>();
        var response = await client.GetAsync(request);

        if (response.IsSuccessStatusCode)
        {
          var result = JsonConvert.DeserializeObject<ScryFallCardType>(await response.Content.ReadAsStringAsync());
          var list = result.data;
          cards.AddRange(ConversionManager.ConvertScryFallCard(list));

          if (result.has_more)
          {
            cards.AddRange(await GetCards(result.next_page));
          }
        }
        return cards;
      }
    }
  }
}
