using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTreeTest
{
    public static class PerformanceTest
    {
        private static ConcurrentDictionary<string, Values> conDic { get; set; } = new ConcurrentDictionary<string, Values>();
        private static List<Values> list = new List<Values>();
        private static Dictionary<string, Values> dic { get; set; } = new Dictionary<string, Values>();
        private static BTree.BTree<string, Values> bTree { get; set; }

        public static void Test(int n, bool clear = true, int degree = 3)
        {
            int multiple = 100;

            if (clear)
            {
                Console.WriteLine("开始初始化数据·······");
                conDic.Clear();
                list.Clear();
                dic.Clear();
                bTree = new BTree.BTree<string, Values>(degree);
                for (int i = 1; i <= n; i++)
                {
                    var iStr = i.ToString();
                    var value = new Values()
                    {
                        Key = iStr,
                        Value = iStr
                    };
                    bTree.Insert(iStr, value);
                    conDic.TryAdd(iStr, value);
                    dic.Add(iStr, value);
                    //list.Add(value);

                }
                Console.WriteLine("初始化数据完成");
            }
            var key = n.ToString();

            var tbTree = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                string value = string.Empty;
                for (int i = 1; i <= n; i++)
                {
                    var data = bTree.Search(key);
                    if (data != null)
                    {
                        value = data.Pointer.Value;
                    }
                    else
                    {
                        Console.WriteLine($"{i} BTree");
                    }
                };
                Console.WriteLine($"运行时间：{sw.ElapsedMilliseconds}/ms,Value:{value}，tbTree");
                sw.Stop();
            });
            var tconDic = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                string value = string.Empty;
                for (int i = 1; i <= n; i++)
                {
                    var data = conDic[key];
                    if (data != null)
                    {
                        value = data.Value;
                    }
                    else
                    {
                        Console.WriteLine($"{i} ConcurrentDictionary");
                    }
                }
                Console.WriteLine($"运行时间：{sw.ElapsedMilliseconds}/ms,Value:{value},tconDic");
                sw.Stop();
            });
            var tdic = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = Stopwatch.StartNew();
                string value = string.Empty;
                for (int i = 1; i <= n; i++)
                {
                    var data = dic[key];
                    if (data != null)
                    {
                        value = data.Value;
                    }
                    else
                    {
                        Console.WriteLine($"{i} Dictionary");
                    }

                };

                Console.WriteLine($"运行时间：{sw.ElapsedMilliseconds}/ms,Value:{value},tdic");
                sw.Stop();
            });
            //var tlist = Task.Factory.StartNew(() =>
            //{
            //    Stopwatch sw = Stopwatch.StartNew();
            //    string value = string.Empty;
            //    for (int i = 1; i <= n; i++)
            //    {
            //        value = list.Find(l => l.Key == i.ToString()).Value;
            //    };
            //    Console.WriteLine($"运行时间：{sw.ElapsedMilliseconds}/ms,Value:{value},tlist");
            //    sw.Stop();
            //});

            Task.WaitAll(tconDic, tdic, tbTree);
            Console.WriteLine($"ALL OK.");
        }

    }
    public class Values
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
