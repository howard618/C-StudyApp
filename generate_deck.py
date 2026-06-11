from pathlib import Path
from zipfile import ZipFile
import re
import xml.etree.ElementTree as ET

path = Path('C#Interview.docx')
with ZipFile(path) as z:
    root = ET.fromstring(z.read('word/document.xml'))

ns = {'w': 'http://schemas.openxmlformats.org/wordprocessingml/2006/main'}
questions = []
for p in root.findall('.//w:p', ns):
    text = ''.join(''.join(t.itertext()) for t in p.findall('.//w:t', ns)).strip()
    if re.match(r'^Q\d+:', text):
        question = re.sub(r'^Q\d+:\s*', '', text).strip()
        question = re.sub(r'\s*Related To:.*$', '', question, flags=re.I).strip()
        if question:
            questions.append(question)


def topic_for(question: str) -> str:
    lowered = question.lower()
    if any(word in lowered for word in ['class', 'object', 'interface', 'abstract', 'sealed', 'inherit', 'polymorphism', 'encapsulation', 'overriding', 'overloading']):
        return 'OOP'
    if any(word in lowered for word in ['exception', 'try', 'catch', 'finally', 'throw', 'using']):
        return 'Exceptions & Resources'
    if any(word in lowered for word in ['linq', 'ienumerable', 'ilist', 'icollection', 'idictionary', 'lazy', 'delegate', 'action', 'func', 'predicate', 'lambda', 'record', 'generic', 'dynamic', 'nullable', 'enum']):
        return 'Collections & Language Features'
    if any(word in lowered for word in ['thread', 'task', 'async', 'await', 'dispose', 'finalize', 'garbage', 'boxing', 'unboxing', 'memory']):
        return 'Runtime & Concurrency'
    if any(word in lowered for word in ['array', 'struct', 'value type', 'reference type', 'serial', 'marshal', 'pointer', 'scope', 'namespace', 'partial', 'static constructor', 'volatile', 'weak reference']):
        return 'Core .NET Concepts'
    return 'C# Fundamentals'


def tip_for(question: str) -> str:
    lowered = question.lower()
    if 'difference between' in lowered:
        return 'Compare the two concepts and remember when each one is used.'
    if 'what is' in lowered or 'what are' in lowered:
        return 'Use the definition and the common use case.'
    if 'why' in lowered or 'when' in lowered:
        return 'Tie the concept to a real scenario.'
    if 'how' in lowered:
        return 'Think through the execution flow or usage pattern.'
    return 'Recall the core behavior and practical use.'

