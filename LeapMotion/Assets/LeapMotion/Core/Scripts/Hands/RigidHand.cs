/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2018.                                 *
 * Leap Motion proprietary and confidential.                                  *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Leap;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

namespace Leap.Unity {
  /** A physics model for our rigid hand made out of various Unity Collider. */
  public class RigidHand : SkeletalHand {
    public override ModelType HandModelType {
      get {
        return ModelType.Physics;
      }
    }
    public float filtering = 0.5f;
        //variáveis para o projeto
        public RawImage info_exercicio;
        public Text exercicioConcluido;
        public Text proximoExercicio;
        //public Text conta_text_Abducao;
        public Text booleano_botao;
        public Text nome_exercicio;
        public Text conta_text_Pinch;
        public GameObject cuboProximoExercicio;
        public static int contadorAbducao = 0;
        public static int qtdAbducao = int.Parse(comecarExercicio.passAbdAduInd==null? "0" : comecarExercicio.passAbdAduInd);// vai tirar depois
        public static int qtdAbdInd = int.Parse(comecarExercicio.passAbdAduInd==null? "0" : comecarExercicio.passAbdAduInd);
        public static int qtdAbdMed = int.Parse(comecarExercicio.passAbdAduMed==null ? "0" : comecarExercicio.passAbdAduMed);
        public static int qtdAbdAnl = int.Parse(comecarExercicio.passAbdAduAnl==null? "0" : comecarExercicio.passAbdAduAnl);
        public static int qtdAbdMindi = int.Parse(comecarExercicio.passAbdAduMindi==null? "0" : comecarExercicio.passAbdAduMindi);
        public static int contadorLevant = 0;//esse � pra levantar dedo
        public static int qtdLevantamento = int.Parse(comecarExercicio.passarLevantamento==null? "0" : comecarExercicio.passarLevantamento);
        public static int contadorNumeroDeExercicios = 0;
        public static int contadorPinch;
        public static int qtdPinch = int.Parse(comecarExercicio.passPinchInd==null? "0" : comecarExercicio.passPinchInd);// vai tirar depois
        public static int qtdPinchInd = int.Parse(comecarExercicio.passPinchInd==null? "0" : comecarExercicio.passPinchInd);
        public static int qtdPinchMed = int.Parse(comecarExercicio.passPinchMed==null? "0" : comecarExercicio.passPinchMed);
        public static int qtdPinchAnl = int.Parse(comecarExercicio.passPinchAnl==null? "0" : comecarExercicio.passPinchAnl);
        public static int qtdPinchMindi = int.Parse(comecarExercicio.passPinchMindi==null? "0" : comecarExercicio.passPinchMindi);
        //public static bool bol = false;
        public static bool[] exerciciosBolean = { true, false, false, false, false, false, false, false, false };
        public static bool[] PinchBolean = { true, false, false, false };
        public static bool concluido = false;
        public static bool aux_texto_abd = true;
        public static bool aux_texto_levant = true;//esse � pra levantar dedo
        public static bool auxiliarPinch = true;
        public static int contadorPinchInd = 0;
        public static int contadorPinchMed = 0;
        public static int contadorPinchAnl = 0;
        public static int contadorPinchMindi = 0;
        //array para guardar o índice da linha na tabela
        public static int[] indiceLinha = { -1, -1, -1, -1, -1 };
        public static string infoAngulos = "";
        public static List<string[]> inputCsv = exportarCsv.dadosLinha;

        public override bool SupportsEditorPersistence() {
            return true;
        }

