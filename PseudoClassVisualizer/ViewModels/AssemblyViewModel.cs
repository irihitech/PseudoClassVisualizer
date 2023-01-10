using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using PseudoClassVisualizer.Views;

namespace PseudoClassVisualizer.ViewModels;

public class AssemblyViewModel: ObservableObject
{
    private string? _assemblyName;
    public string? AssemblyName
    {
        get => _assemblyName;
        set => SetProperty(ref _assemblyName, value);
    }

    private string? _assemblyFullName;

    public string? AssemblyFullName
    {
        get => _assemblyFullName;
        set => SetProperty(ref _assemblyFullName, value);
    }

    private string? _version;

    public string? Version
    {
        get => _version;
        set => SetProperty(ref _version, value);
    }

    private string _queryString;

    public string QueryString
    {
        get => _queryString;
        set
        {
            SetProperty(ref _queryString, value);
            OnQueryStringChange(_queryString);
        }
    }
    
    public ObservableCollection<TypeViewModel> Types { get; set; }
    
    private ObservableCollection<TypeViewModel> _selectedTypes;
    public ObservableCollection<TypeViewModel> SelectedTypes
    {
        get => _selectedTypes;
        set => SetProperty( ref _selectedTypes, value);
    }

    public AssemblyViewModel()
    {
        
    }
    public AssemblyViewModel(Assembly assembly)
    {
        var assemblyName = assembly.GetName();
        AssemblyName = assemblyName.Name;
        AssemblyFullName = assemblyName.FullName;
        Version = assemblyName.Version?.ToString();
        var types = assembly.GetTypes();
        List<TypeViewModel> list = new List<TypeViewModel>();
        foreach (var type in types)
        {
            var typeViewModel = new TypeViewModel(type);
            if (typeViewModel.PseudoClasses.Any())
            {
                list.Add(typeViewModel);
            }
        }

        list.Sort((a, b) => String.Compare(a.TypeName, b.TypeName, StringComparison.Ordinal));
        Types = new ObservableCollection<TypeViewModel>(list);
        SelectedTypes = new ObservableCollection<TypeViewModel>(list);
    }

    private void OnQueryStringChange(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            SelectedTypes = new ObservableCollection<TypeViewModel>(Types);
            return;
        }

        var trimmed = s.Trim().ToLowerInvariant();
        var types = Types.Where(a => a.TypeName.ToLowerInvariant().Contains(trimmed));
        SelectedTypes = new ObservableCollection<TypeViewModel>(types);
    }
}