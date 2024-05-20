// TS-01:38:54-02:19:55 Created and namespace modified to match directory
namespace GameStore.Frontend.Models;

// TS-01:38:54-02:19:55 Created Genre to consist of all Genre with GenreId
public class Genre
{  
    // TS-01:38:54-02:19:55 
    public int Id {get; set;}

    // TS-01:38:54-02:19:55 
    public required string Name {get; set;}

}