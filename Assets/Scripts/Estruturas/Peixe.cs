namespace Assets.Scripts.Estruturas
{
    public readonly struct Peixe
    {
        public readonly float valor;
        public readonly int tamGrupo, ampMovimento;
        public readonly string tipo;

        public Peixe(float valor, int tamGrupo, int ampMovimento, string tipo)
        {
            this.valor = valor;
            this.tamGrupo = tamGrupo;
            this.ampMovimento = ampMovimento;
            this.tipo = tipo;
        }
    }
}
