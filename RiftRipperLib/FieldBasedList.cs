using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RiftRipperLib;

public class FieldBasedEqualityComparer<T> : IEqualityComparer<T>
{
    private Func<T, T, bool> _equalsFunc;

    public FieldBasedEqualityComparer(Func<T, T, bool> equalsFunc)
    {
        _equalsFunc = equalsFunc ?? new Func<T, T, bool>((obj1, obj2) =>
        {
            FieldInfo[] obj1Fields = obj1.GetType().GetFields();
            FieldInfo[] obj2Fields = obj2.GetType().GetFields();
            bool[] similarityChecker = new bool[obj1Fields.Length];

            for(int i = 0; i < obj1Fields.Length; i++)
            {
                FieldInfo? field = obj1Fields[i];
                FieldInfo? fieldHomonym = obj2Fields[i];
                if (field == fieldHomonym)
                {
                    similarityChecker[i] = true;
                    continue;
                }
                if(field.GetValue(obj1) == fieldHomonym.GetValue(obj2))
                {
                    similarityChecker[i] = true;
                    continue;
                };
            }

            if(similarityChecker.All((x) => x == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        });
    }

    public bool Equals(T x, T y) => _equalsFunc(x, y);

    public int GetHashCode(T obj) => obj.GetHashCode();
}

// This is used in the Archive File System.
public class FieldBasedList<T>
{
    private List<T> _innerList;
    private IEqualityComparer<T> _equalityComparer;

    public FieldBasedList(Func<T, T, bool> equalsFunc)
    {
        _innerList = new List<T>();
        _equalityComparer = new FieldBasedEqualityComparer<T>(equalsFunc);
    }

    public void Add(T item)
    {
        if (!_innerList.Contains(item, _equalityComparer))
        {
            _innerList.Add(item);
        }
    }

    public void Clear()
    {
        _innerList.Clear();
    }

    public bool Contains(T item)
    {
        return _innerList.Contains(item);
    }

    public void Remove(T item)
    {
        _innerList.Remove(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach(T item in _innerList)
        {
            yield return item;
        }
    }

    public T FirstOrDefault(Func<T, bool> predicate) => _innerList.FirstOrDefault(predicate);
}
