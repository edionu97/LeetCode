public class Solution 
{
   /// <summary>
    /// Determines whether the entire input matches a pattern where '.' matches any single
    /// character and '*' matches zero or more occurrences of the preceding element.
    /// </summary>
    /// <param name="s">The input string, which may be empty.</param>
    /// <param name="p">A valid pattern in which each '*' follows a literal character or '.'.</param>
    /// <returns>True if the pattern matches the entire input; otherwise, false.</returns>
    public bool IsMatch(string s, string p)
    {
        (int finalState, Transition[][] transitions) = BuildFiniteAutomata(p);

        // Each slot represents an input position and state; null means not yet evaluated.
        int width = finalState + 1;
        bool?[] memo = new bool?[(s.Length + 1) * width];

        return IsMatchInternal(0, 0);

        // Memoizes whether the remaining input can reach the accepting state from this position.
        bool IsMatchInternal(int sIndex, int state)
        {
            // Reaching the accepting state succeeds only when no input remains.
            if (state == finalState)
            {
                return sIndex == s.Length;
            }

            int slot = sIndex * width + state;
            bool? cached = memo[slot];

            if (cached is not null)
            {
                return cached.Value;
            }

            foreach (Transition transition in transitions[state])
            {
                // Character transitions consume exactly one input character.
                if (sIndex < s.Length &&
                    (transition.RequiredChar == '.' || transition.RequiredChar == s[sIndex]))
                {
                    if (!IsMatchInternal(sIndex + 1, transition.ToState))
                    {
                        continue;
                    }

                    memo[slot] = true;
                    return true;
                }

                // Epsilon transitions can advance even when no input remains.
                if (transition.RequiredChar == null)
                {
                    if (!IsMatchInternal(sIndex, transition.ToState))
                    {
                        continue;
                    }

                    memo[slot] = true;
                    return true;
                }
            }

            memo[slot] = false;
            return false;
        }
    }

    /// <summary>
    /// Builds a nondeterministic finite automaton with character-consuming transitions
    /// and epsilon transitions that advance without consuming input.
    /// </summary>
    /// <param name="pattern">A valid pattern supporting literals, '.', and '*'.</param>
    /// <returns>The accepting state and outgoing transitions indexed by source state.</returns>
    private static FiniteAutomata BuildFiniteAutomata(string pattern)
    {
        // Pattern length bounds the number of states; '*' tokens need no separate state.
        Transition[][] transitionMap = new Transition[pattern.Length + 1][];

        int state = 0;
        for (int index = 0; index < pattern.Length; index++, ++state)
        {
            // A starred element can repeat through a self-loop or be skipped through an epsilon edge.
            if (index + 1 < pattern.Length && pattern[index + 1] == '*')
            {
                transitionMap[state] =
                [
                    new Transition(state, pattern[index]),
                    new Transition(state + 1, null)
                ];

                index++;
                continue;
            }

            transitionMap[state] = [new Transition(state + 1, pattern[index])];
        }

        return new FiniteAutomata
        {
            FinalState = state,
            Transitions = transitionMap
        };
    }

    /// <summary>
    /// Represents an automaton whose initial state is 0 and whose accepting state is FinalState.
    /// </summary>
    private record class FiniteAutomata
    {
        /// <summary>
        /// Gets the state that accepts a match when the entire input has been consumed.
        /// </summary>
        public required int FinalState { get; init; }

        /// <summary>
        /// Gets outgoing transitions indexed by state; accepting and unused entries are null.
        /// </summary>
        public required Transition[][] Transitions { get; init; }

        /// <summary>
        /// Deconstructs the automaton into its accepting state and transition table.
        /// </summary>
        public void Deconstruct(out int finalState, out Transition[][] transitions)
        {
            finalState = this.FinalState;
            transitions = this.Transitions;
        }
    }

    /// <summary>
    /// Represents an edge that consumes a matching character, or no input when RequiredChar is null.
    /// </summary>
    /// <param name="ToState">The destination state.</param>
    /// <param name="RequiredChar">A literal character, '.' for any character, or null for an epsilon transition.</param>
    private record class Transition(int ToState, char? RequiredChar);
}