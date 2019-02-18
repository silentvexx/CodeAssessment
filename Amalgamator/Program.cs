using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amalgamator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> partials = new List<string> {
                "This is a lon",
                "a longer s",
                "is is a longer sent",
                "that I chopped up o",
                "sentence that I ",
                " a different test.",
                "up on my own,",
                "own, so that I",
                "hat I could have"
            };
            var algamator = new Amalgamator(partials);
            var result = algamator.amalgamate();
            Console.WriteLine(algamator.amalgamate());
        }
    }
}
