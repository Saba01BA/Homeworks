using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ToDo
{
    public class Task
    {
        public string Name { get; set; } = string.Empty;

        public bool isCompleted { get; set; } = false;
        
    }
}
