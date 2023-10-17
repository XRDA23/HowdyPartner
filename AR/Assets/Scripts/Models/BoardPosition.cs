using Enums;

namespace Models
{
    
    public class BoardPosition
    {
        public QuadrantEnum quadrantEnum { get; set; }
        public TileNumberEnum tileNo { get; set; }
    
        public BoardPosition(QuadrantEnum quadrantEnum, TileNumberEnum tileNo)
        {
            this.quadrantEnum = quadrantEnum;
            this.tileNo = tileNo;
        }
    
        /* 
         * Should we include the homeBase and endBase here?
         * --could look something like this in addition to what is already here
         *
         * private bool homeBase;
         * private bool endBase;
         * private int endBasePosition; --> 0-5
         * public BoardPosition(QuadrantEnum? quadrantEnum, TileNumberEnum? tileNo, bool homeBase, bool endBase, int? endBasePosition)
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
         *          this.quadrantEnum = quadrantEnum;
         *          this.tileNo = tileNo;
         *      }
         * }
         */
    }
}
