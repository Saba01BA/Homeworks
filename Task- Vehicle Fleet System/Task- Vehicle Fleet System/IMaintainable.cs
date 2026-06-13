using System;
using System.Collections.Generic;
using System.Text;

namespace Task__Vehicle_Fleet_System
{
    internal interface IMaintainable
    {
        public bool NeedsMaintenance();
        public string MaintenanceInfo();
    }
}
