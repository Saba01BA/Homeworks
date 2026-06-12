using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork10
{

    internal class Class1 : FileWorker
    {

        public override void Delete()
        {
            Console.WriteLine($" i Delete {Storage} GB of Data ");
        }

        public override void Edit()
        {
            Console.WriteLine($" i Edit {Storage} GB of Data ");
        }

      
        public override void Read()
        {
            Console.WriteLine($" i Read {Storage} GB of Data ");
        }

        public override void Write()
        {
            Console.WriteLine($" i Write {Storage} GB of Data ");
        }
    }
}
