using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BlazorApp1.Models;

[Table("flashcards")]
public class Flashcard : BaseModel
{
    [PrimaryKey("id", false)] 
    public Guid Id { get; set; }

    [Column("category")]
    public string Category { get; set; } = "Ogólne";

    [Column("question")]
    public string Question { get; set; } = string.Empty;

    [Column("answer")]
    public string Answer { get; set; } = string.Empty;
    
    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}