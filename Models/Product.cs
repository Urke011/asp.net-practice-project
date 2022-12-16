using System.ComponentModel.DataAnnotations.Schema;

namespace test_app.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        //Veza dve tabele
        public long CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
