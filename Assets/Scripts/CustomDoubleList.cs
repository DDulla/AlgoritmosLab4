using UnityEngine;

public class CustomDoubleLinkedList<T> : SimpleLinkedList<T>
{
    public Node<T> Peak { get; private set; }

    public CustomDoubleLinkedList()
    {
        Peak = null;
    }

    // Agregar un nuevo valor
    public override void Add(T value)
    {
        if (Peak != last && Peak != null)
        {
            // Eliminar nodos posteriores al puntero Peak
            Node<T> currentNode = Peak.Next;
            while (currentNode != null)
            {
                Remove(currentNode.Value);
                currentNode = currentNode.Next;
            }
        }

        // Agregar el nuevo nodo y actualizar Peak
        base.Add(value);
        Peak = last;
    }

    // Avanzar al siguiente turno
    public void PeakNext()
    {
        if (Peak != null && Peak.Next != null)
        {
            Peak = Peak.Next;
        }
    }

    // Retroceder al turno anterior
    public void PeakPrev()
    {
        if (head == null || Peak == head) return; // Validar limites

        Node<T> currentNode = head;
        while (currentNode.Next != Peak)
        {
            currentNode = currentNode.Next;
        }

        Peak = currentNode; // Mover Peak al anterior
    }
}