using System;
using Avalonia.Input;
using Avalonia.Xaml.Interactions.DragAndDrop;
using PseudoClassVisualizer.ViewModels;

namespace PseudoClassVisualizer.Behaviors;

public class FileDropHandler: DropHandlerBase
{
    public override bool Validate(object? sender, DragEventArgs e, object? sourceContext, object? targetContext, object? state)
    {
        return true;
        return base.Validate(sender, e, sourceContext, targetContext, state);
    }

    public override void Drop(object? sender, DragEventArgs e, object? sourceContext, object? targetContext)
    {
        var names = e.Data.GetFileNames();
        if (targetContext is MainWindowViewModel vm)
        {
            vm.LoadAssemblies(names);
        }
        base.Drop(sender, e, sourceContext, targetContext);
    }
}