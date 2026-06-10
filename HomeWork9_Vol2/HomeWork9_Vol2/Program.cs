
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
namespace HomeWork9_Vol2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var saba = new LazyStudent();
            var sababefore = new GoodStudent();
            List<Student> students = new List<Student> { saba, sababefore };
            var classRoom = new ClassRoom(students);
            classRoom.WholeClass();

        }
    }


    public class Student
    {
        public string Name { get; set; }
        public virtual void Study()
        {
            Console.WriteLine("Student Studies");
        }
        public virtual void Read()
        {
            Console.WriteLine("Studen Reads");
        }
        public virtual void Write()
        {
            Console.WriteLine("Student Writes");
        }
        public virtual void Relax()
        {
            Console.WriteLine("Student Relaxes");
        }
    }

    public class LazyStudent : Student
    {
        public override void Relax()
        {
            Console.WriteLine("The Lazy Student is Relaxing");
        }
        public override void Read()
        {
            Console.WriteLine("The Lazy Student is not Reading");
        }
        public override void Study()
        {
            Console.WriteLine("The Lazy student is not Studying");
        }
        public override void Write()
        {
            Console.WriteLine("The Lazy student is not Writing");
        }
    }

    public class GoodStudent : Student
    {
        public override void Read()
        {
            Console.WriteLine("The good Student is Reading");
        }
        public override void Study()
        {
            Console.WriteLine("The good student is Studying");
        }
        public override void Write()
        {
            Console.WriteLine("The good student is Writing");
        }
        public override void Relax()
        {
            Console.WriteLine("The good Student is Relaxing");
        }
    }

    public class ClassRoom
        
    {
        public List<Student> StudentsList { get; set; }
        public ClassRoom (List<Student> students)
        {
            StudentsList = students;
        }

        public void WholeClass()
        {
            foreach (var item in StudentsList)
            {
                item.Study();
                item.Read();
                item.Relax();
                item.Write();
            }
        }
    }

}
