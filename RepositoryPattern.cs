using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DesignPatterns
{
    /*Repository Pattern is an abstraction of the Data Access Layer.
    It hides the details of how exactly the data is saved or retrieved from the underlying data source.
    The details of how the data is stored and retrieved is in the respective repository. 
    The Repository Pattern is a design pattern used to abstract the data layer of an application.
    It provides a way to separate the logic that retrieves data from the business logic, making the application more modular, testable, and
    maintainable.The repository pattern acts as an intermediary between the application and the data source, such as a database or an external
    service.
    Repository = A repository is used to separate the concerns between the domain logic and data layer of an application. It helps the domain layer to be decoupled from the data layer.*/
    /* Types: 
    * Simple Repository: For straightforward scenarios with a single entity type.
    * Generic Repository: For applications with multiple entities that share similar operations.
    * Entity Framework Repository: For applications that interact with a database using an Object Relation Mapping.
    * Unit of Work with Repository: For managing multiple repositories and ensuring transactional consistency.
    
     Benifits:
    Separation of Concerns:The repository pattern decouples the data access logic from the business logic, allowing each to evolve independently.
    Abstraction: It provides an abstraction layer over the data access code, allowing the business logic to interact with the data layer through a well-defined interface without needing to know the details of how the data is persisted or retrieved.
    Testability:By abstracting the data access code, it becomes easier to write unit tests. The repository can be mocked, allowing you to test business logic without needing a live database.
    Centralized Data Access Logic:The repository pattern centralizes data access logic, ensuring that all data operations are performed in a consistent manner.
    Ease of Maintenance: Changes to the data source (e.g., switching from SQL to NoSQL) can be made with minimal impact on the business logic, as only the repository layer needs to be modified*/


    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }

    //#1 :  Simple Repository:
    // Concrete Repository Implementation
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
            }
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }

    //#2 : Repository with EntityFramework : In most real-world applications, repositories are used with an ORM (Object-Relational Mapper) like Entity Framework. This allows the repository to interact with a database in a more structured way.
    public class ProductRepositoryWithEntityFramework : IProductRepository
    {
        private readonly DbContext _context;

        public ProductRepositoryWithEntityFramework(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Set<Product>().ToList();
        }

        public Product GetById(int id)
        {
            return _context.Set<Product>().Find(id);
        }

        public void Add(Product product)
        {
            _context.Set<Product>().Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Set<Product>().Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Set<Product>().Remove(product);
                _context.SaveChanges();
            }
        }
    }

    //#3 : Generic Repository Implementation. A generic repository is a more flexible implementation that can handle multiple entity types. This is useful if you have many entities with similar CRUD operations.
    // Generic Repository Interface
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }


    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _entities = new List<T>();

        public IEnumerable<T> GetAll()
        {
            return _entities;
        }

        public T GetById(int id)
        {
            // Assuming T has an Id property (you would need to handle this in practice)
            dynamic entity = _entities.FirstOrDefault(e => ((dynamic)e).Id == id);
            return entity;
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            // Logic to update entity
        }

        public void Delete(int id)
        {
            T entity = GetById(id);
            if (entity != null)
            {
                _entities.Remove(entity);
            }
        }
    }

    // #4 : Unit of Work pattern :  For managing multiple repositories and ensuring transactional consistency.
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        int Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        public IProductRepository Products { get; private set; }

        public UnitOfWork(DbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        public void Usage()
        {
            using (var unitOfWork = new UnitOfWork(new YourDbContext()))
            {
                var products = unitOfWork.Products.GetAll();
                unitOfWork.Complete();
            }
        }
    }

}
