using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PseudoClassVisualizer.Views;

public partial class TypeView : UserControl
{
    public TypeView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Label { Content: not null } l && TopLevel.GetTopLevel(this)?.Clipboard is { } clipboard ) 
        {
            await clipboard.SetTextAsync(l.Content?.ToString());
        }
    }
}