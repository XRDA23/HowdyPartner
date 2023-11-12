using System;

namespace Enums
{
    /**
 * Enum definition that contains all possible quadrants.
 * This can be extended for the 8 version game.
 * **Quadrants is not a good name because it implies that it's divided in four
 * ** but I use it for lack of better wording
 */
    public enum QuadrantEnum
    {
        Blue = 0,
        Red = 1,
        Yellow = 2,
        Green = 3
    }

    public static class QuadrantEnumExtensions
    {
        public static QuadrantEnum GetNextQuadrant(this QuadrantEnum currentQuadrant)
        {
            return currentQuadrant switch
            {
                QuadrantEnum.Blue => QuadrantEnum.Red,
                QuadrantEnum.Red => QuadrantEnum.Yellow,
                QuadrantEnum.Yellow => QuadrantEnum.Green,
                QuadrantEnum.Green => QuadrantEnum.Blue,
                _ => throw new ArgumentException("Invalid QuadrantEnum value")
            };
        }
        
        public static QuadrantEnum GetPreviousQuadrant(this QuadrantEnum currentQuadrant)
        {
            return currentQuadrant switch
            {
                QuadrantEnum.Blue => QuadrantEnum.Green,
                QuadrantEnum.Red => QuadrantEnum.Blue,
                QuadrantEnum.Yellow => QuadrantEnum.Red,
                QuadrantEnum.Green => QuadrantEnum.Yellow,
                _ => throw new ArgumentException("Invalid QuadrantEnum value")
            };
        }
    }
}
