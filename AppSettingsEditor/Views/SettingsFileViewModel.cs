using CommunityToolkit.Mvvm.ComponentModel;

namespace AppSettingsEditor.Views;

public partial class SettingsFileViewModel : ObservableObject
{
    public SettingsFileViewModel(string filePath)
    {
        FilePath = filePath;
    }

    public string FilePath { get; }
    public string Name => System.IO.Path.GetFileName(FilePath);
}

public class DesignTimeSettingsFileViewModel : SettingsFileViewModel
{
    public DesignTimeSettingsFileViewModel() : base("DesignTime.json")
    {
    }
}