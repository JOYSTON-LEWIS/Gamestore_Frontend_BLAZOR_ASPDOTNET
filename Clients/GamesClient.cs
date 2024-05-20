// TS-01:21:39-01:38:54 
using GameStore.Frontend.Models;

// TS-01:21:39-01:38:54 
namespace GameStore.Frontend.Clients;

// TS-01:21:39-01:38:54 
// TS-03:55:36-04:20:39 Using the injected Http Client from Program.cs
public class GamesClient(HttpClient httpClient)
{
    // TS-01:21:39-01:38:54 moved here from Home.razor
    // TS-01:21:39-01:38:54 Home component continues to use an array
    // TS-01:21:39-01:38:54 this one will use a list
    // TS-01:21:39-01:38:54 changed GameSummary[] to List<GameSummary>
    // TS-01:21:39-01:38:54 a simple function to modify the private List here
    // TS-01:21:39-01:38:54 Control + . suggestion to make this field into readonly as
    // TS-01:21:39-01:38:54 earlier private List<GameSummary> games = [, now private readonly List<GameSummary> games = [
    private readonly List<GameSummary> games = [
   new(){
            Id = 1,
            Name = "Street Fighter II",
            Genre = "Fighting",
            // TS-00:56:37-01:13:00 M specifies decimal
            Price = 19.99M,
            ReleaseDate = new DateOnly(1992, 7, 15)
        },
    new(){
            Id = 2,
            Name = "Final Fantasy XIV",
            Genre = "Roleplaying",
            // TS-00:56:37-01:13:00 M specifies decimal
            Price = 59.99M,
            ReleaseDate = new DateOnly(2010, 9, 30)
        },
    new(){
            Id = 3,
            Name = "FIFA 23",
            Genre = "Sports",
            // TS-00:56:37-01:13:00 M specifies decimal
            Price = 69.99M,
            ReleaseDate = new DateOnly(2022, 9, 27)
        }
    ];

    // TS-01:38:54-02:19:55 instance to fetch the Genres
    // TS-02:32:35-02:53:03 No need to do dependency injection here
    // TS-02:32:35-02:53:03 As later on we would manage this with actual database
    // TS-03:55:36-04:20:39 adding the parameter to avoid build warnins, to be removed soon
    // TS-04:31:03-04:55:35 Following Code Not Needed
    private readonly Genre[] genres = new GenresClient(httpClient).getGenres();

