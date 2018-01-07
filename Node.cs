
using System.Collections;
using System.Collections.Generic;


//=====单向链表元素=====
class Node<T>
{
    public T Value;
    public Node<T> NextNode;
    //added 
    public Node() : this(default(T)) { }
    public Node(T value)
    {
        Value = value;
        NextNode = null;
    }
}




//操作有 增加、删除、修改、清空、遍历、索引器等
//======参考 ICollection<T>===
public interface ICollection<T> : IEnumerable<T>
{
    int Count { get; }
    bool IsReadOnly { get; }
    void Add(T item);
    void Clear();
    bool Contains(T item);
    void CopyTo(T[] array, int arrayIndex);
    bool Remove(T item);
}
//======参考 IList<T>===
public interface IList<T> : ICollection<T>
{
    T this[int index] { get; set; }
    int IndexOf(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
}

//=====单向链表=====
public class SinglyLinkedList<T> : IList<T>
{
    private Node<T> head = null;
    private int _count = 0;
    //---添加元素并计数
    public void Add(T item)
    {
        if (head == null)
        {
            head = new Node<T>(item);
            _count++;
        }
        else
        {
            Node<T> node = head;
            while (node.NextNode != null)
            {
                node = node.NextNode;
                _count++;
            }
            node.NextNode = new Node<T>(item);
        }
    }
    //---计数---
    public int Count
    {
        get { return _count; }
    }
}


public class run
{
    //learned from url: http://www.cnblogs.com/leemano/p/4937741.html
    public static void Main(string[] args)
    {
        SinglyLinkedList<int> slist = new SinglyLinkedList<int>();
        //Console.WriteLine($"count: {slist.Count}");
        slist.Add(1);
        slist.Add(2);
        slist.Add(3);
        slist.Add(4);
        //Console.WriteLine($"count: {slist.Count}");

        //输出结果：
        //count: 0
        //count: 4
    }
}