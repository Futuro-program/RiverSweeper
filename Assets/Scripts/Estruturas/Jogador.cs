namespace Assets.Scripts.Estruturas
{
    public readonly struct Jogador
    {
        public readonly float dinheiro;
        public readonly int totalLixoColetado;
        public readonly int faseAtual;
        public readonly string varaEquipada;
        public readonly string[] varasCompradas;

        public Jogador(
            float dinheiro, 
            int totalLixoColetado, 
            int faseAtual, 
            string varaEquipada, 
            string[] varasCompradas
        )
        {
            this.dinheiro = dinheiro;
            this.totalLixoColetado = totalLixoColetado;
            this.faseAtual = faseAtual;
            this.varaEquipada = varaEquipada;
            this.varasCompradas = varasCompradas;
        }
    }
}
