public class RandomizedSet 
{
    private readonly HashSet<int> _set = new();
    private readonly Random _r = new();

    public RandomizedSet() 
    {
    }
    
    public bool Insert(int val) => _set.Add(val);
    
    public bool Remove(int val) => _set.Remove(val);
    
    public int GetRandom() 
    {
        var idx = _r.Next(0, _set.Count);

        return _set.ElementAt(idx);
    }
}

/**
 * Your RandomizedSet object will be instantiated and called as such:
 * RandomizedSet obj = new RandomizedSet();
 * bool param_1 = obj.Insert(val);
 * bool param_2 = obj.Remove(val);
 * int param_3 = obj.GetRandom();
 */