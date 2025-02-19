using System;
using System.Linq;
using System.Threading.Tasks;
using AppSettingsEditor.Messages;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging;

namespace AppSettingsEditor.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<MainWindow, SelectFolderMessage>(this,
            static (win, msg) => { msg.Reply(win.ChooseFolderAsync()); });
    }

    async Task<string?> ChooseFolderAsync()
    {
        var results = await StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            AllowMultiple = false,
        });
        var folder = results.Select(r => r.TryGetLocalPath()).FirstOrDefault();
        return folder;
    }

    protected override void OnClosed(EventArgs e)
    {
        WeakReferenceMessenger.Default.UnregisterAll(this);
        base.OnClosed(e);
    }
}