namespace LoopApi.Interfaz
{
    public interface IFrequentlyQuestionDBSettings
    {
        string FrequentlyQuestionCategoriesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
