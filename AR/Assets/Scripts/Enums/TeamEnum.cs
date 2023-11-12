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
        YellowOrStar = 2,
        GreenOrEmerald = 3
    }
    public static class TeamEnumExtensions
    {
        public static QuadrantEnum ToQuadrant(this TeamEnum team)
        {
            return team switch
            {
                TeamEnum.BlueOrWater => QuadrantEnum.Blue,
                TeamEnum.RedOrHeart => QuadrantEnum.Red,
                TeamEnum.GreenOrEmerald => QuadrantEnum.Green,
                TeamEnum.YellowOrStar => QuadrantEnum.Yellow,
                _ => throw new ArgumentException("Invalid TeamEnum value")
            };
        }

        public static TeamEnum GetNextTeam(this TeamEnum currentTeam)
        {
            return currentTeam switch
            {
                TeamEnum.BlueOrWater => TeamEnum.RedOrHeart,
                TeamEnum.RedOrHeart => TeamEnum.YellowOrStar,
                TeamEnum.YellowOrStar => TeamEnum.GreenOrEmerald,
                TeamEnum.GreenOrEmerald => TeamEnum.BlueOrWater,
                _ => throw new ArgumentException("Invalid TeamEnum value")
            };
        }
    }
}
