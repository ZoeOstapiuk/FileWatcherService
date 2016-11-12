using FileWatcherLogger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileWatcherService
{
    public partial class WatcherService : ServiceBase
    {
        private DirectoryChangesHandler handler;

        public WatcherService()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        protected override void OnStart(string[] args)
        {
            handler = new DirectoryChangesHandler("D:\\temp");

            Thread loggerThread = new Thread(handler.Start);
            loggerThread.Start();
        }

        protected override void OnStop()
        {
            handler.Stop();
            Thread.Sleep(1000);
        }
    }
}
