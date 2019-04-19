namespace MagicTheGatheringApp.Models.MTG
{
  public class CardCluster
  {
    public Card card;
    public int clusterNo;

    public CardCluster(Card c, int no)
    {
      card = c;
      clusterNo = no;
    }
  }
}
