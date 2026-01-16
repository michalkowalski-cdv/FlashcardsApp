using BlazorApp1.Models;
using Supabase.Postgrest;

namespace BlazorApp1.Services;

public class SupabaseFlashcardService : IFlashcardService
{
    private readonly Supabase.Client _client;
    private readonly UserService _userService;

    public SupabaseFlashcardService(Supabase.Client client, UserService userService)
    {
        _client = client;
        _userService = userService;
    }

    public async Task<List<Flashcard>> GetAllAsync()
    {
        if (string.IsNullOrEmpty(_userService.CurrentUserId))
            return new List<Flashcard>();

        var response = await _client.From<Flashcard>()
            .Filter("user_id", Constants.Operator.Equals, _userService.CurrentUserId)
            .Get();

        return response.Models;
    }

    public async Task AddAsync(Flashcard flashcard)
    {
        if (string.IsNullOrEmpty(_userService.CurrentUserId)) return;

        flashcard.UserId = _userService.CurrentUserId;
        await _client.From<Flashcard>().Insert(flashcard);
    }

    public async Task UpdateAsync(Flashcard flashcard)
    {
        if (string.IsNullOrEmpty(_userService.CurrentUserId)) return;

        flashcard.UserId = _userService.CurrentUserId;
        await _client.From<Flashcard>().Update(flashcard);
    }

    public async Task DeleteAsync(Guid id)
    {
        if (string.IsNullOrEmpty(_userService.CurrentUserId)) return;
        
        await _client.From<Flashcard>()
            .Filter("id", Constants.Operator.Equals, id.ToString()) 
            .Filter("user_id", Constants.Operator.Equals, _userService.CurrentUserId)
            .Delete();
    }

    public async Task<List<string>> GetCategoriesAsync()
    {
        if (string.IsNullOrEmpty(_userService.CurrentUserId))
            return new List<string>();

        var response = await _client.From<Flashcard>()
            .Select("category")
            .Filter("user_id", Constants.Operator.Equals, _userService.CurrentUserId)
            .Get();

        return response.Models
            .Select(x => x.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        var response = await _client.From<Flashcard>()
            .Select("id")
            .Filter("user_id", Constants.Operator.Equals, userId)
            .Limit(1)
            .Get();

        return response.Models.Any();
    }
}