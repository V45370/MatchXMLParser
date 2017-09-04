namespace MatchXMLParser.Models
{
    public interface IEntity
    {
        int Id { get; set; }
        //int ExternalId { get; set; }
        string ExternalId { get; set; }
    }
}
