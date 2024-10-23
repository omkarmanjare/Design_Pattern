using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /*
     The Adapter Pattern is a structural design pattern that allows incompatible interfaces to work together. It acts as a bridge between two objects, enabling them to communicate even if they have incompatible interfaces. The pattern is particularly useful when you want to integrate a new component into an existing or any legacy system without modifying the existing code.
     */

    //Suppose you have an existing class that provides functionality but doesn't match the interface required by the client code.
    
    public interface ILegacyGetWeatherInfo
    {
        string GetLegacyWatherInfo();
    }

    public class LegacyGetWeatherInfo : ILegacyGetWeatherInfo
    {
        public string GetLegacyWatherInfo()
        {
            return "Weather data from legacy system";
        }
    }

    // Target interface expected by the client code
    public interface INewGetWeatheInfo
    {
        string GetNewWeatherInfo();
    }

    public class NewGetWeatheInfo : INewGetWeatheInfo
    {
        public string GetNewWeatherInfo()
        {
            return "Weather data from New system";
        }
    }

    // Adapter class that adapts new system the target interface
    public class NewWeatherSystemAdapter : ILegacyGetWeatherInfo
    {
        private readonly INewGetWeatheInfo _newWeatherInfo;

        public NewWeatherSystemAdapter(INewGetWeatheInfo newGetWeatheInfo)
        {
            _newWeatherInfo = newGetWeatheInfo;
        }

        public string GetLegacyWatherInfo()    //in the old method name , call the new method object
        {
            // Adapting the existing method to the target interface
            return _newWeatherInfo.GetNewWeatherInfo();
        }
    }

    // Client code using the target interface
    public class Client
    {
        public void DisplayWeatherInfo(ILegacyGetWeatherInfo weatherService)
        {
            string weatherInfo = weatherService.GetLegacyWatherInfo();
            Console.WriteLine(weatherInfo);
        }
    }

    public class Implement
    {
        public void Method()
        {
            Client client = new Client();

            // Existing weather service
            ILegacyGetWeatherInfo weatherService = new LegacyGetWeatherInfo();
            client.DisplayWeatherInfo(weatherService);

            // New weather service adapted using the adapter
            INewGetWeatheInfo newWeatherService = new NewGetWeatheInfo();
            ILegacyGetWeatherInfo adaptedWeatherService = new NewWeatherSystemAdapter(newWeatherService);
            client.DisplayWeatherInfo(adaptedWeatherService);
        }
    }

   
}


interface IOld
{
    void OldMethod1();
}

class OldClass : IOld
{
    public void OldMethod1()
    {

    }
}

interface INew
{
    void NewMethod1();
}

class NewClass : INew
{
    public void NewMethod1() { }
}

class AdapteorClass : IOld
{
    INew _iNew;
    public AdapteorClass(INew iNew)
    {
        _iNew = iNew;            
    }
    public void OldMethod1()
    {
        _iNew.NewMethod1();
    }
}

class Clinet
{
    void Execute(IOld old);
    {
        old.OldMethod1();
    }
}

// 
Client c = new Client();

IOld old = new OldClass();
c.Execute(old);

IOld new1 = new INew();
c.Execute(new1);
//




