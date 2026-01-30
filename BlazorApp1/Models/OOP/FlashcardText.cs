namespace BlazorApp1.Models.OOP;

public class FlashcardText : FlashcardBase
{
    public FlashcardText(string question, string correctAnswer) 
        : base(question, correctAnswer) { }

    public override bool ValidateAnswer(string userAnswer)
    {
        if (string.IsNullOrWhiteSpace(userAnswer)) return false;
        return string.Equals(userAnswer.Trim(), CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}