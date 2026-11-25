public class LRUCache 
{
    private readonly Dictionary<int, Node> _data = new();
    private Node _first, _last;
    private readonly int _capacity;

    public LRUCache(int capacity) 
    {
        _capacity = capacity;
    }
    
    public int Get(int key) 
    {
        if(!_data.ContainsKey(key))
        {
            return -1;
        }

        Upsert(_data[key]);

        return _data[key].Val;
    }
    
    public void Put(int key, int value) 
    {
        // ensure the element is added into the dictionary
        if(!_data.ContainsKey(key))
        {
            _data[key] = new Node
            {
                Val = value,
                Key = key
            };
        }
        _data[key].Val = value;

        Upsert(_data[key]);
    }

    private void Upsert(Node node)
    {
        // delete the last node in the 
        if(_data.Count > _capacity)
        {
            // remove the last node from list
            _data.Remove(_last.Key);  

            if(_last.Prev is not null)
            {
                _last.Prev.Next = null;
            }
            
            _last = _last.Prev;
        } 

        // empty list
        if(_first is null)
        {
            _first = _last = node;
            return;
        }

        if(_last is null)
        {
            _last = _first;
        }

        // value is already first
        if(_first == node)
        {
            return;
        }

        if(node == _last)
        {
            _last = _last.Prev;
            _last.Next = null;
        }

        // get node before and after
        var beforeNode = node.Prev;
        var afterNode = node.Next;

        if(beforeNode is not null)
        {
            beforeNode.Next = afterNode; 
        }

        if(afterNode is not null)
        {
            afterNode.Prev = beforeNode;
        }

        node.Next = _first;
        _first.Prev = node;
        _first = node;
    }

    private class Node
    {
        public int Val { get; set; }
        public Node Next { get; set; }
        public Node Prev { get; set; }
        public int Key { get; set;  }
    }
}


/**
 * Your LRUCache object will be instantiated and called as such:
 * LRUCache obj = new LRUCache(capacity);
 * int param_1 = obj.Get(key);
 * obj.Put(key,value);
 */