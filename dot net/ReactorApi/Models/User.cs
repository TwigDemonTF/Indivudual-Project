namespace ReactorApi.Models
{
    public class User
    {
        public int id { get; set; }
        public int reactorId { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string minecraftUsername { get; set; }
    }
}
