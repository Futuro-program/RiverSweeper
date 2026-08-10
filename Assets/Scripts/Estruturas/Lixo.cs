namespace Assets.Scripts.Classes
{
    public readonly struct Lixo
    {
        public readonly float valor;
        public readonly float peso;
        public readonly string tipo;

        public Lixo(float valor, float peso, string tipo)
        {
            this.valor = valor;
            this.peso = peso;
            this.tipo = tipo;
        }
    }
}