answers = {
    'What is C#?': 'C# is a modern, object-oriented, type-safe programming language developed by Microsoft as part of the .NET platform.',
    'What is an Object?': 'An object is an instance of a class, carrying state (fields) and behavior (methods) in memory.',
    'What is the difference between continue and break statements in C#?': 'break exits the enclosing loop or switch immediately, while continue skips the rest of the current iteration and proceeds to the next one.',
    'What are Property Accessors?': 'Property accessors are the get and set blocks that control reading and writing of a property value, allowing encapsulation and validation.',
    'What is the difference between a Struct and a Class in C#?': 'Structs are value types stored on the stack (or inline) and copied by value, while classes are reference types stored on the heap and passed by reference.',
    'What is an Abstract Class?': 'An abstract class is a base class that cannot be instantiated directly and can contain abstract members that derived classes must implement.',
    'What is namespace in C#?': 'A namespace organizes code into logical groups and helps avoid naming conflicts across types.',
    'What are Nullable types in C#?': 'Nullable types allow value types such as int, bool, and DateTime to represent both a valid value and null.',
    'How is Exception Handling implemented in C#?': 'Exception handling uses try, catch, finally, and throw to detect errors, handle them, and clean up resources.',
    'What you understand by Value types and Reference types in .NET? Provide some comparison.': 'Value types store their data directly and are copied by value, while reference types store a reference to an object on the heap and share identity.',
    'What is Serialization?': 'Serialization converts an object into a stream or text format so it can be persisted, transmitted, or reconstructed later.',
    'What are the different types of classes in C#?': 'C# supports concrete classes, abstract classes, sealed classes, static classes, partial classes, and nested classes, each with different behaviors.',
    'What is the difference between string and StringBuilder in C#?': 'string is immutable and creates new instances on changes, while StringBuilder is mutable and efficient for repeated text modifications.',
    'What is LINQ in C#?': 'LINQ is Language Integrated Query, which lets you query collections, XML, and databases using C# syntax and operators.',
    'What are Reference Types in C#?': 'Reference types store references to objects allocated on the managed heap, including class, interface, delegate, array, and string types.',
    'What is Managed or Unmanaged Code?': 'Managed code runs under the .NET runtime, which provides memory management, type safety, and garbage collection; unmanaged code runs outside that runtime control.',
    'Can multiple catch blocks be executed?': 'No. Only the first matching catch block executes, and execution continues after the catch chain.',
    'Can this be used within a Static method?': 'No, this refers to the current instance, so it cannot be used inside a static method because there is no instance.',
    'What are partial classes?': 'Partial classes allow a class definition to be split across multiple files, which is useful for code generation and organization.',
    'Why to use finally block in C#?': 'finally executes regardless of whether an exception was thrown, making it ideal for cleanup and resource release.',
    'What are generics in C#?': 'Generics allow reusable type-safe classes, methods, interfaces, and delegates that work with any data type.',
    'What are dynamic type variables in C#?': 'dynamic variables defer type checking until runtime, allowing late-bound operations that the compiler cannot validate.',
    'What is Boxing and Unboxing?': 'Boxing converts a value type to an object, and unboxing converts that boxed object back to the original value type.',
    'What is enum in C#?': 'An enum is a named set of constants backed by an integral type that improves readability and type safety.',
    'In how many ways you can pass parameters to a method?': 'Parameters can be passed by value, by reference with ref, by reference with out, and as optional/default or params arrays, depending on the method design.',
    'What is the difference between a class and a structure?': 'Classes are reference types with inheritance and heap allocation; structures are value types, copied by value, and do not support inheritance.',
    'Explain Anonymous type in C#': 'Anonymous types let you create read-only, inferred objects without explicitly defining a class, commonly used in LINQ projections.',
    'Explain Code Compilation in C#': 'C# source is compiled by the Roslyn compiler into IL, which is then JIT-compiled into native machine code when the app runs.',
    'What is scope of a Internal member variable of a C# class?': 'Internal members are visible only within the same assembly, not outside of it.',
    'What is the difference between ref and out keywords?': 'ref passes an existing variable into a method and expects it to be initialized, while out passes an uninitialized variable and the method must assign it before returning.',
    'What is Virtual Method in C#?': 'A virtual method is designed to be overridden in derived classes, enabling polymorphic behavior.',
    'What is the difference between dynamic type variables and object type variables?': 'object is statically typed and requires casting, while dynamic uses runtime binding and lets the compiler postpone type checks.',
    'What is difference between constant and readonly?': 'const values are compile-time constants and must be initialized at declaration, while readonly values are assigned at declaration or in a constructor and can vary per instance.',
    'Is there a difference between throw and throw ex?': 'throw rethrows the current exception while preserving the original stack trace; throw ex resets the stack trace to the current location.',
    'What is the difference between Interface and Abstract Class?': 'Interfaces define contracts with no implementation, while abstract classes can provide shared implementation and state but cannot be instantiated.',
    'What is the difference between Equality Operator (==) and Equals() Method in C#?': '== compares object identity or overloaded operator logic, while Equals() calls virtual/object equality semantics, often overridden for value-based comparison.',
    'What are the uses of using in C#': 'using imports namespaces, creates scope-limited resource disposal blocks, and ensures disposable objects are cleaned up.',
    'What is the difference between Virtual method and Abstract method?': 'A virtual method provides a default implementation that may be overridden, while an abstract method has no implementation and must be overridden.',
    'What is Extension Method in C# and how to use them?': 'Extension methods add instance-like methods to existing types without modifying them, typically as static methods in static classes.',
    'Why can\'t you specify the accessibility modifier for methods inside the Interface?': 'Interface members are implicitly public, and the language does not allow redefining accessibility because all implementers must see them as public contracts.',
    'What is an anonymous function in C#?': 'An anonymous function is an unnamed method written as a lambda expression or anonymous delegate that can be passed around as a value.',
    'What is sealed Class in C#?': 'A sealed class cannot be inherited, preventing further derivation.',
    'What is Reflection in C#.Net?': 'Reflection inspects and examines metadata, types, members, attributes, and runtime information in assemblies.',
    'What is a Destructor in C# and when shall I create one?': 'A destructor is a finalizer used to release unmanaged resources before an object is collected, typically only when unmanaged resources need cleanup.',
    'How encapsulation is implemented in C#?': 'Encapsulation hides internal state behind properties and methods, exposing only the necessary interface and enforcing validation.',
    'What is lambda expressions in C#?': 'Lambda expressions provide concise inline function syntax used with LINQ, delegates, and functional patterns.',
    'What is the difference between overloading and overriding?': 'Overloading defines multiple methods with the same name but different parameters, while overriding changes a base virtual method implementation in a derived class.',
    'What is the use of Null Coalescing Operator (??) in C#?': 'The ?? operator returns the left operand if it is not null, otherwise returns the right operand.',
    'How can you prevent a class from overriding in C#?': 'Mark the class as sealed or mark specific methods as non-virtual to prevent overriding.',
    'Is there a way to catch multiple exceptions at once and without code duplication?': 'Yes, catch multiple exception types in one catch block by using a base class or by catching AggregateException in async scenarios.',
    'Explain the difference between Task and Thread in .NET': 'A Thread is an OS-level execution path, while Task is a higher-level abstraction that schedules work and integrates with async patterns.',
    'What is the use of the IDisposable interface?': 'IDisposable defines Dispose() so types can release unmanaged resources and deterministic cleanup.',
    'What is Record in C#?': 'A record is a reference type optimized for immutable data modeling, with value-based equality and concise syntax.',
    'When to use Record vs Class vs Struct in C#?': 'Use records for immutable data models, classes for rich behavior and inheritance, and structs for small value objects.',
    'Explain assignment vs shallow copy vs deep copy for a Record in C#': 'Assignment copies references, shallow copy duplicates top-level values and shared nested references, and deep copy duplicates nested object graphs.',
    'Test if a Number belongs to the Fibonacci Series': 'A number belongs to the Fibonacci series if it satisfies the property that one of the generated Fibonacci values is equal to it or can be detected via the 5n² ± 4 test.',
    'Explain what is Ternary Search?': 'Ternary search divides a sorted unimodal array into three parts and recursively searches the appropriate interval to find an element or minimum.',
    'What are pointer types in C#?': 'Pointer types allow unsafe code to manipulate memory addresses directly, typically using unsafe blocks and the * operator.',
    'Can you create a function in C# which can accept varying number of arguments?': 'Yes, use params arrays or optional parameters to accept a variable number of arguments.',
    'What is difference between late binding and early binding in C#?': 'Early binding resolves types at compile time and enables IntelliSense, while late binding resolves types at runtime using reflection or dynamic.',
    'What is the difference between is and as operators in C#?': 'is checks type compatibility and returns a bool, while as performs safe casting and returns null if the cast fails.',
    'What is scope of a Protected Internal member variable of a C# class?': 'A protected internal member is accessible within the same assembly and to derived types outside the assembly.',
    'Can Multiple Inheritance implemented in C# ?': 'C# does not support multiple inheritance of classes, but classes can implement multiple interfaces.',
    'Is operator overloading supported in C#?': 'Yes, C# allows operator overloading for selected operators, enabling custom operator behavior on types.',
    'What interface should your data structure implement to make the Where method work?': 'The data structure should implement IEnumerable<T> so LINQ extension methods like Where can enumerate it.',
    'What is Marshalling and why do we need it?': 'Marshalling converts managed data to unmanaged data formats and vice versa so interop with native APIs works correctly.',
    'What is the difference between System.ApplicationException class and System.SystemException class?': 'SystemException is reserved for runtime failures, while ApplicationException is intended for application-defined exceptions, though modern guidance often uses custom exceptions directly.',
    'Why to use lock statement in C#?': 'lock serializes access to shared resources, preventing race conditions in multithreaded code.',
    'What are the different ways a method can be overloaded?': 'A method can be overloaded by changing the number of parameters, parameter types, or parameter order.',
    'What is the yield keyword used for in C#?': 'yield creates an iterator, allowing a method to return values lazily one at a time while preserving state across iterations.',
    'IEnumerable vs List - What to Use? How do they work?': 'IEnumerable is a read-only forward-only enumeration contract, while List is a concrete mutable collection that implements IEnumerable and provides indexing, adding, and removing.',
    'When to use ArrayList over array[] in C#?': 'ArrayList is useful when you need a dynamically sized collection of objects without generics, but generics are preferred in modern C#.',
    'What is the use of conditional preprocessor directive in C#?': 'Preprocessor directives like #if and #define enable conditional compilation and platform-specific code paths.',
    'When would you use delegates in C#?': 'Delegates are used to pass methods as arguments, implement callbacks, and support event patterns.',
    'What is the Constructor Chaining in C#?': 'Constructor chaining calls one constructor from another using this(...) or base(...) to reuse initialization logic.',
    'What\'s the difference between StackOverflowError and OutOfMemoryError?': 'StackOverflowError occurs when the stack grows beyond its limit, while OutOfMemoryError indicates the managed or native memory pool has been exhausted.',
    'What is the difference between dispose and finalize methods in C#?': 'Dispose is explicit deterministic cleanup, while finalize is nondeterministic cleanup performed by the GC for unmanaged resources.',
    'What is Indexer in C#?': 'An indexer lets an object be accessed like an array using index syntax, often over a collection or internal storage.',
    'What is the difference between Func<string,string> and delegate?': 'Func<string,string> is a predefined generic delegate for methods returning a value, while a custom delegate defines a specific method signature.',
    'Explain what is Short-Circuit Evaluation in C#': 'Short-circuit evaluation stops evaluating boolean expressions as soon as the result is known, using && and ||.',
    'Explain the difference between Select and Where': 'Where filters elements, while Select projects each element into a new shape or value.',
    'What is the best practice to have best performance using Lazy objects?': 'Use Lazy<T> when initialization is expensive and you want deferred, thread-safe creation only when the value is actually needed.',
    'What is a static constructor?': 'A static constructor runs once per type before the first instance or static member access, and it initializes static state.',
    'Explain how does Asynchronous tasks Async/Await work in .NET?': 'async/await turns asynchronous methods into state machines, letting code continue later without blocking the thread while awaiting I/O or long-running work.',
    'What happens when we Box or Unbox Nullable types?': 'Boxing a nullable value type boxes the underlying value if it has a value, otherwise it boxes null; unboxing returns the nullable value or throws if the boxed value is not compatible.',
    'Can you explain the difference between Interface, abstract class, sealed class, static class and partial class in C#?': 'Interfaces define contracts, abstract classes provide partial implementation, sealed classes cannot be inherited, static classes hold only static members, and partial classes split a type across files.',
    'How to solve Circular Reference?': 'Break the reference cycle by redesigning ownership, using weak references, or removing mutual dependencies so objects can be collected.',
    'What is Multicast Delegate in C#?': 'A multicast delegate holds multiple method targets and invokes them in order when called.',
    'What is jagged array in C# and when to prefer jagged arrays over multidimensional arrays?': 'A jagged array is an array of arrays, useful when rows have different lengths; multidimensional arrays are better for rectangular matrices.',
    'Why Abstract class can not be sealed or static?': 'Abstract classes are meant to be inherited, so sealing or making them static would conflict with that purpose.',
    'What are the benefits of a Deferred Execution in LINQ?': 'Deferred execution reduces work until enumeration, allows streaming, and lets queries compose efficiently.',
    'Could you explain the difference between destructor, dispose and finalize method?': 'A destructor in C# is a finalizer pattern, dispose is deterministic cleanup, and finalize is GC-triggered cleanup for unmanaged resources.',
    'What are the differences between IEnumerable and IQueryable?': 'IEnumerable operates in memory, while IQueryable supports provider translation for remote sources like databases.',
    'What is the difference between Lambdas and Delegates?': 'Lambdas are inline anonymous functions, while delegates are the type-safe function pointers used to represent method signatures.',
    'What is the method MemberwiseClone() doing?': 'MemberwiseClone creates a shallow copy of an object by copying field values, not deep-copying nested references.',
    'What\'s the difference between the System.Array.CopyTo() and System.Array.Clone()?': 'CopyTo copies elements into an existing array, while Clone creates a new array with copied elements.',
    'What are Circular References in C#?': 'Circular references are object graphs that reference each other, which can complicate lifetime management and garbage collection unless broken or weak referenced.',
    'What is deep or shallow copy concept in C#?': 'Shallow copy duplicates references, while deep copy duplicates the entire object graph recursively.',
    'List some different ways for equality check in .NET': 'Use ==, Equals(), object.ReferenceEquals, IEquatable<T>, and custom equality members depending on the type semantics.',
    'What is a preprocessor directives in C#?': 'Preprocessor directives control compilation symbols, conditional compilation, and include behavior before the compiler processes the code.',
    'What is the use of static constructors?': 'Static constructors initialize static data exactly once before first use of the type.',
    'What is the volatile keyword used for?': 'volatile tells the compiler and processor not to optimize away reads/writes, ensuring visibility across threads for simple state flags.',
    'Explain what is Weak Reference in C#?': 'A weak reference allows access to an object without preventing it from being garbage collected, useful for caches and memory-sensitive scenarios.',
    'Could you explain the difference between Func vs. Action vs. Predicate?': 'Func represents methods returning a value, Action represents methods returning void, and Predicate represents boolean-returning methods used for filtering.',
    'Can you add extension methods to an existing static class?': 'No, extension methods must be defined in a static class, but they extend types, not existing static classes themselves.',
    'in C#, when should we use abstract classes instead of interfaces with extension methods?': 'Use abstract classes when you need shared implementation, state, or base-class behavior; use interfaces when you need flexible contracts and multiple inheritance of behavior.',
    'Implement the Where method in C#. Explain.': 'Where filters a sequence using a predicate and returns the matching elements, commonly implemented with deferred execution over an iterator.',
    'Explain the difference between IQueryable, ICollection, IList & IDictionary interfaces?': 'IQueryable supports provider-backed queries, ICollection defines collection semantics, IList provides index-based access, and IDictionary manages key/value pairs.',
    'You have defined a destructor in a class that you have developed by using the C#, but the destructor never executed. Why?': 'In C#, destructors are finalizers and are not guaranteed to run promptly because GC decides when to collect; they may never run before process exit, and deterministic cleanup should use Dispose().',
    'Why doesn\'t C# allow static methods to implement an interface?': 'Interface implementation requires instance behavior tied to an object, while static methods belong to the type and cannot satisfy instance contracts.',
    'Explain when to use Finalize vs Dispose?': 'Use Dispose for deterministic cleanup of managed and unmanaged resources; use Finalize only as a backup for unmanaged resources when Dispose is not called.',
}

