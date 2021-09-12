using System;
using System.Threading.Tasks;
using MemeJudger.Core;

namespace MemeJudger
{
    class Program
    {
        static void Main(string[] args)
            => new CoreHandler().Main().GetAwaiter().GetResult();
    }
}