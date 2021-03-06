// | CODING STANDARD-COMPLIANT REFERENCE C# FILE
// |   _____     ____
// |  / ___/  __/ / /_
// | / /__   /_  . __/
// | \___/  /_  . __/
// |         /_/_/
// |
// |[About this file]
// |    - This is a 'living document' and will be updated as our standard evolves.
// |    - The '// |'-style comments denote documentation markup, and are not part of the actual sample.
// |    - To avoid redundancy, rules inline in the reference code are only mentioned once. Assume they apply generally unless noted, or if obvious from the context.
// |    - This code is only intended to demonstrate conventions, and as a result sometimes gets nonsensical. Pay no attention to the substance, only the form.
// |    - For clarification, or reporting of ambiguities or bugs, ask [Unity employee name in charge].
// |
// |[General]
// |    - Our standard extends Microsoft's Framework Design Guidelines, which defines a number of rules not covered by this document. (see https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/).
// |    - This document inlcludes a subset of the most used rules as well as any additions and exceptions to the FDG.
// |    - If there is any disagreement between this file and the FDG, this file always wins. Exceptions to the FDG are marked with [FDG Exception]
// |    - If the compiler does not require something, leave it out (i.e. 'this.' prefix, default access levels, 'Attribute' postfix, etc.)
// |    - Use #region to group blocks of code in long files (100+ lines)
// |    - No hard-coded “magic numbers” and “magic strings”. Declare them as constants
// |    - Use singletons sparingly. A project should ideally have < 3 singleton classes
// |    - Avoid GameObject.Find and similar find methods, especially inside Update
// |    - Cache component lookups
// |    - Separate UI code from core functionality code into separate scripts
// |    - Avoid referencing objects/components/coroutines by string
// |    - Use a namespace to avoid scope conflicts (e.g. with Third Party assets)
// |    - All scripts must compile and run without exceptions errors and warnings
// |        - Warnings from 3rd party scripts are allowed, but should be fixed when possible
// |
// |[Encoding]
// |    - Text file encoding is UTF8 with no BOM, using LF (unix) line endings.
// |    - 4-wide tabstops, using spaces only (no tab characters)
// |    - No trailing whitespace on lines, but always include a single newline at the end of the file.
// |    - (All of the above are ensured by a combination of automated tools. Please refer to .editorconfig we provide)
// |
// |[Files]
// |    - No file header, copyright, etc. of any kind. Some IDE's may add them automatically - please remove them.
// |    - Maintain the style of surrounding code if it has its own separate standard (i.e. is or heavily derived from external).
// |
// |[Class Members]
// |    - Should be grouped into the following sections:
// |        - Constants
// |        - Statics
// |        - Fields
// |        - Properties
// |        - Constructors
// |        - Unity LifeCycle Methods (Awake, OnEnable, Update, …)
// |        - Events / Delegates
// |        - Methods
// |        - Nested types
// |    - Each of these groups should be ordered by access:
// |        - public
// |        - internal
// |        - protected
// |        - private
// |    - Casing:
// |        - Constants: PascalCase
// |        - Variables: camelCase
// |        - Properties: PascalCase
// |        - Methods: PascalCase
// |
// |[Naming]
// |    - No 'Hungarian notation' or other prefixes, except where noted.
// |    - Spell words using correct US-English spelling. Note that there are a few legacy exceptions that use GB-English that we must preserve, but do not add new ones.
// |    - Use descriptive and accurate names, even if it makes them longer. Favor readability over brevity.
// |    - Avoid abbreviations when possible unless the abbreviation is commonly accepted.
// |    - Acronyms are PascalCase, unless they are exactly two letters, in which case they are UPPERCASE. (ex. htmlText, GetCpuCycles(), IOStream)
// |    - Use semantically interesting names rather than language-specific keywords for type names (i.e. GetLength > GetInt).
// |    - Use a common name, such as value/item/element, rather than repeating the type name, in the rare cases when an identifier has no semantic meaning and the type is not important (i.e. newElements > newInts).
// |    - Booleans should start with 'is' or 'has' (e.g. isRunning, hasFinished).
// |    - Collections use the plural form (e.g. activeButtons) and should avoid include 'List', 'Array' and similar in their name
// |
// |    File & Folder Naming:
// |        - Use PascalCase for file names, typically matching the name of the dominant type in the file (or if none is dominant, use a reasonable category name).
// |        - Do not use version numbers, or words to indicate progress (e.g. WIP, final)
// |
// |    Type Naming:
// |        - Make type names unambiguous across namespaces and problem domains by avoiding common terms or adding a prefix or a suffix. (ex. use 'PhysicsSolver', not 'Solver')
// |        - Avoid unnecessary prefixes/suffixes, even when other types in the namespace have a prefix (ex. use 'LightEstimationData', not 'ARLightEstimationData', even though 'ARSession' exists)
// |        - Avoid naming types after terms that represent different concepts in different domains (ex. use 'AndroidInput', not 'Input')
// |        - Do not use namespaces to enable reusing an existing type name, as resolving conflicts in user code requires using aliases or fully qualified type names.
// |
// |    Methods and Parameters:
// |        - When a method takes one or more arguments that imply a binary condition, consider the following options:
// |            - If the method name clearly conveys a binary condition, then use a bool argument (e.g., void SetActive(bool isActive))
// |            - If the method name conveys a condition that could conceivably have more than two states:
// |                - You can create multiple named method variants (e.g., void Repaint() and void RepaintImmediately(), T GetComponent() and T GetComponentReadOnly()).
// |                    (+) Avoids adding new enumerated type to the API surface.
// |                    (-) Increases number of named methods (and thus number of search points in API reference).
// |                    (-) API surface will grow if support for new modes is added later.
// |                - You can create a single method and add an enum for the condition (e.g., void ApplyChanges(InteractionMode mode = InteractionMode.UserAction)).
// |                    (+) More robust to future support of additional modes.
// |                    (+) Single named API point communicates common intent, parameter conveys implementation/execution details.
// |                    (-) Adds more enumerated types to the API, which may not be widely used.
// |        - In other cases, prefer overloads rather than additional methods with different names (e.g., void GetItems(List<T> items) and void GetItems(T[] items, int length) instead of void GetItemsDynamic(List<T> items) and void GetItemsPreAllocated(T[] items, int length)).
// |        - Give careful consideration to method parameter names. Users can invoke methods using named parameters, and the API updater currently cannot fix these usages.
// |
// |    Definitions:
// |        - camelCase: words* capitalized, except the first
// |        - PascalCase: all words* capitalized
// |        - UPPERCASE: all letters in all words* capitalized
// |        * A "word" may only contain letters and numbers (no underscores, no Unicode characters or other symbols).
// |
// |    Readability examples:
// |        - HorizontalAlignment instead of AlignmentHorizontal (more English-readable)
// |        - CanScrollHorizontally instead of ScrollableX ('x' is somewhat obscure reference to the x axis)
// |        - DirectionalVector instead of DirVec (unnecessary and use of nonstandard abbreviation)
// |
// |[Spacing]
// |    - Space before opening parenthesis?
// |        - If it looks like a function call, no space (function calls, function definitions, typeof(), sizeof())
// |        - If it opens a scope, add a space (if, while, catch, switch, for, foreach, using, lock, fixed)
// |    - No spaces immediately inside any parens or brackets (e.g. no 'if ( foo )' or 'x = ( y * z[ 123 ] )')
// |    - Comma and semicolon spacing as in English ('int a, float b' and 'for (int i = 0; i < 10; ++i)')
// |    - Exactly one space is required after the // in a C++ style comment.
// |    - Do not add a space between a unary operator and its operand (!expr, +30, -1.4, i++, --j, &expr, *expr, (int)obj, etc.).
// |    - Do not add spaces around member access operators (a.b, a->b, etc.).
// |    - Spaces are required both before and after all other operators (math, assignment, comparison, lambdas, etc.).
// |
// |[Wrapping]
// |    - Wrap code once it gets to around 120 columns wide to keep side-by-side diffs sane (not a hard limit; use your judgment).
// |    - When necessary, break lines after boolean operators in conditional expressions, after ';' in for-statements, and after ',' in function calls
// |
// |[Comments]
// |    - Documenting the 'why' is far more important than the 'what' or 'how'.
// |    - Document anything that would surprise another engineer (or yourself in six months when you've forgotten it).
// |    - Avoid obvious comments (e.g. 'set the color', 'iterate through array')
// |    - Begin comment with an uppercase letter
// |    - Insert one space between the comment delimiter (//) and the text
// |    - Avoid checking in commented out code to your version control system, unless it's example code or frequently used debug code.
// |________________________________________________________________________________________________

