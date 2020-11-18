using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket.WPF.ViewModels.Core
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MWViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
    }
}
