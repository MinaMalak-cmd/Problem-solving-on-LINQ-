using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Subject
    {
        public int Code { get; set; }
        public string Name { get; set; }       
    }
    class Student
    {
        public int ID { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Subject[] subjects { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region Apply some method expressions
            List<int> numbers = new List<int>
              () { 2, 4, 6, 7, 1, 4, 2, 9, 1 };
            var q1 = numbers.Distinct().OrderBy(n=>n);
            //foreach (var item in q1)
            //{
            //    Console.WriteLine("< Number="+item+", Multiply="+item*item+">");
            //}
            #endregion

            #region Apply Method Expression And Query Expression
            string[] names = { "Tom", "Dick", "Harry", "MARY", "Jay" };
            var q2 = names.Where(n => n.Length == 3);
            var q3 = from n in names
                     where n.Length == 3
                     select n;
            //select new{name=n}
            //select new { name = n.ToUpper(), lenght = n.Length }
            //foreach (var item in q2)
            //{
            //    Console.WriteLine(item);
            //}
            //foreach (var item in q3)
            //{
            //    Console.WriteLine(item);
            //}

            //Pb2:Select names contains Select names that contains “a” letter (Capital or Small )then sort them by length 
            //(Use toLower method and Contains method ) 

            //using Lambda/Method expression
            var q4 = names.Where(n => n.ToLower().Contains('a')).OrderBy(n=>n.Length);

            //using query expression
            var q5 = from n in names where n.ToLower().Contains('a')
                     orderby n.Length
                   select n ;
            //foreach (var item in q5)
            //{
            //    Console.WriteLine(item);
            //}

            //Pb3:Display the first 2 names 
            //using Lambda/Method expression
            var q6 = names.Take(2);
            foreach (var item in q6)
            {
                //Console.WriteLine(item);
            }

            //using Query expression
            var q7 = (from n in names                    
                     select n).Take(2);                    
            foreach (var item in q7)
            {
                //Console.WriteLine(item);
            }
            #endregion

            #region Applying Method expressions on some Object Data to simulate DB behaviour
            List<Student> students = new List<Student>(){
            new Student(){ ID=1, FirstName="Ali", LastName="Mohammed", subjects=new Subject[]{ new Subject(){ Code=22,Name="EF"}, new Subject(){ Code=33,Name="UML"}}},
            new Student(){ ID=2, FirstName="Mona", LastName="Gala", subjects=new Subject []{ new Subject(){ Code=22,Name="EF"}, new Subject (){ Code=34,Name="XML"},new Subject (){ Code=25, Name="JS"}}},             
            new Student(){ ID=3, FirstName="Yara", LastName="Yousf", subjects=new Subject []{ new Subject (){ Code=22,Name="EF"}, new Subject (){ Code=25,Name="JS"}}},
            new Student(){ ID=1, FirstName="Ali", LastName="Ali", subjects=new Subject []{  new Subject (){ Code=33,Name="UML"}}},
            };

            //Pb1: Using Method Chaining Display Full name and number of subjects for each student 
            var q8 = students.Select(n => new { FullName = n.FirstName+ n.LastName, No_of_Subjects = n.subjects.Length });
            foreach (var item in q8)
            {
                //Console.WriteLine(item);
            }

            //pb2:Write a query which orders the elements in the list by FirstName Descending
            //then by LastName Ascending and result of query displays 
            //only first names and last names for the elements in list as follow 
            var q9 = students.OrderByDescending(n => n.FirstName).ThenBy(x => x.LastName);
            
            foreach (var item in q9)
            {
                //Console.WriteLine(item.FirstName+"\t"+item.LastName);
            }

            //pb3:Display each student and student’s subject(use selectMany)

            var q10 = students.SelectMany(std => std.subjects,
                                             (student, subject) => new
                                             {
                                                 StudentName = student.FirstName+student.LastName,
                                                 SubjectName = subject.Name
                                             }
                                             )
                                        .ToList();
            foreach (var item in q10)
            {
                //Console.WriteLine(item.StudentName + " => " + item.SubjectName);
            }
            var q11 = students.SelectMany(std => std.subjects,
                                             (student, subject) => new
                                             {
                                                 StudentName = student.FirstName + student.LastName,
                                                 SubjectName = subject.Name
                                             }
                                             )
                                        ;
            //pb4:Then use GroupBy
            var results = q11.GroupBy(
                p => p.StudentName,
                p => p.SubjectName.ToList(),
                (key, g) => new { StudentName = key, Subjects = g.ToString() });
            Console.WriteLine(results);
            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
            #endregion

        }
    }
}
