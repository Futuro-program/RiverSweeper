namespace Assets.Scripts.Estruturas
{
    public readonly struct Lixo
    {
        public readonly float valor;
        public readonly float massa;
        public readonly float volume;
        public readonly string tipo;

        public Lixo(float valor, float massa, float volume, string tipo)
        {
            this.valor = valor;
            this.massa = massa;
            this.volume = volume;
            this.tipo = tipo;
        }
    }
}
