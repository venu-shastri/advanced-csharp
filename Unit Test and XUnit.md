### Unit Test

---

- Test specific aspects of a functionality
- Execute rapidly
- Independent of each other
- Independent of the surrounding environment
- Independent of execution order
- Automated

#### Value-based vs. state-based vs. interaction testing

---

- Value-based testing checks the value returned from a function
- State-based testing is about checking for noticeable behavior changes in the system under test, after changing its state.
- Interaction testing is testing how an object sends messages (calls methods) to other objects



#### External Dependency and Unit Test

---

> An external dependency is an object in your system that your code under test interacts with and over which you have no control. (Common examples are file systems, threads, memory, time, and so on.)

#### Inherent challenges in testing dependent objects

---

- Objects dependent on ephemeral objects produce unpredictable behavior

- User Interfaces, Databases and the like are inherently ephemeral

- How do you test that your object interacts with other objects correctly?

  

##### Stub

----

**A** **stub is a controllable replacement for an existing dependency** **(or** **collaborator) in the system. By using a stub, you can test your code without** **dealing with the dependency directly**

##### Mock

---

Complex collaborator interactions cannot be captured with a simple stub 

Defining interaction testing using  Mock Object

> A Mock Object is a fake dependency or stub  that verifies that the code being tested interacts with its collaborator properly.
>
> A mock object is a fake object in the system that decides whether the unit test has passed or failed. It does so by verifying whether the object under test called the fake object as expected. **There’s usually no more than one mock per test**.
>
> - The test tells the mock
>   - The expected calls
>   -  In the expected order
>   - What to return



###  TDD

---

> you take the input tape,manually type in the output tape you expect, then program until the actual output tape matches the
> expected output.

- Is a programming practice

- Unit tests are written before the domain code
- Write a test that fails
- Write code to make the test pass
- Refactor
- Unit Tests and Refactoring are the tools of TDD



### The Benefits of TDD

---

- TDD ensures quality code from the start. Developers are encouraged to write only the code needed to make the test pass and thus fulfill the requirement. 
- Whether by design or by coincidence, most TDD practitioners write code that follows the SOLID principals
- TDD ensures a high degree of fidelity between the code and the business requirements.
- If your requirements are written as tests, and your tests all pass, you can say with a high degree of confidence that your code meets the needs of the business
- TDD encourages the creation of simpler, more focused libraries and APIs
- TDD encourages communication with the business. To create these tests, you are encouraged to interact with the business users. This way, you can make sure that the input and output combinations make sense, and you can help the users understand what they are building.
- TDD helps keep unused code out of the system. Most developers have written applications in which they designed interfaces and wrote methods based on what might happen. This leads to systems with large parts of code or functionality that are never used. This code is expensive. TDD helps keep this parasite code out of your system.
- TDD provides built - in regression testing. As changes are made to the system and your code, you always have the suite of tests you created to ensure that tomorrow ’ s changes do not damage today ’ s functionality.
- TDD puts a stop to recurring bugs, With TDD, as soon as a defect is reported, a new test is written to expose it. When this test passes, and continues to pass, you know the defect is gone for good
- When developing applications with testability in mind, the result is an architecture that is open, extensible and flexible. Dependency Injection  is a key component of both TDD and a loosely coupled architecture

### The Rules of TDD

---

- You are not allowed to write any production code until you have first written a failing unit test.
- You are not allowed to write more of a unit test than is sufficient to fail – and not compiling is failing
- You are not allowed to write more production code than is sufficient to pass the currently failing unit test

### TDD Steps

---

- **Write** a test.

- **Run** the test. It **fails to compile** because the code you're trying to test doesn't even exist yet! (This is the same thing as failing.)

- **Write a bare-bones stub** to make the **test compile**.

- Run the test. It should **fail**. (If it doesn't, then the test wasn't very good.)

- **Implement the code** to make the test pass.

