namespace InventarioApi.Models.Dto
{
    public class ProductUpdateDto
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
