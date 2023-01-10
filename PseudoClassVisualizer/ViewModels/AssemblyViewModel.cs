using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PseudoClassVisualizer.ViewModels;

public class AssemblyViewModel: ObservableObject
{
    private string _assemblyName;
    public string AssemblyName
    {
        get => _assemblyName;
        set => SetProperty(ref _assemblyName, value);
    }

    private string _assemblyFullName;

    public string AssemblyFullName
    {
        get => _assemblyFullName;
        set => SetProperty(ref _assemblyFullName, value);
    }
    
    public ObservableCollection<TypeViewModel> Types { get; set; }

    public AssemblyViewModel(Assembly assembly)
    {
        Types = new ObservableCollection<TypeViewModel>();
        var assemblyName = assembly.GetName();
        AssemblyName = assemblyName.Name;
        AssemblyFullName = assemblyName.FullName;
        var types = assembly.GetTypes();
        foreach (var type in types)
        {
            var typeViewModel = new TypeViewModel(type);
            if (typeViewModel.PseudoClasses.Any())
            {
                Types.Add(typeViewModel);
            }
            
        }
    }
}