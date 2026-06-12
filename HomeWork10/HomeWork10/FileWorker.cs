using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork10
{
    internal abstract class FileWorker
    {
        public  int Storage { get; set; }
        public  abstract void Read();
        public abstract void Write();
        public abstract void Edit();
        public abstract void Delete();

    }
}
