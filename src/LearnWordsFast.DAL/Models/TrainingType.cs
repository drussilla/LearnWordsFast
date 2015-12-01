namespace LearnWordsFast.DAL.Models
{
    public enum TrainingType
    {
        RepeatTranslation = 0,
        ChooseTranslation = 100,
        ChooseOriginal = 200,
        MatchWords = 300,
        ComposeOriginal = 400,
        TypeTranslation = 500,
        TypeOriginal = 600,

        NoWords = 999999
    }
}