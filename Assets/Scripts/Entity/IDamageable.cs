namespace Entity
{
    public interface IDamageable : IDieable
    {
        public float GetCurrentHealth();
        public float GetMaxHealth();
        public void TakeDamage(int damage);
    }
}
