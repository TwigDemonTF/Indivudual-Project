namespace ReactorApi.Models
{
    // add-migration "migration message here"
    // update-database
    public class Reactor
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public string? name { get; set; }

        public ReactorData reactorData;

        public List<ReactorValues> reactorHistroy;
    }
}
