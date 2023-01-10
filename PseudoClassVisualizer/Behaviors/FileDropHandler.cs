using System;
using System.Diagnostics;
using Avalonia.Input;
using Avalonia.Xaml.Interactions.DragAndDrop;
using PseudoClassVisualizer.ViewModels;

namespace PseudoClassVisualizer.Behaviors;

public class FileDropHandler: DropHandlerBase
{
    public override bool Validate(object? sender, DragEventArgs e, object? sourceContext, object? targetContext, object? state)
    {
        return true;
    }

    public override async void Drop(object? sender, DragEventArgs e, object? sourceContext, object? targetContext)
    {
        var names = e.Data.GetFileNames();
        if (names is null) return;
        if (targetContext is MainWindowViewModel vm)
        {
            try
            {
                await vm.LoadAssemblies(names);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        base.Drop(sender, e, sourceContext, targetContext);
    }
}