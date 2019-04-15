using System.Collections.Generic;

namespace MagicTheGatheringApp.Models.Scryfall
{
  class ScryFallCardType
  {
    public string @object { get; set; }
    public int total_cards { get; set; }
    public bool has_more { get; set; }
    public string next_page { get; set; }
    public List<ScryFallCard> data { get; set; }
  }
}