missing = []
for question in questions:
    answer = answers.get(question)
    if answer is None:
        if 'difference between' in question.lower():
            answer = 'The two concepts serve different purposes: one is typically used for behavior or structure, while the other is used for a different contract or storage model. The key distinction is the scenario where each applies.'
        elif 'what is' in question.lower() or 'what are' in question.lower():
            answer = 'This concept represents a core C# or .NET feature used to structure code, manage data, or control execution in managed applications.'
        elif 'why' in question.lower() or 'when' in question.lower():
            answer = 'Use this feature when the scenario calls for that specific behavior, usually to improve correctness, performance, or maintainability.'
        elif 'how' in question.lower():
            answer = 'The implementation typically follows the normal language or runtime pattern, and the key idea is to use the feature in the right context.'
        else:
            answer = 'This is a core .NET or C# concept that is used to model, execute, or manage code and data in managed applications.'
        missing.append(question)

lines = [
    'using System.Collections.ObjectModel;',
    '',
    'namespace FlashCards.Data;',
    '',
    'public class FlashCardDeckService',
    '{',
    '    private readonly List<FlashCard> _cards = new()',
    '    {',
]
for question in questions:
    answer = answers[question] if question in answers else 'This concept is a core .NET or C# feature used to structure code, manage data, or control execution in managed applications.'
    lines.append(f'        new("{question.replace("\\", "\\\\").replace("\"", "\\\"")}", "{answer.replace("\\", "\\\\").replace("\"", "\\\"")}", "{topic_for(question)}", "{tip_for(question).replace("\\", "\\\\").replace("\"", "\\\"")}"),')

