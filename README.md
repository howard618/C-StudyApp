# FlashCards

A local C# and .NET flashcard study guide built with Blazor Server. The app includes concept flashcards plus coding-practice prompts so students can review interview topics from a browser.

## Requirements

- Git
- .NET 8 SDK

Download .NET 8 from Microsoft if you do not already have it installed:

https://dotnet.microsoft.com/download/dotnet/8.0

## Clone And Run

```powershell
git clone https://github.com/YOUR_USERNAME/FlashCards.git
cd FlashCards
dotnet run
```

After the app starts, open the local URL printed in the terminal. It is usually:

```text
http://localhost:5000
```

## Study Modes

- Concept flashcards: C# and .NET interview questions with answer-only card backs.
- Coding practice: coding prompts with compact summaries, implementations, patterns, and explanations.

## Notes

- The app runs locally; it does not require a database or an internet connection after setup.
- The coding-practice deck uses `Coding questions.docx`, which is included in the repository.
- If port `5000` is already in use, .NET may choose a different local URL. Use the URL shown in the terminal.
