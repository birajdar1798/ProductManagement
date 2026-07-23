namespace ProductManagementAPI.Dto
{
    public class ProductItem
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int Quantity { get; set; }
    }
}
