namespace LoopApi.DTO
{
    public class CouponDTO
    {
        public long id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public int isEnable { get; set; }
        public long storeId { get; set; }
        public int giftPoints { get; set; }
        public string startedDate { get; set; }
        public string endDate { get; set; }
        public string image { get; set; }
        public int isAutomatic { get; set; }
        public string lastModifiedBy { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
    }
}
