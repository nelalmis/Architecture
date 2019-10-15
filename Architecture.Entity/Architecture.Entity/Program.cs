using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Entity
{
    class Program
    {
        static void Main(string[] args)
        {

            Architecture.Entity.Model2.ModelArc entity = new Model2.ModelArc();
            foreach (var item in entity.Action.ToList())
            {
                Console.WriteLine(item.CommandName);
            }
            Console.ReadLine();
        }
    }
}
