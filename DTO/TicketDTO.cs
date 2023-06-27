namespace LoopApi.DTO
{
    public class TicketDTO
    {
        public long id { get; set; }
        public string ticketNumber { get; set; }
        public int machineNumber { get; set; }
        public int isEnable { get; set; }
        public string createdDate { get; set; }
        public string updatedDate { get; set; }
        public int isApplied { get; set; }
        public int userId { get; set; }
        public string classTicket { get; set; }
    }
}
