public interface IDamageable
{
    // amount: cantidad de daño
    // damageType: "Stun", "Physical", "Fire"
    void TakeDamage(float amount, string damageType);
}