using SQLite;

namespace MagicTheGatheringApp.Models.MTG
{
  [Table("MtgSet")]
  public class MTGSet
  {
    [MaxLength(64)]
    public string name { get; set; }
    [PrimaryKey, MaxLength(3)]
    public string code { get; set; }
    public int number { get; set; }
    public int amount { get; set; }
    [MaxLength(24)]
    public string plane { get; set; }
    [MaxLength(24)]
    public string type { get; set; }
    public bool downloaded { get; set; }

    public MTGSet() { }

    public MTGSet(string _name, string _code, int _number, int _amount, string _plane, string _type)
    {
      name = _name;
      code = _code;
      number = _number;
      amount = _amount;
      plane = _plane;
      type = _type;
    }
  }
}
