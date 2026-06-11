using System.Collections.ObjectModel;
using System.IO.Compression;
using System.Xml.Linq;

namespace FlashCards.Data;

public class FlashCardDeckService
{
    private readonly List<FlashCard> _cards = new()
    {
        new("What is C#?", "C# is a modern, object-oriented, type-safe programming language developed by Microsoft as part of the .NET platform.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is an object?", "An object is an instance of a class, carrying state (fields) and behavior (methods) in memory.", "OOP", "Use the definition and the common use case."),
        new("What is the difference between continue and break statements in C#?", "break exits the enclosing loop or switch immediately, while continue skips the rest of the current iteration and proceeds to the next one.", "C# Fundamentals", "Compare the two concepts and remember when each one is used."),
        new("What are property accessors?", "Property accessors are the get and set blocks that control reading and writing of a property value, allowing encapsulation and validation.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is the difference between a struct and a class in C#?", "Structs are value types stored on the stack (or inline) and copied by value, while classes are reference types stored on the heap and passed by reference.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("What is an abstract class?", "An abstract class is a base class that cannot be instantiated directly and can contain abstract members that derived classes must implement.", "OOP", "Use the definition and the common use case."),
        new("What is a namespace in C#?", "A namespace organizes code into logical groups and helps avoid naming conflicts across types.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("What are nullable types in C#?", "Nullable types allow value types such as int, bool, and DateTime to represent both a valid value and null.", "Collections & Language Features", "Use the definition and the common use case."),
        new("How is exception handling implemented in C#?", "Exception handling uses try, catch, finally, and throw to detect errors, handle them, and clean up resources.", "Exceptions & Resources", "Think through the execution flow or usage pattern."),
        new("What is the difference between value types and reference types in .NET?", "Value types store their data directly and are copied by value, while reference types store a reference to an object on the heap and share identity.", "Core .NET Concepts", "Recall the core behavior and practical use."),
        new("What is serialization?", "Serialization converts an object into a stream or text format so it can be persisted, transmitted, or reconstructed later.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("What are the different types of classes in C#?", "C# supports concrete classes, abstract classes, sealed classes, static classes, partial classes, and nested classes, each with different behaviors.", "OOP", "Use the definition and the common use case."),
        new("What is the difference between string and StringBuilder in C#?", "string is immutable and creates new instances on changes, while StringBuilder is mutable and efficient for repeated text modifications.", "C# Fundamentals", "Compare the two concepts and remember when each one is used."),
        new("What is LINQ in C#?", "LINQ is Language Integrated Query, which lets you query collections, XML, and databases using C# syntax and operators.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What are reference types in C#?", "Reference types store references to objects allocated on the managed heap, including class, interface, delegate, array, and string types.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("What is the difference between managed and unmanaged code?", "Managed code runs under the .NET runtime, which provides memory management, type safety, and garbage collection; unmanaged code runs outside that runtime control.", "C# Fundamentals", "Use the definition and the common use case."),
        new("Can multiple catch blocks be executed?", "No. Only the first matching catch block executes, and execution continues after the catch chain.", "Exceptions & Resources", "Recall the core behavior and practical use."),
        new("Can this be used inside a static method?", "No, this refers to the current instance, so it cannot be used inside a static method because there is no instance.", "C# Fundamentals", "Recall the core behavior and practical use."),
        new("What are partial classes?", "Partial classes allow a class definition to be split across multiple files, which is useful for code generation and organization.", "OOP", "Use the definition and the common use case."),
        new("Why use a finally block in C#?", "finally executes regardless of whether an exception was thrown, making it ideal for cleanup and resource release.", "Exceptions & Resources", "Tie the concept to a real scenario."),
        new("What are generics in C#?", "Generics allow reusable type-safe classes, methods, interfaces, and delegates that work with any data type.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What are dynamic type variables in C#?", "dynamic variables defer type checking until runtime, allowing late-bound operations that the compiler cannot validate.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What are boxing and unboxing?", "Boxing converts a value type to an object, and unboxing converts that boxed object back to the original value type.", "Runtime & Concurrency", "Use the definition and the common use case."),
        new("What is an enum in C#?", "An enum is a named set of constants backed by an integral type that improves readability and type safety.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What are the ways to pass parameters to a method?", "Parameters can be passed by value, by reference with ref, by reference with out, and as optional/default or params arrays, depending on the method design.", "C# Fundamentals", "Think through the execution flow or usage pattern."),
        new("What is the difference between a class and a structure?", "Classes are reference types with inheritance and heap allocation; structures are value types, copied by value, and do not support inheritance.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("What is an anonymous type in C#?", "Anonymous types let you create read-only, inferred objects without explicitly defining a class, commonly used in LINQ projections.", "C# Fundamentals", "Recall the core behavior and practical use."),
        new("How does code compilation work in C#?", "C# source is compiled by the Roslyn compiler into IL, which is then JIT-compiled into native machine code when the app runs.", "C# Fundamentals", "Recall the core behavior and practical use."),
        new("What is the scope of an internal member in a C# class?", "Internal members are visible only within the same assembly, not outside of it.", "OOP", "Use the definition and the common use case."),
        new("What is the difference between ref and out keywords?", "ref passes an existing variable into a method and expects it to be initialized, while out passes an uninitialized variable and the method must assign it before returning.", "C# Fundamentals", "Compare the two concepts and remember when each one is used."),
        new("What is a virtual method in C#?", "A virtual method is designed to be overridden in derived classes, enabling polymorphic behavior.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is the difference between dynamic type variables and object type variables?", "object is statically typed and requires casting, while dynamic uses runtime binding and lets the compiler postpone type checks.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("What is the difference between const and readonly?", "const values are compile-time constants and must be initialized at declaration, while readonly values are assigned at declaration or in a constructor and can vary per instance.", "C# Fundamentals", "Compare the two concepts and remember when each one is used."),
        new("Is there a difference between throw and throw ex?", "throw rethrows the current exception while preserving the original stack trace; throw ex resets the stack trace to the current location.", "Exceptions & Resources", "Compare the two concepts and remember when each one is used."),
        new("What is the difference between an interface and an abstract class?", "Interfaces define contracts with no implementation, while abstract classes can provide shared implementation and state but cannot be instantiated.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("What is the difference between the equality operator (==) and the Equals() method in C#?", "== compares object identity or overloaded operator logic, while Equals() calls virtual/object equality semantics, often overridden for value-based comparison.", "C# Fundamentals", "Compare the two concepts and remember when each one is used."),
        new("What are the uses of using in C#?", "using imports namespaces, creates scope-limited resource disposal blocks, and ensures disposable objects are cleaned up.", "Exceptions & Resources", "Use the definition and the common use case."),
        new("What is the difference between a virtual method and an abstract method?", "A virtual method provides a default implementation that may be overridden, while an abstract method has no implementation and must be overridden.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("What is an extension method in C#, and how do you use one?", "Extension methods add instance-like methods to existing types without modifying them, typically as static methods in static classes.", "C# Fundamentals", "Use the definition and the common use case."),
        new("Why can't you specify an accessibility modifier for methods inside an interface?", "Interface members are implicitly public, and the language does not allow redefining accessibility because all implementers must see them as public contracts.", "OOP", "Tie the concept to a real scenario."),
        new("What is an anonymous function in C#?", "An anonymous function is an unnamed method written as a lambda expression or anonymous delegate that can be passed around as a value.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What is a sealed class in C#?", "A sealed class cannot be inherited, preventing further derivation.", "OOP", "Use the definition and the common use case."),
        new("What is reflection in C#/.NET?", "Reflection inspects and examines metadata, types, members, attributes, and runtime information in assemblies.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is a destructor in C#, and when should you create one?", "A destructor is a finalizer used to release unmanaged resources before an object is collected, typically only when unmanaged resources need cleanup.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("How is encapsulation implemented in C#?", "Encapsulation hides internal state behind properties and methods, exposing only the necessary interface and enforcing validation.", "OOP", "Think through the execution flow or usage pattern."),
        new("What are lambda expressions in C#?", "Lambda expressions provide concise inline function syntax used with LINQ, delegates, and functional patterns.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What is the difference between overloading and overriding?", "Overloading defines multiple methods with the same name but different parameters, while overriding changes a base virtual method implementation in a derived class.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("What is the null-coalescing operator (??) used for in C#?", "The ?? operator returns the left operand if it is not null, otherwise returns the right operand.", "C# Fundamentals", "Use the definition and the common use case."),
        new("How can you prevent a class from being inherited in C#?", "Mark the class as sealed or mark specific methods as non-virtual to prevent overriding.", "OOP", "Think through the execution flow or usage pattern."),
        new("Is there a way to catch multiple exceptions at once and without code duplication?", "Yes, catch multiple exception types in one catch block by using a base class or by catching AggregateException in async scenarios.", "Exceptions & Resources", "Recall the core behavior and practical use."),
        new("Explain the difference between Task and Thread in .NET", "A Thread is an OS-level execution path, while Task is a higher-level abstraction that schedules work and integrates with async patterns.", "Runtime & Concurrency", "Compare the two concepts and remember when each one is used."),
        new("What is the use of the IDisposable interface?", "IDisposable defines Dispose() so types can release unmanaged resources and deterministic cleanup.", "OOP", "Use the definition and the common use case."),
        new("What is a record in C#?", "A record is a reference type optimized for immutable data modeling, with value-based equality and concise syntax.", "Collections & Language Features", "Use the definition and the common use case."),
        new("When should you use a record, class, or struct in C#?", "Use records for immutable data models, classes for rich behavior and inheritance, and structs for small value objects.", "OOP", "Tie the concept to a real scenario."),
        new("What is the difference between assignment, shallow copy, and deep copy for a record in C#?", "Assignment copies references, shallow copy duplicates top-level values and shared nested references, and deep copy duplicates nested object graphs.", "Collections & Language Features", "Recall the core behavior and practical use."),
        new("How do you test whether a number belongs to the Fibonacci series?", "A number belongs to the Fibonacci series if it satisfies the property that one of the generated Fibonacci values is equal to it or can be detected via the 5n^2 +/- 4 test.", "C# Fundamentals", "Recall the core behavior and practical use."),
        new("What is ternary search?", "Ternary search divides a sorted unimodal array into three parts and recursively searches the appropriate interval to find an element or minimum.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What are pointer types in C#?", "Pointer types allow unsafe code to manipulate memory addresses directly, typically using unsafe blocks and the * operator.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("Can you create a function in C# that accepts a variable number of arguments?", "Yes, use params arrays or optional parameters to accept a variable number of arguments.", "Collections & Language Features", "Recall the core behavior and practical use."),
        new("What is the difference between late binding and early binding in C#?", "Early binding resolves types at compile time and enables IntelliSense, while late binding resolves types at runtime using reflection or dynamic.", "C# Fundamentals", "Compare the two concepts and remember when each one is used."),
        new("What is the difference between is and as operators in C#?", "is checks type compatibility and returns a bool, while as performs safe casting and returns null if the cast fails.", "C# Fundamentals", "Compare the two concepts and remember when each one is used."),
        new("What is the scope of a protected internal member in a C# class?", "A protected internal member is accessible within the same assembly and to derived types outside the assembly.", "OOP", "Use the definition and the common use case."),
        new("Can multiple inheritance be implemented in C#?", "C# does not support multiple inheritance of classes, but classes can implement multiple interfaces.", "OOP", "Recall the core behavior and practical use."),
        new("Is operator overloading supported in C#?", "Yes, C# allows operator overloading for selected operators, enabling custom operator behavior on types.", "OOP", "Recall the core behavior and practical use."),
        new("What interface should your data structure implement to make the Where method work?", "The data structure should implement IEnumerable<T> so LINQ extension methods like Where can enumerate it.", "OOP", "Recall the core behavior and practical use."),
        new("What is marshalling, and why do we need it?", "Marshalling converts managed data to unmanaged data formats and vice versa so interop with native APIs works correctly.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("What is the difference between System.ApplicationException and System.SystemException?", "SystemException is reserved for runtime failures, while ApplicationException is intended for application-defined exceptions, though modern guidance often uses custom exceptions directly.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("Why use the lock statement in C#?", "lock serializes access to shared resources, preventing race conditions in multithreaded code.", "C# Fundamentals", "Tie the concept to a real scenario."),
        new("What are the different ways a method can be overloaded?", "A method can be overloaded by changing the number of parameters, parameter types, or parameter order.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is the yield keyword used for in C#?", "yield creates an iterator, allowing a method to return values lazily one at a time while preserving state across iterations.", "C# Fundamentals", "Use the definition and the common use case."),
        new("When should you use IEnumerable instead of List, and how do they work?", "IEnumerable is a read-only forward-only enumeration contract, while List is a concrete mutable collection that implements IEnumerable and provides indexing, adding, and removing.", "Collections & Language Features", "Think through the execution flow or usage pattern."),
        new("When should you use ArrayList instead of an array in C#?", "ArrayList is useful when you need a dynamically sized collection of objects without generics, but generics are preferred in modern C#.", "Core .NET Concepts", "Tie the concept to a real scenario."),
        new("What are conditional preprocessor directives used for in C#?", "Preprocessor directives like #if and #define enable conditional compilation and platform-specific code paths.", "C# Fundamentals", "Use the definition and the common use case."),
        new("When would you use delegates in C#?", "Delegates are used to pass methods as arguments, implement callbacks, and support event patterns.", "Collections & Language Features", "Tie the concept to a real scenario."),
        new("What is constructor chaining in C#?", "Constructor chaining calls one constructor from another using this(...) or base(...) to reuse initialization logic.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("What's the difference between StackOverflowError and OutOfMemoryError?", "StackOverflowError occurs when the stack grows beyond its limit, while OutOfMemoryError indicates the managed or native memory pool has been exhausted.", "Runtime & Concurrency", "Compare the two concepts and remember when each one is used."),
        new("What is the difference between dispose and finalize methods in C#?", "Dispose is explicit deterministic cleanup, while finalize is nondeterministic cleanup performed by the GC for unmanaged resources.", "Runtime & Concurrency", "Compare the two concepts and remember when each one is used."),
        new("What is an indexer in C#?", "An indexer lets an object be accessed like an array using index syntax, often over a collection or internal storage.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is the difference between Func<string,string> and delegate?", "Func<string,string> is a predefined generic delegate for methods returning a value, while a custom delegate defines a specific method signature.", "Collections & Language Features", "Compare the two concepts and remember when each one is used."),
        new("What is short-circuit evaluation in C#?", "Short-circuit evaluation stops evaluating boolean expressions as soon as the result is known, using && and ||.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is the difference between Select and Where?", "Where filters elements, while Select projects each element into a new shape or value.", "C# Fundamentals", "Compare the two concepts and remember when each one is used."),
        new("What is the best practice for performance when using Lazy<T> objects?", "Use Lazy<T> when initialization is expensive and you want deferred, thread-safe creation only when the value is actually needed.", "OOP", "Use the definition and the common use case."),
        new("What is a static constructor?", "A static constructor runs once per type before the first instance or static member access, and it initializes static state.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("How do async and await work in .NET?", "async/await turns asynchronous methods into state machines, letting code continue later without blocking the thread while awaiting I/O or long-running work.", "Runtime & Concurrency", "Think through the execution flow or usage pattern."),
        new("What happens when you box or unbox nullable types?", "Boxing a nullable value type boxes the underlying value if it has a value, otherwise it boxes null; unboxing returns the nullable value or throws if the boxed value is not compatible.", "Collections & Language Features", "Tie the concept to a real scenario."),
        new("What is the difference between an interface, abstract class, sealed class, static class, and partial class in C#?", "Interfaces define contracts, abstract classes provide partial implementation, sealed classes cannot be inherited, static classes hold only static members, and partial classes split a type across files.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("How do you resolve circular references?", "Break the reference cycle by redesigning ownership, using weak references, or removing mutual dependencies so objects can be collected.", "C# Fundamentals", "Think through the execution flow or usage pattern."),
        new("What is a multicast delegate in C#?", "A multicast delegate holds multiple method targets and invokes them in order when called.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What is a jagged array in C#, and when should you prefer one over a multidimensional array?", "A jagged array is an array of arrays, which is useful when each inner array may have a different length. A multidimensional array is better for rectangular data with fixed row and column sizes.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("Why can't an abstract class be sealed or static?", "Abstract classes are meant to be inherited, so sealing or making them static would conflict with that purpose.", "OOP", "Tie the concept to a real scenario."),
        new("What are the benefits of deferred execution in LINQ?", "Deferred execution reduces work until enumeration, allows streaming, and lets queries compose efficiently.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What is the difference between a destructor, Dispose, and Finalize?", "A destructor in C# is a finalizer pattern, dispose is deterministic cleanup, and finalize is GC-triggered cleanup for unmanaged resources.", "Runtime & Concurrency", "Compare the two concepts and remember when each one is used."),
        new("What are the differences between IEnumerable and IQueryable?", "IEnumerable operates in memory, while IQueryable supports provider translation for remote sources like databases.", "Collections & Language Features", "Use the definition and the common use case."),
        new("What is the difference between Lambdas and Delegates?", "Lambdas are inline anonymous functions, while delegates are the type-safe function pointers used to represent method signatures.", "Collections & Language Features", "Compare the two concepts and remember when each one is used."),
        new("What does the MemberwiseClone() method do?", "MemberwiseClone creates a shallow copy of an object by copying field values, not deep-copying nested references.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What's the difference between the System.Array.CopyTo() and System.Array.Clone()?", "CopyTo copies elements into an existing array, while Clone creates a new array with copied elements.", "Core .NET Concepts", "Compare the two concepts and remember when each one is used."),
        new("What are circular references in C#?", "Circular references are object graphs that reference each other, which can complicate lifetime management and garbage collection unless broken or weak referenced.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is the difference between deep copy and shallow copy in C#?", "Shallow copy duplicates references, while deep copy duplicates the entire object graph recursively.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What are some ways to check equality in .NET?", "Use ==, Equals(), object.ReferenceEquals, IEquatable<T>, and custom equality members depending on the type semantics.", "C# Fundamentals", "Recall the core behavior and practical use."),
        new("What are preprocessor directives in C#?", "Preprocessor directives control compilation symbols, conditional compilation, and include behavior before the compiler processes the code.", "C# Fundamentals", "Use the definition and the common use case."),
        new("What is the use of static constructors?", "Static constructors initialize static data exactly once before first use of the type.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("What is the volatile keyword used for?", "volatile tells the compiler and processor not to optimize away reads/writes, ensuring visibility across threads for simple state flags.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("What is a weak reference in C#?", "A weak reference allows access to an object without preventing it from being garbage collected, useful for caches and memory-sensitive scenarios.", "Core .NET Concepts", "Use the definition and the common use case."),
        new("What is the difference between Func, Action, and Predicate?", "Func represents methods returning a value, Action represents methods returning void, and Predicate represents boolean-returning methods used for filtering.", "Collections & Language Features", "Compare the two concepts and remember when each one is used."),
        new("Can you add extension methods to an existing static class?", "No, extension methods must be defined in a static class, but they extend types, not existing static classes themselves.", "OOP", "Recall the core behavior and practical use."),
        new("In C#, when should you use abstract classes instead of interfaces with extension methods?", "Use abstract classes when you need shared implementation, state, or base-class behavior; use interfaces when you need flexible contracts and multiple inheritance of behavior.", "OOP", "Tie the concept to a real scenario."),
        new("Implement the Where method in C#. Explain.", "Where filters a sequence using a predicate and returns the matching elements, commonly implemented with deferred execution over an iterator.", "C# Fundamentals", "Recall the core behavior and practical use."),
        new("What is the difference between IQueryable, ICollection, IList, and IDictionary?", "IQueryable supports provider-backed queries, ICollection defines collection semantics, IList provides index-based access, and IDictionary manages key/value pairs.", "OOP", "Compare the two concepts and remember when each one is used."),
        new("You defined a destructor in a C# class, but it never executed. Why?", "In C#, destructors are finalizers and are not guaranteed to run promptly because GC decides when to collect; they may never run before process exit, and deterministic cleanup should use Dispose().", "OOP", "Tie the concept to a real scenario."),
        new("Why doesn't C# allow static methods to implement an interface?", "Interface implementation requires instance behavior tied to an object, while static methods belong to the type and cannot satisfy instance contracts.", "OOP", "Tie the concept to a real scenario."),
        new("When should you use Finalize instead of Dispose?", "Use Dispose for deterministic cleanup of managed and unmanaged resources; use Finalize only as a backup for unmanaged resources when Dispose is not called.", "Runtime & Concurrency", "Tie the concept to a real scenario."),
    };

    private readonly List<FlashCard> _studyPile = new();
    private readonly List<string> _topicOrder = new();
    private readonly Dictionary<string, List<int>> _topicIndices = new();
    private readonly List<string> _topicOptions = new();

    private List<FlashCard> _currentDeck = new();
    private readonly List<CodingPracticeCard> _codingDemoCards = new();
    private readonly List<CodingPracticeCard> _codingStudyPile = new();
    private readonly List<string> _codingTopicOrder = new();
    private readonly Dictionary<string, List<int>> _codingTopicIndices = new();
    private readonly List<string> _codingTopicOptions = new();
    private readonly List<CodingPatternGuide> _codingPatternGuides = new();
    private readonly List<string> _parsedCodingQuestions = new();
    private List<CodingPracticeCard> _currentCodingDeck = new();
    private int _currentIndex;
    private int _codingIndex;
    private int _studyPileIndex;
    private int _codingStudyPileIndex;
    private bool _reviewingStudyPile;
    private bool _reviewingCodingStudyPile;
    private bool _isShuffled;
    private bool _isCodingShuffled;
    private bool _codingModeActive;
    private string _selectedTopic = "All cards";
    private string _selectedCodingTopic = "All prompts";
    private string _selectedCodingPattern = string.Empty;

    public FlashCardDeckService()
    {
        BuildTopicIndex();
        ResetDeckState();
        LoadCodingPracticeCards();
    }

    public ReadOnlyCollection<FlashCard> Cards => _cards.AsReadOnly();

    public IReadOnlyList<string> Topics => _topicOptions.AsReadOnly();

    public string SelectedTopic => _selectedTopic;

    public string OrderSummary => _isShuffled ? "Shuffled" : "In order";

    public int TotalCards => _reviewingStudyPile && _studyPile.Count > 0 ? _studyPile.Count : _currentDeck.Count;

    public IReadOnlyList<FlashCard> StudyPile => _studyPile.AsReadOnly();

    public int StudyPileCount => _studyPile.Count;

    public bool IsReviewingStudyPile => _reviewingStudyPile;

    public string StudyModeLabel => _reviewingStudyPile ? "Study review" : "Main deck";

    public bool IsFlipped { get; private set; }

    public FlashCard CurrentCard => _reviewingStudyPile && _studyPile.Count > 0 ? _studyPile[_studyPileIndex] : _currentDeck[_currentIndex];

    public int CurrentIndex => _reviewingStudyPile && _studyPile.Count > 0 ? _studyPileIndex : _currentIndex;

    public string CurrentTopicBadge => GetTopicLabel(CurrentCard);

    public IReadOnlyList<CodingPracticeCard> CodingCards => _codingDemoCards.AsReadOnly();

    public IReadOnlyList<string> CodingTopics => _codingTopicOptions.AsReadOnly();

    public string SelectedCodingTopic => _selectedCodingTopic;

    public string CodingOrderSummary => _isCodingShuffled ? "Shuffled" : "In order";

    public IReadOnlyList<CodingPatternGuide> CodingPatternGuides => _codingPatternGuides.AsReadOnly();

    public string SelectedCodingPattern => _selectedCodingPattern;

    public CodingPatternGuide? CurrentCodingPatternGuide => _codingPatternGuides.FirstOrDefault(pattern => pattern.Name == _selectedCodingPattern)
        ?? _codingPatternGuides.FirstOrDefault();

    public int CodingCardCount => _reviewingCodingStudyPile && _codingStudyPile.Count > 0 ? _codingStudyPile.Count : _currentCodingDeck.Count;

    public IReadOnlyList<CodingPracticeCard> CodingStudyPile => _codingStudyPile.AsReadOnly();

    public int CodingStudyPileCount => _codingStudyPile.Count;

    public bool IsReviewingCodingStudyPile => _reviewingCodingStudyPile;

    public string CodingStudyModeLabel => _reviewingCodingStudyPile ? "Study review" : "Main deck";

    public int ParsedCodingQuestionCount => _parsedCodingQuestions.Count;

    public bool IsCodingMode => _codingModeActive;

    public CodingPracticeCard CurrentCodingCard => _reviewingCodingStudyPile && _codingStudyPile.Count > 0 ? _codingStudyPile[_codingStudyPileIndex] : _currentCodingDeck[_codingIndex];

    public string Progress => $"{CurrentIndex + 1} of {TotalCards}";

    public int CurrentCodingIndex => _reviewingCodingStudyPile && _codingStudyPile.Count > 0 ? _codingStudyPileIndex : _codingIndex;

    public string CodingProgress => $"{CurrentCodingIndex + 1} of {CodingCardCount}";

    public string GetCodingPatternLabel(CodingPracticeCard card)
    {
        return GetCodingPatternCategory(card);
    }

    public void ActivateCodingMode()
    {
        _codingModeActive = true;
        _codingIndex = Math.Clamp(_codingIndex, 0, Math.Max(_currentCodingDeck.Count - 1, 0));
        IsFlipped = false;
    }

    public void ActivateConceptMode()
    {
        _codingModeActive = false;
        IsFlipped = false;
    }

    public void NextCodingCard()
    {
        if (_reviewingCodingStudyPile && _codingStudyPile.Count > 0)
        {
            _codingStudyPileIndex = (_codingStudyPileIndex + 1) % _codingStudyPile.Count;
            IsFlipped = false;
            return;
        }

        if (_currentCodingDeck.Count == 0)
        {
            return;
        }

        IsFlipped = false;
        _codingIndex = (_codingIndex + 1) % _currentCodingDeck.Count;
    }

    public void PreviousCodingCard()
    {
        if (_reviewingCodingStudyPile && _codingStudyPile.Count > 0)
        {
            _codingStudyPileIndex = (_codingStudyPileIndex - 1 + _codingStudyPile.Count) % _codingStudyPile.Count;
            IsFlipped = false;
            return;
        }

        if (_currentCodingDeck.Count == 0)
        {
            return;
        }

        IsFlipped = false;
        _codingIndex = (_codingIndex - 1 + _currentCodingDeck.Count) % _currentCodingDeck.Count;
    }

    public void ToggleFlip()
    {
        IsFlipped = !IsFlipped;
    }

    public void SelectTopic(string topic)
    {
        _selectedTopic = topic;
        RebuildCurrentDeck(orderBy: false);
        _reviewingStudyPile = false;
        _studyPileIndex = 0;
        IsFlipped = false;
    }

    public void ShuffleAllCards()
    {
        SelectTopic("All cards");
        RebuildCurrentDeck(orderBy: true);
    }

    public void ShuffleSelectedTopic()
    {
        if (_selectedTopic == "All cards")
        {
            return;
        }

        RebuildCurrentDeck(orderBy: true);
    }

    public void SelectCodingTopic(string topic)
    {
        _selectedCodingTopic = topic;
        RebuildCurrentCodingDeck(orderBy: false);
        _reviewingCodingStudyPile = false;
        _codingStudyPileIndex = 0;
        IsFlipped = false;
    }

    public void ShuffleAllCodingCards()
    {
        SelectCodingTopic("All prompts");
        RebuildCurrentCodingDeck(orderBy: true);
    }

    public void ShuffleSelectedCodingTopic()
    {
        if (_selectedCodingTopic == "All prompts")
        {
            return;
        }

        RebuildCurrentCodingDeck(orderBy: true);
    }

    public void SelectCodingPattern(string pattern)
    {
        if (_codingPatternGuides.Any(guide => guide.Name == pattern))
        {
            _selectedCodingPattern = pattern;
        }
    }

    public void PracticeSelectedCodingPattern()
    {
        var guide = CurrentCodingPatternGuide;
        if (guide is null)
        {
            return;
        }

        _selectedCodingTopic = $"Pattern: {guide.Name}";
        _currentCodingDeck = guide.Problems.ToList();
        _reviewingCodingStudyPile = false;
        _codingStudyPileIndex = 0;
        _codingIndex = 0;
        _isCodingShuffled = false;
        IsFlipped = false;
    }

    public void NextCard()
    {
        IsFlipped = false;

        if (_reviewingStudyPile && _studyPile.Count > 0)
        {
            _studyPileIndex = (_studyPileIndex + 1) % _studyPile.Count;
            return;
        }

        if (_currentDeck.Count == 0)
        {
            return;
        }

        _currentIndex = (_currentIndex + 1) % _currentDeck.Count;
    }

    public void PreviousCard()
    {
        IsFlipped = false;

        if (_reviewingStudyPile && _studyPile.Count > 0)
        {
            _studyPileIndex = (_studyPileIndex - 1 + _studyPile.Count) % _studyPile.Count;
            return;
        }

        if (_currentDeck.Count == 0)
        {
            return;
        }

        _currentIndex = (_currentIndex - 1 + _currentDeck.Count) % _currentDeck.Count;
    }

    public void MarkCurrentCardAsUnknown()
    {
        var card = CurrentCard;

        if (!_studyPile.Contains(card))
        {
            _studyPile.Add(card);
        }

        IsFlipped = false;
        NextCard();
    }

    public void MarkCurrentCodingCardAsUnknown()
    {
        var card = CurrentCodingCard;

        if (!_codingStudyPile.Contains(card))
        {
            _codingStudyPile.Add(card);
        }

        IsFlipped = false;
        NextCodingCard();
    }

    public void StartStudyReview()
    {
        if (_studyPile.Count == 0)
        {
            return;
        }

        _reviewingStudyPile = true;
        _studyPileIndex = 0;
        IsFlipped = false;
    }

    public void StartCodingStudyReview()
    {
        if (_codingStudyPile.Count == 0)
        {
            return;
        }

        _reviewingCodingStudyPile = true;
        _codingStudyPileIndex = 0;
        IsFlipped = false;
    }

    public void ReturnToDeck()
    {
        _reviewingStudyPile = false;
        _studyPileIndex = 0;
        IsFlipped = false;
    }

    public void ReturnToCodingDeck()
    {
        _reviewingCodingStudyPile = false;
        _codingStudyPileIndex = 0;
        IsFlipped = false;
    }

    public void ClearStudyPile()
    {
        _studyPile.Clear();
        _reviewingStudyPile = false;
        _studyPileIndex = 0;
        IsFlipped = false;
    }

    public void ClearCodingStudyPile()
    {
        _codingStudyPile.Clear();
        _reviewingCodingStudyPile = false;
        _codingStudyPileIndex = 0;
        IsFlipped = false;
    }

    public void ResetDeckState()
    {
        _studyPile.Clear();
        _reviewingStudyPile = false;
        _studyPileIndex = 0;
        _selectedTopic = "All cards";
        _isShuffled = false;
        RebuildCurrentDeck(orderBy: false);
        IsFlipped = false;
    }

    public void ResetCodingDeckState()
    {
        _codingStudyPile.Clear();
        _reviewingCodingStudyPile = false;
        _codingStudyPileIndex = 0;
        _selectedCodingTopic = "All prompts";
        _isCodingShuffled = false;
        RebuildCurrentCodingDeck(orderBy: false);
        IsFlipped = false;
    }

    public string GetTopicLabel(FlashCard card)
    {
        var inferred = InferTopic(card);
        return inferred;
    }

    private void LoadCodingPracticeCards()
    {
        _codingDemoCards.Clear();
        _parsedCodingQuestions.Clear();

        var docPath = Path.Combine(AppContext.BaseDirectory, "Coding questions.docx");
        if (!File.Exists(docPath))
        {
            docPath = Path.Combine(Directory.GetCurrentDirectory(), "Coding questions.docx");
        }

        if (!File.Exists(docPath))
        {
            return;
        }

        var prompts = ExtractCodingQuestions(docPath);
        _parsedCodingQuestions.AddRange(prompts);

        foreach (var prompt in prompts)
        {
            _codingDemoCards.Add(CodingPracticeCardCatalog.Build(prompt));
        }

        BuildCodingTopicIndex();
        BuildCodingPatternGuides();
        ResetCodingDeckState();
    }

    private static List<string> ExtractCodingQuestions(string docPath)
    {
        var questionTitles = new List<string>();
        var skipHeadings = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "coding questions:",
            "easy",
            "medium",
            "hard",
            "arrays & strings",
            "linked lists",
            "stack & queue",
            "stack questions",
            "queue questions",
            "basic questions",
            "beginner dp",
            "sorting and searching",
            "hash tables / dictionaries",
            "trees",
            "dynamic programming"
        };
        var seenCanonicalQuestions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        using var archive = ZipFile.OpenRead(docPath);
        var documentEntry = archive.GetEntry("word/document.xml");

        if (documentEntry is null)
        {
            return questionTitles;
        }

        var xdoc = XDocument.Load(documentEntry.Open());
        var ns = XNamespace.Get("http://schemas.openxmlformats.org/wordprocessingml/2006/main");

        foreach (var paragraph in xdoc.Descendants(ns + "p"))
        {
            var text = string.Concat(paragraph.Descendants(ns + "t").Select(node => node.Value)).Trim();

            if (string.IsNullOrWhiteSpace(text))
            {
                continue;
            }

            if (text.StartsWith("1.", StringComparison.OrdinalIgnoreCase)
                || text.StartsWith("2.", StringComparison.OrdinalIgnoreCase)
                || text.StartsWith("3.", StringComparison.OrdinalIgnoreCase)
                || text.StartsWith("4.", StringComparison.OrdinalIgnoreCase)
                || text.StartsWith("5.", StringComparison.OrdinalIgnoreCase)
                || text.StartsWith("6.", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (skipHeadings.Contains(text))
            {
                continue;
            }

            var canonicalQuestion = NormalizeCodingQuestion(text);
            if (!seenCanonicalQuestions.Add(canonicalQuestion))
            {
                continue;
            }

            if (text.Contains("Questions", StringComparison.OrdinalIgnoreCase)
                || text.Contains("problem", StringComparison.OrdinalIgnoreCase)
                || text.Contains("implementation", StringComparison.OrdinalIgnoreCase)
                || text.Contains("traversal", StringComparison.OrdinalIgnoreCase)
                || text.Contains("maximum depth", StringComparison.OrdinalIgnoreCase))
            {
                if (skipHeadings.Contains(text.ToLowerInvariant()))
                {
                    continue;
                }
            }

            questionTitles.Add(text);
        }

        return questionTitles.Where(text => !IsKnownHeading(text)).ToList();
    }

    private static string NormalizeCodingQuestion(string text)
    {
        var lowered = text.Trim().ToLowerInvariant();

        return lowered switch
        {
            "two sum problem" => "two sum",
            _ => lowered
        };
    }

    private static bool IsKnownHeading(string text)
    {
        var lowered = text.Trim().ToLowerInvariant();

        return lowered is "coding questions:" or "easy" or "medium" or "hard"
            or "arrays & strings" or "linked lists" or "stack & queue" or "stack questions"
            or "queue questions" or "basic questions" or "beginner dp" or "sorting and searching"
            or "hash tables / dictionaries" or "trees" or "dynamic programming";
    }

    private void BuildTopicIndex()
    {
        _topicOrder.Clear();
        _topicIndices.Clear();
        _topicOptions.Clear();
        _topicOptions.Add("All cards");

        for (var index = 0; index < _cards.Count; index++)
        {
            var card = _cards[index];
            var topic = InferTopic(card);

            if (!_topicOrder.Contains(topic))
            {
                _topicOrder.Add(topic);
                _topicIndices[topic] = new List<int>();
            }

            _topicIndices[topic].Add(index);
        }

        foreach (var topic in _topicOrder)
        {
            _topicOptions.Add(topic);
        }
    }

    private void BuildCodingTopicIndex()
    {
        _codingTopicOrder.Clear();
        _codingTopicIndices.Clear();
        _codingTopicOptions.Clear();
        _codingTopicOptions.Add("All prompts");

        for (var index = 0; index < _codingDemoCards.Count; index++)
        {
            var card = _codingDemoCards[index];

            if (!_codingTopicOrder.Contains(card.Topic))
            {
                _codingTopicOrder.Add(card.Topic);
                _codingTopicIndices[card.Topic] = new List<int>();
            }

            _codingTopicIndices[card.Topic].Add(index);
        }

        foreach (var topic in _codingTopicOrder)
        {
            _codingTopicOptions.Add(topic);
        }
    }

    private void BuildCodingPatternGuides()
    {
        _codingPatternGuides.Clear();

        var patternOrder = new[]
        {
            "Hash Maps and Sets",
            "Two Pointers",
            "Sliding Window",
            "BFS and DFS",
            "Modified Binary Search",
            "Fast & Slow Pointers",
            "Merge Intervals"
        };

        foreach (var pattern in patternOrder)
        {
            var info = GetPatternGuideText(pattern);
            var problems = _codingDemoCards
                .Where(card => GetCodingPatternCategory(card) == pattern)
                .OrderBy(card => card.Prompt)
                .ToList();

            _codingPatternGuides.Add(new CodingPatternGuide(
                pattern,
                info.Explanation,
                info.HowToIdentify,
                info.InterviewLanguage,
                problems));
        }

        _selectedCodingPattern = _codingPatternGuides.FirstOrDefault()?.Name ?? string.Empty;
    }

    private void RebuildCurrentDeck(bool orderBy)
    {
        _currentDeck = _selectedTopic == "All cards"
            ? new List<FlashCard>(_cards)
            : _cards.Where((card, index) => _topicIndices[_selectedTopic].Contains(index)).Select(card => card).ToList();

        if (orderBy)
        {
            ShuffleDeck(_currentDeck);
            _isShuffled = true;
        }
        else
        {
            _isShuffled = false;
        }

        _currentIndex = 0;
    }

    private void RebuildCurrentCodingDeck(bool orderBy)
    {
        if (_selectedCodingTopic.StartsWith("Pattern: ", StringComparison.OrdinalIgnoreCase))
        {
            var pattern = _selectedCodingTopic["Pattern: ".Length..];
            _currentCodingDeck = _codingDemoCards.Where(card => GetCodingPatternCategory(card) == pattern).ToList();
        }
        else
        {
            _currentCodingDeck = _selectedCodingTopic == "All prompts"
            ? new List<CodingPracticeCard>(_codingDemoCards)
            : _codingDemoCards.Where((card, index) => _codingTopicIndices[_selectedCodingTopic].Contains(index)).Select(card => card).ToList();
        }

        if (orderBy)
        {
            ShuffleDeck(_currentCodingDeck);
            _isCodingShuffled = true;
        }
        else
        {
            _isCodingShuffled = false;
        }

        _codingIndex = 0;
    }

    private static (string Explanation, string HowToIdentify, string InterviewLanguage) GetPatternGuideText(string pattern)
    {
        return pattern switch
        {
            "Hash Maps and Sets" => (
                "Use dictionaries, hash maps, or sets to track frequencies, map values to indices, or remember elements you have already seen.",
                "Reach for this when the prompt asks for pairs that sum to a target, duplicates in an array, anagrams, or most and least frequent elements.",
                "A dictionary or set lets me avoid comparing every item to every other item, which usually improves the solution from O(n^2) to O(n)."),
            "Two Pointers" => (
                "Use two variables to walk through a structure from different directions or to keep separate read and write positions.",
                "Reach for this with sorted arrays or linked lists, reversing arrays or strings, removing items in place, checking palindromes, or finding target sums.",
                "I can use one pointer to represent one side of the search and the other pointer to represent the other side, then move the pointer that helps narrow the answer."),
            "Sliding Window" => (
                "Maintain a contiguous window over an array or string, expanding and shrinking it as conditions change.",
                "Reach for this when the problem asks for a longest, shortest, maximum, or minimum contiguous subarray or substring.",
                "I will keep a valid window and move the left side only when the current window violates the rule."),
            "BFS and DFS" => (
                "Use breadth-first search to explore level by level, and depth-first search to explore one path or subtree deeply before backtracking.",
                "Use BFS for shortest paths in unweighted graphs and tree level-order traversal. Use DFS for tree recursion, exploring all paths, topological-style thinking, and maze or island-style problems.",
                "If I need nearest or level-by-level information I will use BFS; if I need to fully explore branches or subtrees I will use DFS."),
            "Modified Binary Search" => (
                "Adapt binary search when the data is sorted, partially sorted, rotated, or when the answer can be found by repeatedly cutting the search space in half.",
                "Reach for this when finding an element in sorted data, finding a minimum or maximum in a rotated array, or deciding which half of the search space is still valid.",
                "Because the data has sorted structure, I can inspect the middle and eliminate half of the remaining candidates each step."),
            "Fast & Slow Pointers" => (
                "Use two pointers moving at different speeds through a sequence.",
                "Reach for this when identifying cycles in a linked list or finding the exact middle of a linked list.",
                "The fast pointer reveals information about the whole list while the slow pointer lands on the important position."),
            "Merge Intervals" => (
                "Sort ranges by start time or start position, then merge, insert, or compare overlapping ranges.",
                "Reach for this in scheduling, calendar, meeting-room, timeline, or overlapping-block problems.",
                "Once intervals are sorted, overlapping ranges sit next to each other, so I can scan once and maintain the merged range."),
            _ => (
                "This pattern is a reusable way to organize the core state and loop for this class of problem.",
                "Look for repeated constraints, input shape, and the operation the problem keeps asking you to do efficiently.",
                "I will name the pattern first, then explain the state I need to maintain while I walk through the input.")
        };
    }

    private static string GetCodingPatternCategory(CodingPracticeCard card)
    {
        var pattern = card.Pattern.ToLowerInvariant();
        var prompt = card.Prompt.ToLowerInvariant();
        var topic = card.Topic.ToLowerInvariant();

        if (prompt.Contains("cycle") || prompt.Contains("middle node") || pattern.Contains("floyd") || pattern.Contains("slow and fast"))
        {
            return "Fast & Slow Pointers";
        }

        if (prompt.Contains("merge intervals") || pattern.Contains("sort and sweep"))
        {
            return "Merge Intervals";
        }

        if (pattern.Contains("sliding window") || pattern.Contains("monotonic deque") || prompt.Contains("substring") || prompt.Contains("sliding window"))
        {
            return "Sliding Window";
        }

        if (topic.Contains("trees") || pattern.Contains("depth-first") || pattern.Contains("breadth-first") || pattern.Contains("bfs"))
        {
            return "BFS and DFS";
        }

        if (prompt.Contains("binary search") || prompt.Contains("rotated") || prompt.Contains("search in sorted"))
        {
            return "Modified Binary Search";
        }

        if (pattern.Contains("hash") || pattern.Contains("frequency") || pattern.Contains("canonical key") || pattern.Contains("cycle detection with a set"))
        {
            return "Hash Maps and Sets";
        }

        if (pattern.Contains("two pointers")
            || pattern.Contains("pointer")
            || pattern.Contains("merge step")
            || pattern.Contains("split, reverse, merge")
            || pattern.Contains("elementary addition"))
        {
            return "Two Pointers";
        }

        return "Hash Maps and Sets";
    }

    private static void ShuffleDeck<TCard>(List<TCard> deck)
    {
        var shuffled = deck.OrderBy(_ => Random.Shared.Next()).ToList();
        deck.Clear();
        deck.AddRange(shuffled);
    }

    private string InferTopic(FlashCard card)
    {
        var pool = $"{card.Question} {card.Answer} {card.Topic}".ToLowerInvariant();

        if (pool.Contains("memory") || pool.Contains("garbage collection") || pool.Contains("weak reference") || pool.Contains("dispose") || pool.Contains("finalize") || pool.Contains("allocation"))
        {
            return "Memory & Allocation";
        }

        if (pool.Contains("async") || pool.Contains("await") || pool.Contains("task") || pool.Contains("thread") || pool.Contains("lock") || pool.Contains("concurrency"))
        {
            return "Async & Concurrency";
        }

        if (pool.Contains("lambda") || pool.Contains("linq") || pool.Contains("delegate") || pool.Contains("func") || pool.Contains("action") || pool.Contains("predicate") || pool.Contains("yield") || pool.Contains("select") || pool.Contains("where"))
        {
            return "Delegates & LINQ";
        }

        if (pool.Contains("record") || pool.Contains("nullable") || pool.Contains("dynamic") || pool.Contains("enum") || pool.Contains("generic") || pool.Contains("params"))
        {
            return "Language Features";
        }

        if (pool.Contains("interface") || pool.Contains("abstract") || pool.Contains("class") || pool.Contains("inherit") || pool.Contains("override") || pool.Contains("virtual") || pool.Contains("sealed") || pool.Contains("polymorphism") || pool.Contains("encapsulation"))
        {
            return "OOP Design";
        }

        if (pool.Contains("exception") || pool.Contains("try") || pool.Contains("catch") || pool.Contains("finally") || pool.Contains("throw"))
        {
            return "Exceptions";
        }

        if (pool.Contains("array") || pool.Contains("list") || pool.Contains("dictionary") || pool.Contains("collection") || pool.Contains("queue") || pool.Contains("stack") || pool.Contains("enumerable"))
        {
            return "Collections";
        }

        if (pool.Contains("struct") || pool.Contains("value type") || pool.Contains("reference type"))
        {
            return "Value Types";
        }

        if (pool.Contains("serialization") || pool.Contains("reflection") || pool.Contains("namespace") || pool.Contains("marshalling"))
        {
            return "Runtime & Framework";
        }

        return card.Topic;
    }
}
