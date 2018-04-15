﻿using System;
using System.Collections.Generic;
using Binding.Different.Ways.Model;
using Binding.Tests.Builders;
using Binding.Tests.PhoneBuilders;
using NUnit.Framework;
using Binding.Tests.InvoiceDomains;
using System.Diagnostics;

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
            Invoice invoice = new InvoiceBuilder().withInvoiceLines(new InvoiceLines(new List<InvoiceItem>()));
            List<InvoiceItem> expected = new List<InvoiceItem>
            {
                new InvoiceItem (1, "IPad", 20.00d),
                new InvoiceItem (2, "Stand for IPad", 15.00d),
                new InvoiceItem (3,"LapTop Asus", 505.00d)
            };
          
            // Act 
            List<InvoiceItem> invoiceItems = invoice.lines.GetInvoceList();
            // Assert
            Assert.That(invoiceItems, Is.EqualTo(expected));
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

    }
}