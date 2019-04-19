using SQLite;
using System;

namespace MagicTheGatheringApp.Models.MTG
{
  [Table("Card")]
  public class Card
  {
    public string name { get; set; }
    public string type { get; set; }
    public string set { get; set; }
    public string text { get; set; }
    public string lore { get; set; }
    public string artist { get; set; }
    public string number { get; set; }
    public Uri imageUrl { get; set; }
    [PrimaryKey, MaxLength(64)]
    public string id { get; set; }
    public string power { get; set; }
    public string toughness { get; set; }
    public string manacost { get; set; }
    public float cmc { get; set; }
    public string loyalty { get; set; }
    public string clusterNo { get; set; }

    public Card() { }

    public Card(string _name, string _type, string _set, string _text, string _lore, string _artist, string _number, Uri _imageUrl, string _manacost, float _cmc, string _toughness, string _power, string _loyalty, string _id)
    {
      name = _name;
      type = _type;
      set = _set;
      text = _text;
      lore = _lore;
      artist = _artist;
      number = _number;
      imageUrl = _imageUrl;
      manacost = _manacost;
      cmc = _cmc;
      power = _power;
      toughness = _toughness;
      loyalty = _loyalty;
      id = _id;
      clusterNo = "cluster: ";
    }
  }
}