// |[Usings]
// |    - Located at file scope at the top of the file, never within a namespace.
// |    - Three groups, which are, top to bottom: System, non-System, aliases. Keep each group sorted.
// |    - Strip unused 'usings' except the 'minimally-required set', which is marked with *required below.
// |    - Only use aliases when required by the compiler for disambiguation, and not for hiding rarely-used symbols behind a prefix.
using System;                                                                                       // | Not required, but strongly encouraged
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEngine;

using Component = UnityEngine.Component;                                                            // | Start of aliases group
using Debug = UnityEngine.Debug;

namespace MyProjectNamespace                                                                        // | Full contents of namespace indented
{
    // |[Enums]
    // |    - Use a singular type name, and no prefix or suffix (e.g. no E- prefix or -Enum suffix).
    // |    - Constant names should have no prefix or suffix.
    // |    - Do not specify constant values unless absolutely required (e.g. for version-safe protocols - rare).
    enum WindingOrder                                                                               // | Drop redundant access specifiers (leave off 'internal' at file scope)
    {                                                                                               // | Opening brace is always on its own line at the same level of indentation as its parent
        Clockwise,                                                                                  // | Code within the braces always indented one tab stop
        CounterClockwise,
        Charm,
        Singularity,                                                                                // | Trail last element in a list with ','
    }                                                                                               // | Closing brace is on its own line at same level of indentation as parent
                                                                                                    // | Put exactly one blank line between multi-line types