    public override void InitHand() {
      base.InitHand();
            //definir as linhas que terão no arquivo de acordo com os exercícios a serem feitos
      
      if (qtdAbdInd > 0)
      {
        string[] linhaAbdInd = new string[3];
        linhaAbdInd[0] = "Adução/Abdução";
        linhaAbdInd[1] = "Indicador";

        inputCsv.Add(linhaAbdInd);
        indiceLinha[0] = inputCsv.Count - 1; 
      }
        if (qtdAbdMed > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Adução/Abdução";
            linhaAbdInd[1] = "Médio";

            inputCsv.Add(linhaAbdInd);
            indiceLinha[1] = inputCsv.Count - 1;
        }
        if (qtdAbdAnl > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Adução/Abdução";
            linhaAbdInd[1] = "Anelar";

            inputCsv.Add(linhaAbdInd);
            indiceLinha[2] = inputCsv.Count - 1;
        }
        if (qtdAbdMindi > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Adução/Abdução";
            linhaAbdInd[1] = "Mindinho";

            inputCsv.Add(linhaAbdInd);
            indiceLinha[3] = inputCsv.Count - 1;
        }
        if (qtdLevantamento > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Extensão";
            linhaAbdInd[1] = "Indicador";

            inputCsv.Add(linhaAbdInd);
            indiceLinha[4] = inputCsv.Count - 1;
        }
        /*if (qtdPinchInd > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Pinça";
            linhaAbdInd[1] = "Indicador";

            inputCsv.Add(linhaAbdInd);
            indiceLinha[5] = inputCsv.Length - 1;
        }
        if (qtdPinchMed > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Pinça";
            linhaAbdInd[1] = "Médio";

            inputCsv.Add(linhaAbdInd);
            indiceLinha[6] = inputCsv.Length - 1;
        }
        if (qtdPinchAnl > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Pinça";
            linhaAbdInd[1] = "Anelar";

            inputCsv.Add(linhaAbdInd);
            indiceLinha[7] = inputCsv.Length - 1;
        }
        if (qtdPinchMindi > 0)
        {
            string[] linhaAbdInd = new string[3];
            linhaAbdInd[0] = "Pinça";
            linhaAbdInd[1] = "Mindinho";

            inputCsv.Add(linhaAbdInd);
            indiceLinha[8] = inputCsv.Length - 1;
        }*/
    }
        public bool pinchouDedos(Vector b1, Vector b2)
        {
            bool retorno = false;
            float distancia = b1.DistanceTo(b2);
            if (distancia<=.03f) //medida utilizada pelo próprio pinch detector do leap
            {
                retorno = true;
            }
            return retorno;
        }

        public float produto_escalar(Vector3 vetor1, Vector3 vetor2)
        {
            float resultado = 0.0f;
            resultado = (vetor1.x * vetor2.x) + (vetor1.y * vetor2.y) + (vetor1.z * vetor2.z);
            return resultado;
        }

       /* public float angulo_dedos(Finger f1, Finger f2)
        {
            Vector3 proximal1 = f1.Bone(Bone.BoneType.TYPE_PROXIMAL).Direction;//ajeitar isso mais tarde!!!!
            Vector3 direcao1 = new Vector3(f1.TipPosition.x - proximal1.x, f1.TipPosition.y - proximal1.y, f1.TipPosition.z - proximal1.z);
            Vector3 direcao2 = new Vector3(f2.TipPosition.x - b2.PrevJoint.x, f2.TipPosition.y - b2.PrevJoint.y, f2.TipPosition.z - b2.PrevJoint.z);
            return Mathf.Acos((produto_escalar(direcao1, direcao2)) / (modulo_vetor(direcao1) * modulo_vetor(direcao2)));
        }*/

        public float modulo_vetor(Vector vet)
        {
            return Mathf.Sqrt(Mathf.Pow(vet.x, 2) + Mathf.Pow(vet.y, 2) + Mathf.Pow(vet.z, 2));
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Mathf.PI);
        }

