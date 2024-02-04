using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace Test.InfrastructureTest
{
    [Table("Contacts")]
    internal class Contacts
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
    }

    internal class ContactsRepository : GenericRepository<Contacts>
    {
        internal ContactsRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    internal class ContactsDbContext : DbContext
    {
        internal ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options)
        {
        }

        internal DbSet<Contacts> Contacts  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=#rain1011;Database=mym");
        }
    }

    [TestClass]
	public class GenericRepositoryTest
    {
        private readonly ContactsRepository repository;

        public GenericRepositoryTest()
        {
            var dbContext = new ContactsDbContext(new DbContextOptions<ContactsDbContext>());
            repository = new ContactsRepository(dbContext);
        }

		[TestMethod]
		public void GetAll_Test()
		{
            var contacts = repository.GetAll();
            Assert.AreEqual(10, contacts.Count());
        }

        [TestMethod]
        public void GetById_Test()
        {
            var contact = repository.GetById(1);
            Assert.AreEqual("rain", contact.Name);
            Assert.AreEqual("me", contact.Description);
        }

        [TestMethod]
        public void FindAll_Test()
        {
            var contacts = repository.FindAll(new { Description = "colleague" });
            Assert.AreEqual(3, contacts.Count());
        }

        [TestMethod]
        public void Find_Test()
        {
            var contact = repository.Find(new { Description = "me" });
            Assert.AreEqual("rain", contact.Name);
        }

        [TestMethod]
        public void Insert_And_Delete_Test()
        {
            Contacts contact = new Contacts
            {
                Id = 11,
                Name = "Milly",
                Description = "friend"
            };

            repository.Insert(contact);
            var contacts = repository.GetAll();
            Assert.AreEqual(11, contacts.Count());
            Assert.AreEqual("Milly", contacts.Last().Name);

            repository.Delete(contact.Id);
            contacts = repository.GetAll();
            Assert.AreEqual(10, contacts.Count());
            Assert.AreNotEqual("Milly", contacts.Last().Name);
        }

        [TestMethod]
        public void Update_Test()
        {
            Contacts contact = new Contacts
            {
                Id = 2,
                Name = "eva",
                Description = "wife"
            };

            repository.Update(contact);
            var result = repository.GetById(contact.Id);
            Assert.AreEqual("wife", result.Description);

            contact = new Contacts
            {
                Id = 2,
                Name = "eva",
                Description = "girlfriend"
            };

            repository.Update(contact);
            result = repository.GetById(contact.Id);
            Assert.AreEqual("girlfriend", result.Description);
        }
    }
}

