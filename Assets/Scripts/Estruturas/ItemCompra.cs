namespace Assets.Scripts.Estruturas
{
    public readonly struct ItemCompra
    {
        public readonly string nome;
        public readonly float valor;

        public ItemCompra(string nome, float valor)
        {
            this.nome = nome;
            this.valor = valor;
        }
    }
}
