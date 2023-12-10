using System.Collections;

namespace CopperEngine.Utility;

public class RandomList<T> : IEnumerable<T>
{
    private readonly Random listRandom = new(Guid.NewGuid().GetHashCode());
    private readonly List<T> list;

    public RandomList(IEnumerable<T> list)
    {
        this.list = list.ToList();
    }

    public RandomList(params T[] list)
    {
        this.list = list.ToList();
    }

    public RandomList()
    {
        this.list = new List<T>();
    }

    public T GetRandomValue()
    {
        return list[listRandom.Next(list.Count - 1)];
    }

    public static implicit operator T(RandomList<T> value)
    {
        return value.GetRandomValue();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add(T value)
    {
        list.Add(value);
    }
}