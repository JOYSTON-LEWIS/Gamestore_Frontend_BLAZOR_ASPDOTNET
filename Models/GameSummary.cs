// TS-00:56:37-01:13:00 Created and namespace modified to match directory
namespace GameStore.Frontend.Models;

// TS-00:56:37-01:13:00 Created
public class GameSummary
{
    public int Id {get; set;}

    // TS-00:56:37-01:13:00 for string values compiler wants to know if its nullable or required accordingly define it
    public required string Name {get; set;}
    // TS-00:56:37-01:13:00 
    public required string Genre {get; set;}

    // TS-00:56:37-01:13:00 
    public decimal Price {get; set;}

    // TS-00:56:37-01:13:00 
    public DateOnly ReleaseDate {get; set;}

}
