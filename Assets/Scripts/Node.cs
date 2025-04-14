using UnityEngine;

public class Node<T>
{
    #region Properties
    private T value;
    private Node<T> next;
    #endregion

    #region Getters
    public T Value => value;
    public Node<T> Next => next;
    #endregion

    #region Constructors
    public Node(T value)
    {
        this.value = value;
        this.next = null;
    }

    public void SetNext(Node<T> next)
    {
        this.next = next;
    }
    #endregion

}