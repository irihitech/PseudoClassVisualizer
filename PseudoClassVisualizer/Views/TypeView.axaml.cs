using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
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
        if (sender is Label l && l.Content != null) 
        {
            await Application.Current?.Clipboard.SetTextAsync(l.Content?.ToString());
        }
    }
}