using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PseudoClassVisualizer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public void LoadAssemblies(IEnumerable<string> names)
    {
        foreach (var name in names)
        {
            var assembly = Assembly.LoadFile(name);
            var types = assembly.GetTypes();
            
        }        
    }
}