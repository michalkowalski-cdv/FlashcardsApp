namespace BlazorApp1.Models.OOP;


public abstract class FlashcardBase
{
    public Guid Id { get; set; }
    public string Category { get; set; } = "Ogólne";
    public string Question { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    
    protected string CorrectAnswer { get; set; } = string.Empty;

    protected FlashcardBase(string question, string correctAnswer)
    {
        Question = question;
        CorrectAnswer = correctAnswer;
    }

    public abstract bool ValidateAnswer(string userAnswer);
    
    public string GetCorrectAnswer() => CorrectAnswer;
}