namespace LoopApi.Util
{
    public class MensajeApi
    {       
        public string title { get; set; }
        public int status { get; set; }
        public string message { get; set; }

        public MensajeApi(string title, int status, string message)
        {
            this.title = title;
            this.status = status;
            this.message = message;
        }
    }
}
