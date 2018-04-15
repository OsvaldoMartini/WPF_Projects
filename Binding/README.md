Builder Pattern tests with Immutable Domains

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
         
	Step 1: Create a class with a method that creates an Employee.
		public class EmployeeBuilder
		{
			private int id = 1;
			private string firstname = "first";
			private string lastname = "last";
			private DateTime birthdate = DateTime.Today;
			private string street = "street";
			public EmployeeBuilder Build()
			{
				return new EmployeeDomain(id, firstname, lastname, birthdate, street);
			}
			...
			//Methods
			...
		}
	
	
	Step 2: Create modification methods
	
		//Modification methods
        public void WithFirstName(string firstname)
        {
            this.firstname = firstname;
        }
        
	Unit Test Usage:
		
		EmployeeBuilder builder = new EmployeeBuilder();
		builder.WithFirstName("Kenneth");
		builder.WithLastName("Truyers");
		Employee emp = builder.Build();
	
	
	Step 3: Create a fluent API
		Modifying the modifier-methods
		
		public EmployeeBuilder WithFirstName(string firstname) {
		 this.firstname = firstname;
		 return this;
		}
		
	Unit Test Usage:
		As you can see, instead of returning void, all methods now return the current instance. 
		This allows us to **chain **the methods and rewrite our client code to this:
		
		EmployeeBuilder builder = new EmployeeBuilder().WithFirstName("Kenneth").WithLastName("Truyers");
		Employee emp = builder.Build();
		
	Step 4: Provide automatic conversion - 
		C# Feature called Operator Overloading
		It let’s you define some code you can execute when the runtime tries to convert a type to another type.
		
		We would like to convert an EmployeeBuilder to an Employee.
		
		public static implicit operator Employee(EmployeeBuilder instance) {
		 return instance.Build();
		}
		
		This is a static method that receives an instance of an EmployeeBuilder and returns an Employee. 
		In this case we perform the conversion by calling the Build-method on the builder, 
		thereby returning the constructed Employee. 
		The implicit keyword tells the compiler that an explicit cast is not required. 
		This let’s us write our client code like this:
		
		Unit Test Usage:
		
		Employee emp = new EmployeeBuilder().WithFirstName("Kenneth") .WithLastName("Truyers");
		
		
		Conclusions
		
		Expressiveness: 
			By explicitly passing just the necessary data we improve the value of our unit tests as a form of documentation. 
			Just by looking at the test you can determine what the method does.
		
		Flexibility: 
			By decoupling the test from the constructor, we made sure that future changes won’t break our existing unit tests. 
			This is important for maintenance reasons (you don’t want to go in and change a lot of tests, because of some code changes).
		
		Reliability: 
			Because our unit test is flexible towards changes, you won’t have to modify it often. 
			In general, a unit test gets more reliable when it matures. 
			To imagine this, you can compare the effect of a failing unit test that you just wrote over one that was written months ago. 
			A recently written unit test that fails can have a lot of reasons (an error in the test, some code that is not written yet, etc.). 
			On the other hand, a test that has been working for months but suddenly fails is more concerning and will most definitely indicate a problem with new code.
		
		