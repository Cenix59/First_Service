using System;
using System.ComponentModel;
using System.IO;
using System.ServiceProcess;
using System.Timers;

namespace IncrementService
{
    public class IncrementService : ServiceBase
    {
        private Timer timer;
        private int counter;

        public IncrementService()
        {
            this.ServiceName = "IncrementService";
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;

        }

        protected override void OnStart(string[] args)
        {
            counter = 0;

            string path = @"C:\Users\nicof\OneDrive\Escritorio\6to\Windows\Service\Increment.txt";
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(OnTimer);
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            counter++;

            string path = @"C:\Users\nicof\OneDrive\Escritorio\6to\Windows\Service\Increment.txt";
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(counter);
            }
        }
    }
}

