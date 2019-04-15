using Plugin.Connectivity;

namespace MagicTheGatheringApp.Managers
{
  public class ConnectionManager
  {
    public static bool CheckConnection()
    {
      return CrossConnectivity.Current.IsConnected;
    }
  }
}
