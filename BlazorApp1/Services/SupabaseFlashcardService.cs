using BlazorApp1.Models;
using BlazorApp1.Models.OOP;
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

    public async Task<List<FlashcardBase>> GetAllOOPAsync()
    {
        if (string.IsNullOrEmpty(_userService.CurrentUserId)) return new List<FlashcardBase>();

        var response = await _client.From<FlashcardEntity>()
            .Filter("user_id", Constants.Operator.Equals, _userService.CurrentUserId)
            .Get();

        var oopList = new List<FlashcardBase>();

        foreach (var entity in response.Models)
        {
            FlashcardBase flashcard;

            if (entity.Type == "CHOICE")
            {
                var opts = string.IsNullOrEmpty(entity.Options) 
                    ? new List<string>() 
                    : entity.Options.Split('|').ToList();
                
                flashcard = new FlashcardChoice(entity.Question, entity.Answer, opts);
            }
            else
            {
                flashcard = new FlashcardText(entity.Question, entity.Answer);
            }

            flashcard.Id = entity.Id;
            flashcard.Category = entity.Category;
            flashcard.UserId = entity.UserId;
            
            oopList.Add(flashcard);
        }

        return oopList;
    }

    public async Task AddTextAsync(string category, string question, string answer)
    {
        var entity = new FlashcardEntity
        {
            UserId = _userService.CurrentUserId,
            Category = category,
            Question = question,
            Answer = answer,
            Type = "TEXT",
            Options = null
        };
        await _client.From<FlashcardEntity>().Insert(entity);
    }

    public async Task AddChoiceAsync(string category, string question, string answer, List<string> options)
    {
        var entity = new FlashcardEntity
        {
            UserId = _userService.CurrentUserId,
            Category = category,
            Question = question,
            Answer = answer,
            Type = "CHOICE",
            Options = string.Join("|", options) 
        };
        await _client.From<FlashcardEntity>().Insert(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _client.From<FlashcardEntity>()
            .Filter("id", Constants.Operator.Equals, id.ToString())
            .Delete();
    }
    
    public async Task<List<string>> GetCategoriesAsync()
    {
        var response = await _client.From<FlashcardEntity>()
            .Select("category")
            .Filter("user_id", Constants.Operator.Equals, _userService.CurrentUserId)
            .Get();

        return response.Models.Select(x => x.Category).Distinct().OrderBy(c => c).ToList();
    }

    public async Task<bool> UserExistsAsync(string userId)
    {
        var response = await _client.From<FlashcardEntity>()
            .Select("id")
            .Filter("user_id", Constants.Operator.Equals, userId)
            .Limit(1)
            .Get();
        return response.Models.Any();
    }
}