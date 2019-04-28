namespace AmnesiaStoryteller.Abstractions
{
    public interface ICustomStorySettingsValidator
    {
        bool StructureIsValid(string filePath);
    }
}
