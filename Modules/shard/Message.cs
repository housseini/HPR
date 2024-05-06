namespace HPRBackend.Modules.shard
{
    public class Message
    {
        public Boolean status { get; set; }
        public String? message { get; set; }

        public Message(Boolean s, string m)
        {
            this.status = s;
            this.message = m;

        }
    }
}
