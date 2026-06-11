# C-StudyApp
![image alt](https://github.com/howard618/C-StudyApp/blob/3248f33f88b1a5b6d3c2c5f687a5b4251d509de9/studyapp.jpg)
![image alt](https://github.com/howard618/C-StudyApp/blob/262591e7ec917b0674d77b0532d47964ddcc23db/studyapp1.jpg)



A locally hosted flashcard app for studying C# and .NET basics. The app includes concept flashcards plus coding-practice prompts so students can review interview topics from a browser or mobile device. This runs locally so I utilize a VPN set up on my local network so I can access the flascards while not on my LAN to avoid open ports. 

## Requirements

- Git
- .NET 8 SDK

Download .NET 8 from Microsoft if you do not already have it installed:

https://dotnet.microsoft.com/download/dotnet/8.0

## Clone And Run

```powershell
git clone https://github.com/howard618/C-StudyApp.git
cd C-StudyApp
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
