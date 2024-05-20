// TS-01:38:54-02:19:55 Created and namespace modified to match directory
using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients;

// TS-01:38:54-02:19:55 GenresClient to pull genres from backend
// TS-03:55:36-04:20:39 Using the injected Http Client from Program.cs
public class GenresClient(HttpClient httpClient)
{  
    
// TS-01:38:54-02:19:55 we wont be modifying the genres so readonly meaning static genre are good enough
    private readonly Genre[] genres = 
    [
        // TS-01:38:54-02:19:55 
        new(){
            Id = 1,
            Name = "Fighting"
        },
        // TS-01:38:54-02:19:55 
        new(){
            Id = 2,
            Name = "Roleplaying"
        },
        // TS-01:38:54-02:19:55 
        new(){
            Id = 3,
            Name = "Sports"
        },
        // TS-01:38:54-02:19:55 
        new(){
            Id = 4,
            Name = "Racing"
        },
        // TS-01:38:54-02:19:55 
        new(){
            Id = 5,
            Name = "Kids and Family"
        }
    ];

    
    // TS-01:38:54-02:19:55 function to retrieve this private list of genre 
    public Genre[] getGenres ()=> genres;
    // TS-04:31:03-04:55:35 converting to async await, earlier code below
    // TS-04:31:03-04:55:35 public Genre[] GetGenres() => genres;
    // TS-04:31:03-04:55:35 
    public async Task<Genre[]> GetGenresAsync()
        => await httpClient.GetFromJsonAsync<Genre[]>("genres") ?? [];
}