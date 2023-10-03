using Board;

public class Partners
{
    public Team Team1 { get; private set; }
    public Team Team2 { get; private set; }

    public Partners(Team player1, Team player2)
    {
        Team1 = player1;
        Team2 = player2;
    }
    
}