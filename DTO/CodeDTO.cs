namespace LoopApi.DTO
{
    public class CodeDTO
    {
        public long id { get; set; }
        public long couponId { get; set; }
        public long code { get; set; }
        public int isActive { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
    }
}
