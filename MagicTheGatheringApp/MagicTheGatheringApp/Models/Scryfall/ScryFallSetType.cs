using System.Collections.Generic;

namespace MagicTheGatheringApp.Models.Scryfall
{
  class ScryFallSetType
  {
    public string @object { get; set; }
    public bool has_more { get; set; }
    public List<ScryFallSet> data { get; set; }
  }
}
