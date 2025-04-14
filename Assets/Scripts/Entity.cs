using UnityEngine;

public class Entity
{
    public string EntityName { get; private set; }
    public int Resistance { get; private set; }
    public int Damage { get; private set; }

    public Entity(string entityName, int resistance, int damage)
    {
        EntityName = entityName;
        Resistance = resistance;
        Damage = damage;
    }

    // Reducir la resistencia
    public void ReduceResistance(int amount)
    {
        Resistance = Mathf.Max(Resistance - amount, 0); // Aseguramos que no baje de 0
    }

    // Aplicar daño
    public void ApplyDamage(int damage)
    {
        Resistance = Mathf.Max(Resistance - damage, 0); 
    }

    // Clonar una entidad para guardar en cada nodo los datos
    public Entity Clone()
    {
        return new Entity(EntityName, Resistance, Damage);
    }
}