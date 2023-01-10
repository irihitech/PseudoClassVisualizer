using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PseudoClassVisualizer.Views;

public partial class AssemblyView : UserControl
{
    public AssemblyView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}