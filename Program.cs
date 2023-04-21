using System;
using System.Threading;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {

            #region singleton class
            // Will try to get two instance s1 and s2.            
            SingletonClasses s1 = SingletonClasses.getInstance;
            // But due to if condition in theSingletonClasses class,only one instance is created and while
            // second instance is tries to create, we get refrence of first instace only
            // and that reference will be be assigned to s2
            //counter value is expected to get increased only once as objet is created once
            SingletonClasses s2 = SingletonClasses.getInstance;
            if (s1.GetHashCode().Equals(s2.GetHashCode()))
                Console.WriteLine("S1 and S2 are same");

            Console.WriteLine("s1.Conuter" + s1.Counter);
            Console.WriteLine("s2.Conuter" + s2.Counter);

            #endregion

            #region ThreadUnsafe singleton class
            //Thread  unsafe demo . You cas create two objects here despite having null check
            SingletonClasses x;
            SingletonClasses y;

            //Here we create two threads and call the respective methods

            Thread thread1 = new Thread(() => { x = SingletonClasses.getInstance; });
            Thread thread2 = new Thread(() => { y = SingletonClasses.getInstance; });
            thread1.Start();
            thread2.Start();

            //whwn we debug the code we can see the constructor of SingletonClass will be called twice
            // and the counter value will be set to 102 after thread.start2()
            #endregion

            #region Call to thread safe singleton class

            Thread thread3 = new Thread(() => { var x = ThreadSafeSingletonClass.getInstance; });
            Thread thread4 = new Thread(() => { var y = ThreadSafeSingletonClass.getInstance; });
            thread3.Start();
            thread4.Start();
            //despite calling thread4.Start() it wont be able to create an object and value of counter will remain 101
            //
            #endregion



            Console.ReadLine();
       
        }
    }
}
