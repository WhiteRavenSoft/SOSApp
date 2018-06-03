using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WhiteRaven.Svc.Tasking
{
    public partial class Task
    {
        private ITask task;
        private bool enabled;
        private Type taskType;
        private string name;
        private bool stopOnError;
        private XmlNode configNode;
        private DateTime lastStarted;
        private DateTime lastSuccess;
        private DateTime lastEnd;
        private bool isRunning;

        private Task()
        {
            this.enabled = true;
        }

        public Task(Type taskType, XmlNode configNode)
        {
            this.enabled = true;
            this.configNode = configNode;
            this.taskType = taskType;
            if ((configNode.Attributes["enabled"] != null) && !bool.TryParse(configNode.Attributes["enabled"].Value, out this.enabled))
            {
                this.enabled = true;
            }
            if ((configNode.Attributes["stopOnError"] != null) && !bool.TryParse(configNode.Attributes["stopOnError"].Value, out this.stopOnError))
            {
                this.stopOnError = true;
            }
            if (configNode.Attributes["name"] != null)
            {
                this.name = configNode.Attributes["name"].Value;
            }
        }

        private ITask CreateTask()
        {
            if (this.Enabled && (this.task == null))
            {
                if (this.taskType != null)
                {
                    this.task = Activator.CreateInstance(this.taskType) as ITask;
                }
                this.enabled = this.task != null;
            }
            return this.task;
        }

        public void Execute()
        {
            this.isRunning = true;
            try
            {
                var task = this.CreateTask();
                if (task != null)
                {
                    this.lastStarted = DateTime.Now;
                    task.Execute(this.configNode);
                    this.lastEnd = this.lastSuccess = DateTime.Now;
                }
            }
            catch (Exception exception)
            {
                this.enabled = !this.StopOnError;
                this.lastEnd = DateTime.Now;
                TaskManager.Instance.ProcessException(this, exception);
            }
            this.isRunning = false;
        }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
        }

        public DateTime LastStarted
        {
            get
            {
                return this.lastStarted;
            }
        }

        public DateTime LastEnd
        {
            get
            {
                return this.lastEnd;
            }
        }

        public DateTime LastSuccess
        {
            get
            {
                return this.lastSuccess;
            }
        }

        public Type TaskType
        {
            get
            {
                return this.taskType;
            }
        }

        public bool StopOnError
        {
            get
            {
                return this.stopOnError;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
        }
    }
}
