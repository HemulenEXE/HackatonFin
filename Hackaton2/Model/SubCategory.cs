using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hackaton2.Model
{
    public class SubCategory
    {
        public SubCategory() { }

        public SubCategory(int id, string name, int categoryId)
        {
            this.Id = id;
            this.name = name;
            this.CategoryId = categoryId;
        }

        [Key]
        public int Id { get; set; }

        public string? name { get; set; }

        public int? CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        //public virtual Category? Category { get; set; }
    }
}
