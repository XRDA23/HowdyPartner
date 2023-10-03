namespace Board
{
    
    public class BoardPosition
    {
        private Quadrant quadrant;
        private TileNumber tileNo;
    
        public BoardPosition(Quadrant quadrant, TileNumber tileNo)
        {
            this.quadrant = quadrant;
            this.tileNo = tileNo;
        }
    
        /* 
         * Should we include the homeBase and endBase here?
         * --could look something like this in addition to what is already here
         *
         * private bool homeBase;
         * private bool endBase;
         * private int endBasePosition; --> 0-5
         * public BoardPosition(Quadrant? quadrant, TileNumber? tileNo, bool homeBase, bool endBase, int? endBasePosition)
         * {
         *      this.homeBase = homeBase;
         *      this.endBase = endBase;
         * 
         *      if (endBase)
         *      {
         *          this.endBasePosition = endBasePosition;
         *      }
         * 
         *      if (!endBase && !homeBase)
         *      {
         *          this.quadrant = quadrant;
         *          this.tileNo = tileNo;
         *      }
         * }
         */
    }
}
