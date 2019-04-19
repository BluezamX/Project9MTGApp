using MagicTheGatheringApp.Models.MTG;
using System.Collections.Generic;

namespace MagicTheGatheringApp.Managers
{
  public static class ClusterObjectManager
  {
    // All scales go from 0-16

    public static List<ClusterObject> Cluster(Card ogCard, MTGSet set)
    {
      int totalCardCount = 0;
      totalCardCount = DatabaseManager.GetCards(set).Count;

      List<ClusterObject> toReturn = new List<ClusterObject>();

      foreach (Card card in DatabaseManager.GetCards(set))
      {
        toReturn.Add(new ClusterObject
          (
          card.name,
          FillName(ogCard, card),
          FillCost(ogCard, card),
          FillType(ogCard, card),
          FillKeyword(ogCard, card),
          FillEffect(ogCard, card),
          FillPower(ogCard, card),
          FillToughness(ogCard, card),
          FillLoyalty(ogCard, card)
          )
        );
      }

      return toReturn;
    }
    // !
    public static float FillName(Card toCompare, Card card)
    {
      int toReturn = 0;


      return toReturn;
    }

    public static float FillCost(Card toCompare, Card card)
    {
      float toReturn = 0;

      // Compare existing mc
      if (card.manacost.Contains("G"))
        if (!toCompare.manacost.Contains("G"))
          toReturn += 2;
      if (card.manacost.Contains("W"))
        if (!toCompare.manacost.Contains("W"))
          toReturn += 2;
      if (card.manacost.Contains("U"))
        if (!toCompare.manacost.Contains("U"))
          toReturn += 2;
      if (card.manacost.Contains("B"))
        if (!toCompare.manacost.Contains("B"))
          toReturn += 2;
      if (card.manacost.Contains("R"))
        if (!toCompare.manacost.Contains("R"))
          toReturn += 2;

      // Compare unexisting mc
      if (!card.manacost.Contains("G"))
        if (toCompare.manacost.Contains("G"))
          toReturn += 2;
      if (!card.manacost.Contains("W"))
        if (toCompare.manacost.Contains("W"))
          toReturn += 2;
      if (!card.manacost.Contains("U"))
        if (toCompare.manacost.Contains("U"))
          toReturn += 2;
      if (!card.manacost.Contains("B"))
        if (toCompare.manacost.Contains("B"))
          toReturn += 2;
      if (!card.manacost.Contains("R"))
        if (toCompare.manacost.Contains("R"))
          toReturn += 2;

      // Compare cmc
      float diff = card.cmc - toCompare.cmc;
      if (diff < 0)
        diff *= -1;
      toReturn += diff / 2;

      if (toReturn > 16)
        toReturn = 16;

      return toReturn;
    }
    // !
    public static float FillType(Card toCompare, Card card)
    {
      float toReturn = 0;

      if (card.type == toCompare.type)
        return 0;

      List<string> cardTypes = new List<string>(card.type.Split(' '));
      List<string> toCompareTypes = new List<string>(toCompare.type.Split(' '));

      foreach (string s in cardTypes)
      {
        if (!toCompareTypes.Contains(s))
          toReturn += 1;
      }

      return toReturn;
    }
    // !
    public static float FillKeyword(Card toCompare, Card card)
    {
      int toReturn = 0;


      return toReturn;
    }
    // !
    public static float FillEffect(Card toCompare, Card card)
    {
      int toReturn = 0;


      return toReturn;
    }

    public static float FillPower(Card toCompare, Card card)
    {
      float toReturn = 0;

      try
      {
        toReturn = (float.Parse(card.power) - float.Parse(toCompare.power));
      }
      catch
      {
        if (card.power == "X" && card.power == "X")
          toReturn = 0;
        else
          toReturn = 16;
      }
      return toReturn;
    }

    public static float FillToughness(Card toCompare, Card card)
    {
      float toReturn = 0;

      try
      {
        toReturn = (float.Parse(card.toughness) - float.Parse(toCompare.toughness));
      }
      catch
      {
        if (card.toughness == "X" && card.toughness == "X")
          toReturn = 0;
        else
          toReturn = 16;
      }
      return toReturn;
    }

    public static float FillLoyalty(Card toCompare, Card card)
    {
      float toReturn = 0;

      try
      {
        toReturn = (float.Parse(card.loyalty) - float.Parse(toCompare.loyalty));
        if (toReturn < 0)
          toReturn *= -1;
      }
      catch
      {
        if (card.loyalty == "X" && card.loyalty == "X")
          toReturn = 0;
      }
      return toReturn * 3.2f;
    }
  }
}