    // TS-01:21:39-01:38:54 declaring a function to access the list above from other components
    // TS-01:21:39-01:38:54 changing public GameSummary[] GetGames() => games.ToArray();
    // TS-01:21:39-01:38:54 to optimised or standardized code formatting as suggested by C# suggestion on using Control + .
    // TS-01:21:39-01:38:54 public GameSummary[] GetGames() => [.. games]; is collection expression
    // TS-04:31:03-04:55:35 implementing async, previous code :
    // TS-04:31:03-04:55:35 public GameSummary[] GetGames() => [.. games];
    // TS-04:31:03-04:55:35 GetFromJsonAsync to get the data from API call asynchronously
    // TS-04:31:03-04:55:35 as most of our backend endpoints have the games in endpoints
    // TS-04:31:03-04:55:35 Tasks will be retuned by this code so apply task type. can be 
    // TS-04:31:03-04:55:35 handled with async await and many other methods in c#
    // TS-04:31:03-04:55:35 if null return [] condition
    // TS-04:31:03-04:55:35 Rename methods as Async wherever we use async to let others know the type of method whether its sync or async
    public async Task <GameSummary[]> GetGamesAsync() 
        => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];

    
    // TS-04:31:03-04:55:35 implementing async
    // TS-04:31:03-04:55:35 public void AddGame(GameDetails game) changed to public async Task AddGame(GameDetails game), created separately better
    // TS-04:31:03-04:55:35 the "games" here already receives the base localhost link from http client and so
    // TS-04:31:03-04:55:35 it simply appends the remaining part of the endpoint provided in the await call parameters
    public async Task AddGameAsync(GameDetails game)
        => await httpClient.PostAsJsonAsync("games", game);
    
    // TS-01:38:54-02:19:55 public function to add new game to this list
    // TS-04:31:03-04:55:35 Function to add game using mock data in frontend
    // TS-04:31:03-04:55:35 Following Code Not Needed as async implemented in project but can be a reference for mock data practice
    public void AddGame(GameDetails game)
    {
        // TS-03:08:57-03:55:36 Creating helper functions for repeated code
        // TS-03:08:57-03:55:36 passing in genreId instead of full gameobject game
        Genre genre = GetGenreById(game.GenreId);

        // TS-01:38:54-02:19:55 we need to convert the GameDetails Instance to Game Summary instance to map it to the list
        var gameSummary = new GameSummary()
        {
            Id = games.Count + 1,
            Name = game.Name,
            // TS-01:38:54-02:19:55 We need to define another array to map the genreId to the GenreName
            // TS-01:38:54-02:19:55 so for that lets create another list for Genre
            // TS-01:38:54-02:19:55 patching the Genre Name instead of GenreId which we generated above
            Genre = genre.Name,
            Price = game.Price,
            ReleaseDate = game.ReleaseDate
        };
        games.Add(gameSummary);
    }

    // TS-04:31:03-04:55:35
    // TS-04:31:03-04:55:35 the "games" here already receives the base localhost link from http client and so
    // TS-04:31:03-04:55:35 it simply appends the remaining part of the endpoint provided in the await call parameters
    public async Task<GameDetails> GetGamesAsync(int id)
        => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}")
        ?? throw new Exception("Could not find game!");

    // TS-02:53:03-03:08:57 Defining a function for edit game to retrieve existing data
    // TS-04:31:03-04:55:35 Following Code Not Needed as async implemented in project but can be a reference for mock data practice
    public GameDetails GetGame(int id)
    {
        // TS-03:08:57-03:55:36 removed the null check aqs its handled in the function called,
        // TS-03:08:57-03:55:36  it should return standard game object
        GameSummary game = GetGameSummaryById(id);
        // TS-02:53:03-03:08:57 Single property for array
        // TS-02:53:03-03:08:57 When we do witj backend this stringcomparison 
        // TS-02:53:03-03:08:57 will be replaced soon as its not the best approach
        var genre = genres.Single(genre => string.Equals(
           genre.Name,
           game.Genre,
           StringComparison.OrdinalIgnoreCase));

        // TS-02:53:03-03:08:57 
        return new GameDetails
        {
            // TS-02:53:03-03:08:57 
            Id = game.Id,
            // TS-02:53:03-03:08:57 
            Name = game.Name,
            // TS-02:53:03-03:08:57 
            GenreId = genre.Id.ToString(),
            // TS-02:53:03-03:08:57 
            Price = game.Price,
            // TS-02:53:03-03:08:57 
            ReleaseDate = game.ReleaseDate
        };
    }

    // TS-04:31:03-04:55:35
    // TS-04:31:03-04:55:35 the "games" here already receives the base localhost link from http client and so
    // TS-04:31:03-04:55:35 it simply appends the remaining part of the endpoint provided in the await call parameters
    public async Task UpdateGameAsync(GameDetails updatedGame)
        => await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);

    // TS-03:08:57-03:55:36 function to update the existing game
    // TS-04:31:03-04:55:35 Following Code Not Needed as async implemented in project but can be a reference for mock data practice
    public void UpdateGame(GameDetails updatedGame)
    {
        // TS-03:08:57-03:55:36 
        var genre = GetGenreById(updatedGame.GenreId);
        // TS-03:08:57-03:55:36 
        GameSummary existingGame = GetGameSummaryById(updatedGame.Id);
        // TS-03:08:57-03:55:36 
        existingGame.Name = updatedGame.Name;
        // TS-03:08:57-03:55:36 
        existingGame.Genre = genre.Name;
        // TS-03:08:57-03:55:36 
        existingGame.Price = updatedGame.Price;
        // TS-03:08:57-03:55:36 
        existingGame.ReleaseDate = updatedGame.ReleaseDate;
    }

    // TS-04:31:03-04:55:35 
    // TS-04:31:03-04:55:35 the "games" here already receives the base localhost link from http client and so
    // TS-04:31:03-04:55:35 it simply appends the remaining part of the endpoint provided in the await call parameters
    public async Task DeleteGameAsync(int id)
        => await httpClient.DeleteAsync($"games/{id}");

    // TS-03:08:57-03:55:36 Delete Game Logic
    // TS-04:31:03-04:55:35 Following Code Not Needed as async implemented in project but can be a reference for mock data practice
    public void DeleteGame(int id)
    {
        // TS-03:08:57-03:55:36 fetching the id of the game using our helper function
        var game = GetGameSummaryById(id);
        // TS-03:08:57-03:55:36 deleting the game from the list of games we have
        games.Remove(game);
    }

    // TS-03:08:57-03:55:36 Good Convention to keep all Helper Methods always at end
    
    // TS-03:08:57-03:55:36 Creating helper functions for repeated code
    // TS-03:08:57-03:55:36 now we dont want entire GameDetails game, 
    // TS-03:08:57-03:55:36 we only need Id so lets refactor our parameter 
    // TS-03:08:57-03:55:36 from private Genre GetGenreById(GameDetails game) to private Genre GetGenreById(string? id)
    private Genre GetGenreById(string? id)
    {
        // TS-01:38:54-02:19:55 throws exception and program breaks here
        // TS-03:08:57-03:55:36 replacing ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);
        // TS-03:08:57-03:55:36 with ArgumentException.ThrowIfNullOrWhiteSpace(id);
        ArgumentException.ThrowIfNullOrWhiteSpace(id);

        // TS-01:38:54-02:19:55 fetching the genre name that matches the genre Id
        // TS-01:38:54-02:19:55 handle the nullable case to remove all warnings using ArgumentException
        // TS-03:08:57-03:55:36 replacing var genre = genres.Single(genre => genre.Id == int.Parse(game.GenreId));
        // TS-03:08:57-03:55:36 with var genre = genres.Single(genre => genre.Id == int.Parse(id)); 
        // TS-03:08:57-03:55:36 reducing 2 lines of code below to one liner
        // TS-03:08:57-03:55:36 var genre = genres.Single(genre => genre.Id == int.Parse(id));
        // TS-03:08:57-03:55:36 return genre;
        return genres.Single(genre => genre.Id == int.Parse(id));
    }

    // TS-03:08:57-03:55:36 
    private GameSummary GetGameSummaryById(int id)
    {
        // TS-02:53:03-03:08:57 Find property for List
        GameSummary? game = games.Find(game => game.Id == id);
        // TS-02:53:03-03:08:57 Game can be null so add exception handle
        ArgumentNullException.ThrowIfNull(game);
        return game;
    }
}