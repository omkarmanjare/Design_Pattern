using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    // In C#, a singleton class is designed to ensure that only one instance of the class can ever be created.
    //The primary goal of using a singleton pattern is to control access to a shared resource, ensuring that there is only one instance of a
    //class that provides a global point of access to that instance.
    //usage example:
    //Database Connection Pooling
    //service that connects to the cache. One

    public class SingletonClasses
    {
        private static SingletonClasses instance;
        private static int counter = 100;

        //Why Private Constructor : Access to constructor will ensure object creation. Thus a private constructor
        //will prevent other classes or code outside the singleton class from creating new instances
        private SingletonClasses()
        {
            counter++;
        }

        public static SingletonClasses Instance  // static method used here. A static method also can be used that does the same 
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

   
    public class TestSingleTonClass
    {

        private static TestSingleTonClass instance;  //this is static member

        private TestSingleTonClass()   // this is private constructor
        {
                
        }

        public static TestSingleTonClass Instance  // static property
        {
            get {
                if (instance == null)
                {
                    instance = new TestSingleTonClass();                  
                }
                return instance;
            }
        }
    }  


}

//If a class inherits a singleton class, can that derived class create an instance of the single ton class?
//    - No, if a class inherits from a singleton class, the derived class cannot create a new instance of the singleton class. This is because the singleton pattern is specifically designed to restrict the instantiation of a class to a single instance.
//    WHY:
//    Private Constructor:  In a singleton class, the constructor is private, meaning it cannot be called directly from outside the class, including by any derived classes. 
//    If you allow inheritance from a singleton class, it undermines the singleton pattern because each derived class could potentially try to create its own instance, leading to multiple instances, which violates the singleton principle.
//    Typically, a singleton class is marked as sealed to prevent inheritance, which helps maintain the singleton property by disallowing subclassing. If you remove the sealed keyword and allow inheritance (not recommended), the derived class still won't be able to create an instance of the base Singleton class due to the private constructor. If the derived class tries to create an instance of the singleton, it will get a compilation error.