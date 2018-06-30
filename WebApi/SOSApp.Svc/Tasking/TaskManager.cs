using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SOSApp.Svc.Tasking
{
    public partial class TaskManager
    {
        private static readonly TaskManager taskManager = new TaskManager();
        private List<TaskThread> taskThreads = new List<TaskThread>();

        private TaskManager()
        {
        }

        internal void ProcessException(Task task, Exception exception)
        {
            try
            {
                //IoC.Resolve<LogDataService>().LogError(exception, ErrorTypeEnum.TaskError);
            }
            catch
            {
            }
        }

        public void Initialize(string configFile, string nodePath)
        {
            var document = new XmlDocument();
            document.Load(configFile);
            Initialize(document.SelectSingleNode(nodePath));
        }

        public void Initialize(XmlNode node)
        {
            if (node == null)
                return;

            this.taskThreads.Clear();
            foreach (XmlNode node1 in node.ChildNodes)
            {
                if (node1.Name.ToLower() == "thread")
                {
                    var taskThread = new TaskThread(node1);
                    this.taskThreads.Add(taskThread);
                    foreach (XmlNode node2 in node1.ChildNodes)
                    {
                        if (node2.Name.ToLower() == "task")
                        {
                            var attribute = node2.Attributes["type"];
                            var taskType = Type.GetType(attribute.Value);
                            if (taskType != null)
                            {
                                var task = new Task(taskType, node2);
                                taskThread.AddTask(task);
                            }
                        }
                    }
                }
            }
        }

        public void Start()
        {
            foreach (var taskThread in this.taskThreads)
            {
                taskThread.InitTimer();
            }
        }

        public void Stop()
        {
            foreach (var taskThread in this.taskThreads)
            {
                taskThread.Dispose();
            }
        }

        public static TaskManager Instance
        {
            get
            {
                return taskManager;
            }
        }

        public IList<TaskThread> TaskThreads
        {
            get
            {
                return new ReadOnlyCollection<TaskThread>(this.taskThreads);
            }
        }
    }
}
