
using System.Collections;
using System.Collections.Generic;
using System;

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
    public void Add_old(T item)
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

    //---添加元素并计数
    public void Add(T item)
    {
        _count++;
        Insert(_count - 1, item);
        _count--;
    }

    //---计数---
    public int Count
    {
        get { return _count; }
    }

    //added next go on 
    //---实现可枚举
    public IEnumerator<T> GetEnumerator()
    {
        Node<T> node = head;
        Node<T> result = new Node<T>();
        while (node != null)
        {
            result = node;
            node = node.NextNode;
            yield return result.Value;
        }
    }
    //---不用填写，只调用上面的
    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    //---清空---
    public void Clear()
    {
        head = null;
        _count = 0;
    }


    public void Insert(int index, T item)
    {
        if (index >= 0 && index < _count)
        {
            Node<T> node = head;
            Node<T> prev = null;
            Node<T> next = null;
            //头元素永远是head，所以要专门弄个index=0的特殊
            if (index == 0)
            {
                next = head;
                head = new Node<T>(item);
                head.NextNode = next;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    prev = node;
                    node = node.NextNode;
                }
                next = node;
                node = new Node<T>(item);
                node.NextNode = next;
                prev.NextNode = node;
            }
            _count++;
        }
        else throw new Exception("Out of Range !");
    }

    //---查找---
    public int IndexOf(T item)
    {
        int result = -1;
        Node<T> node = head;
        for (int i = 0; i < _count; i++)
        {
            if (node.Value.Equals(item))
            {
                result = i;
                break;
            }
            node = node.NextNode;
        }
        return result;
    }

    //---包含---
    public bool Contains(T item)
    {
        return IndexOf(item) > -1 ? true : false;
    }
    //---删除---
    public void RemoveAt(int index)
    {
        if (index >= 0 && index < _count)
        {
            Node<T> prev = null;
            Node<T> node = head;
            if (index == 0)
            {
                head = head.NextNode;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    prev = node;
                    node = node.NextNode;
                }
                prev.NextNode = node.NextNode;
            }
            _count--;
        }
        else throw new Exception("Out of Range !");
    }
    //---删除---
    public bool Remove(T item)
    {
        int n = IndexOf(item);
        if (n < 0) { return false; }
        RemoveAt(n);
        return true;
    }



    //---索引器
    public T this[int index]
    {
        get
        {
            if (index >= 0 && index < _count)
            {
                Node<T> node = head;
                for (int i = 0; i < index; i++)
                {
                    node = node.NextNode;
                }
                return node.Value;
            }
            else throw new Exception("Out of Range !");
        }
        set
        {
            Insert(index, value);
        }
    }

    //---拷贝
    public void CopyTo(T[] array, int arrayIndex)
    {
        Node<T> node = head;
        for (int i = 0; i < _count; i++)
        {
            array[arrayIndex + i] = node.Value;
            node = node.NextNode;
        }
    }



}


namespace ok
{

//=====单向链表=====
public class SinglyLinkedList<T> : IList<T> where T : IComparable<T>
{
    private Node<T> head = null;
    private int _count = 0;
    //---添加元素并计数---
    public void Add(T item)
    {
        _count++;
        Insert(_count - 1, item);
        _count--;
    }
    //---计数---
    public int Count
    {
        get { return _count; }
    }
    //---实现可枚举
    public IEnumerator<T> GetEnumerator()
    {
        Node<T> node = head;
        Node<T> result = new Node<T>();
        while (node != null)
        {
            result = node;
            node = node.NextNode;
            yield return result.Value;
        }
    }
    //---不用填写，只调用上面的
    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
    //---清空---
    public void Clear()
    {
        head = null;
        _count = 0;
    }
    //---插入值---
    public void Insert(int index, T item)
    {
        if (index >= 0 && index < _count)
        {
            Node<T> node = head;
            Node<T> prev = null;
            Node<T> next = null;
            //头元素永远是head，所以要专门弄个index=0的特殊
            if (index == 0)
            {
                next = head;
                head = new Node<T>(item);
                head.NextNode = next;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    prev = node;
                    node = node.NextNode;
                }
                next = node;
                node = new Node<T>(item);
                node.NextNode = next;
                prev.NextNode = node;
            }
            _count++;
        }
        else throw new Exception("Out of Range !");
    }
    //---查找---
    public int IndexOf(T item)
    {
        int result = -1;
        Node<T> node = head;
        for (int i = 0; i < _count; i++)
        {
            if (node.Value.Equals(item))
            {
                result = i;
                break;
            }
            node = node.NextNode;
        }
        return result;
    }
    //---包含---
    public bool Contains(T item)
    {
        return IndexOf(item) > -1 ? true : false;
    }
    //---删除---
    public void RemoveAt(int index)
    {
        if (index >= 0 && index < _count)
        {
            Node<T> prev = null;
            Node<T> node = head;
            if (index == 0)
            {
                head = head.NextNode;
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    prev = node;
                    node = node.NextNode;
                }
                prev.NextNode = node.NextNode;
            }
            _count--;
        }
        else throw new Exception("Out of Range !");
    }
    //---删除---
    public bool Remove(T item)
    {
        int n = IndexOf(item);
        if (n < 0) { return false; }
        RemoveAt(n);
        return true;
    }
    //---索引器---
    public T this[int index]
    {
        get
        {
            if (index >= 0 && index < _count)
            {
                Node<T> node = head;
                for (int i = 0; i < index; i++)
                {
                    node = node.NextNode;
                }
                return node.Value;
            }
            else throw new Exception("Out of Range !");
        }
        set
        {
            Insert(index, value);
        }
    }
    //---只读？---
    public bool IsReadOnly
    {
        get { return false; }
    }
    //---拷贝---
    public void CopyTo(T[] array, int arrayIndex)
    {
        Node<T> node = head;
        for (int i = 0; i < _count; i++)
        {
            array[arrayIndex + i] = node.Value;
            node = node.NextNode;
        }
    }
}
//=====单向链表元素=====
class Node<T>
{
    public T Value;
    public Node<T> NextNode;
    public Node() : this(default(T)) { }
    public Node(T value)
    {
        Value = value;
        NextNode = null;
    }
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