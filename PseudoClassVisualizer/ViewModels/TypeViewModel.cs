using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Avalonia.Controls.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PseudoClassVisualizer.ViewModels;

public class TypeViewModel: ObservableObject
{
    private string _typeName;

    public string TypeName
    {
        get => _typeName;
        set => SetProperty(ref _typeName, value);
    }

    private string _namespace;

    public string Namespace
    {
        get => _namespace;
        set => SetProperty( ref _namespace, value);
    }
    
    public ObservableCollection<string> PseudoClasses { get; set; }

    public TypeViewModel(Type type)
    {
        Namespace = type.Namespace;
        TypeName = type.Name;
        HashSet<string> pseudoClasses = new HashSet<string>();
        var attributes = type.GetCustomAttributes<PseudoClassesAttribute>();
        foreach (var attribute in attributes)
        {
            var pcs = attribute.PseudoClasses;
            foreach (var pc in pcs)
            {
                pseudoClasses.Add(pc);
            }
        }
        PseudoClasses = new ObservableCollection<string>(pseudoClasses);
    }
}