- Run the test. It should **pass.** (If it doesn't, back up one step and try again.)

- Start over with a new test!

### When We Should Not Use TDD

---

- When Requirements are Volatile and Fuzzy
- When some parts of project are so simple, small, or less complex where complexity and risks are low
- TDD won’t help in getting a good architecture
  - If you are faced with fundamental decisions about frameworks, libraries, technologies, or architecture patterns, TDD will not help you
- The project is short, simple and straight-forward

### Why Developers Don’t Use TDD

---

- "Management Doesn’t Allow Us”
-  “Not Enough Time to Write Tests”
- “My Team Disagrees on Whether We Should Use TDD"
- “Return on Investment in TDD Isn’t Proven”
- “We Tried but It Didn’t Work”
- “Slow Build Process”
- “I Don’t Know All the Requirements Upfront to Write All Tests”



### How to Succeed With TDD

---

- Make each test very small and focused.
- Avoid making tests depend on each other, either explicitly or implicitly
- Make each test express its intent very clearly.
- Pay attention to failure messages. Make each failure message as helpful for diagnosis as you can.
- Involve as little of the system as you possibly can in each test

### TDD - Constraints

---

- Development speed - Steady Pace
  - when the target for test execution is not the same as the host for developing software, a delay is introduced into development
- Cross-compilation issues
  - Difference in processor architecture or build tool chain
- Hardware dependencies
  - External dependencies, like hardware interaction, complicate the automation of tests. Concurrent Hardware





###  Writing untestable code

----

###  Tight coupling



```C#
 public  class LoanProcessor
    {
        private  EligibilityChecker eligibilityChecker = new EligibilityChecker();

        public  double CalculateCarLoanRate(Loan loan)
        {
            //
        }
    }
```



#### Monster Constructor

```C#
public static class LoanProcessor
    {
        private  EligibilityChecker eligibilityChecker;
        private CurrencyConverter currencyConverter;

        public LoanProcessor(){
             eligibilityChecker=new EligibilityChecker();
             currencyConverter=new CurrencyConverter();
            currencyConverter.DownloadCurrentRates();
            eligibilityChecker.CurrentRate=currencyConverter.Rates;
             
        }
        public  double CalculateCarLoanRate(Loan loan)
        {
            ///
        }
    }
```



#### SRP Violation

```C#
//class has a mix of both methods with code for data retrieval and business rules processing
public class LoanProcessor
    {
        private EligibilityChecker eligibilityChecker;
        private DbContext dbContext;

        public LoanProcessor(EligibilityChecker eligibilityChecker, DbContext dbContext)
        {
            this.eligibilityChecker = eligibilityChecker;
            this.dbContext = dbContext;
        }

        public double CalculateCarLoanRate(Loan loan)
        {
            double rate = 12.5F;
            bool isEligible = eligibilityChecker.IsApplicantEligible(loan);
            if (isEligible)
                rate = rate - loan.DiscountFactor;
            return rate;
        }

        public List<CarLoan> GetCarLoans()
        {
            return dbContext.CarLoan;
        }
    }
```

#### Static objects

----

>In testing code containing statics, especially static methods, can create testing issues because you cannot override static methods in a subclass and using mocking frameworks to mock static methods is a very daunting task:

```
public static class LoanProcessor
    {
        private static  EligibilityChecker eligibilityChecker=new EligibilityChecker() ;
      

        public static double CalculateCarLoanRate(Loan loan)
        {
            double rate = 12.5F;
            bool isEligible = eligibilityChecker.IsApplicantEligible(loan);
            if (isEligible)
                rate = rate - loan.DiscountFactor;
            return rate;
        }

       
    }
```

#### Law of Demeter (principle of least knowledge)

---

- Each unit should have only limited knowledge about other units: only
  units "closely" related to the current unit.

- Each unit should only talk to its friends; don't talk to strangers.

- #### Rules

  - A member function is allowed to call other member functions in its own class scope directly.
  - A member function is allowed to call member functions on member variables that are in its class scope directly.
  - If a member function has parameters, the member function is allowed to call the member functions of these parameters directly.
  - If a member function creates local objects, the member function is allowed to call member functions on those local objects. Object form of Law of Demeter (DemeterW3; Martin, 2003)

- ### Simplified (Martin)

  A method m of a class C, can only call methods of:
  - C
  - An object passed as an argument to m.
  - An object created by m.
  - An object held in an instance variable of C



### Using the xUnit test framework

---

- Tests are methods decorated with either with the **[Fact]** or with the **[Theory]** attributes

- The **[Fact]** attribute is used when a method defines just one test

- ```C#
  [Fact]
  public void Test1()
  {
  var myInstanceToTest = new ClassToTest();
  Assert.Equal(5, myInstanceToTest.MethodToTest(1));
  }
  ```

  

- The **[Theory]** attribute is used when the same method defines several tests, each on a different tuple of data (**Data Driven Tests**). 

- **Tuples** of data can be specified in several ways and are injected in the test as method parameters.

- ```C#
  [Theory]
  /* Each InlineData attribute specifies a tuple to be injected in the method parameters*/
  [InlineData(1, 5)] 
  [InlineData(3, 10)]
  [InlineData(5, 20)]
  public void Test1(int testInput, int testOutput)
  {
      var myInstanceToTest = new ClassToTest();
      Assert.Equal(testOutput,myInstanceToTest.MethodToTest(testInput);
  }
               
  //xUnit gives you also the possibility to take all data tuples from a class that implements IEnumerable
  public class Test1Data: IEnumerable<object[]>
  {
      public IEnumerator<object[]> GetEnumerator()
      {
          yield return new object[] { 1, 5};
          yield return new object[] { 3, 10 };
          yield return new object[] { 5, 20 };
      }
      IEnumerator IEnumerable.GetEnumerator()
      {
              return GetEnumerator();
      }
  }
  ...
  ...
  [Theory]
   //The type of the class that provides the test data is specified with the ClassData attribute.
  [ClassData(typeof(Test1Data))]
  public void Test1(int testInput, int testOutput)
  {
      var myInstanceToTest = new ClassToTest();
      Assert.Equal(testOutput,
      myInstanceToTest.MethodToTest(testInput);
  }
  
  /* Similar to the ClassData attribute available in the xUnit.net,We can create a custom attribute to load data from any source. A custom attribute class must extend DataAttribute, which is an abstract class that represents a data source to be used by a theory. The custom attribute class must override and implement the GetData method. This method returns IEnumerable<object[]>, which is used to wrap the content of the dataset to be returned */
      [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
      public class CsvDataAttribute : DataAttribute
      {
          private readonly string filePath;
          private readonly bool hasHeaders;
          public CsvDataAttribute(string filePath, bool hasHeaders)
          {
              this.filePath = filePath;
              this.hasHeaders = hasHeaders;
          }
  
          public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
          {
              var methodParameters = methodInfo.GetParameters();
              var parameterTypes = methodParameters.Select(x => x.ParameterType).ToArray();
              using (var streamReader = new StreamReader(filePath))
              {
                  if(hasHeaders)
                      streamReader.ReadLine();
                  string csvLine=string.Empty;
                  while ((csvLine = streamReader.ReadLine()) != null)
                  {
                      var csvRow = csvLine.Split(',');
                      yield return ConvertCsv((object[])csvRow, parameterTypes);
                  }
              }
          }
  
          private static object[] ConvertCsv(IReadOnlyList<object> csvRow, IReadOnlyList<Type> parameterTypes)
          {
              var convertedObject = new object[parameterTypes.Count];
              //convert object if integer
              for (int i = 0; i < parameterTypes.Count; i++)
                  convertedObject[i] = (parameterTypes[i] == typeof(int)) ? Convert.ToInt32(csvRow[i]) : csvRow[i]; 
              return convertedObject;
          }
          
      }
                   
                   
      //Test Code
       public class PalindromeCheckerTest
      {
          
          [Theory, CsvData(@"C:\data.csv", false)]
          public void Test_IsWordPalindrome_ShouldReturnTrue(string word)
          {
              PalindromeChecker palindromeChecker = new PalindromeChecker();
              Assert.True(palindromeChecker.IsWordPalindrome(word));
          }
      }
  
  ```

  

- **Test preparation** and **tear-down scenarios**

  - Use Constructor : A new instance of the test class is created before running each test, so the test preparation code contained in the class constructor is executed before each test of the class. If you also need tear-down code, the test class must implement the IDisposable interface so that the tear-down code can be included in the IDisposable.Dispose method
  - Use Fixture Class : Sometimes the preparation code contains very time-consuming operations, such as opening a connection with a database, that **don't need to be repeated before each test** but can be **executed once** before all the tests contained in the same class. In **xUnit**, this kind of test preparation code can't be included in the test class constructor; since a different instance of the test class is created before every single test, it must be factored out in a separate class called a **fixture** class

```C#
public class DatabaseFixture : IDisposable
{
    public DatabaseFixture()
    {
    Db = new SqlConnection("MyConnectionString");
    }
    public void Dispose()
    {
    Db.Close()
    }
    public SqlConnection Db { get; private set; }
}

/* The fixture class is associated with each test class by letting the test class implement the
Marker interface IClassFixture<T> interface */

public class MyTestsClass : IClassFixture<DatabaseFixture>
{
	DatabaseFixture fixture;
	public MyDatabaseTests(DatabaseFixture fixture)
	{
		this.fixture = fixture;
	}
...
...
}
```



- **Mock** Dependencies Using **Moq**

  - The Moq framework available in the Moq NuGet package

    ```C#
    using Moq;
    public class MyTestClass{
     
     [Fact]
     public void MyTestMethod(){
    	 var myMockDependency = new Mock<IMyInterface>();
         /*
         The behavior of the mock dependency on specific input of the specific method can be defined with 	   the Setup/Return method pair as follows
         */
         myMockDependency.Setup(x=>x.MyMethod(5)).Returns(10);
         myMockDependency.Setup(x => x.MyMethod(It.IsAny<int>)).Returns(10);
        /* Once configured the mock dependency we may extract the mocked instance from its Object
           property and use it as if it were an actual implementation */
         var myMockedInstance=myMockDependency.Object;
         //Verify The Interaction Test 
         myMockDependency.Verify(x => x.MyMethod(1), Times.AtLeast(2))
     }
    
    }
    
    ```

    

### Unit testing consideration for ASP.NET MVC Core

____

##### Unit Testing Middleware

----

[Test ASP.NET Core middleware](https://docs.microsoft.com/en-us/aspnet/core/test/middleware?view=aspnetcore-5.0)

##### Unit Testing Controllers

---

[Test controller logic in ASP.NET Core ](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing?view=aspnetcore-5.0)
