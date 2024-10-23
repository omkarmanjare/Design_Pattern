using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class EuropeanPlug  //Adaptee class
    {
        public void PlugIn()
        {
            Console.WriteLine("European plug connected.");
        }
    }

    public class AmericanPlug  //Adaptee class
    {
        public void PlugIn()
        {
            Console.WriteLine("American plug connected.");
        }
    }

    public interface IDevice
    {
        void ConnectDevice();
    }

    public class TravelAdapter : IDevice  //Adaptor class
    {
        private EuropeanPlug _europeanPlug;

        public TravelAdapter(EuropeanPlug europeanPlug)
        {
            _europeanPlug = europeanPlug;
        }

        public void ConnectDevice()
        {
            _europeanPlug.PlugIn();
        }
    }

    // Client Code
    public class AdapterExample
    {
        public static void Main(string[] args)
        {
            EuropeanPlug europeanPlug = new EuropeanPlug();
            IDevice adapter = new TravelAdapter(europeanPlug);

            // Using the adapter to connect the European plug
            adapter.ConnectDevice();
        }
    }
}
