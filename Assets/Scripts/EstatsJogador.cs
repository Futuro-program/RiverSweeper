using UnityEngine;
using System;
using System.IO;
using System.Linq;
using Assets.Scripts.Estruturas;

public class EstatsJogador : MonoBehaviour
{
    const string CAMINHORELATIVO = "/estats-jogador.json";
    readonly IServicoDados servicoDados = new APISalve();

    public Jogador CarregarEstatisticas()
    {
        Jogador jog;

        try
        {
            jog = servicoDados.CarregarDados<Jogador>(CAMINHORELATIVO, true);
        }
        catch (FileNotFoundException)
        {
            jog = new(0, 0, 1, "madeira", new string[] {"madeira"});
        }

        return jog;
    }

    void SalvarDinheiro(float valor)
    {
        Jogador jog = CarregarEstatisticas();

        Jogador novoJog = new(
            valor, jog.totalLixoColetado, jog.faseAtual, jog.varaEquipada, jog.varasCompradas
        );

        servicoDados.SalvarDados(CAMINHORELATIVO, novoJog, true);
    }

    public void Vender(float valorTotal)
    {
        SalvarDinheiro(CarregarEstatisticas().dinheiro + valorTotal);
    }

    public void Pagar(float valor)
    {
        float dinheiro = CarregarEstatisticas().dinheiro - valor;

        if (dinheiro < 0)
            throw new Exception("Não é possível comprar o item!");

        SalvarDinheiro(dinheiro);
    }

    public void IncrementarLixoColetado(int quant)
    {
        Jogador jog = CarregarEstatisticas();

        Jogador novoJog = new(
            jog.dinheiro, jog.totalLixoColetado + quant, jog.faseAtual, jog.varaEquipada, jog.varasCompradas
        );

        servicoDados.SalvarDados(CAMINHORELATIVO, novoJog, true);
    }

    public void PassarFase()
    {
        Jogador jog = CarregarEstatisticas();

        Jogador novoJog = new(
            jog.dinheiro, jog.totalLixoColetado, jog.faseAtual + 1, jog.varaEquipada, jog.varasCompradas
        );

        servicoDados.SalvarDados(CAMINHORELATIVO, novoJog, true);
    }

    public void EquiparVara(string vara)
    {
        Jogador jog = CarregarEstatisticas();

        if (!jog.varasCompradas.Contains(vara))
            throw new Exception("Não é possível equipar uma vara não comprada!");

        Jogador novoJog = new(
            jog.dinheiro, jog.totalLixoColetado, jog.faseAtual, vara, jog.varasCompradas
        );
        servicoDados.SalvarDados(CAMINHORELATIVO, novoJog, true);
    }
    
    public void AdicionarVara(string novaVara)
    {
        Jogador jog = CarregarEstatisticas();

        string[] varasCompradas = (string[])jog.varasCompradas.Clone();
        
        Array.Resize(ref varasCompradas, varasCompradas.Length + 1);

        varasCompradas[^1] = novaVara;

        Jogador novoJog = new(
            jog.dinheiro, jog.totalLixoColetado, jog.faseAtual, novaVara, varasCompradas
        );
        servicoDados.SalvarDados(CAMINHORELATIVO, novoJog, true);
    }

    public void RedefinirProgresso()
    {
        Jogador jog = new(0, 0, 1, "madeira", new string[] {"madeira"});
        servicoDados.SalvarDados(CAMINHORELATIVO, jog, true);
    }
}
