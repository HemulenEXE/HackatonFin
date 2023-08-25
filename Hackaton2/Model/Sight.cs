using System.ComponentModel.DataAnnotations.Schema;

namespace Hackaton2.Model
{
    public class Sight
    {
        public Sight() { }

        public Sight(int id, string? name, string? category, string? subCategory, double rating, double CoordinateX, double CoordinateY)
        {
            this.Id = id;
            this.name = name;
            this.category = category;
            this.subCategory = subCategory;
            this.rating = rating;
            this.CoordinateX = CoordinateX;
            this.CoordinateY = CoordinateY;
        }

        public int Id { get; set; }
        public string? name { get; set; }
        public string? category { get; set; }
        public string? subCategory { get; set; }
        public double? rating { get; set; }

        public double? CoordinateX { get; set; }
        public double? CoordinateY { get; set; }

    }
}
