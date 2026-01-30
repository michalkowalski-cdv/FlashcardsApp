using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace BlazorApp1.Models;

[Table("flashcards")]
public class FlashcardEntity : BaseModel 
{
    [PrimaryKey("id", false)] 
    public Guid Id { get; set; }

    [Column("category")] 
    public string Category { get; set; } = "Ogólne";

    [Column("question")] 
    public string Question { get; set; } = string.Empty;

    [Column("answer")] 
    public string Answer { get; set; } = string.Empty;
    
    [Column("options")] 
    public string? Options { get; set; } 

    [Column("type")] 
    public string Type { get; set; } = "TEXT";

    [Column("user_id")] 
    public string UserId { get; set; } = string.Empty;

    [Column("created_at")] 
    public DateTime CreatedAt { get; set; }
}