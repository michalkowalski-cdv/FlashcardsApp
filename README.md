# FiszkiApp - Aplikacja do nauki

FiszkiApp to aplikacja internetowa typu Single Page Application (SPA) zbudowana w technologii **Blazor WebAssembly** . Program umożliwia użytkownikom tworzenie własnych zestawów fiszek (tekstowych oraz typu quiz ABCD), zarządzanie nimi oraz naukę w dwóch trybach: przeglądania oraz interaktywnego quizu z naliczaniem punktów.

## Wersja Live
Aplikacja jest wdrożona i dostępna pod adresem:
[https://flashcards-app-orcin.vercel.app/](https://flashcards-app-orcin.vercel.app/)

---

## Instrukcja uruchomienia (Rider / Visual Studio)

1. **Wymagania:** Zainstalowane środowisko .NET 9.0 SDK.
2. **Otwarcie projektu:** Otwórz plik `FlashcardsApp.sln` w środowisku JetBrains Rider lub Visual Studio.
3. **Pakiety NuGet:** Środowisko powinno automatycznie przywrócić pakiety. Jeśli tak się nie stanie, wykonaj `dotnet restore`. Główne biblioteki to:
    - `Supabase` (dostęp do bazy danych)
    - `Blazored.LocalStorage` (zapamiętywanie sesji użytkownika)
4. **Konfiguracja:** Dane dostępowe do bazy Supabase (URL i Key) są już skonfigurowane w pliku `Config.cs`.
5. **Uruchomienie:**
    - Wybierz profil `http` lub `https` w Riderze.
    - Kliknij **Run** (Shift + F10).
    - Aplikacja uruchomi się pod adresem `http://localhost:5284`.

---

## Spełnienie wymagań technicznych

### 1. Instrukcje warunkowe
Wykorzystywane do sterowania logiką aplikacji i renderowaniem UI.
- **Przykład:** W `MainLayout.razor` sprawdzanie stanu zalogowania `@if (UserService.IsLoggedIn)` w celu wyświetlenia odpowiednich elementów nawigacji.
- **Przykład:** W `Quiz.razor` weryfikacja poprawności odpowiedzi użytkownika.

### 2. Pętle
Używane do przetwarzania list danych i generowania dynamicznych elementów interfejsu.
- **Przykład:** Renderowanie listy kategorii w `Study.razor` oraz wyświetlanie wszystkich fiszek w `Manage.razor` za pomocą pętli `@foreach`.

### 3. Kolekcje generyczne
Aplikacja zarządza danymi przy użyciu list generycznych `List<T>`.
- **Przykład:** `List<FlashcardBase>` przechowująca obiekty różnych typów fiszek pobrane z bazy danych.
- **Przykład:** `List<string>` do obsługi opcji w pytaniach typu Quiz.

### 4. Filary Obiektowości

#### A. Dziedziczenie (Inheritance)
W folderze `Models/OOP/` znajduje się struktura klas oparta na dziedziczeniu:
- Klasa bazowa: `FlashcardBase`.
- Klasy pochodne: `FlashcardText` (fiszka z wpisywaną odpowiedzią) oraz `FlashcardChoice` (fiszka z opcjami wyboru) dziedziczą po klasie bazowej.

#### B. Polimorfizm (Polymorphism)
Zastosowano polimorfizm dynamiczny (metody wirtualne/abstrakcyjne):
- Klasa `FlashcardBase` definiuje abstrakcyjną metodę `ValidateAnswer(string userAnswer)`.
- Klasy pochodne dostarczają własną implementację tej metody. Dzięki temu system quizu wywołuje tę samą metodę na liście obiektów typu `FlashcardBase`, a program automatycznie wykonuje logikę właściwą dla danego typu fiszki.

#### C. Hermetyzacja (Encapsulation)
- Pole `CorrectAnswer` w klasie `FlashcardBase` jest chronione (`protected`), co ogranicza dostęp do niego tylko dla klas pochodnych.
- Zewnętrzne komponenty uzyskują dostęp do poprawnej odpowiedzi jedynie poprzez publiczną metodę `GetCorrectAnswer()`, co zapobiega nieautoryzowanej modyfikacji danych.

### 5. Połączenie z bazą danych
Aplikacja integruje się z bazą danych **Supabase (PostgreSQL)**.
- Implementacja znajduje się w `SupabaseFlashcardService.cs`.
- Dane są mapowane z bazy na obiekty C# przy użyciu modelu `FlashcardEntity`.

---

## Elementy dodatkowe

1. **Interfejs Użytkownika:** Responsywny design zbudowany w Blazor WebAssembly z wykorzystaniem frameworka Bootstrap 5.
2. **Klasy Abstrakcyjne i Interfejsy:**
    - Wykorzystanie klasy abstrakcyjnej `FlashcardBase` do zdefiniowania wspólnego kontraktu.
    - Wykorzystanie interfejsu `IFlashcardService` (Dependency Injection), co pozwala na łatwą zmianę źródła danych (np. na mock lub JSON).
3. **LINQ:**
    - Zaawansowane operacje na kolekcjach, np. mieszanie kolejności fiszek: `.OrderBy(x => Random.Shared.Next())`.
    - Filtrowanie kategorii: `.Where(c => c.Category == category)`.
    - Pobieranie unikalnych wartości: `.Select(x => x.Category).Distinct()`.

---
**Autor:** Michał Kowalski
**Projekt:** Programowanie Obiektowe - Laboratorium (CDV)