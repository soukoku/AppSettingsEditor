using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AppSettingsEditor.Messages;

public class SelectFolderMessage : AsyncRequestMessage<string?>
{
    public FolderPickerOpenOptions Options { get; }

    public SelectFolderMessage(FolderPickerOpenOptions options)
    {
        Options = options;
    }
}