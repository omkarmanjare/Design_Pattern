using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class ThreadSafeSingletonClass
    {
            private static ThreadSafeSingletonClass instance;
            private static readonly object lock1 = new object();  //simply create an object that will be used as token to hold an object
            private static int counter = 100;

            private ThreadSafeSingletonClass()
            {
                counter++;
                
            }

            public static ThreadSafeSingletonClass getInstance
            {
                get
                {
                // here apply the lock on this module of object creation so     
                //two threads wont come at a time and 

                lock (lock1)  
                    {
                        if (instance == null)
                        {
                            instance = new ThreadSafeSingletonClass();

                            //instance.
                        }
                        return instance;
                    }
                }
            }
            public int Counter { get { return counter; } }
    }
}
