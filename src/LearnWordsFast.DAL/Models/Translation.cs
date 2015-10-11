namespace LearnWordsFast.DAL.Models
{
    public class Translation : IdModel
    {
        public virtual Language Language { get; set; }
        public virtual string TranslationText { get; set; }
    }
}