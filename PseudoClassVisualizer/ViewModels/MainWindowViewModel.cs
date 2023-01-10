using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PseudoClassVisualizer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<AssemblyViewModel> Assemblies { get; set; }
    private AssemblyViewModel? _selectedAssembly;

    public AssemblyViewModel? SelectedAssembly
    {
        get => _selectedAssembly;
        set => SetProperty(ref _selectedAssembly, value);
    }

    public MainWindowViewModel()
    {
        Assemblies = new ObservableCollection<AssemblyViewModel>();
    }
    
    public async Task LoadAssemblies(IEnumerable<string> fileNames)
    {
        Assemblies.Clear();
        await Task.Run(() =>
        {
            foreach (var name in fileNames)
            {
                Assembly? assembly = null;
                try
                {
                    assembly = Assembly.LoadFile(name);
                    var assemblyViewModel = new AssemblyViewModel(assembly);
                    if (assemblyViewModel.Types.Any())
                    {
                        Assemblies.Add(assemblyViewModel);
                    }
                }
                catch (ReflectionTypeLoadException rex)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var a in assemblies)
                    {
                        if (assembly.GetName().Name == a.GetName().Name)
                        {
                            var assemblyViewModel = new AssemblyViewModel(a);
                            if (assemblyViewModel.Types.Any())
                            {
                                Assemblies.Add(assemblyViewModel);
                            }
                            break;;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    
                }
            }
        });
        
        
    }
}