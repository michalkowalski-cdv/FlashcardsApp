namespace BlazorApp1.Models.OOP;

public class FlashcardChoice : FlashcardBase
{
    public List<string> Options { get; private set; }

    public FlashcardChoice(string question, string correctAnswer, List<string> options) 
        : base(question, correctAnswer)
    {
        Options = options;
    }

    public override bool ValidateAnswer(string userAnswer)
    {
        return userAnswer == CorrectAnswer;
    }
}