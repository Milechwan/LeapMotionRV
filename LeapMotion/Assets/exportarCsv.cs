using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class exportarCsv : MonoBehaviour {
    public static List<string[]> dadosLinha = new List<string[]>();//deixar público para que linhas com informações sejam adicionadas a partir do UpdateHand
    public static int[] indiceLinha = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void inicializarLinhasArquivo()
    {
        //nomes das colunas
        string[] primeiraLinhaInfo = new string[3];
        primeiraLinhaInfo[0] = "Exercício";
        primeiraLinhaInfo[1] = "Dedo";
        primeiraLinhaInfo[2] = "Ângulos obtidos";
        dadosLinha.Add(primeiraLinhaInfo);
        //definir as linhas que terão no arquivo de acordo com os exercícios a serem feitos
        int qtdAbducaoInd = int.Parse(comecarExercicio.passAbdAduInd == null ? "0" : comecarExercicio.passAbdAduInd);
        int qtdAbdMed = int.Parse(comecarExercicio.passAbdAduMed == null ? "0" : comecarExercicio.passAbdAduMed);
        int qtdAbdAnl = int.Parse(comecarExercicio.passAbdAduAnl == null ? "0" : comecarExercicio.passAbdAduAnl);
        int qtdAbdMindi = int.Parse(comecarExercicio.passAbdAduMindi == null ? "0" : comecarExercicio.passAbdAduMindi);
        int qtdLevantamento = int.Parse(comecarExercicio.passarLevantamento == null ? "0" : comecarExercicio.passarLevantamento);
        int qtdPinchInd = int.Parse(comecarExercicio.passPinchInd == null ? "0" : comecarExercicio.passPinchInd);
        int qtdPinchMed = int.Parse(comecarExercicio.passPinchMed == null ? "0" : comecarExercicio.passPinchMed);
        int qtdPinchAnl = int.Parse(comecarExercicio.passPinchAnl == null ? "0" : comecarExercicio.passPinchAnl);
        int qtdPinchMindi = int.Parse(comecarExercicio.passPinchMindi == null ? "0" : comecarExercicio.passPinchMindi);
        int qtdExtensaoMed = int.Parse(comecarExercicio.passarExtMed == null ? "0" : comecarExercicio.passarExtMed);
        if (qtdAbducaoInd > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Adução/Abdução";
            linhaAbdInd[1] = "Indicador";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[0] = dadosLinha.Count - 1;
        }
        if (qtdAbdMed > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Adução/Abdução";
            linhaAbdInd[1] = "Médio";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[1] = dadosLinha.Count - 1;
        }
        if (qtdAbdAnl > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Adução/Abdução";
            linhaAbdInd[1] = "Anelar";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[2] = dadosLinha.Count - 1;
        }
        if (qtdAbdMindi > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Adução/Abdução";
            linhaAbdInd[1] = "Mindinho";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[3] = dadosLinha.Count - 1;
        }
        if (qtdLevantamento > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Extensão";
            linhaAbdInd[1] = "Indicador";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[4] = dadosLinha.Count - 1;
        }
        if (qtdExtensaoMed > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Extensão";
            linhaAbdInd[1] = "Médio";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[5] = dadosLinha.Count - 1;
        }
        if (qtdPinchInd > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Pinça";
            linhaAbdInd[1] = "Indicador";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[6] = dadosLinha.Count - 1;
        }
        if (qtdPinchMed > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Pinça";
            linhaAbdInd[1] = "Médio";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[7] = dadosLinha.Count - 1;
        }
        if (qtdPinchAnl > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Pinça";
            linhaAbdInd[1] = "Anelar";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[8] = dadosLinha.Count - 1;
        }
        if (qtdPinchMindi > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Pinça";
            linhaAbdInd[1] = "Mindinho";

            dadosLinha.Add(linhaAbdInd);
            indiceLinha[9] = dadosLinha.Count - 1;
        }
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
        string dataHora = DateTime.Now.ToString("HH-mm-ss-ddMMyyyy");

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
