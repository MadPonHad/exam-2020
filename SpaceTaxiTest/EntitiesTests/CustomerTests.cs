using System.Collections.Generic;
using DIKUArcade;
using NUnit.Framework;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.LevelLoading;

namespace SpaceTaxiTest.EntitiesTests {
    public class CustomerTests {
        private LevelCreator levelCreator;
        private Customer customer;
        private List<Customer> customers;

        [SetUp]
        public void Init() {
            Window.CreateOpenGLContext();                        
        }

        [TestCase]
        public void LevelSnScustomer() {
            levelCreator = new LevelCreator();
            customers = new List<Customer>();
            levelCreator.CreateLevel("short-n-sweet.txt");
            foreach (var customer in levelCreator.Customer) {
                Customer cust = new Customer(customer);
                customers.Add(cust);
            }

            Assert.True(customers[0].name == "Alice");
            Assert.True(customers[0].SpawnTime == 1);
            Assert.True(customers[0].StartPlatform == '1');
            Assert.True(customers[0].DropOffPlatform == 'J');
            Assert.True(customers[0].dropOffTime == 10);
            Assert.True(customers[0].dropOffPoints == 100);
        }
        
        [TestCase]
        public void LevelTbCustomer() {
            levelCreator = new LevelCreator();
            customers = new List<Customer>();
            levelCreator.CreateLevel("the-beach.txt");
            foreach (var customer in levelCreator.Customer) {
                Customer cust = new Customer(customer);
                customers.Add(cust);
            }

            Assert.True(customers[0].name == "Bob");
            Assert.True(customers[0].SpawnTime == 3);
            Assert.True(customers[0].StartPlatform == 'J');
            Assert.True(customers[0].DropOffPlatform == 'r');
            Assert.True(customers[0].dropOffTime == 10);
            Assert.True(customers[0].dropOffPoints == 100);
            
            Assert.True(customers[1].name == "Carol");
            Assert.True(customers[1].SpawnTime == 30);
            Assert.True(customers[1].StartPlatform == 'r');
            Assert.True(customers[1].DropOffPlatform == '^');
            Assert.True(customers[1].dropOffTime == 10);
            Assert.True(customers[1].dropOffPoints == 100);
        }
    }
}