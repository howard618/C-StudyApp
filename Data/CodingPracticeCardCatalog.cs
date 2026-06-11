namespace FlashCards.Data;

public static class CodingPracticeCardCatalog
{
    public static CodingPracticeCard Build(string prompt)
    {
        var normalized = prompt.Trim().ToLowerInvariant();

        return normalized switch
        {
            "reverse an array" => Card(prompt, "Use in-place swapping to reverse the array.", """
                public static int[] ReverseArray(int[] numbers)
                {
                    int left = 0;
                    int right = numbers.Length - 1;

                    while (left < right)
                    {
                        (numbers[left], numbers[right]) = (numbers[right], numbers[left]);
                        left++;
                        right--;
                    }

                    return numbers;
                }
                """, "Two pointers", "Each swap fixes one value at the front and one at the back, so the array is reversed in one linear pass.", "Arrays & Strings"),
            "find the maximum element in an array" => Card(prompt, "Scan once and keep the largest value seen so far.", """
                public static int FindMax(int[] numbers)
                {
                    int max = numbers[0];

                    for (int i = 1; i < numbers.Length; i++)
                    {
                        if (numbers[i] > max)
                        {
                            max = numbers[i];
                        }
                    }

                    return max;
                }
                """, "Single pass scan", "The best answer can be updated as each element is visited, so no extra data structure is needed.", "Arrays & Strings"),
            "find duplicates in an array" => Card(prompt, "Track seen values and collect values that appear again.", """
                public static List<int> FindDuplicates(int[] numbers)
                {
                    var seen = new HashSet<int>();
                    var duplicates = new HashSet<int>();

                    foreach (int number in numbers)
                    {
                        if (!seen.Add(number))
                        {
                            duplicates.Add(number);
                        }
                    }

                    return duplicates.ToList();
                }
                """, "Hash set tracking", "A hash set remembers prior values, so duplicate checks are constant-time on average.", "Arrays & Strings"),
            "move zeros to the end" => Card(prompt, "Compact non-zero values first, then fill the rest with zeroes.", """
                public static void MoveZeros(int[] numbers)
                {
                    int writeIndex = 0;

                    for (int readIndex = 0; readIndex < numbers.Length; readIndex++)
                    {
                        if (numbers[readIndex] != 0)
                        {
                            numbers[writeIndex++] = numbers[readIndex];
                        }
                    }

                    while (writeIndex < numbers.Length)
                    {
                        numbers[writeIndex++] = 0;
                    }
                }
                """, "Two pointers", "The read pointer scans every value while the write pointer marks the next non-zero slot.", "Arrays & Strings"),
            "check if two strings are anagrams" => Card(prompt, "Count characters and ensure both strings have identical counts.", """
                public static bool AreAnagrams(string first, string second)
                {
                    if (first.Length != second.Length)
                    {
                        return false;
                    }

                    var counts = new Dictionary<char, int>();

                    foreach (char character in first)
                    {
                        counts[character] = counts.GetValueOrDefault(character) + 1;
                    }

                    foreach (char character in second)
                    {
                        if (!counts.ContainsKey(character) || counts[character] == 0)
                        {
                            return false;
                        }

                        counts[character]--;
                    }

                    return true;
                }
                """, "Character frequency", "Anagrams contain the same characters the same number of times, and counting captures that directly.", "Arrays & Strings"),
            "reverse a string" => Card(prompt, "Convert to a character array and reverse it with two pointers.", """
                public static string ReverseString(string text)
                {
                    char[] characters = text.ToCharArray();
                    int left = 0;
                    int right = characters.Length - 1;

                    while (left < right)
                    {
                        (characters[left], characters[right]) = (characters[right], characters[left]);
                        left++;
                        right--;
                    }

                    return new string(characters);
                }
                """, "Two pointers", "Strings are immutable in C#, so the character array gives you mutable positions to swap.", "Arrays & Strings"),
            "find the first non-repeating character" => Card(prompt, "Count every character, then scan again for the first count of one.", """
                public static char? FirstNonRepeatingCharacter(string text)
                {
                    var counts = new Dictionary<char, int>();

                    foreach (char character in text)
                    {
                        counts[character] = counts.GetValueOrDefault(character) + 1;
                    }

                    foreach (char character in text)
                    {
                        if (counts[character] == 1)
                        {
                            return character;
                        }
                    }

                    return null;
                }
                """, "Frequency map", "The first pass knows uniqueness; the second preserves the original order.", "Arrays & Strings"),
            "remove duplicates from a sorted array" => Card(prompt, "Use a write pointer to keep only the first copy of each value.", """
                public static int RemoveDuplicates(int[] numbers)
                {
                    if (numbers.Length == 0)
                    {
                        return 0;
                    }

                    int writeIndex = 1;

                    for (int readIndex = 1; readIndex < numbers.Length; readIndex++)
                    {
                        if (numbers[readIndex] != numbers[readIndex - 1])
                        {
                            numbers[writeIndex++] = numbers[readIndex];
                        }
                    }

                    return writeIndex;
                }
                """, "Two pointers", "Because duplicates are adjacent in a sorted array, a single scan can compact unique values.", "Arrays & Strings"),
            "rotate an array by k positions" => Card(prompt, "Reverse the whole array, then reverse each rotated segment.", """
                public static void Rotate(int[] numbers, int k)
                {
                    if (numbers.Length == 0)
                    {
                        return;
                    }

                    k %= numbers.Length;
                    Array.Reverse(numbers);
                    Array.Reverse(numbers, 0, k);
                    Array.Reverse(numbers, k, numbers.Length - k);
                }
                """, "Array reversal", "Three reversals move the last k values to the front while preserving both segment orders.", "Arrays & Strings"),
            "find the missing number in an array" => Card(prompt, "Compare the expected range sum with the actual array sum.", """
                public static int MissingNumber(int[] numbers)
                {
                    int n = numbers.Length;
                    int expected = n * (n + 1) / 2;
                    int actual = numbers.Sum();

                    return expected - actual;
                }
                """, "Math / checksum", "For numbers from 0 through n, the sum formula gives the complete total, so the difference is the missing value.", "Arrays & Strings"),
            "two sum problem" or "two sum" => Card(prompt, "Store previous numbers so complements can be found quickly.", """
                public static int[] TwoSum(int[] numbers, int target)
                {
                    var seen = new Dictionary<int, int>();

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        int complement = target - numbers[i];

                        if (seen.TryGetValue(complement, out int index))
                        {
                            return new[] { index, i };
                        }

                        seen[numbers[i]] = i;
                    }

                    return Array.Empty<int>();
                }
                """, "Hash map / lookup table", "Each number asks whether its matching complement has already appeared, avoiding a nested loop.", "Arrays & Strings"),
            "product of array except self" => Card(prompt, "Build prefix products and multiply them by suffix products.", """
                public static int[] ProductExceptSelf(int[] numbers)
                {
                    int[] result = new int[numbers.Length];
                    int prefix = 1;

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        result[i] = prefix;
                        prefix *= numbers[i];
                    }

                    int suffix = 1;
                    for (int i = numbers.Length - 1; i >= 0; i--)
                    {
                        result[i] *= suffix;
                        suffix *= numbers[i];
                    }

                    return result;
                }
                """, "Prefix and suffix products", "The product before and after each index can be combined without division.", "Arrays & Strings"),
            "longest substring without repeating characters" => Card(prompt, "Maintain a window containing only unique characters.", """
                public static int LengthOfLongestSubstring(string text)
                {
                    var lastSeen = new Dictionary<char, int>();
                    int left = 0;
                    int best = 0;

                    for (int right = 0; right < text.Length; right++)
                    {
                        if (lastSeen.TryGetValue(text[right], out int index) && index >= left)
                        {
                            left = index + 1;
                        }

                        lastSeen[text[right]] = right;
                        best = Math.Max(best, right - left + 1);
                    }

                    return best;
                }
                """, "Sliding window", "When a duplicate appears, the left edge jumps past its previous position, keeping the window valid.", "Arrays & Strings"),
            "container with most water" => Card(prompt, "Move the pointer at the shorter line inward after each area check.", """
                public static int MaxArea(int[] heights)
                {
                    int left = 0;
                    int right = heights.Length - 1;
                    int best = 0;

                    while (left < right)
                    {
                        int area = Math.Min(heights[left], heights[right]) * (right - left);
                        best = Math.Max(best, area);

                        if (heights[left] < heights[right])
                        {
                            left++;
                        }
                        else
                        {
                            right--;
                        }
                    }

                    return best;
                }
                """, "Two pointers", "Moving the shorter wall is the only move that can possibly improve the limiting height.", "Arrays & Strings"),
            "group anagrams" => Card(prompt, "Use each word's sorted letters as the grouping key.", """
                public static IList<IList<string>> GroupAnagrams(string[] words)
                {
                    var groups = new Dictionary<string, IList<string>>();

                    foreach (string word in words)
                    {
                        string key = new string(word.OrderBy(character => character).ToArray());

                        if (!groups.ContainsKey(key))
                        {
                            groups[key] = new List<string>();
                        }

                        groups[key].Add(word);
                    }

                    return groups.Values.ToList();
                }
                """, "Canonical key", "Anagrams share the same sorted character signature, so equal keys belong together.", "Arrays & Strings"),
            "merge intervals" => Card(prompt, "Sort intervals by start, then merge overlapping ranges.", """
                public static int[][] Merge(int[][] intervals)
                {
                    Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
                    var merged = new List<int[]>();

                    foreach (int[] interval in intervals)
                    {
                        if (merged.Count == 0 || merged[^1][1] < interval[0])
                        {
                            merged.Add(interval);
                        }
                        else
                        {
                            merged[^1][1] = Math.Max(merged[^1][1], interval[1]);
                        }
                    }

                    return merged.ToArray();
                }
                """, "Sort and sweep", "Sorting puts possible overlaps next to each other, so one pass can combine them.", "Arrays & Strings"),
            "spiral matrix traversal" => Card(prompt, "Walk the matrix boundaries inward layer by layer.", """
                public static IList<int> SpiralOrder(int[][] matrix)
                {
                    var result = new List<int>();
                    int top = 0, bottom = matrix.Length - 1;
                    int left = 0, right = matrix[0].Length - 1;

                    while (top <= bottom && left <= right)
                    {
                        for (int col = left; col <= right; col++) result.Add(matrix[top][col]);
                        top++;

                        for (int row = top; row <= bottom; row++) result.Add(matrix[row][right]);
                        right--;

                        if (top <= bottom)
                        {
                            for (int col = right; col >= left; col--) result.Add(matrix[bottom][col]);
                            bottom--;
                        }

                        if (left <= right)
                        {
                            for (int row = bottom; row >= top; row--) result.Add(matrix[row][left]);
                            left++;
                        }
                    }

                    return result;
                }
                """, "Boundary simulation", "Shrinking the top, right, bottom, and left bounds prevents revisiting cells.", "Arrays & Strings"),
            "set matrix zeroes" => Card(prompt, "Use the first row and column as zero markers.", """
                public static void SetZeroes(int[][] matrix)
                {
                    bool firstRowZero = matrix[0].Any(value => value == 0);
                    bool firstColZero = matrix.Any(row => row[0] == 0);

                    for (int row = 1; row < matrix.Length; row++)
                    {
                        for (int col = 1; col < matrix[0].Length; col++)
                        {
                            if (matrix[row][col] == 0)
                            {
                                matrix[row][0] = 0;
                                matrix[0][col] = 0;
                            }
                        }
                    }

                    for (int row = 1; row < matrix.Length; row++)
                    {
                        for (int col = 1; col < matrix[0].Length; col++)
                        {
                            if (matrix[row][0] == 0 || matrix[0][col] == 0)
                            {
                                matrix[row][col] = 0;
                            }
                        }
                    }

                    if (firstRowZero) Array.Fill(matrix[0], 0);
                    if (firstColZero)
                    {
                        for (int row = 0; row < matrix.Length; row++) matrix[row][0] = 0;
                    }
                }
                """, "In-place markers", "The first row and column store which rows and columns must be cleared without extra marker arrays.", "Arrays & Strings"),
            "longest consecutive sequence" => Card(prompt, "Start sequences only from numbers that have no predecessor.", """
                public static int LongestConsecutive(int[] numbers)
                {
                    var values = new HashSet<int>(numbers);
                    int best = 0;

                    foreach (int number in values)
                    {
                        if (values.Contains(number - 1))
                        {
                            continue;
                        }

                        int current = number;
                        int length = 1;

                        while (values.Contains(current + 1))
                        {
                            current++;
                            length++;
                        }

                        best = Math.Max(best, length);
                    }

                    return best;
                }
                """, "Hash set sequence starts", "Only checking true sequence starts keeps each number involved in at most one forward walk.", "Arrays & Strings"),
            "encode and decode strings" => Card(prompt, "Prefix each string with its length and a delimiter.", """
                public static string Encode(IList<string> values)
                {
                    return string.Concat(values.Select(value => $"{value.Length}#{value}"));
                }

                public static IList<string> Decode(string encoded)
                {
                    var result = new List<string>();
                    int index = 0;

                    while (index < encoded.Length)
                    {
                        int delimiter = encoded.IndexOf('#', index);
                        int length = int.Parse(encoded[index..delimiter]);
                        index = delimiter + 1;
                        result.Add(encoded.Substring(index, length));
                        index += length;
                    }

                    return result;
                }
                """, "Length-prefix encoding", "The stored length tells the decoder exactly how many characters belong to the next string.", "Arrays & Strings"),
            "reverse a linked list" => Card(prompt, "Reverse each node's next pointer while walking the list.", """
                public static ListNode ReverseList(ListNode head)
                {
                    ListNode previous = null;
                    ListNode current = head;

                    while (current != null)
                    {
                        ListNode next = current.Next;
                        current.Next = previous;
                        previous = current;
                        current = next;
                    }

                    return previous;
                }
                """, "Iterative pointer reversal", "Holding previous, current, and next lets you safely redirect links without losing the rest of the list.", "Linked Lists"),
            "detect a cycle in linked list" => Card(prompt, "Move a slow pointer and a fast pointer until they meet or the list ends.", """
                public static bool HasCycle(ListNode head)
                {
                    ListNode slow = head;
                    ListNode fast = head;

                    while (fast?.Next != null)
                    {
                        slow = slow.Next;
                        fast = fast.Next.Next;

                        if (slow == fast)
                        {
                            return true;
                        }
                    }

                    return false;
                }
                """, "Floyd's cycle detection", "In a cycle, the faster pointer eventually laps the slower pointer.", "Linked Lists"),
            "find middle node" => Card(prompt, "Advance one pointer twice as fast as the other.", """
                public static ListNode MiddleNode(ListNode head)
                {
                    ListNode slow = head;
                    ListNode fast = head;

                    while (fast?.Next != null)
                    {
                        slow = slow.Next;
                        fast = fast.Next.Next;
                    }

                    return slow;
                }
                """, "Slow and fast pointers", "When the fast pointer reaches the end, the slow pointer has traveled half the distance.", "Linked Lists"),
            "merge two sorted linked lists" => Card(prompt, "Use a dummy head and attach the smaller current node each time.", """
                public static ListNode MergeTwoLists(ListNode first, ListNode second)
                {
                    var dummy = new ListNode(0);
                    ListNode tail = dummy;

                    while (first != null && second != null)
                    {
                        if (first.Value <= second.Value)
                        {
                            tail.Next = first;
                            first = first.Next;
                        }
                        else
                        {
                            tail.Next = second;
                            second = second.Next;
                        }

                        tail = tail.Next;
                    }

                    tail.Next = first ?? second;
                    return dummy.Next;
                }
                """, "Merge step", "Both lists are already sorted, so the smallest remaining head is always the next output node.", "Linked Lists"),
            "remove duplicates from linked list" => Card(prompt, "Walk a sorted linked list and skip repeated next nodes.", """
                public static ListNode DeleteDuplicates(ListNode head)
                {
                    ListNode current = head;

                    while (current?.Next != null)
                    {
                        if (current.Value == current.Next.Value)
                        {
                            current.Next = current.Next.Next;
                        }
                        else
                        {
                            current = current.Next;
                        }
                    }

                    return head;
                }
                """, "Pointer skip", "In a sorted list, duplicates sit together, so repeated next nodes can be bypassed in place.", "Linked Lists"),
            "remove nth node from end" => Card(prompt, "Keep two pointers n nodes apart, then remove the node after the slow pointer.", """
                public static ListNode RemoveNthFromEnd(ListNode head, int n)
                {
                    var dummy = new ListNode(0) { Next = head };
                    ListNode fast = dummy;
                    ListNode slow = dummy;

                    for (int i = 0; i <= n; i++)
                    {
                        fast = fast.Next;
                    }

                    while (fast != null)
                    {
                        fast = fast.Next;
                        slow = slow.Next;
                    }

                    slow.Next = slow.Next.Next;
                    return dummy.Next;
                }
                """, "Two pointers with gap", "The n-node gap means slow lands just before the target when fast reaches the end.", "Linked Lists"),
            "reorder linked list" => Card(prompt, "Split the list, reverse the second half, then weave both halves together.", """
                public static void ReorderList(ListNode head)
                {
                    ListNode slow = head;
                    ListNode fast = head.Next;

                    while (fast?.Next != null)
                    {
                        slow = slow.Next;
                        fast = fast.Next.Next;
                    }

                    ListNode second = Reverse(slow.Next);
                    slow.Next = null;
                    ListNode first = head;

                    while (second != null)
                    {
                        ListNode nextFirst = first.Next;
                        ListNode nextSecond = second.Next;
                        first.Next = second;
                        second.Next = nextFirst;
                        first = nextFirst;
                        second = nextSecond;
                    }
                }
                """, "Split, reverse, merge", "Reversing the back half makes the desired outside-in order available with a simple alternating merge.", "Linked Lists"),
            "copy linked list with random pointer" => Card(prompt, "Map each original node to its copied node, then wire next and random links.", """
                public static RandomNode CopyRandomList(RandomNode head)
                {
                    var copies = new Dictionary<RandomNode, RandomNode>();

                    for (RandomNode current = head; current != null; current = current.Next)
                    {
                        copies[current] = new RandomNode(current.Value);
                    }

                    for (RandomNode current = head; current != null; current = current.Next)
                    {
                        copies[current].Next = current.Next is null ? null : copies[current.Next];
                        copies[current].Random = current.Random is null ? null : copies[current.Random];
                    }

                    return head is null ? null : copies[head];
                }
                """, "Hash map node cloning", "The map preserves identity, so random pointers can target the corresponding copied nodes.", "Linked Lists"),
            "add two numbers represented by linked lists" => Card(prompt, "Add digit by digit while carrying overflow to the next node.", """
                public static ListNode AddTwoNumbers(ListNode first, ListNode second)
                {
                    var dummy = new ListNode(0);
                    ListNode tail = dummy;
                    int carry = 0;

                    while (first != null || second != null || carry != 0)
                    {
                        int sum = carry + (first?.Value ?? 0) + (second?.Value ?? 0);
                        carry = sum / 10;
                        tail.Next = new ListNode(sum % 10);
                        tail = tail.Next;
                        first = first?.Next;
                        second = second?.Next;
                    }

                    return dummy.Next;
                }
                """, "Elementary addition", "Each list node acts like a digit, so the same carry logic used in hand addition applies.", "Linked Lists"),
            "intersection of two linked lists" => Card(prompt, "Switch each pointer to the other list after it reaches the end.", """
                public static ListNode GetIntersectionNode(ListNode first, ListNode second)
                {
                    ListNode a = first;
                    ListNode b = second;

                    while (a != b)
                    {
                        a = a is null ? second : a.Next;
                        b = b is null ? first : b.Next;
                    }

                    return a;
                }
                """, "Pointer length balancing", "After switching lists, both pointers travel the same total distance and align at the intersection.", "Linked Lists"),
            "valid parentheses" => Card(prompt, "Push opening brackets and require each closing bracket to match the top.", """
                public static bool IsValid(string text)
                {
                    var stack = new Stack<char>();
                    var pairs = new Dictionary<char, char>
                    {
                        [')'] = '(',
                        [']'] = '[',
                        ['}'] = '{'
                    };

                    foreach (char character in text)
                    {
                        if (pairs.ContainsValue(character))
                        {
                            stack.Push(character);
                        }
                        else if (!stack.TryPop(out char open) || open != pairs[character])
                        {
                            return false;
                        }
                    }

                    return stack.Count == 0;
                }
                """, "Stack matching", "The most recent unmatched opening bracket must close first, which is exactly stack behavior.", "Stack & Queue"),
            "min stack implementation" => Card(prompt, "Store a second stack of minimum values alongside the regular stack.", """
                public class MinStack
                {
                    private readonly Stack<int> values = new();
                    private readonly Stack<int> minimums = new();

                    public void Push(int value)
                    {
                        values.Push(value);
                        minimums.Push(minimums.Count == 0 ? value : Math.Min(value, minimums.Peek()));
                    }

                    public void Pop()
                    {
                        values.Pop();
                        minimums.Pop();
                    }

                    public int Top() => values.Peek();
                    public int GetMin() => minimums.Peek();
                }
                """, "Auxiliary stack", "The minimum stack mirrors each push, so the current minimum is always available in constant time.", "Stack & Queue"),
            "evaluate reverse polish notation" => Card(prompt, "Use a stack and apply operators to the two most recent numbers.", """
                public static int EvalRpn(string[] tokens)
                {
                    var stack = new Stack<int>();

                    foreach (string token in tokens)
                    {
                        if (int.TryParse(token, out int number))
                        {
                            stack.Push(number);
                            continue;
                        }

                        int right = stack.Pop();
                        int left = stack.Pop();
                        stack.Push(token switch
                        {
                            "+" => left + right,
                            "-" => left - right,
                            "*" => left * right,
                            "/" => left / right,
                            _ => throw new InvalidOperationException()
                        });
                    }

                    return stack.Pop();
                }
                """, "Stack evaluation", "RPN places operands before the operator, so the needed values are always on top of the stack.", "Stack & Queue"),
            "daily temperatures" => Card(prompt, "Keep a stack of unresolved day indices with decreasing temperatures.", """
                public static int[] DailyTemperatures(int[] temperatures)
                {
                    int[] waits = new int[temperatures.Length];
                    var stack = new Stack<int>();

                    for (int day = 0; day < temperatures.Length; day++)
                    {
                        while (stack.Count > 0 && temperatures[day] > temperatures[stack.Peek()])
                        {
                            int previous = stack.Pop();
                            waits[previous] = day - previous;
                        }

                        stack.Push(day);
                    }

                    return waits;
                }
                """, "Monotonic stack", "When a warmer day appears, it resolves every colder day waiting on top of the stack.", "Stack & Queue"),
            "largest rectangle in histogram" => Card(prompt, "Use a monotonic stack to find each bar's widest valid rectangle.", """
                public static int LargestRectangleArea(int[] heights)
                {
                    var stack = new Stack<int>();
                    int best = 0;

                    for (int i = 0; i <= heights.Length; i++)
                    {
                        int current = i == heights.Length ? 0 : heights[i];

                        while (stack.Count > 0 && current < heights[stack.Peek()])
                        {
                            int height = heights[stack.Pop()];
                            int left = stack.Count == 0 ? -1 : stack.Peek();
                            best = Math.Max(best, height * (i - left - 1));
                        }

                        stack.Push(i);
                    }

                    return best;
                }
                """, "Monotonic stack", "Popping a bar when a shorter bar appears reveals the maximum width where that bar is the limiting height.", "Stack & Queue"),
            "implement queue using stacks" => Card(prompt, "Use one stack for incoming values and one stack for outgoing values.", """
                public class MyQueue
                {
                    private readonly Stack<int> input = new();
                    private readonly Stack<int> output = new();

                    public void Push(int value) => input.Push(value);

                    public int Pop()
                    {
                        MoveIfNeeded();
                        return output.Pop();
                    }

                    public int Peek()
                    {
                        MoveIfNeeded();
                        return output.Peek();
                    }

                    public bool Empty() => input.Count == 0 && output.Count == 0;

                    private void MoveIfNeeded()
                    {
                        if (output.Count == 0)
                        {
                            while (input.Count > 0) output.Push(input.Pop());
                        }
                    }
                }
                """, "Two-stack queue", "Moving values from input to output reverses order once, producing FIFO behavior.", "Stack & Queue"),
            "design circular queue" => Card(prompt, "Keep a fixed array plus head, count, and capacity.", """
                public class MyCircularQueue
                {
                    private readonly int[] values;
                    private int head;
                    private int count;

                    public MyCircularQueue(int k) => values = new int[k];

                    public bool EnQueue(int value)
                    {
                        if (IsFull()) return false;
                        values[(head + count) % values.Length] = value;
                        count++;
                        return true;
                    }

                    public bool DeQueue()
                    {
                        if (IsEmpty()) return false;
                        head = (head + 1) % values.Length;
                        count--;
                        return true;
                    }

                    public int Front() => IsEmpty() ? -1 : values[head];
                    public int Rear() => IsEmpty() ? -1 : values[(head + count - 1) % values.Length];
                    public bool IsEmpty() => count == 0;
                    public bool IsFull() => count == values.Length;
                }
                """, "Circular buffer", "Modulo arithmetic wraps indices around the fixed array without moving existing values.", "Stack & Queue"),
            "sliding window maximum" => Card(prompt, "Keep candidate indices in decreasing value order.", """
                public static int[] MaxSlidingWindow(int[] numbers, int k)
                {
                    var deque = new LinkedList<int>();
                    var result = new List<int>();

                    for (int right = 0; right < numbers.Length; right++)
                    {
                        while (deque.Count > 0 && deque.First!.Value <= right - k) deque.RemoveFirst();
                        while (deque.Count > 0 && numbers[deque.Last!.Value] <= numbers[right]) deque.RemoveLast();

                        deque.AddLast(right);

                        if (right >= k - 1)
                        {
                            result.Add(numbers[deque.First!.Value]);
                        }
                    }

                    return result.ToArray();
                }
                """, "Monotonic deque", "The deque front is always the largest valid value in the current window.", "Stack & Queue"),
            "rotten oranges problem" => Card(prompt, "Run BFS from all initially rotten oranges at the same time.", """
                public static int OrangesRotting(int[][] grid)
                {
                    var queue = new Queue<(int Row, int Col)>();
                    int fresh = 0;

                    for (int row = 0; row < grid.Length; row++)
                    for (int col = 0; col < grid[0].Length; col++)
                    {
                        if (grid[row][col] == 2) queue.Enqueue((row, col));
                        if (grid[row][col] == 1) fresh++;
                    }

                    int minutes = 0;
                    int[][] directions = { new[] { 1, 0 }, new[] { -1, 0 }, new[] { 0, 1 }, new[] { 0, -1 } };

                    while (queue.Count > 0 && fresh > 0)
                    {
                        for (int i = queue.Count; i > 0; i--)
                        {
                            var (row, col) = queue.Dequeue();
                            foreach (int[] direction in directions)
                            {
                                int nextRow = row + direction[0];
                                int nextCol = col + direction[1];
                                if (nextRow < 0 || nextCol < 0 || nextRow == grid.Length || nextCol == grid[0].Length || grid[nextRow][nextCol] != 1) continue;
                                grid[nextRow][nextCol] = 2;
                                fresh--;
                                queue.Enqueue((nextRow, nextCol));
                            }
                        }

                        minutes++;
                    }

                    return fresh == 0 ? minutes : -1;
                }
                """, "Multi-source BFS", "Each BFS layer represents one minute of spread from every rotten orange at once.", "Stack & Queue"),
            "contains duplicate" => Card(prompt, "Try adding every number to a set and stop when one is already present.", """
                public static bool ContainsDuplicate(int[] numbers)
                {
                    var seen = new HashSet<int>();

                    foreach (int number in numbers)
                    {
                        if (!seen.Add(number))
                        {
                            return true;
                        }
                    }

                    return false;
                }
                """, "Hash set tracking", "A set can tell whether a value has appeared before without scanning previous elements.", "Hash Tables"),
            "happy number" => Card(prompt, "Repeatedly replace the number with the sum of squared digits and detect loops.", """
                public static bool IsHappy(int number)
                {
                    var seen = new HashSet<int>();

                    while (number != 1 && seen.Add(number))
                    {
                        int next = 0;

                        while (number > 0)
                        {
                            int digit = number % 10;
                            next += digit * digit;
                            number /= 10;
                        }

                        number = next;
                    }

                    return number == 1;
                }
                """, "Cycle detection with a set", "If the process repeats a number before reaching one, it is trapped in a cycle.", "Hash Tables"),
            "isomorphic strings" => Card(prompt, "Maintain a one-to-one mapping in both directions.", """
                public static bool IsIsomorphic(string first, string second)
                {
                    var forward = new Dictionary<char, char>();
                    var backward = new Dictionary<char, char>();

                    for (int i = 0; i < first.Length; i++)
                    {
                        char a = first[i];
                        char b = second[i];

                        if (forward.TryGetValue(a, out char mapped) && mapped != b) return false;
                        if (backward.TryGetValue(b, out char reverseMapped) && reverseMapped != a) return false;

                        forward[a] = b;
                        backward[b] = a;
                    }

                    return true;
                }
                """, "Bidirectional map", "Both maps prevent two characters from collapsing into the same target character.", "Hash Tables"),
            "top k frequent elements" => Card(prompt, "Count values, then take the k highest-frequency entries.", """
                public static int[] TopKFrequent(int[] numbers, int k)
                {
                    var counts = new Dictionary<int, int>();

                    foreach (int number in numbers)
                    {
                        counts[number] = counts.GetValueOrDefault(number) + 1;
                    }

                    return counts.OrderByDescending(pair => pair.Value)
                        .Take(k)
                        .Select(pair => pair.Key)
                        .ToArray();
                }
                """, "Frequency map", "Counting turns the problem into selecting the largest frequencies.", "Hash Tables"),
            "subarray sum equals k" => Card(prompt, "Track how often each prefix sum has appeared.", """
                public static int SubarraySum(int[] numbers, int k)
                {
                    var prefixCounts = new Dictionary<int, int> { [0] = 1 };
                    int sum = 0;
                    int count = 0;

                    foreach (int number in numbers)
                    {
                        sum += number;
                        count += prefixCounts.GetValueOrDefault(sum - k);
                        prefixCounts[sum] = prefixCounts.GetValueOrDefault(sum) + 1;
                    }

                    return count;
                }
                """, "Prefix sum with hash map", "If currentSum - k appeared earlier, the values between that point and now sum to k.", "Hash Tables"),
            "inorder traversal" => Card(prompt, "Visit left subtree, current node, then right subtree.", """
                public static IList<int> InorderTraversal(TreeNode root)
                {
                    var result = new List<int>();
                    Traverse(root);
                    return result;

                    void Traverse(TreeNode node)
                    {
                        if (node is null) return;
                        Traverse(node.Left);
                        result.Add(node.Value);
                        Traverse(node.Right);
                    }
                }
                """, "Depth-first traversal", "The recursive call order mirrors the required left-node-right visitation order.", "Trees"),
            "preorder traversal" => Card(prompt, "Visit the current node before its children.", """
                public static IList<int> PreorderTraversal(TreeNode root)
                {
                    var result = new List<int>();
                    Traverse(root);
                    return result;

                    void Traverse(TreeNode node)
                    {
                        if (node is null) return;
                        result.Add(node.Value);
                        Traverse(node.Left);
                        Traverse(node.Right);
                    }
                }
                """, "Depth-first traversal", "Preorder processes the root first, then recursively handles left and right subtrees.", "Trees"),
            "postorder traversal" => Card(prompt, "Visit children before the current node.", """
                public static IList<int> PostorderTraversal(TreeNode root)
                {
                    var result = new List<int>();
                    Traverse(root);
                    return result;

                    void Traverse(TreeNode node)
                    {
                        if (node is null) return;
                        Traverse(node.Left);
                        Traverse(node.Right);
                        result.Add(node.Value);
                    }
                }
                """, "Depth-first traversal", "Postorder waits until both subtrees are done before adding the node itself.", "Trees"),
            "level order traversal" => Card(prompt, "Use a queue to process the tree one level at a time.", """
                public static IList<IList<int>> LevelOrder(TreeNode root)
                {
                    var result = new List<IList<int>>();
                    if (root is null) return result;

                    var queue = new Queue<TreeNode>();
                    queue.Enqueue(root);

                    while (queue.Count > 0)
                    {
                        var level = new List<int>();

                        for (int i = queue.Count; i > 0; i--)
                        {
                            TreeNode node = queue.Dequeue();
                            level.Add(node.Value);
                            if (node.Left != null) queue.Enqueue(node.Left);
                            if (node.Right != null) queue.Enqueue(node.Right);
                        }

                        result.Add(level);
                    }

                    return result;
                }
                """, "Breadth-first search", "A queue preserves level order because children are processed after all nodes already in the queue.", "Trees"),
            "maximum depth of binary tree" => Card(prompt, "Recursively compute the deeper child depth plus one.", """
                public static int MaxDepth(TreeNode root)
                {
                    if (root is null)
                    {
                        return 0;
                    }

                    return 1 + Math.Max(MaxDepth(root.Left), MaxDepth(root.Right));
                }
                """, "Recursive tree height", "Each node's depth depends only on the greater depth of its two subtrees.", "Trees"),
            "invert binary tree" => Card(prompt, "Swap every node's left and right children recursively.", """
                public static TreeNode InvertTree(TreeNode root)
                {
                    if (root is null)
                    {
                        return null;
                    }

                    (root.Left, root.Right) = (InvertTree(root.Right), InvertTree(root.Left));
                    return root;
                }
                """, "Recursive subtree swap", "Inverting a tree means inverting both subtrees and exchanging their positions at every node.", "Trees"),
            "check if trees are identical" => Card(prompt, "Compare both trees node by node in the same positions.", """
                public static bool IsSameTree(TreeNode first, TreeNode second)
                {
                    if (first is null || second is null)
                    {
                        return first == second;
                    }

                    return first.Value == second.Value
                        && IsSameTree(first.Left, second.Left)
                        && IsSameTree(first.Right, second.Right);
                }
                """, "Parallel DFS", "Two trees are identical only when each matching node has the same value and matching child structure.", "Trees"),
            "climbing stairs" => Card(prompt, "Each step count equals the sum of the previous two counts.", """
                public static int ClimbStairs(int n)
                {
                    if (n <= 2)
                    {
                        return n;
                    }

                    int oneStepBack = 2;
                    int twoStepsBack = 1;

                    for (int step = 3; step <= n; step++)
                    {
                        int current = oneStepBack + twoStepsBack;
                        twoStepsBack = oneStepBack;
                        oneStepBack = current;
                    }

                    return oneStepBack;
                }
                """, "Dynamic programming", "The last move is either one step from n - 1 or two steps from n - 2, creating a Fibonacci-style recurrence.", "Dynamic Programming"),
            "house robber" => Card(prompt, "At each house, choose between robbing it or keeping the previous best.", """
                public static int Rob(int[] houses)
                {
                    int skip = 0;
                    int take = 0;

                    foreach (int money in houses)
                    {
                        int nextTake = skip + money;
                        skip = Math.Max(skip, take);
                        take = nextTake;
                    }

                    return Math.Max(skip, take);
                }
                """, "Dynamic programming", "The choice at each house depends on the best result with and without robbing the previous house.", "Dynamic Programming"),
            "coin change" => Card(prompt, "Build the fewest coins needed for every amount up to the target.", """
                public static int CoinChange(int[] coins, int amount)
                {
                    int[] dp = Enumerable.Repeat(amount + 1, amount + 1).ToArray();
                    dp[0] = 0;

                    for (int current = 1; current <= amount; current++)
                    {
                        foreach (int coin in coins)
                        {
                            if (current >= coin)
                            {
                                dp[current] = Math.Min(dp[current], dp[current - coin] + 1);
                            }
                        }
                    }

                    return dp[amount] > amount ? -1 : dp[amount];
                }
                """, "Bottom-up DP", "Each amount reuses the best answer for amount minus one chosen coin.", "Dynamic Programming"),
            "maximum subarray" => Card(prompt, "Track the best subarray ending here and the best seen overall.", """
                public static int MaxSubArray(int[] numbers)
                {
                    int current = numbers[0];
                    int best = numbers[0];

                    for (int i = 1; i < numbers.Length; i++)
                    {
                        current = Math.Max(numbers[i], current + numbers[i]);
                        best = Math.Max(best, current);
                    }

                    return best;
                }
                """, "Kadane's algorithm", "A subarray ending at the current index should either extend the previous one or start fresh.", "Dynamic Programming"),
            "unique paths" => Card(prompt, "Count paths to each grid cell from the top and left neighbors.", """
                public static int UniquePaths(int rows, int columns)
                {
                    int[] dp = Enumerable.Repeat(1, columns).ToArray();

                    for (int row = 1; row < rows; row++)
                    {
                        for (int col = 1; col < columns; col++)
                        {
                            dp[col] += dp[col - 1];
                        }
                    }

                    return dp[^1];
                }
                """, "Grid DP", "Every cell can only be reached from above or left, so those path counts add together.", "Dynamic Programming"),
            _ => Card(prompt, "Break the prompt into inputs, outputs, constraints, and the core data-structure pattern.", """
                public static void Solve()
                {
                    // Identify the input shape, choose the pattern, and implement the core loop.
                    // Add edge-case checks before coding the final interview solution.
                }
                """, "Pattern extraction", "Naming the pattern first makes it easier to pick the right state, traversal, or lookup structure.", "General")
        };
    }

    private static CodingPracticeCard Card(
        string prompt,
        string shortSummary,
        string implementation,
        string pattern,
        string whyItWorks,
        string topic)
    {
        return new CodingPracticeCard(prompt, shortSummary, implementation, pattern, whyItWorks, topic);
    }
}
