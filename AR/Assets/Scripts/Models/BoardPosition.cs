using Enums;
using UnityEngine;

namespace Models
{
    public class BoardPosition
    {
        public QuadrantEnum quadrantEnum { get; set; }
        public TileNumberEnum tileNo { get; set; }
        public Vector3 vector3Position { get; set; }
    
        public BoardPosition(QuadrantEnum quadrantEnum, TileNumberEnum tileNo, Vector3 vector3Position)
        {
            this.quadrantEnum = quadrantEnum;
            this.tileNo = tileNo;
            this.vector3Position = vector3Position;
        }
    }
}
