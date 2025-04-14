using UnityEngine;
using System.Collections.Generic; 

public class CustomNode : Node<List<Entity>> 
{
    // Atributos especificos de un turno
    public int TurnNumber { get; private set; }
    public List<Entity> Entities { get; private set; }

    public CustomNode(int turnNumber, List<Entity> entities) : base(entities)
    {
        TurnNumber = turnNumber;
        Entities = entities;
    }
}