using System;
using System.Collections.Generic;
using Binding.Different.Ways.Model;
using Binding.Tests.Builders;
using Binding.Tests.PhoneBuilders;
using Binding.Tests.InvoiceDomains;
using System.Diagnostics;
using System.Linq;
using BindingTests.HelperAssert;
using BindingTests.PersonDomain;
using NUnit.Framework;
using DeepObj = NUnit.DeepObjectCompare.Is;

namespace Binding.Tests
{

    [TestFixture]
    public class Testing_Immutable_Domains
    {

        /*
         Cons
         1-This test is not as expressive as it could be.
         2-The irrelevant data pollutes our test
         3-From just looking at the test, it’s not clear whether they influence the outcome our not.
           In this example it might be obvious, but in a real situation that’s not always the case.
         4-If at any given point in time, we need to add an unrelated item to the constructor, 
           it will break the test and we need to modify it. 
         
          We Wish
          1-For our production code, we want the Employee-class to be immutable.
          2-This way the Employee-class can encapsulate its data and assure that operations work on the correct data that has not been tampered with.
            (PT)Dessa forma, a classe Employee pode encapsular seus dados e garantir que as operações funcionem nos dados corretos que não foram adulterados.
          3-We would like to be able to only provide some data so we can test the relevant methods.
          
          The Problem
            In essence, the problem we’re facing is that our unit test is bound to the constructor.
         
          Solution
            A common design pattern to resolve this dependency is the builder pattern.
         
         */
        [Test]
        public void GetFullNameReturnsCombination_LessExpressive_Test()
        {
            // Arrange
            Employee emp = new Employee(1, "Kenneth", "Truyers", new DateTime(1970, 1, 1), "My Street");
            // Act 
            string fullname = emp.getFullName();
            // Assert 
            Assert.That(fullname, Is.EqualTo("Kenneth Truyers"));
        }


        [Test]
        public void GetFullNameReturnsCombination_MoreExpressive_Test()
        {
            // Arrange
            Employee emp = new EmployeeBuilder().WithFirstName("Kenneth").WithLastName("Truyers");
            // Act
            string fullname = emp.getFullName();
            // Assert 
            Assert.That(fullname, Is.EqualTo("Kenneth Truyers"));
        }
        [Test]
        public void GetAgeReturnsCorrectValue_MoreExpressive_Test()
        {
            // Arrange
            Employee emp = new EmployeeBuilder().WithBirthDate(new DateTime(1983, 1, 1));
            // Act 
            int age = emp.getAge();
            // Assert
            Assert.That(age, Is.EqualTo(DateTime.Today.Year - 1983));
        }

        #region Invoice Tests
        [Test]
        public void GetInvoiceObjct_MoreExpressive_Test()
        {
            // Arrange
            var expected = new List<InvoiceItem>
            {
                new InvoiceItem (1, "IPad", 20.00d),
                new InvoiceItem (2, "Stand for IPad", 15.00d),
                new InvoiceItem (3,"LapTop Asus", 505.00d)
            }.ToList();
          
            // Act 
            Invoice invoice = new InvoiceBuilder().WithInvoiceLines();
            var resultTypeTest = invoice.GetInvoiceItems();
            var resultValues = resultTypeTest.InvoiceList.ToList();
            // Assert


            Assert.IsInstanceOf<InvoiceLines>(resultTypeTest);
            Assert.That(expected, DeepObj.DeepEqualTo(resultValues));
            //AssertHelper.HasEqualFieldValues(expected, resultValues);
            
            //Assert.IsInstanceOf<List<InvoiceItem>>(lines);

            //Assert.That(expected, Is.EqualTo(lines));

            //Assert.That("Hello", Is.TypeOf(typeof(string)));
            //Assert.That("Hello", Is.Not.TypeOf(typeof(int)));
            //Assert.That(invoice.lines, Is.EqualTo(expected));
            //Assert.That(new[] { 1.05, 2.05, 3.05 }, Is.EquivalentTo(new[] { 3.0, 2.0, 1.0 }));
        }
        #endregion



        #region Phone Builder Test
        [Test]
        public void MobilePhone_Android_BuilderPattern_Test()
        {
            // Arrange
            Manufacturer newManufacturer = new Manufacturer();
            IPhoneBuilder phoneBuilder = null; // Lets have the Builder class ready

            // Creating an Android phone
            phoneBuilder = new AndroidPhoneBuilder();
            newManufacturer.Construct(phoneBuilder);
            Debug.WriteLine("A new Phone built:\n\n{0}", phoneBuilder.Phone.ToString());

            // Now let us create a Windows Phone
            phoneBuilder = new WindowsPhoneBuilder();
            newManufacturer.Construct(phoneBuilder);
            Debug.WriteLine("A new Phone built:\n\n{0}", phoneBuilder.Phone.ToString());

        }
        #endregion


        [Test]
        // [ExpectedException(typeof(AssertFailedException))]
        public void ShouldFailForDifferentClasses()
        {
            var actual = new PersonDomain("John", "A");
            var expected = new PersonDomain("John", "A");

            PersonDomain person1 = new PersonDomain("John", "A");
            PersonDomain person2 = new PersonDomain("John", "A");

            Debug.WriteLine("Calling Equals:");
            Debug.WriteLine(person1.Equals(person2));

            Debug.WriteLine("\nCasting to an Object and calling Equals:");
            Debug.WriteLine(((object)person1).Equals((object)person2));  


            AssertHelper.HasEqualFieldValues(expected, actual);
        }

    }
}