using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AppSettingsEditor.Messages;
using Avalonia.Platform.Storage;
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
                         .Select(f => new { Path = f, Parts = f.Split('.') })
                         .OrderBy(f => f.Parts.Length)
                         .ThenBy(f => f.Path))
            {
                Files.Add(new SettingsFileViewModel(file.Path));
            }
        }
    }

    public ObservableCollection<SettingsFileViewModel> Files { get; } = new();

    [RelayCommand]
    async Task ChooseFolder()
    {
        var msg = new SelectFolderMessage(new FolderPickerOpenOptions());
        var folder = await WeakReferenceMessenger.Default.Send(msg);
        LoadFilesInFolder(folder);
    }
}