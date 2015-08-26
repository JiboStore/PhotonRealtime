// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Exit Games GmbH">
//   Exit Games GmbH, 2015
// </copyright>
// <summary>
// An extended version of the PhotonPlayer class. The additional position is not currently used though.
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------
using ExitGames.Client.Photon.LoadBalancing;

using Hashtable = ExitGames.Client.Photon.Hashtable;


public class DemoPlayer : Player
{
    private int posX;
    private int posY;

    protected internal DemoPlayer(string name, int actorID, bool isLocal, Hashtable actorProperties) : base(name, actorID, isLocal, actorProperties)
    {
    }

    public override string ToString()
    {
        return base.ToString() + " pos: " + posX;
    }
}