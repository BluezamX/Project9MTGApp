namespace MagicTheGatheringApp.Models.Scryfall
{
  class CardFace
  {
    public string @object { get; set; }
    public string name { get; set; }
    public string mana_cost { get; set; }
    public string type_line { get; set; }
    public string oracle_text { get; set; }
    public string watermark { get; set; }
    public string artist { get; set; }
    public string illustration_id { get; set; }
  }
}
