using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WhiteRaven.Svc.Tasking
{
    public partial interface ITask
    {
        void Execute(XmlNode node);
    }
}
