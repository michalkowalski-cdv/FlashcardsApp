using BlazorApp1.Models.OOP;

namespace BlazorApp1.Services;

public interface IFlashcardService
{
    Task<List<FlashcardBase>> GetAllOOPAsync(); 
    
    Task AddTextAsync(string category, string question, string answer);
    Task AddChoiceAsync(string category, string question, string answer, List<string> options);
    
    Task DeleteAsync(Guid id);
    Task<List<string>> GetCategoriesAsync();
    Task<bool> UserExistsAsync(string userId);
}