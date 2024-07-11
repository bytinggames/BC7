using System;

namespace BC7Runner.Round1
{
    internal class Examples : Bot
    {
        //// I recommend not using a constructor. Instead write your "begin" code in the Initialize method. There you have access to all the things :o (See Examples.cs)

        //protected override Color[] Initialize()
        //{
        //    // Do some initialization code here if you want...
        //    // Use "Env." for getting data from the game. (Env stands for Environment)
        //    // Examples:
        //    // Env.Bots // list of all bots in the current match
        //    // this // get your own bot
        //    // Env.Goals // list of all goal tiles
        //    // Env.Walls // list of all walls (the skyscrapers)
        //    // Env.BotCars // list of all cars that belong to bots

        //    // Env.Bots.Count // amount of bots in the game(usually 2)
        //    // Env.Bots[0] // bot at index 0
        //    // this.BotIndex // your own index
        //    // this.Car.Pos // know where your car is
        //    // this.Car.Shape // every entity has a Shape that defines it's collision boundary (Car is a Polygon, Bot is a Circle) Ask me if you want to do collision checks with it!
        //    // this.Pos // your own position
        //    // this.Car.Shape.CollidesWith(this.Shape) // check if your car collides with your bot
        //    // this.Car.ShapePolygon.Vertices[0] // the car is made up of a polygon with 4 corners (vertices). Get the position of the first corner. ShapePolygon == (Polygon)Shape
        //    // ShapeCircle.Radius // get radius of player



        //    // MATH:
        //    // ToRadians() and ToDegrees() to convert between them if you want to use degrees (default for all math is radians)
        //    // AngleToVector() and VectorToAngle() to convert between those
        //    // Use MathF. or MathExtension. for math calculations
        //    // examples:
        //    // MathF.Sin(angleInRadians)
        //    // MathExtension.AngleDistance(angleFrom, angleTo); // gets smallest distance between two angles

        //    // Rand. if you want random values

        //    // choose some colors:
        //    return new Color[]
        //    {
        //        Color.Transparent, // leg color
        //        Color.Transparent, // body color
        //        Color.Transparent, // head color
        //    };
        //}

        //protected override Decision Update()
        //{


        //    return new Decision(Vector2.Zero, 0f, false);
        //}


        //protected override void DrawCustom(SpriteBatch spriteBatch)
        //{
        //    // draw the kick shape
        //    if (IsKicking)
        //        KickPolygon.Draw(spriteBatch, Color.Black * 0.5f);
        //}

        //protected override void DrawCustomOnScreen(SpriteBatch spriteBatch)
        //{
        //    // (0, 0) is top left of the screen
        //    spriteBatch.DrawString(Font, $"{Env.SecondsLeft:N0} seconds left", new Vector2(0, 0), Color.White);
        //}
        protected override DecisionPhase1 GetDecisionPhase1()
        {
            throw new NotImplementedException();
        }

        protected override DecisionPhase2 GetDecisionPhase2()
        {
            throw new NotImplementedException();
        }
    }
}
