using System;
using System.Configuration;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace MyFirstWindowsService
{
    public partial class WindowsService : ServiceBase
    {
        private System.Timers.Timer timer = null;
        private int interval = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"]);


        public WindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LoggerUtility.LogServiceMessage(LoggerUtility.LogInfo, "Started Service at - " + DateTime.Now);

                this.InitializeServiceTimer();
            }
            catch (Exception ex)
            {
                LoggerUtility.LogServiceMessage(LoggerUtility.LogError, "ERROR: Method - OnStart. Details - " + (ex.Message + ex.StackTrace + ex.Source));

            }
        }

        private void InitializeServiceTimer()
        {
            try
            {
                LoggerUtility.LogServiceMessage(LoggerUtility.LogInfo, "Service will run at " + DateTime.Now.AddMinutes(interval));

                timer = new System.Timers.Timer();
                timer.AutoReset = true;
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
                timer.Interval = interval * 60 * 1000;
                timer.Start();
            }
            catch (Exception ex)
            {
                LoggerUtility.LogServiceMessage(LoggerUtility.LogError, "ERROR: Method - InitializeServiceTimer. Details - " + (ex.Message + ex.StackTrace + ex.Source));
                throw ex;
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                LoggerUtility.LogServiceMessage(LoggerUtility.LogInfo, "Starting at: " + DateTime.Now);
                this.timer.Enabled = false;

                // My Operation
                MyOperation();

                LoggerUtility.LogServiceMessage(LoggerUtility.LogInfo, "Completed at: " + DateTime.Now + ". Service will run at " + DateTime.Now.AddMinutes(interval));

            }
            catch (Exception ex)
            {
                LoggerUtility.LogServiceMessage(LoggerUtility.LogError, "ERROR: Method - timer_Elapsed. Details: - " + (ex.Message + ex.StackTrace + ex.Source));

                this.InitializeServiceTimer();

                throw ex;
            }
            finally
            {                
                this.timer.Enabled = true;
            }
        }

        private void MyOperation()
        {
            // open database connection
            DataBaseOperation.OpenConnection();
            // your operation:
        }

        protected override void OnStop()
        {
            LoggerUtility.LogServiceMessage(LoggerUtility.LogInfo, "- Stoping Service - ");

            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Dispose();
                this.timer = null;
            }
        }


        public void RunOnDebug()
        {
            OnStart(null);
            Thread.Sleep(Timeout.Infinite);
        }
    }
}
