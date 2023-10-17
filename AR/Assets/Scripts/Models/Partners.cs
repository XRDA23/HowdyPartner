using Enums;

namespace Models
{
    public class Partners
    {
        public TeamEnum Team1 { get; private set; }
        public TeamEnum Team2 { get; private set; }

        public Partners(TeamEnum player1, TeamEnum player2)
        {
            Team1 = player1;
            Team2 = player2;
        }
    
    }
}