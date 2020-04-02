using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Microsoft.Owin.Hosting;
using System;
using Topshelf;

namespace MyWorkbench.Service
{
    static class Program {
        private static string Endpoint = Config.HangFireUri;

        static void Main() {
            LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());

            HostFactory.Run(x => {
                x.Service<Application>(s => {
                    s.ConstructUsing(name => new Application());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("MyWorkbench Windows Service");
                x.SetDisplayName("MyWorkbench Windows Service");
                x.SetServiceName("MyWorkbenchService");
            });
        }

        private class Application {
            private IDisposable _host;

            public void Start() {
                _host = WebApp.Start<Startup>(Endpoint);

                Console.WriteLine();
                Console.WriteLine("MyWorkbench Server started.");
                Console.WriteLine("Dashboard is available at {0}/hangfire", Endpoint);
                Console.WriteLine();
            }

            public void Stop() {
                _host.Dispose();
            }
        }
    }
}
