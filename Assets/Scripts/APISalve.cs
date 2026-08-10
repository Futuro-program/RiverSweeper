using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

public class APISalve : IServicoDados
{
    const string CHAVE = "PyRM7z750q48vA843hczRBiK4DQSh7xv941LD4fwH+M=";
    const string IV = "lUC6SHWMtH9DUKeJawhWFg==";

    void EscreverDadosEncriptados<T>(T dados, FileStream fluxo)
    {
        using Aes provedorAes = Aes.Create();
        provedorAes.Key = Convert.FromBase64String(CHAVE);
        provedorAes.IV = Convert.FromBase64String(IV);

        using ICryptoTransform transformCripto = provedorAes.CreateEncryptor();
        using CryptoStream fluxoCripto = new(fluxo, transformCripto, CryptoStreamMode.Write);

        /*Debug.Log($"Vetor de Inicialização (IV): {Convert.ToBase64String(provedorAes.IV)}");
        Debug.Log($"Chave: {Convert.ToBase64String(provedorAes.Key)}");*/
        fluxoCripto.Write(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dados)));
    }

    T LerDadosEncriptados<T>(string caminho)
    {
        byte[] bytesArquivo = File.ReadAllBytes(caminho);
        using Aes provedorAes = Aes.Create();
        provedorAes.Key = Convert.FromBase64String(CHAVE);
        provedorAes.IV = Convert.FromBase64String(IV);

        using ICryptoTransform transformCripto = provedorAes.CreateDecryptor(
            provedorAes.Key,
            provedorAes.IV
        );
        using MemoryStream fluxoDecripto = new(bytesArquivo);
        using CryptoStream fluxoCripto = new(fluxoDecripto, transformCripto, CryptoStreamMode.Read);
        using StreamReader leitor = new(fluxoCripto);

        string resultado = leitor.ReadToEnd();

        Debug.Log(resultado);
        return JsonConvert.DeserializeObject<T>(resultado);
    }

    public bool SalvarDados<T>(string caminhoRel, T dados, bool encriptado)
    {
        string caminho = Application.persistentDataPath + caminhoRel;

        if (File.Exists(caminho))
        {
            Debug.Log("Dados já existem.");
            File.Delete(caminho);
        }
        else
            Debug.Log("Dados ainda não existem.");

        try
        {
            Debug.Log("Salvando dados...");
            using FileStream fluxo = File.Create(caminho);
            if (encriptado)
                EscreverDadosEncriptados(dados, fluxo);
            else
            {
                fluxo.Close();
                File.WriteAllText(caminho, JsonConvert.SerializeObject(dados));
            }
            
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Não pode salvar dados, porque: {e.Message} {e.StackTrace}");
            return false;
        }
    }

    public T CarregarDados<T>(string caminhoRel, bool encriptado)
    {
        string caminho = Application.persistentDataPath + caminhoRel;

        if (!File.Exists(caminho))
        {
            // Debug.LogError("Não pode carregar um arquivo que não existe!");
            throw new FileNotFoundException($"caminho {caminho} não existe!");
        }

        try
        {
            Debug.Log("Carregando dados...");
            string textoTodo = File.ReadAllText(caminho);
            T dados;

            if (encriptado)
                dados = LerDadosEncriptados<T>(caminho);
            else 
                dados = JsonConvert.DeserializeObject<T>(textoTodo);
            
            return dados;
        }
        catch (Exception e)
        {
            Debug.LogError($"Não pode carregar dados, porque: {e.Message} {e.StackTrace}");
            throw e;
        }
    }
}
