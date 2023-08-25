namespace Hackaton2.Model
{
    public  class Category
    {
        public Category() { }
        public Category(int id, string namecategory)
        {
            this.Id = id;
            this.NameCategory = namecategory;
        }
        public int Id { get; set; }

        public string? NameCategory { get; set; } = "";

        //public virtual ICollection<SubCategory> SubCategory { get; set; }


    }
}
