public interface IServicoDados
{
    bool SalvarDados<T>(string caminhoRel, T dados, bool encriptado);

    T CarregarDados<T>(string caminhoRel, bool encriptado);
}
