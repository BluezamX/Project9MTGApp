using System.Collections.Generic;

namespace MagicTheGatheringApp.Models.Scryfall
{
  class ScryFallCard
  {
    public string @object { get; set; }
    public string id { get; set; }
    public string oracle_id { get; set; }
    public List<object> multiverse_ids { get; set; }
    public string name;
    public string lang { get; set; }
    public string released_at { get; set; }
    public string uri;
    public string scryfall_uri { get; set; }
    public string layout { get; set; }
    public bool highres_image { get; set; }
    public ImageUris image_uris { get; set; }
    public string mana_cost { get; set; }
    public float cmc { get; set; }
    public string type_line { get; set; }
    public string oracle_text { get; set; }
    public List<object> colors { get; set; }
    public List<object> color_identity { get; set; }
    public Legalities legalities { get; set; }
    public List<string> games { get; set; }
    public bool reserved { get; set; }
    public bool foil { get; set; }
    public bool nonfoil { get; set; }
    public bool oversized { get; set; }
    public bool promo { get; set; }
    public bool reprint { get; set; }
    public string set { get; set; }
    public string set_name { get; set; }
    public string set_uri { get; set; }
    public string set_search_uri { get; set; }
    public string scryfall_set_uri { get; set; }
    public string rulings_uri { get; set; }
    public string prints_search_uri { get; set; }
    public string collector_number { get; set; }
    public bool digital { get; set; }
    public string rarity { get; set; }
    public string watermark { get; set; }
    public string illustration_id { get; set; }
    public string artist { get; set; }
    public string border_color { get; set; }
    public string frame { get; set; }
    public string frame_effect { get; set; }
    public bool full_art { get; set; }
    public bool story_spotlight { get; set; }
    public RelatedUris related_uris { get; set; }
    public PurchaseUris purchase_uris { get; set; }
    public string power { get; set; }
    public string toughness { get; set; }
    public string flavor_text { get; set; }
    public int? edhrec_rank { get; set; }
    public string loyalty { get; set; }
    public int? mtgo_id { get; set; }
    public int? tcgplayer_id { get; set; }
    public string usd { get; set; }
    public List<CardFace> card_faces { get; set; }
  }
}