    // |[Flags enums]
    // |    - Use a plural type name, and no prefix or suffix (e.g. no E- prefix and no -Flag or -Flags suffix).
    // |    - Constant names should have no prefix or suffix.
    // |    - Use bit shift expressions for the constants (instead of 2, 4, 8, etc.)
    [Flags]
    public enum VertexStreams
    {
        Position = 1 << 0,
        Normal = 1 << 1,
        Tangent = 1 << 2,
        Color = 1 << 3,
        UV = 1 << 4,
    }

    // |[Interfaces]
    // |    - Name interfaces with adjective phrases, or occasionally with nouns or noun phrases.
    // |        - Nouns and noun phrases should be used rarely and they might indicate that the type should be an abstract class, and not an interface.
    // |    - Use 'I' prefix to indicate an interface.
    // |    - Ensure that the names differ only by the 'I' prefix on the interface name when you are defining a class-interface pair, where the class is a standard implementation of the interface.
    public interface IThingAgent
    {
        string OperationDescription { get; }
        float Scale { get; }

        // |[Methods]
        // |    - Give methods names that are verbs or verb phrases.
        // |    - Parameter names are camelCase
        bool DoThing(string propertyDescription, int spinCount);
    }

    // |[Classes]
    // |    - Name classes and structs with nouns or noun phrases.
    // |    - No prefix on class names (no 'C' or 'S' etc.).
    class Example : MonoBehaviour
    {
        // |[Fields]
        // |    - Drop redundant initializers (i.e. no '= 0' on the ints, '= null' on ref types, etc.).
        // |    - Drop redundant access specifiers (leave off 'private' at type scope).
        // |    - Never expose public fields which are not const or static readonly. These fields should be published through a property.
        // |    - Use readonly where const isn't possible.

        // | Constants
        public const int        TotalCount = 123;                                                   // | When it enhances readability, try to column-align blocks of variable definitions at symbol name and assignment tab stops
        static readonly Vector3 DefaultLength = new Vector3(1, 2, 3);                               // | static readonly is treadted the same as const in naming
        const int               MaxCount = 3;
        const string            CorrectMessage = "Correct Message";

        // Statics                                                                                  // | You can comment that each group starts from here
        static int sharedCount;                                                                     // | Note no "= 0". All memory is zero'd out by default, so do not redundantly assign.

                                                                                                    // | You can group by section using #region
        #region Fields
        int currentCount;

        [SerializeField]
        string showThisOnInspector = "Test string";
        #endregion

        // | Properties
        public string DefaultName { get { return Environment.MachineName; } }

        [Example]                                                                                   // | Drop 'Attribute' postfix when applying an attribute
        public int CurrentCount
        {
            get { return currentCount; }                                                            // | Getters are always trivial and do not mutate state (this includes first-run cached results); use a full method if you want to do calculations or caching
            set { currentCount = value; }
        }                                                                                           // | Put exactly one blank line between multi-line methods and properties

        // | Constructors
        Example()
        {
            currentCount = -1;
        }

        // | Unity LifeCycle Methods
        void Update()
        {
            UpdateSubsystems();                                                                     // | No large blocks of code in Update() - extract private methods
        }

