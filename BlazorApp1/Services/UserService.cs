using Blazored.LocalStorage;

namespace BlazorApp1.Services;

public class UserService
{
    private readonly ILocalStorageService _localStorage;
    private const string Key = "user_identifier";

    public event Action? OnChange;

    public string CurrentUserId { get; private set; } = string.Empty;

    public UserService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task InitializeAsync()
    {
        var storedId = await _localStorage.GetItemAsync<string>(Key);
        if (!string.IsNullOrEmpty(storedId))
        {
            CurrentUserId = storedId;
            NotifyStateChanged();
        }
    }

    public async Task LoginAsync(string userId)
    {
        CurrentUserId = userId;
        await _localStorage.SetItemAsync(Key, userId);
        NotifyStateChanged();
    }

    public async Task LogoutAsync()
    {
        CurrentUserId = string.Empty;
        await _localStorage.RemoveItemAsync(Key);
        NotifyStateChanged();
    }

    public bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUserId);

    private void NotifyStateChanged() => OnChange?.Invoke();
}