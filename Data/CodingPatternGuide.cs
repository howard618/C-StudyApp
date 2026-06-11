namespace FlashCards.Data;

public record CodingPatternGuide(
    string Name,
    string Explanation,
    string HowToIdentify,
    string InterviewLanguage,
    IReadOnlyList<CodingPracticeCard> Problems);
