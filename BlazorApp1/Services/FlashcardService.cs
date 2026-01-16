using BlazorApp1.Models;

namespace BlazorApp1.Services;

public interface IFlashcardService
{
    Task<List<Flashcard>> GetAllAsync();
    Task AddAsync(Flashcard flashcard);
    Task UpdateAsync(Flashcard flashcard);
    Task DeleteAsync(Guid id);
    Task<List<string>> GetCategoriesAsync();
    
    Task<bool> UserExistsAsync(string userId);
}