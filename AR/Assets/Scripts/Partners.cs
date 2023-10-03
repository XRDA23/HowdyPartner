public class Partners
{
    public TeamLia Team1 { get; private set; }
    public TeamLia Team2 { get; private set; }

    public Partners(TeamLia player1, TeamLia player2)
    {
        Team1 = player1;
        Team2 = player2;
    }
    
}