using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace SOSApp.Svc.Tasking
{
    public partial class TaskThread : IDisposable
    {
        private Timer timer;
        private bool disposed;
        private DateTime started;
        private bool isRunning;
        private Dictionary<string, Task> tasks;
        private int seconds;

        private TaskThread()
        {
            this.tasks = new Dictionary<string, Task>();
            this.seconds = 10 * 60;
        }

        internal TaskThread(XmlNode node)
        {
            this.tasks = new Dictionary<string, Task>();
            this.seconds = 10 * 60;
            this.isRunning = false;
            if ((node.Attributes["seconds"] != null) && !int.TryParse(node.Attributes["seconds"].Value, out this.seconds))
            {
                this.seconds = 10 * 60;
            }
        }

        private void Run()
        {
            this.started = DateTime.Now;
            this.isRunning = true;
            foreach (Task task in this.tasks.Values)
            {
                task.Execute();
            }
            this.isRunning = false;
        }

        private void TimerHandler(object state)
        {
            this.timer.Change(-1, -1);
            this.Run();
            this.timer.Change(this.Interval, this.Interval);
        }

        public void Dispose()
        {
            if ((this.timer != null) && !this.disposed)
            {
                lock (this)
                {
                    this.timer.Dispose();
                    this.timer = null;
                    this.disposed = true;
                }
            }
        }

        public void InitTimer()
        {
            if (this.timer == null)
            {
                this.timer = new Timer(new TimerCallback(this.TimerHandler), null, this.Interval, this.Interval);
            }
        }

        public void AddTask(Task task)
        {
            if (!this.tasks.ContainsKey(task.Name))
            {
                this.tasks.Add(task.Name, task);
            }
        }

        public int Seconds
        {
            get
            {
                return this.seconds;
            }
        }

        public DateTime Started
        {
            get
            {
                return this.started;
            }
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public IList<Task> Tasks
        {
            get
            {
                var list = new List<Task>();
                foreach (var task in this.tasks.Values)
                {
                    list.Add(task);
                }
                return new ReadOnlyCollection<Task>(list);
            }
        }

        public int Interval
        {
            get
            {
                return this.seconds * 1000;
            }
        }
    }
}
