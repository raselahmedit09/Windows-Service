using System;
using System.ServiceProcess;

namespace MyFirstWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            log4net.GlobalContext.Properties["BaseDirectory"] = AppDomain.CurrentDomain.BaseDirectory;
            log4net.Config.XmlConfigurator.Configure();
           
            #if DEBUG //when running under debug mode

            WindowsService serviceInstance = new WindowsService();
            serviceInstance.RunOnDebug();
#else   //other than debug mode (i.e release)

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WindowsService()
            };
            ServiceBase.Run(ServicesToRun);

#endif
        }
    }
}