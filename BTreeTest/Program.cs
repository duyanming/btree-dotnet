using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreeTest
{
    using BTree;
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "BTreeTest";
            BTree<string, Person> bTree = new BTree<string, Person>(3);//Btree 非线程安全
            int[] data = new int[] { 
            17,35,8,12,26,30,65,87,3,5,9,10,13,15,28,29,36,60,75,79,90,99
            };
            int len = 1000;
            data = new int[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = i;
            }
            foreach (int i in data)
            {
                var person = new Person()
                {
                    Name = i + "Name;",
                    Age = i
                };
                bTree.Insert(i.ToString(), person);
            }
            //var person1 = new Person()
            //{
            //    Name = 1 + "Nameperson1;",
            //    Age = 1
            //};
            //bTree.Insert(1.ToString(), person1);

            //Parallel.For(0,100,(i)=> {
            //    var person = new Person() { 
            //        Name=i+"Name;",
            //        Age=i
            //    };
            //    bTree.Insert(i.ToString(), person);
            //});
            //bTree.Delete("6");
            //for (int i = 0; i < 10; i++)
            //{
            //    var entity = bTree.Search(i.ToString());
            //}
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(bTree);
            Console.ReadLine();
        }
    }
    public class Person
    {
        public String Name { get; set; }
        public int Age { get; set; }
    }
}
