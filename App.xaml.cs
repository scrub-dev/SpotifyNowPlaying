﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NowPlaying.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace NowPlaying
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder().ConfigureServices(services => {
                services.AddSingleton<Server.Server>();
                services.AddSingleton(s => new MainWindowViewModel()
                {
                    Server = s.GetRequiredService<Server.Server>()
                });
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainWindowViewModel>(),
                });
                // add singleton server
                // add singleton main view model
                // add singleton main view with datacontext main view model
            
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
             
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }

}
