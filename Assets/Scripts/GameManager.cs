using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Entidades del juego")]
    [SerializeField] private Entity Player; 
    [SerializeField] private Entity NPC;    

    [Header("Historial de Turnos")]
    [SerializeField] private CustomDoubleLinkedList<CustomNode> TurnHistory; 

    void Start()
    {
        TurnHistory = new CustomDoubleLinkedList<CustomNode>();

        // Crear entidades con valores iniciales
        Player = new Entity("Jugador", 100, 10);
        NPC = new Entity("NPC", 100, 8);

        AddNewTurn();
    }

    // Añadir un nuevo turno
    public void AddNewTurn()
    {
        // Crear copias de las entidades actuales
        List<Entity> entities = new List<Entity>
        {
            Player.Clone(),
            NPC.Clone()
        };

        // Crear un nuevo nodo con las entidades clonadas
        CustomNode newTurn = new CustomNode(TurnHistory.count + 1, entities);

        TurnHistory.Add(newTurn);
        Debug.Log("Turno: " + newTurn.TurnNumber + " añadido.");
    }

    // Navegar entre turnos
    public void MoveToNextTurn()
    {
        TurnHistory.PeakNext();
        if (TurnHistory.Peak != null && TurnHistory.Peak.Value != null)
        {
            Debug.Log("Turno actual: " + TurnHistory.Peak.Value.TurnNumber);
            ShowTurnStatistics(TurnHistory.Peak.Value.Entities);
        }
        else
        {
            Debug.Log("No hay un turno siguiente.");
        }
    }

    public void MoveToPreviousTurn()
    {
        TurnHistory.PeakPrev();
        if (TurnHistory.Peak != null && TurnHistory.Peak.Value != null)
        {
            Debug.Log("Turno actual: " + TurnHistory.Peak.Value.TurnNumber);
            ShowTurnStatistics(TurnHistory.Peak.Value.Entities);
        }
        else
        {
            Debug.Log("No hay un turno anterior.");
        }
    }

    // Mostrar estadisticas del turno actual
    private void ShowTurnStatistics(List<Entity> entities)
    {
        foreach (var entity in entities)
        {
            Debug.Log("Entidad: " + entity.EntityName + ", Resistencia: " + entity.Resistance);
        }
    }

    // Resolver el combate entre jugador y NPC
    public void ResolveCombat(string playerAction)
    {
        string npcAction = GetRandomAction(); // NPC toma una accion aleatoria
        Debug.Log("Jugador: " + playerAction + ", NPC: " + npcAction);

        if (playerAction == "Atacar" && npcAction == "Esquivar")
        {
            NPC.ReduceResistance(5); // NPC pierde resistencia
            Debug.Log("El NPC esquivo, pero perdio resistencia.");
        }
        else if (playerAction == "Esquivar" && npcAction == "Atacar")
        {
            Player.ReduceResistance(5); // Jugador pierde resistencia
            Debug.Log("El jugador esquivo, pero perdio resistencia.");
        }
        else if (playerAction == "Atacar" && npcAction == "Atacar")
        {
            Player.ApplyDamage(NPC.Damage); // Ambos atacan y sufren daño
            NPC.ApplyDamage(Player.Damage);
            Debug.Log("Ambos atacaron y sufrieron daño.");
        }
        else if (playerAction == "Esquivar" && npcAction == "Esquivar")
        {
            Player.ReduceResistance(2);
            NPC.ReduceResistance(2);
            Debug.Log("Ambos esquivaron y perdieron resistencia.");
        }

        // Mostrar el estado actualizado
        Debug.Log("Estado del Jugador: Resistencia = " + Player.Resistance);
        Debug.Log("Estado del NPC: Resistencia = " + NPC.Resistance);

        // Comprobar si alguien fue derrotado
        CheckForDefeat();
    }

    // Verificar derrotas
    private void CheckForDefeat()
    {
        if (Player.Resistance <= 0)
        {
            Debug.Log("El Jugador ha sido derrotado. Fin del combate.");
        }
        else if (NPC.Resistance <= 0)
        {
            Debug.Log("El NPC ha sido derrotado. Fin del combate.");
        }
    }

    // Generar una accion aleatoria para el NPC
    private string GetRandomAction()
    {
        string[] actions = { "Atacar", "Esquivar" };
        return actions[Random.Range(0, actions.Length)];
    }
}