        // | Events / Delegates
        // |[Events]
        // TODO: @Fred Please edit here appropriately
                // FYI, the below is from our guildelines.
                // |    - Use UnityEvent only when you need to use them from the inspector. Otherwise, use delegates/Actions/Funcs.
                // |    - If a UnityEvent is set to call a script method, add a comment above the method indicating that it’s going to be called by a UnityEvent and which objects are expected to call it.
                // |    - It is preferable to set up UnityEvents from the script by calling AddListener(), rather than from the inspector.
        // |    - Do not declare new delegate types. Use Action<...> instead.
        // |    - Do not expose public delegate fields. Use events instead.
        // |    - Include one participle-form verb in the event name (generally ending in -ed or -ing, ex. occurred, loading, started, given)
        // |    - *EventArgs struct parameters are not necessary, but they should be used if the data sent to the event has the possibility of needing to be changed. [FDG Exception]
        public event Action<ThingHappenedEventArgs> ThingHappened;

        #region Methods
        void UpdateSubsystems()
        {
            // | Empty method because this is an example. You should remove empty methods :-)
        }

        [Description("I do things"), DebuggerNonUserCode]                                           // | Attributes always go on a line separate from what they apply to (unless a parameter), and joining them is encouraged if they are short
        public void DoThings(IEnumerable<IThingAgent> thingsToDo, string propertyDescription)       // | For types that are already internal (like class Example), use public instead of internal for members and nested types
        {
            var doneThings = new List<IThingAgent>();                                               // | 'var' required on any 'new' where the type we want is the same as what is being constructed
            var indent = new string(' ', 4);                                                        // | ...even primitive types
                                                                                                    // | When appropriate, separate code blocks by a single empty line
            IList<string> doneDescriptions = new List<string>();                                    // | (This is a case where 'var' not required because the types of the variable vs the ctor are different)

            foreach (var thingToDo in thingsToDo)                                                   // | 'var' required in all foreach
            {
                if (!thingToDo.DoThing(propertyDescription, currentCount))
                    break;                                                                          // | Braces not required for single statements under if or else, but that single statement must be on its own line

                using (File.CreateText(@"path\to\something.txt"))                                   // | Use @"" style string literal for paths with backslashes and regular expression patterns
                using (new ComputeBuffer(10, 20))                                                   // | Don't use braces for directly nested using's
                {                                                                                   // | Braces required for deepest level of nested using's
                    doneThings.Add(thingToDo);
                }
            }

            foreach (var doneThing in doneThings)
            {                                                                                       // | Braces are required for loops (foreach, for, while, do) as well as 'fixed' and 'lock'
                doneDescriptions.Add(doneThing.OperationDescription);
                Debug.Log(indent + "Doing thing: " + doneThing.OperationDescription);               // | Prefer a + b + c over string.Concat(a, b, c)
            }
        }

        public void ControlFlow(string message, object someFoo, WindingOrder windingOrder)
        {
            for (int i = 0; i < MaxCount; ++i)                                                      // | Using i and j for trivial local iterators is encouraged
            {
                // all of this is nonsense, and is just meant to demonstrate formatting             // | Place comments about multiple lines of code directly above them, with one empty line above the comment to visually group it with its code
                if ((i % -3) - 1 == 0)                                                              // | Wrap parens around subexpressions is optional but recommended to make operator precedence clear
                {
                    ++currentCount;
                    sharedCount *= (int)DefaultLength.x + TotalCount;

                    do                                                                              // | 'while', 'do', 'for', 'foreach', 'switch' are always on a separate line from the code block they control
                    {
                        i += sharedCount;
                    }
                    while (i < currentCount);
                }
                else                                                                                // | 'else' always at same indentation level as its 'if'
                {
                    Debug.LogWarning("Skipping over " + i);                                         // | Drop 'ToString()' when not required by compiler
                    goto skip;                                                                      // | Goto's not necessarily considered harmful, not disallowed, but should be scrutinized for utility before usage
                }
            }

        skip:                                                                                       // | Goto label targets un-indented from parent scope one tab stop

            // more nonsense code for demo purposes
            switch (windingOrder)
            {
                case WindingOrder.Clockwise:                                                        // | Case labels indented under switch
                case WindingOrder.CounterClockwise:                                                 // | Braces optional if not needed for scope (but note indentation of braces and contents)
                    if (sharedCount == MaxCount)                                                    // | Constants go on the right in comparisons (do not follow 'yoda' style)
                    {
                        var warningDetails = someFoo.ToString();                                    // | 'var' for the result of assignments is optional (either way, good variable naming is most important)
                        for (var i = 0; i < sharedCount; ++i)
                        {
                            Debug.LogWarning("Spinning a " + warningDetails);
                        }
                    }
                    break;                                                                          // | 'break' inside case braces, if any

                case WindingOrder.Charm:
                    Debug.LogWarning("Check quark");                                                // | Indentation is the same, with or without scope braces
                    break;

                case WindingOrder.Singularity:
                {
                    var warningDetails = message;                                                   // | (this seemingly pointless variable is here solely to require braces on the case statements and show the required formatting)

                    if (message == CorrectMessage)
                    {
                        // Already correct so we don't need to do anything here                     // | Empty blocks should (a) only be used when it helps readability, (b) always use empty braces (never a standalone semicolon), and (c) be commented as to why the empty block is there
                    }
                    else if (currentCount > 3)
                    {
                        if (sharedCount < 10)                                                       // | Braces can only be omitted at the deepest level of nested code
                            Debug.LogWarning("Singularity! (" + warningDetails + ")");
                    }
                    else if (sharedCount > 5)                                                       // | 'else if' always on same line together
                        throw new IndexOutOfRangeException();
                    else if ((sharedCount > 7 && currentCount != 0) || message == null)             // | Always wrap subexpressions in parens when peer precedence is close enough to be ambiguous (e.g. && and || are commonly confused)
                        throw new NotImplementedException();

                    break;
                }

                default:
                    throw new InvalidOperationException("What's a " + windingOrder + "?");
            }
        }