lines.extend([
    '    };',
    '',
    '    private int _currentIndex;',
    '',
    '    public ReadOnlyCollection<FlashCard> Cards => _cards.AsReadOnly();',
    '',
    '    public FlashCard CurrentCard => _cards[_currentIndex];',
    '',
    '    public int CurrentIndex => _currentIndex;',
    '',
    '    public int TotalCards => _cards.Count;',
    '',
    '    public bool IsFlipped { get; private set; }',
    '',
    '    public string Progress => $"{_currentIndex + 1} of {TotalCards}";',
    '',
    '    public void ToggleFlip()',
    '    {',
    '        IsFlipped = !IsFlipped;',
    '    }',
    '',
    '    public void NextCard()',
    '    {',
    '        IsFlipped = false;',
    '        _currentIndex = (_currentIndex + 1) % _cards.Count;',
    '    }',
    '',
    '    public void PreviousCard()',
    '    {',
    '        IsFlipped = false;',
    '        _currentIndex = (_currentIndex - 1 + _cards.Count) % _cards.Count;',
    '    }',
    '}',
])

path = Path('Data/FlashCardDeckService.cs')
path.write_text('\n'.join(lines) + '\n', encoding='utf-8')
print(f'Wrote {len(questions)} cards to {path}')
print(f'Missing answers: {len(missing)}')
