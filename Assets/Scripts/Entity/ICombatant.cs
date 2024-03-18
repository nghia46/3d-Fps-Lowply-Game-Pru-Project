namespace Entity
{
    public interface ICombatant
    {
        public void DealDamage(IDamageable target, int damage);
    }
}
