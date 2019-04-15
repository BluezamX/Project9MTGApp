using System.Collections.Generic;
using System.Linq;

namespace MagicTheGatheringApp.Models.MTG
{
  public class ClusterObject
  {
    public string cardName;
    public float name;
    public float cost;
    public float type;
    public float keyword;
    public float effect;
    public float power;
    public float toughness;
    public float loyalty;
    public float average;

    public ClusterObject(string cn, float n, float c, float ty, float k, float e, float p, float to, float l)
    {
      cardName = cn;
      name = n;
      cost = c;
      type = ty;
      keyword = k;
      effect = e;
      power = p;
      toughness = to;
      loyalty = l;
      average = (n + c + ty + k + e + p + to + l) / 8;
    }

    public float GetValue(string wantedValue)
    {
      if (wantedValue == "name")
        return name;
      if (wantedValue == "cost")
        return cost;
      if (wantedValue == "type")
        return type;
      if (wantedValue == "keyword")
        return keyword;
      if (wantedValue == "effect")
        return effect;
      if (wantedValue == "power")
        return power;
      if (wantedValue == "toughness")
        return toughness;
      if (wantedValue == "loyalty")
        return loyalty;
      if (wantedValue == "average")
        return average;
      return 0f;
    }

    public static List<ClusterObject> OrderBy(List<ClusterObject> objects, string value)
    {
      if (value == "name")
        objects.OrderBy(x => x.name);
      else if(value == "cost")
        objects.OrderBy(x => x.cost);
      else if (value == "type")
        objects.OrderBy(x => x.type);
      else if (value == "keyword")
        objects.OrderBy(x => x.keyword);
      else if (value == "effect")
        objects.OrderBy(x => x.effect);
      else if (value == "power")
        objects.OrderBy(x => x.power);
      else if (value == "toughness")
        objects.OrderBy(x => x.toughness);
      else if (value == "loyalty")
        objects.OrderBy(x => x.loyalty);
      else
        objects.OrderBy(x => x.average);
      return objects;
    }
  }
}
