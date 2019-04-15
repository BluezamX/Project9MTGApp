using MagicTheGatheringApp.Models.MTG;
using MagicTheGatheringApp.Models.Scryfall;
using System;
using System.Collections.Generic;

namespace MagicTheGatheringApp.Managers
{
  class ConversionManager
  {
    public static List<MTGSet> ConvertScryFallSet(List<ScryFallSet> scryfall)
    {
      List<MTGSet> newSet = new List<MTGSet>();
      scryfall = ConvertSetType(scryfall);
      foreach (ScryFallSet set in scryfall)
      {
        if (set.set_type != "treasure_chest" && set.set_type != "memorabilia")
          newSet.Add(new MTGSet(set.name, set.code, set.tcgplayer_id, set.card_count, set.block, set.set_type));
      }
      return newSet;
    }

    public static List<Card> ConvertScryFallCard(List<ScryFallCard> scryfall)
    {
      List<Card> newCard = new List<Card>();
      foreach (ScryFallCard card in scryfall)
      {
        try
        {
          newCard.Add(new Card(card.name, card.type_line, card.set, card.oracle_text, card.flavor_text, card.artist, card.collector_number, new Uri(card.image_uris.png), card.mana_cost, card.cmc, card.toughness, card.power, card.loyalty, card.id));
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine(ex);
        }
      }
      return newCard;
    }

    private static List<ScryFallSet> ConvertSetType(List<ScryFallSet> oldset)
    {
      List<ScryFallSet> newset = new List<ScryFallSet>();

      foreach (ScryFallSet set in oldset)
      {
        if (set.set_type == "expansion")
        {
          set.set_type = "Expansion";
        }
        else if (set.set_type == "core")
        {
          set.set_type = "Core";
        }
        else if (set.set_type == "promo")
        {
          set.set_type = "Promo";
        }
        else if (set.set_type == "box")
        {
          set.set_type = "Deck";
        }
        else if (set.set_type == "masters")
        {
          set.set_type = "Masters";
        }
        else if (set.set_type == "masterpiece")
        {
          set.set_type = "Promo";
        }
        else if (set.set_type == "duel_deck")
        {
          set.set_type = "Deck";
        }
        else if (set.set_type == "draft_innovation")
        {
          set.set_type = "Draft";
        }
        else if (set.set_type == "commander")
        {
          set.set_type = "Commander";
        }
        else if (set.set_type == "masterpiece")
        {
          set.set_type = "Promo";
        }
        else if (set.set_type == "planechase")
        {
          set.set_type = "Game Mode";
        }
        else if (set.set_type == "vanguard")
        {
          set.set_type = "Game Mode";
        }
        else if (set.set_type == "Archenemy")
        {
          set.set_type = "Game Mode";
        }
        else if (set.set_type == "funny")
        {
          set.set_type = "Silver Border";
        }
        newset.Add(set);
      }
      return newset;
    }
  }
}
