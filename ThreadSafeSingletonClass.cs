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
                    //two threads wont come at a time. This is called as double-checked locking technique

                    if (instance == null)
                    // First check to avoid unnecessary locking after instance is created
                    {
                        lock (lock1)  
                        {
                            instance = new ThreadSafeSingletonClass();//instance 
                        }
                    }
                    return instance;
                }
            }
            public int Counter { get { return counter; } }
    }

    //Thread safty single ton class can also be implemented using Lazy<T> in C#
}