        public override void UpdateHand() {

            FingerModel f1=null, f2 = null, f3 = null, f4 = null, f5 = null;
            //float angulo= 0.0f;
            for (int f = 0; f < fingers.Length; ++f)
            {
                if (fingers[f] != null)
                {
                    fingers[f].UpdateFinger();
                    //Debug.Log("Dedo "+fingers[f].fingerType+ "Direcao"+ fingers[f].GetBoneDirection(3).ToString()+ "basis osso"+ fingers[f].GetBoneCenter(3));
                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_MIDDLE)
                        f3 = fingers[f];
                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_INDEX)
                        f2 = fingers[f];
                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_THUMB)
                        f1 = fingers[f]; 
                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_RING)
                        f4 = fingers[f];
                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_PINKY)
                        f5 = fingers[f];
                }
            }
                    //  não tá funcionando essa bosta
                    if (Input.GetKeyDown(KeyCode.M))
                    {
                        Debug.Log("apertei essa demonia");
                        comecarExercicio.passarMenu();
                    }
                    //conta_text_Abducao.enabled = true;
            if (concluido && f1!=null)//para evitar a loucura de mostrar que os exercícios estão concluídos sem nem ter começado
            {

                if (contadorNumeroDeExercicios+1 < exerciciosBolean.Length)//trocar essa lógica pois nem todos os exercícios vão ser necessariamente executados
                {
                    Debug.Log("Proximo exercicio");

                    exerciciosBolean[contadorNumeroDeExercicios] = false;
                    contadorNumeroDeExercicios++;
                    Debug.Log(contadorNumeroDeExercicios);
                    contadorAbducao = 0;
                    contadorLevant = 0;
                    auxiliarPinch = false;
                    conta_text_Pinch.text = "0";
                    exercicioConcluido.enabled = false;
                    exerciciosBolean[contadorNumeroDeExercicios] = true;
                    concluido = false;
                }
                else
                {
                    //conta_text_Abducao.enabled = false;
                    exercicioConcluido.enabled = true;
                    exercicioConcluido.text = "TODOS OS EXERCICIOS CONCLUIDOS! Aperte M para ir ao menu!";
                    nome_exercicio.text = "";
                    exportarCsv.dadosLinha = inputCsv;
                    exportarCsv.escreverArquivo();
                    Debug.Log("Parabens");
                }

            }

            /*if (concluido)
            {
                //bol = !bol;
                //Debug.Log("bol:" + bol);
                Debug.Log("Proximo exercicio");

                exerciciosBolean[contadorNumeroDeExercicios] = false;
                contadorNumeroDeExercicios++;
                contadorAbducao = 0;
                Debug.Log("oi " + contadorNumeroDeExercicios);
                contadorLevant = 0;
                auxiliarPinch = false;
                conta_text_Pinch.text = "0";
                exercicioConcluido.enabled = false;
                concluido = false;
                if (contadorNumeroDeExercicios < exerciciosBolean.Length)
                {

                    exerciciosBolean[contadorNumeroDeExercicios] = true;
                    concluido = false;
                }
                else
                {
                   // conta_text_Abducao.enabled = false;
                    exercicioConcluido.enabled = true;
                    exercicioConcluido.text = "TODOS OS EXERCICIOS CONCLUIDOS! Aperte M para ir ao menu!";
                    nome_exercicio.text = "";
                    Debug.Log("Parabens");
                }

            }*/
            if (f1!=null && f2 != null && f3!=null && f4!=null && f5!=null)//é preciso checar se todos os dedos estão visíveis para que não dê problema de compilação ou null inesperado
                    { 
                      
                        if (exerciciosBolean[0]) //AbdInd
                        {
                            //conta_text_Abducao.text = contadorAbducao.ToString();
                            info_exercicio.enabled = true;
                            nome_exercicio.text = "Contador de adução/abdução do indicador: " + contadorAbducao.ToString();
                            double anguloIndicadorMedio = RadianToDegree((double)f2.GetLeapFinger().TipPosition.AngleTo(f3.GetLeapFinger().TipPosition));
                            if (anguloIndicadorMedio > 4 && anguloIndicadorMedio <= 20 && contadorAbducao < qtdAbdInd &&
                                aux_texto_abd)
                            {
                                contadorAbducao++;
                                exercicioConcluido.enabled = false;
                                cuboProximoExercicio.SetActive(false);
                                proximoExercicio.enabled = false;
                                aux_texto_abd = false;
                                Debug.Log("Ângulo: " + anguloIndicadorMedio);
                                infoAngulos += anguloIndicadorMedio.ToString()+";";
                            }
                            if (anguloIndicadorMedio<=3)
                            {
                                aux_texto_abd = true;
                            }
                            if (contadorAbducao == qtdAbdInd)
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                //cuboProximoExercicio.SetActive(true);
                                //proximoExercicio.enabled = true;
                                info_exercicio.enabled = false;
                        // cuboProximoExercicio.SetActive(true); 
                        // proximoExercicio.enabled = true;
                                if (qtdAbdInd > 0)
                                {
                                    string[] auxiliarAngulos = inputCsv[indiceLinha[0]];
                                    auxiliarAngulos[2] = infoAngulos;
                                    inputCsv[indiceLinha[0]] = auxiliarAngulos;
                                }
                                infoAngulos = "";
                            }



                        }
                        else if (exerciciosBolean[1]) //AbdMedio
                        {
                            // proximoExercicio.enabled = true;
                            nome_exercicio.text = "Contador de aducao/abducao medio: "+ contadorAbducao.ToString(); ;
                            //conta_text_Abducao.text = contadorAbducao.ToString();
                            info_exercicio.enabled = true;

                            double anguloIndicadorMedio = RadianToDegree((double)f2.GetLeapFinger().TipPosition.AngleTo(f3.GetLeapFinger().TipPosition));
                            if (anguloIndicadorMedio > 4 && anguloIndicadorMedio <=20 && contadorAbducao < qtdAbdMed &&
                                aux_texto_abd)
                            {
                                contadorAbducao++;
                                exercicioConcluido.enabled = false;
                                cuboProximoExercicio.SetActive(false);
                                proximoExercicio.enabled = false;
                                aux_texto_abd = false;
                                Debug.Log("Ângulo médio: "+anguloIndicadorMedio);
                                infoAngulos += anguloIndicadorMedio.ToString() + ";";

                            }
                            if (anguloIndicadorMedio <=3)
                            {
                                aux_texto_abd = true;
                            }
                            if (contadorAbducao == qtdAbdMed)
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                info_exercicio.enabled = false;
                                if (qtdAbdMed > 0)
                                {
                                    string[] auxiliarAngulos = inputCsv[indiceLinha[1]];
                                    auxiliarAngulos[2] = infoAngulos;
                                    inputCsv[indiceLinha[1]] = auxiliarAngulos;
                                }
                                
                                infoAngulos = "";
                            }

                        }
                        else if (exerciciosBolean[2]) //abdAnl 
                        {

                            nome_exercicio.text = "Contador de aducao/abducao anelar: "+ contadorAbducao.ToString();
                            //conta_text_Abducao.text = contadorAbducao.ToString();
                            info_exercicio.enabled = true;
                            double anguloMedioAnelar = RadianToDegree((double)f3.GetLeapFinger().TipPosition.AngleTo(f4.GetLeapFinger().TipPosition));
                            if (anguloMedioAnelar > 4 && anguloMedioAnelar <= 20 && contadorAbducao < qtdAbdAnl &&
                                aux_texto_abd)
                            {
                                contadorAbducao++;
                                exercicioConcluido.enabled = false;
                                cuboProximoExercicio.SetActive(false);
                                proximoExercicio.enabled = false;
                                aux_texto_abd = false;
                                Debug.Log("Ângulo anelar: " + anguloMedioAnelar);
                                infoAngulos += anguloMedioAnelar.ToString() + ";";
                            }
                            if (anguloMedioAnelar<=3)
                            {
                                aux_texto_abd = true;
                            }
                            if (contadorAbducao == qtdAbdAnl)
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                info_exercicio.enabled = false;
                                if (qtdAbdAnl > 0)
                                {
                                    string[] auxiliarAngulos = inputCsv[indiceLinha[2]];
                                    auxiliarAngulos[2] = infoAngulos;
                                    inputCsv[indiceLinha[2]] = auxiliarAngulos;
                                }
                                infoAngulos = "";
                            }


                        }
                        else if (exerciciosBolean[3]) // abdMindi
                        {
                            nome_exercicio.text = "Contador de aducao/abducao mindinho: "+ contadorAbducao.ToString();
                    //conta_text_Abducao.text = contadorAbducao.ToString();
                            info_exercicio.enabled = true;
                            //double anguloMidiAnelar = RadianToDegree((double)f5.GetLeapFinger().TipPosition.AngleTo(f4.GetLeapFinger().TipPosition));
                   // Debug.Log("Angulo pontas dos dedos: "+anguloMidiAnelar);
                            Bone proximalAnelar = f4.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL);
                            Bone proximalMindinho = f5.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL);
                            double anguloMidiAnelar = RadianToDegree((double)proximalAnelar.NextJoint.AngleTo(proximalMindinho.NextJoint));
                            Debug.Log("Angulo proximais: " + anguloMidiAnelar);
                            
                            if (anguloMidiAnelar > 3.5 && anguloMidiAnelar <=20 && contadorAbducao < qtdAbdMindi &&
                                aux_texto_abd)
                            {
                                contadorAbducao++;
                                exercicioConcluido.enabled = false;
                                cuboProximoExercicio.SetActive(false);
                                proximoExercicio.enabled = false;
                                aux_texto_abd = false;
                                infoAngulos += anguloMidiAnelar.ToString() + ";";
                                //Debug.Log("Angulo proximais: " + anguloMidiAnelar);
                            }
                            if (anguloMidiAnelar <= 3.3)
                            {
                                aux_texto_abd = true;
                            }
                            if (contadorAbducao == qtdAbdMindi)
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                info_exercicio.enabled = false;
                                if (qtdAbdMindi > 0)
                                {
                                    string[] auxiliarAngulos = inputCsv[indiceLinha[3]];
                                    auxiliarAngulos[2] = infoAngulos;
                                    inputCsv[indiceLinha[3]] = auxiliarAngulos;
                                }
                                infoAngulos = "";
                            }
                            //não consigo fazer esse exercício, mudar abordagem de pegar ângulo de ossos proximais!!
                        }
                        else if (exerciciosBolean[4])//Levantamento
                        {
                            
                            //exerc�cio de levantar o dedo indicador
                            nome_exercicio.text = "Contador de extensão do indicador: "+ contadorLevant.ToString();
                    //conta_text_Abducao.text = contadorLevant.ToString();
                            info_exercicio.enabled = true;
                            info_exercicio.texture = (Texture)Resources.Load("rv_instru_levant");
                    // 
                            Vector ponta = f2.GetLeapFinger().TipPosition;
                            Vector juntaProximal = f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                            Vector distanciaPontaJunta = new Vector(ponta.x-juntaProximal.x,ponta.y- juntaProximal.y,ponta.z- juntaProximal.z);
                            double anguloAlfa = RadianToDegree((double)distanciaPontaJunta.AngleTo(f2.GetLeapHand().PalmNormal));
                           /// double anguloAlfa = RadianToDegree((double)f2.GetLeapFinger().TipPosition.AngleTo(f2.GetLeapHand().PalmPosition));
                            anguloAlfa = anguloAlfa - 90; //precisa diminuir pois o ângulo é mais amplo
                            Debug.Log(anguloAlfa);
                            if (anguloAlfa > 4 && anguloAlfa <=30 && aux_texto_levant && contadorLevant < qtdLevantamento)
                            {

                                aux_texto_levant = false;
                                contadorLevant++;
                                exercicioConcluido.enabled = false;
                                cuboProximoExercicio.SetActive(false);
                                proximoExercicio.enabled = false;
                                infoAngulos += anguloAlfa.ToString() + ";";
                            }
                            if (anguloAlfa <= 3)
                            {
                                aux_texto_levant = true;
                            }
                            if (contadorLevant == qtdLevantamento)
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                info_exercicio.enabled = false;
                                if (qtdLevantamento > 0)
                                {
                                    string[] auxiliarAngulos = inputCsv[indiceLinha[4]];
                                    auxiliarAngulos[2] = infoAngulos;
                                    inputCsv[indiceLinha[4]] = auxiliarAngulos;
                                }
                            }

                        }
                        else if (exerciciosBolean[5]) //pinchInd
                        {

                            nome_exercicio.text = "Contador de Pinça (Indicador): "+ conta_text_Pinch.text;
                            info_exercicio.enabled = true;
                            info_exercicio.texture = (Texture)Resources.Load("rv_instru_pinca");
                            //conta_text_Abducao.text = contadorPinchInd.ToString();
                            exercicioConcluido.enabled = false;
                            if ((contadorPinchInd < qtdPinchInd))//indicador - usa o PinchDetector do LeapMotion, que já é calibrado para ele 
                            {
                                contadorPinch = int.Parse(conta_text_Pinch.text);
                                Debug.Log("conta_text_Pinch " + conta_text_Pinch.text);
                                contadorPinchInd = int.Parse(conta_text_Pinch.text);
                                Debug.Log("contadorPinchInd " + contadorPinchInd.ToString());
                                auxiliarPinch = false;
                                //conta_text_Abducao.text = conta_text_Pinch.text;
                            }

                            if ((contadorPinchInd == qtdPinchInd))
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                info_exercicio.enabled = false;
                                /*if(qtdPinchInd > 0)
                                {
                                    string[] auxiliarAngulos = inputCsv[indiceLinha[5]];
                                    auxiliarAngulos[2] = infoAngulos;
                                    inputCsv[indiceLinha[5]] = auxiliarAngulos;
                                }
                                infoAngulos = "";*/
                            }

                        }
                        else if (exerciciosBolean[6])//pinchMed
                        {

                            nome_exercicio.text = "Contador de Pinça (Médio): "+ contadorPinchMed.ToString(); 
                            exercicioConcluido.enabled = false;
                            //dedo médio
                            if (contadorPinchMed < qtdPinchMed && pinchouDedos(f3.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition) && auxiliarPinch)
                            {
                                if ((qtdPinchMed - contadorPinchMed) == 1) { PinchBolean[1] = false; PinchBolean[2] = true; }

                                contadorPinchMed++;
                                Debug.Log("Pinchou médio: " + contadorPinchMed.ToString());
                                Debug.Log("conta_text_Pinch" + conta_text_Pinch.text);
                                auxiliarPinch = false;
                             //   conta_text_Abducao.text = contadorPinchMed.ToString();

                            }
                            else if (!pinchouDedos(f3.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition)) auxiliarPinch = true;

                            if (contadorPinchMed == qtdPinchMed)
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                info_exercicio.enabled = false;
                            }

                        }
                        else if (exerciciosBolean[7])//pinchAnl
                        {

                            nome_exercicio.text = "Contador de Pinça (Anelar): "+ contadorPinchAnl.ToString();
                            exercicioConcluido.enabled = false;
                            if (contadorPinchAnl < qtdPinchAnl && pinchouDedos(f4.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition) && auxiliarPinch)
                            {
                                contadorPinchAnl++;
                                Debug.Log("Pinchou anelar: " + contadorPinchAnl.ToString());
                                auxiliarPinch = false;
                                //conta_text_Abducao.text = contadorPinchAnl.ToString();
                            }
                            else if (!pinchouDedos(f4.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition)) auxiliarPinch = true;

                            if (contadorPinchAnl == qtdPinchAnl)
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                info_exercicio.enabled = false;
                            }

                        }
                        else if (exerciciosBolean[8])//pinchMindi
                        {

                            nome_exercicio.text = "Contador de Pinça (Mindinho): "+ contadorPinchMindi.ToString();
                    //mindinho
                    if (contadorPinchMindi < qtdPinchMindi && pinchouDedos(f5.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition) && auxiliarPinch)
                            {
                                contadorPinchMindi++;
                                Debug.Log("Pinchou mindinho: " + contadorPinchMindi.ToString());
                                auxiliarPinch = false;
                                //conta_text_Abducao.text = contadorPinchMindi.ToString();
                                Debug.Log("Ind " + contadorPinchInd + " Med " + contadorPinchMed + " Anl " + contadorPinchAnl + " Mindi " + contadorPinchMindi);
                            }
                            else if (!pinchouDedos(f5.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition)) auxiliarPinch = true;



                            cuboProximoExercicio.SetActive(false);
                            proximoExercicio.enabled = false;


                            if (contadorPinchMindi == qtdPinchMindi)
                            {
                                concluido = true;
                                exercicioConcluido.enabled = true;
                                info_exercicio.enabled = false;
                            }

                        }
            }
                
            


      if (palm != null) {
        Rigidbody palmBody = palm.GetComponent<Rigidbody>();
        if (palmBody) {
          palmBody.MovePosition(GetPalmCenter());
          palmBody.MoveRotation(GetPalmRotation());
        } else {
          palm.position = GetPalmCenter();
          palm.rotation = GetPalmRotation();
        }
      }

      if (forearm != null) {
        // Set arm dimensions.
        CapsuleCollider capsule = forearm.GetComponent<CapsuleCollider>();
        if (capsule != null) {
          // Initialization
          capsule.direction = 2;
          forearm.localScale = new Vector3(1f / transform.lossyScale.x, 1f / transform.lossyScale.y, 1f / transform.lossyScale.z);

          // Update
          capsule.radius = GetArmWidth() / 2f;
          capsule.height = GetArmLength() + GetArmWidth();
        }

        Rigidbody forearmBody = forearm.GetComponent<Rigidbody>();
        if (forearmBody) {
          forearmBody.MovePosition(GetArmCenter());
          forearmBody.MoveRotation(GetArmRotation());
        } else {
          forearm.position = GetArmCenter();
          forearm.rotation = GetArmRotation();
        }
      }
    }
  }
}
