namespace Hackaton2.Model
{
    public class Coordinate
    {
        public Coordinate() { }

        public Coordinate(double x, double y)
        {
            this.x = x;
            this.y = y;
        }


        public double x { get; set; }
        public double y { get; set; }
    }
}
