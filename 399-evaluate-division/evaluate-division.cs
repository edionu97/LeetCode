public class Solution 
{
    public double[] CalcEquation(
        IList<IList<string>> equations,
        double[] values,
        IList<IList<string>> queries)
    {
        // create expression graph 
        var graph = BuildGraph(equations, values);

        return queries.Select(q => ComputeEquation(q.First(), q.Last())).ToArray();

        double ComputeEquation(string first, string last)
        {
            // if letter is not in original equations we know the answer as -1
            if(!graph.ContainsKey(first) || !graph.ContainsKey(last))
            {
                return -1;
            }

            Queue<EqationPath> queue = new() { new (first, 1) }; HashSet<string> used = new(){ first };
            while(queue.Any())
            {
                // get the node and the product
                var (variable, product) = queue.Dequeue();

                // mark the variable as 
                used.Add(variable);
                
                // at the end return product from first to start
                if(variable == last)
                {
                    return product;
                }

                // for each variable visit the adjacend variable
                foreach(var (@var, w) in graph[variable])
                {
                    // check if variable was already used
                    if(used.Contains(@var))
                    {
                        continue;
                    }

                    // add value in used
                    used.Add(@var);

                    // add product from variable to edge width
                    queue.Add(new (@var, product * w));
                }

            }

            return -1;
        }
    }

    /// Helper method for building the graph
    private static Dictionary<string, IList<Edge>> BuildGraph(IList<IList<string>> equations, double[] values)
    {
        // create the expression graph
        Dictionary<string, IList<Edge>> graph = new();

        // iterate all values and build the graph
        for(var i = 0; i < values.Length; ++i)
        {
            var first = equations[i].First();
            var second = equations[i].Last();

            // always ensure there is an element at graph[i]
            if(!graph.ContainsKey(first))
            {
                graph[first] = new List<Edge>();
            }

            // always ensure there is an element at graph[i]
            if(!graph.ContainsKey(second))
            {
                graph[second] = new List<Edge>();
            }

            // first / second = values[i]
            // second / first = 1 / values[i]
            graph[first].Add(new(second, values[i]));
            graph[second].Add(new(first, 1.0d / values[i]));
        }

        return graph;
    }

    private record Edge(string Node, double Weight);
    private record EqationPath(string Var, double Value);
}

internal static class QueueExtensions
{   
    public static void Add<T>(this Queue<T> queue, T element) => queue.Enqueue(element);
}