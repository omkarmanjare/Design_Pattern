using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class SingletonClassWithLazyInitilization
    {
        private static readonly Lazy<SingletonClassWithLazyInitilization>  instance = 
            new Lazy<SingletonClassWithLazyInitilization>( () => new SingletonClassWithLazyInitilization );

        private SingletonClassWithLazyInitilization()
        {
                
        }

        public static SingletonClassWithLazyInitilization Instance { get { return instance.Value; } }
        // same above line can be written as below
        //  public static SingletonClassWithLazyInitilization Instance => instance.Value;
    }
}
