using System;

namespace Enums
{
    /*
 * This is an enum for the tile numbers inside a quadrant
 * An enum was chose instead of an int for more control over the values that it can take
 */
    public enum TileNumberEnum
    {
        Heart = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Eleven = 11,
        Twelve = 12,
        Thirteen = 13,
        Fourteen = 14,
        Arrow = 15,
        EndBase1 = 16,
        EndBase2 = 17,
        EndBase3 = 18,
        EndBase4 = 19,
        HomeBase = 20
    }
    
    public static class TileNumberEnumExtensions
    {
        public static int GetTileNumberInt(this TileNumberEnum tileNumber)
        {
            return tileNumber switch
            {
                TileNumberEnum.Heart => 0,
                TileNumberEnum.One => 1,
                TileNumberEnum.Two => 2,
                TileNumberEnum.Three => 3,
                TileNumberEnum.Four => 4,
                TileNumberEnum.Five => 5,
                TileNumberEnum.Six => 6,
                TileNumberEnum.Seven => 7,
                TileNumberEnum.Eight => 8,
                TileNumberEnum.Nine => 9,
                TileNumberEnum.Ten => 10,
                TileNumberEnum.Eleven => 11,
                TileNumberEnum.Twelve => 12,
                TileNumberEnum.Thirteen => 13,
                TileNumberEnum.Fourteen => 14,
                TileNumberEnum.Arrow => 15,
                TileNumberEnum.EndBase1 => 16,
                TileNumberEnum.EndBase2 => 17,
                TileNumberEnum.EndBase3 => 18,
                TileNumberEnum.EndBase4 => 19,
                TileNumberEnum.HomeBase => 20,
                _ => throw new ArgumentException("Invalid TeamEnum value")
            };
        }
    }
}
