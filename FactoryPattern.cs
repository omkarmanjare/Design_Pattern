using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //The Factory method is a creational design pattern that provides an interface for creating objects without specifying their concrete classes.
    // It defines a method that we can use to create an object instead of using its constructor.
    // The important thing is that the subclasses can override this method and create objects of different types.
    //Factory = A factory’s main purpose is to create instances of objects. In the case of a web application using Web-API / MVC, the factory is often used to create instances of the view model objects or data model objects.

    public interface ICloudServiceProviderInitilizer
    {
        void CreateUser();
        void ConfigureRegion();
        void AllocateMachinesWithStorageAndCPU();
    }
    public class AmazonCloudServiceProvider : ICloudServiceProviderInitilizer
    {
        public void AllocateMachinesWithStorageAndCPU()
        {
            throw new NotImplementedException();
        }

        public void ConfigureRegion()
        {
            throw new NotImplementedException();
        }

        public void CreateUser()
        {
            throw new NotImplementedException();
        }
    }
    public class GoogleCloudServiceProvider : ICloudServiceProviderInitilizer
    {
        public void AllocateMachinesWithStorageAndCPU()
        {
            throw new NotImplementedException();
        }

        public void ConfigureRegion()
        {
            throw new NotImplementedException();
        }

        public void CreateUser()
        {
            throw new NotImplementedException();
        }
    }


    //Parameterized factoy method where method accepts parameter to create object
    public abstract class CloudServiceProviderCreator
    {
        public abstract ICloudServiceProviderInitilizer CreateUserAccountForSelectedServiceProvider(string cloudProviderName);
    }

    public class ClundServiceProvider : CloudServiceProviderCreator
    {
        public override ICloudServiceProviderInitilizer CreateUserAccountForSelectedServiceProvider(string cloudProviderName)
        {

            ICloudServiceProviderInitilizer vara= new GoogleCloudServiceProvider();
            //Type info = vara.;
            


            switch (cloudProviderName)
            {
                case "GCP" : return new GoogleCloudServiceProvider();
                case "AWS":  return new AmazonCloudServiceProvider();
            }
            return null;

            //insted of switch case, we can also maintain a Dictionary of type Dictionary<string,ICloudServiceProviderInitilizer>() and add the respective 
            //keys as "GCP" and value as GoogleCloudServiceProvider , or key as "AWS" and value as AmazonCloudServiceProvider and then
            //return the object by finding the key
            //but drawback is that the dictionary will have objects unnecessirily created
            //Ather eis also possibility of using reflection 

        }
    }

    //*********************************************
    //Simple Factory(Static Factory Method way of implementing factory pattern)
    //Concept: A static method in a class that returns an instance of different classes based on input parameters.

    public class CloudProviderFactory
    {
        public static ICloudServiceProviderInitilizer GetCloudServiceProvider(string shapeType)
        {
            switch (shapeType.ToLower())
            {
                case "GCP":
                    return new AmazonCloudServiceProvider();
                case "AWS":
                    return new GoogleCloudServiceProvider();
                default:
                    throw new ArgumentException("Invalid shape type");
            }
        }

        public void provider() { 
            ICloudServiceProviderInitilizer cloudProvider = CloudProviderFactory.GetCloudServiceProvider("GCP");
            cloudProvider.AllocateMachinesWithStorageAndCPU();
        }
    }

    //***************************************************
    //factory pattern using IEnumerable/Collection
    public interface ICloudProvider
    {
        public string Type { get; }

        public void someMethod();
    }

    public class CloudProviderA : ICloudProvider
    {
        public string Type
        {
            get
            {
                return "CloudProviderA";
            }
        }

        // public string Type => "CloudProviderA" ; //same as line 125-131
        public void someMethod() { }
    }

    public class CloudProviderB : ICloudProvider
    {
        public string Type
        {
            get
            {
                return "CloudProviderB";
            }
        }

        // public string Type => "CloudProviderA" ; //same as line 125-131
        public void someMethod() { }
    }

    public class CloudProviderIdentifier
    {
        //when application is loaded, the _providers will have all types in it that have inherited ICloudServiceProviderInitilizer
        public readonly IEnumerable<ICloudProvider> _providers;

        public CloudProviderIdentifier(IEnumerable<ICloudProvider> providers)
        {
            _providers = providers;
        }

        public void IdentifyProvider()
        {
            //Identify provider 
            var provider = _providers.FirstOrDefault(p => p.Type == "CloudProviderB"); //here you retirve the specific type from List
        }
    }




}



