using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class exportarCsv : MonoBehaviour {
    public static List<string[]> dadosLinha = new List<string[]>();//deixar público para que linhas com informações sejam adicionadas a partir do UpdateHand
    // Use this for initialization
    void Start () {
        //nomes das colunas
        string[] primeiraLinhaInfo = new string[3];
        primeiraLinhaInfo[0] = "Exercício";
        primeiraLinhaInfo[1] = "Dedo";
        primeiraLinhaInfo[2] = "Ângulos obtidos";
        dadosLinha.Add(primeiraLinhaInfo);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void escreverArquivo()
    {
        string[][] saida = new string[dadosLinha.Count][];
        for(int i = 0; i < saida.Length; i++)
        {
            saida[i] = dadosLinha[i];
        }
        int tamanho = saida.Length;
        string separador = ","; //csv é um tipo de arquivo que separa colunas por vírgula
        StringBuilder stringBuilder = new StringBuilder();
        for(int indice = 0; indice < tamanho; indice++)
        {
            stringBuilder.AppendLine(string.Join(separador,saida[indice]));
        }

        string caminhoArq = getPath();
        StreamWriter sw = System.IO.File.CreateText(caminhoArq);
        sw.WriteLine(stringBuilder);
        sw.Close();

    }

    // Following method is used to retrive the relative path as device platform
    private static string getPath()
    {
        //depois passar no nome do arquivo sexo, idade e mão utilizada para exercícios
        //string dataHora = string.Format("{HH-mm-ss}",DateTime.Now);
        string dataHora = DateTime.Now.ToString("HH-mm-ddMMyyyy");

        #if UNITY_EDITOR
                        return Application.dataPath +"/CSV/"+dataHora+".csv";
        #elif UNITY_ANDROID
                        return Application.persistentDataPath+dataHora+".csv";
        #elif UNITY_IPHONE
                        return Application.persistentDataPath+"/"+dataHora+".csv";
        #else
        return Application.dataPath + "/" + dataHora+".csv";
        #endif
    }
}