        // |[Parameterized Types]
        // |    - When only a single parameterized type is used, naming it 'T' is acceptable.
        // |    - For more than one parameterized type, use descriptive names prefixed with 'T'.
        // |    - Consider indicating constraints placed on a type parameter in the name of the parameter.
        public static TResult Transmogrify<TResult, TComponent>(                                    // | When wrapping params, do not leave any on line with function name
            TComponent component, Func<TComponent, TResult> converter)                              // | When wrapping, only indent one stop (do not line up with paren)
            where TComponent : Component
        {
            return converter(component);
        }
        #endregion

        // | Nested types
        public string Description
        {
            get                                                                                     // | For multiline method bodies, the 'get' and 'set' keywords must be on their own line
            {
                return string.Format(
                    "shared: {0}\ncurrent: {1}\n",
                    sharedCount, currentCount);
            }
        }
    }

    // |[Structs]
    // |    - Name classes and structs with nouns or noun phrases.
    // |    - No prefix on class names (no 'C' or 'S' etc.).
    // |    - Structs may be mutable, but consider immutability when appropriate. [FDG Exception]
    struct MethodQuery
    {
        public string            Name { get; set; }
        public IEnumerable<Type> ParamTypes { get; set; }
        public Type              ReturnType { get; set; }

        public override string ToString()                                                           // | Methods generally are not permitted in structs, with exceptions like this noted in the data-oriented programming guidelines.
        {
            var paramTypeNames = ParamTypes                                                         // | Prefer fluent function call syntax over LINQ syntax (i.e. y.Select(x => z) instead of 'from x in y select z')
                .Select(p => p.ToString())                                                          // | Prefer breaking long fluent operator chains into one line per operator
                .Where(p => p.Length > 2)
                .OrderBy(p => p[0])
                .ToArray();

            return string.Format(
                "{0} {1}({2})",
                ReturnType, Name, string.Join(", ", paramTypeNames));
        }
    }

    // |[EventArgs]
    // |    - Always use structs for EventArgs types, and never extend System.EventArgs [FDG Exception]
    // |    - Make EventArgs structs immutable
    // |    - See the event example above for when to define EventArgs structs.
    struct ThingHappenedEventArgs
    {
        public string ThingThatHappened { get; }

        public ThingHappenedEventArgs(string thingThatHappened)
        {
            ThingThatHappened = thingThatHappened;
        }
    }

    // |[Attributes]
    // |    - Mark up all attributes with an AttributeUsage, as narrow as possible.
    // |    - Postfix attribute class names with "Attribute".
    [AttributeUsage(AttributeTargets.Property)]
    public class ExampleAttribute : Attribute
    {                                                                                               // | Empty types have braces on their own lines
    }

    // |[Exceptions]
    // |    - Postfix exception class names with "Exception".
    // |    - Do not inherit from ApplicationException (see http://stackoverflow.com/a/5685943/14582).
    public class ExampleException : Exception
    {
        public ExampleException() { }
        public ExampleException(string message) : base(message) { }
        public ExampleException(string message, Exception innerException) : base(message, innerException) { }
    }
}
