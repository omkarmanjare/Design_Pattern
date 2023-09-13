using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    //The Factory method is a creational design pattern that provides an interface for creating objects without specifying their concrete classes.
    // It defines a method that we can use to create an object instead of using its constructor.
    // The important thing is that the subclasses can override this method and create objects of different types.
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
}

//Repository = A repository is used to separate the concerns between the domain logic and data layer of an application. It helps the domain layer to be decoupled from the data layer.
//Factory = A factory’s man purpose is to create instances of objects. In the case of a web application using Web-API / MVC, the factory is often used to create instances of the view model objects or data model objects.
