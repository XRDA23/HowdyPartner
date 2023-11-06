using System;

namespace Enums
{
    /**
 * Enum definition that contains all possible teams.
 * This can be extended for the 8 version game.
 */
    public enum TeamEnum
    {
        BlueOrWater = 0,
        RedOrHeart = 1,
        GreenOrEmerald = 2,
        YellowOrStar = 3
    }
    public static class TeamEnumExtensions
    {
        public static QuadrantEnum ToQuadrant(this TeamEnum team)
        {
            switch (team)
            {
                case TeamEnum.BlueOrWater:
                    return QuadrantEnum.Blue;
                case TeamEnum.RedOrHeart:
                    return QuadrantEnum.Red;
                case TeamEnum.GreenOrEmerald:
                    return QuadrantEnum.Green;
                case TeamEnum.YellowOrStar:
                    return QuadrantEnum.Yellow;
                default:
                    throw new ArgumentException("Invalid TeamEnum value");
            }
        }
    }
}
