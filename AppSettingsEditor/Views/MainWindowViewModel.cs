using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppSettingsEditor.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AppSettingsEditor.Views;

public partial class MainWindowViewModel : ObservableObject
{
    public MainWindowViewModel()
    {
        var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        LoadFilesInFolder(folder);
    }

    private void LoadFilesInFolder(string? folder)
    {
        Files.Clear();
        if (Directory.Exists(folder))
        {
            foreach (var file in
                     Directory.EnumerateFiles(folder, "appsettings*.json", SearchOption.TopDirectoryOnly)
                         .Where(f => !f.Contains(".deps.") && !f.Contains(".runtimeconfig."))
                         .OrderBy(Path.GetFileName))
            {
                Files.Add(new SettingsFileViewModel(file));
            }
        }
    }

    public ObservableCollection<SettingsFileViewModel> Files { get; } = new();

    [RelayCommand]
    async Task ChooseFolder()
    {
        var folder = await WeakReferenceMessenger.Default.Send<SelectFolderMessage>();
        LoadFilesInFolder(folder);
    }
}