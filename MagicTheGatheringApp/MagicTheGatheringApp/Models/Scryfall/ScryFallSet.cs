using System;

namespace MagicTheGatheringApp.Models.Scryfall
{
  class ScryFallSet
  {
    public Guid id { get; set; }
    public string code { get; set; }
    public string mtgo_code { get; set; }
    public int tcgplayer_id { get; set; }
    public string name { get; set; }
    public string set_type { get; set; }
    public DateTime released_at { get; set; }
    public string block_code { get; set; }
    public string block { get; set; }
    public string parent_set_code { get; set; }
    public int card_count { get; set; }
    public bool digital { get; set; }
    public bool foil_only { get; set; }
    public Uri icon_svg_uri { get; set; }
    public Uri search_uri { get; set; }
  }
}
