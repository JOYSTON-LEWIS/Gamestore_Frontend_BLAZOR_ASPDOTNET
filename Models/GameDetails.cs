// TS-00:56:37-01:13:00 Created and namespace modified to match directory

// TS-02:53:03-03:08:57 
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GameStore.Frontend.Converters;

namespace GameStore.Frontend.Models;

// TS-00:56:37-01:13:00 Created
// TS-01:38:54-02:19:55 Copy Pasted from GameSummary.cs and Renamed to GameDetails
public class GameDetails
{
    public int Id {get; set;}

    // TS-00:56:37-01:13:00 for string values compiler wants to know if its nullable or required accordingly define it
    // TS-02:53:03-03:08:57 Required Annotation added
    // TS-02:53:03-03:08:57 String Length Annotation added
    [Required]
    [StringLength(50)]
    public required string Name {get; set;}
    // TS-00:56:37-01:13:00
    // TS-01:38:54-02:19:55 Capturing GenreId, keeping it a string as int wont map well with the dropdown
    // TS-01:38:54-02:19:55 we dont need it to be having a value every time 
    // TS-02:53:03-03:08:57 Required Annotation added
    // TS-02:53:03-03:08:57 Adding custom message for each of the annotations to make sense to user
    [Required(ErrorMessage = "The Genre field is required.")]
    // TS-04:31:03-04:55:35 Introducing the Json Converter to convert the int to string if valid.
    [JsonConverter(typeof(StringConverter))]
    public string ? GenreId {get; set;}
    
    // TS-00:56:37-01:13:00 
    // TS-02:53:03-03:08:57 Range Annotation added
    [Range(1,100)]
    public decimal Price {get; set;}

    // TS-00:56:37-01:13:00 
    public DateOnly ReleaseDate {get; set;}

}
