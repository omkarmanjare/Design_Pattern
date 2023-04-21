using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class SingletonClasses
    {
        private static SingletonClasses instance;
        private static int counter = 100;

        private SingletonClasses()
        {
            counter++;
        }

        public static SingletonClasses getInstance
        {
            get{
                // not thread safe. If two threads come at same point and evaluates that this condition is true,
                // both will create instance , which voilates the purpose of the singelton pattern
                if (instance == null)
                {
                    //counter++;
                        instance = new SingletonClasses();
                          
                        //instance.
                    }
                return instance;
               }
        }
        public int Counter { get { return counter; } }

    }

   
}
