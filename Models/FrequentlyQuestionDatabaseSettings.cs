using LoopApi.Interfaz;

namespace LoopApi.Models
{
    public class FrequentlyQuestionDatabaseSettings : IFrequentlyQuestionDBSettings
    {
        public string FrequentlyQuestionCategoriesCollectionName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
