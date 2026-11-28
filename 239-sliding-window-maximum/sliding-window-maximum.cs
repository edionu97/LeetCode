
public class Solution 
{
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        // Handle the case in which the window size is 1, in which case the maximum value in each window is simply the value itself.
        if (k == 1)
        {
            return nums;
        }

        int[] result = new int[nums.Length - k + 1];

        DequeSlidingWindow<WindowElement> deque = new(k);

        for (int i = 0; i < nums.Length; ++i)
        {
            // Remove the element from the front of the queue if it is outside the current window.
            if (deque.Count > 0 && deque.Front.Index <= i - k)
            {
                deque.RemoveFront();
            }

            // Remove element from the back of the queue as long as the current element is greater than the last element in the queue.
            // The reason for this is that if the current element is greater than the last element in the queue, then the last element in the queue can never be the maximum value in any of the subsequent windows.
            while (deque.Count > 0 && nums[i] > deque.Back.Value)
            {
                deque.RemoveBack();
            }

            deque.AddBack(new WindowElement(nums[i], i));

            if (i >= k - 1)
            {
                result[i - k + 1] = deque.Front.Value;
            }
        }

        return result;
    }

    private sealed class DequeSlidingWindow<T>(int capacity)
    {
        private int front = 0, back = 0;

        private readonly T[] buffer = new T[capacity];

        /// <inheritdoc/>
        public int Capacity => capacity;

        /// <inheritdoc/>
        public int Count
        {
            get;
            private set
            {
                // Everytime the count is 0 we need to reset the pointers to first position from buffer
                if (value == 0)
                {
                    front = back = 0;
                }

                field = value;
            }
        }

        /// <inheritdoc/>
        public T Front
        {
            get
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException("Window is empty");
                }

                return buffer[front];
            }
        }

        /// <inheritdoc/>
        public T Back
        {
            get
            {
                if (Count == 0)
                {
                    throw new InvalidOperationException("Window is empty");
                }

                return buffer[back];
            }
        }

        /// <summary>
        /// Adds an element to the back of the sliding window.
        /// If the window is full, the element at the front is discarded.
        /// </summary>
        public void AddBack(T element)
        {
            back = Count == 0 ? 0 : (back + 1) % Capacity;

            // Advance front with one if back and front will point to same direction after increasing
            if (back == front && Count > 0)
            {
                front = (front + 1) % Capacity;
            }

            buffer[back] = element;

            Count = Math.Min(Capacity, Count + 1);
        }

        /// <summary>
        /// Adds an element to the front of the sliding window.
        /// If the window is full, the element at the back is discarded.
        /// </summary>
        public void AddFront(T element)
        {
            front = Count == 0 ? 0 : (front - 1 + Capacity) % Capacity;

            // Decrease back with one if back and front will point to same direction after decreasing
            if (back == front && Count > 0)
            {
                back = (back - 1 + Capacity) % Capacity;
            }

            buffer[front] = element;

            Count = Math.Min(Capacity, Count + 1);
        }

        /// <inheritdoc/>
        public T RemoveFront()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException("Window is empty");
            }

            // Extract the element and remove reference
            T element = buffer[front];
            buffer[front] = default!;

            front = (front + 1) % Capacity;
            --Count;

            return element;
        }

        /// <inheritdoc/>
        public T RemoveBack()
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException("Window is empty");
            }

            T element = buffer[back];
            buffer[back] = default!;

            back = (back - 1 + Capacity) % Capacity;
            --Count;

            return element;
        }
    }

    private readonly record struct WindowElement(int Value, int Index);

}