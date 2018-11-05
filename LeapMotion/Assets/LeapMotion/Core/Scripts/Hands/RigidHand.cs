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
        public static string maoEscolhida = comecarExercicio.maoPaciente==null?"":comecarExercicio.maoPaciente;
        public static int anguloMinAbdInd = int.Parse(comecarExercicio.anguloAbdInd == null ? "4" : comecarExercicio.anguloAbdInd);//não começar em 0 para ter controle melhor
        public static int anguloMinAbdMed = int.Parse(comecarExercicio.anguloAbdMed == null ? "4" : comecarExercicio.anguloAbdMed);
        public static int anguloMinAbdAnl = int.Parse(comecarExercicio.anguloAbdAnl == null ? "4" : comecarExercicio.anguloAbdAnl);
        public static int anguloMinAbdMind = int.Parse(comecarExercicio.anguloAbdMind == null ? "4" : comecarExercicio.anguloAbdMind);
        public static int anguloMinExtensao = int.Parse(comecarExercicio.anguloLevantamento == null ? "10":comecarExercicio.anguloLevantamento);
        public static int anguloMinExtensaoMedio = int.Parse(comecarExercicio.anguloExtMed == null ? "10" : comecarExercicio.anguloExtMed);
        public static int anguloMinExtensaoAnl = int.Parse(comecarExercicio.anguloExtAnl == null ? "10" : comecarExercicio.anguloExtAnl);
        public static int anguloMinExtensaoMindi = int.Parse(comecarExercicio.anguloExtMindi == null ? "10" : comecarExercicio.anguloExtMindi);
        public static int qtdAbdInd = int.Parse(comecarExercicio.passAbdAduInd==null? "0" : comecarExercicio.passAbdAduInd);
        public static int qtdAbdMed = int.Parse(comecarExercicio.passAbdAduMed==null ? "0" : comecarExercicio.passAbdAduMed);
        public static int qtdAbdAnl = int.Parse(comecarExercicio.passAbdAduAnl==null? "0" : comecarExercicio.passAbdAduAnl);
        public static int qtdAbdMindi = int.Parse(comecarExercicio.passAbdAduMindi==null? "0" : comecarExercicio.passAbdAduMindi);
        public static int contadorLevant = 0;//esse � pra levantar dedo
        public static int qtdLevantamento = int.Parse(comecarExercicio.passarLevantamento==null? "0" : comecarExercicio.passarLevantamento);
        public static int qtdExtensaoMedio = int.Parse(comecarExercicio.passarExtMed == null ? "0" : comecarExercicio.passarExtMed);
        public static int qtdExtensaoAnl = int.Parse(comecarExercicio.passarExtAnl == null ? "0" : comecarExercicio.passarExtAnl);
        public static int qtdExtensaoMindi = int.Parse(comecarExercicio.passarExtMindi == null ? "0" : comecarExercicio.passarExtMindi);
        public static int contadorNumeroDeExercicios = 0;
        public static int contadorPinch;
        public static int qtdPinch = int.Parse(comecarExercicio.passPinchInd==null? "0" : comecarExercicio.passPinchInd);// vai tirar depois
        public static int qtdPinchInd = int.Parse(comecarExercicio.passPinchInd==null? "0" : comecarExercicio.passPinchInd);
        public static int qtdPinchMed = int.Parse(comecarExercicio.passPinchMed==null? "0" : comecarExercicio.passPinchMed);
        public static int qtdPinchAnl = int.Parse(comecarExercicio.passPinchAnl==null? "0" : comecarExercicio.passPinchAnl);
        public static int qtdPinchMindi = int.Parse(comecarExercicio.passPinchMindi==null? "0" : comecarExercicio.passPinchMindi);
        //public static bool bol = false;
        public static bool[] exerciciosBolean = { true, false, false, false, false, false, false, false, false, false, false, false };//aumentar tamanho do array para dar conta de todos os outros 3
        public static bool[] PinchBolean = { true, false, false, false };
        public static bool concluido = false;
        public static bool aux_texto_abd = true;
        public static bool aux_texto_levant = true;//esse � pra levantar dedo
        public static bool auxiliarPinch = true;
        public static bool escreveuArquivo = false;
        public static int contadorPinchInd = 0;
        public static int contadorPinchMed = 0;
        public static int contadorPinchAnl = 0;
        public static int contadorPinchMindi = 0;
        //array para guardar o índice da linha na tabela
        public static int[] indiceLinha = exportarCsv.indiceLinha;
        public static string infoAngulos = "";
        public static List<string[]> inputCsv = exportarCsv.dadosLinha;

        public override bool SupportsEditorPersistence() {
            return true;
        }

    public override void InitHand() {
      base.InitHand();
      
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

        public float produto_escalar(Vector vetor1, Vector vetor2)
        {
            float resultado = 0.0f;
            resultado = (vetor1.x * vetor2.x) + (vetor1.y * vetor2.y) + (vetor1.z * vetor2.z);
            return resultado;
        }

        public Vector vetorUnitario(Vector v)
        {
            float modulo = modulo_vetor(v);
            return new Vector((v.x)/modulo,(v.y)/modulo,(v.z)/modulo);
        }

        public float anguloFlexaoProximalPinch(FingerModel f)
        {
            Vector juntaProximal1 = f.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
            Vector juntaProximal2 = f.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint;
            Vector juntaIntermedial = f.GetLeapFinger().Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint;
            Vector vetor1 = new Vector(juntaProximal2.x - juntaProximal1.x, juntaProximal2.y - juntaProximal1.y, juntaProximal2.z - juntaProximal1.z);
            Vector vetor2 = new Vector(juntaIntermedial.x - juntaProximal2.x, juntaIntermedial.y - juntaProximal2.y, juntaIntermedial.z - juntaProximal2.z);
            return vetor2.Normalized.AngleTo(vetor1.Normalized);
        }
        /* public Vector inverteCoordenadas(Vector v)//de acordo com a documentação do Leap, o Unity é orientado pela mão esquerda, portanto as coordenadas devem ser multiplicadas por -1; não uso InteractionBox porque não existe no Unity.
         {
             //v.z *= -1.0f;
             //v.y *= -1.0f;
             v.x *= -1.0f;
             return v.Normalized;
         }*/
        public float angulo_dedos(Vector f1, Vector f2)
         {
             return Mathf.Acos((produto_escalar(f1, f2)) / (modulo_vetor(f1) * modulo_vetor(f2)));
         }

        public float modulo_vetor(Vector vet)
        {
            return Mathf.Sqrt(Mathf.Pow(vet.x, 2) + Mathf.Pow(vet.y, 2) + Mathf.Pow(vet.z, 2));
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Mathf.PI);
        }

        private double anguloProjecao(Vector a, Vector b)
        {
            Vector unitario = vetorUnitario(b);
            float a1 = produto_escalar(a, unitario);
            Vector projecaoDist = new Vector(unitario.x * a1, unitario.y * a1, unitario.z * a1);
            Vector a2 = new Vector(a.x - projecaoDist.x, a.y - projecaoDist.y, a.z - projecaoDist.z);
            float modulosA2Dist = modulo_vetor(a2) / modulo_vetor(projecaoDist);
            float tangente = Mathf.Tan(modulosA2Dist);
            return RadianToDegree(Mathf.Atan(tangente));
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
                    //conta_text_Abducao.enabled = true;
            if (concluido && f1!=null)//para evitar a loucura de mostrar que os exercícios estão concluídos sem nem ter começado
            {

                if (contadorNumeroDeExercicios+1 < exerciciosBolean.Length)//trocar essa lógica pois nem todos os exercícios vão ser necessariamente executados
                {
                   // Debug.Log("Proximo exercicio");

                    exerciciosBolean[contadorNumeroDeExercicios] = false;
                    contadorNumeroDeExercicios++;
                   // Debug.Log(contadorNumeroDeExercicios);
                    contadorAbducao = 0;
                    contadorLevant = 0;
                    /*contadorPinchInd = 0;
                    contadorPinchMed = 0;
                    contadorPinchAnl = 0;
                    contadorPinchMindi = 0;*/
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

                     if (escreveuArquivo == false)//para evitar que seja escrito várias vezes quando passar no else
                     {
                         exportarCsv.dadosLinha = inputCsv;
                         exportarCsv.escreverArquivo();
                         escreveuArquivo = true;
                     }
                    
                }

            }

            if ((f1!=null && f2 != null && f3!=null && f4!=null && f5!=null)&&((f2.GetLeapHand().IsRight && maoEscolhida=="Direita")||(f2.GetLeapHand().IsLeft && maoEscolhida=="Esquerda"
                )))//é preciso checar se todos os dedos estão visíveis para que não dê problema de compilação ou null inesperado
                    {

                if (exerciciosBolean[0]) //AbdInd
                {
                    
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_abd");
                    nome_exercicio.text = "Contador de adução/abdução do indicador: " + contadorAbducao.ToString();
                    Vector metacarpo = f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint;
                    Vector pontaDedoInd = f2.GetLeapFinger().TipPosition;
                    Vector pontaDedoMed = f3.GetLeapFinger().TipPosition;
                    Vector diferencaPontaIndMetacarpo = new Vector(pontaDedoInd.x - metacarpo.x, pontaDedoInd.y - metacarpo.y, pontaDedoInd.z - metacarpo.z);
                    Vector diferencaPontaMedMetacarpo = new Vector(pontaDedoMed.x - metacarpo.x, pontaDedoMed.y - metacarpo.y, pontaDedoMed.z - metacarpo.z);

                    diferencaPontaIndMetacarpo.x *= (-1);//if (f2.GetLeapHand().IsRight)

                    double anguloMetacInd = RadianToDegree(diferencaPontaIndMetacarpo.AngleTo(f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).Direction));
                    double anguloMetacMed = RadianToDegree(diferencaPontaIndMetacarpo.AngleTo(f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).Direction));
                    
                    Debug.Log(anguloMetacInd);
                    if (anguloMinAbdInd <= anguloMetacInd && anguloMetacInd <= 20 && contadorAbducao < qtdAbdInd &&
                        aux_texto_abd)
                    {
                        contadorAbducao++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        aux_texto_abd = false;
                        //Debug.Log("Ângulo indicador usando osso metacarpo médio: " + anguloMetacMed.ToString("n2"));
                        // Debug.Log("Ângulo indicador usando osso metacarpo indicador: " + anguloMetacInd.ToString("n2"));

                        infoAngulos += anguloMetacInd.ToString("n2") + "(indicador)-" + anguloMetacMed.ToString("n2") + "(médio);";

                    }
                    if (anguloMetacInd < anguloMinAbdInd - 2)
                    {
                        //Debug.Log("Ângulo para fechar: " + anguloIndicadorMedio);
                        //Debug.Log("Ângulo usando osso metacarpo pra fechar: " + anguloMetacMed);
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
                    
                    nome_exercicio.text = "Contador de aducao/abducao medio: " + contadorAbducao.ToString(); ;
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_abd");
                    Vector metacarpo = f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint;

                    Vector pontaDedoMed = f3.GetLeapFinger().TipPosition;
                    Vector proximalMedio = f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint;
                    Vector diferencaPontaMedMetacarpo = new Vector(pontaDedoMed.x - metacarpo.x, pontaDedoMed.y - metacarpo.y, pontaDedoMed.z - metacarpo.z);
                    Vector diferencaProxMetacarpo = new Vector(proximalMedio.x - metacarpo.x, proximalMedio.y - metacarpo.y, proximalMedio.z - metacarpo.z);
                    double anguloMetacMed = RadianToDegree(f3.GetLeapHand().Direction.Normalized.AngleTo(diferencaPontaMedMetacarpo.Normalized)) - 8;//correção   
                    double anguloIndicadorMedio = RadianToDegree((double)f4.GetLeapFinger().TipPosition.Normalized.AngleTo(f3.GetLeapFinger().TipPosition.Normalized));

                    diferencaProxMetacarpo.x *= (-1);
                    double anguloMetaPalma = RadianToDegree((double)diferencaProxMetacarpo.AngleTo(f3.GetLeapHand().Direction.Normalized));
                    /*if (anguloIndicadorMedio > 3 && anguloIndicadorMedio <= 20 && contadorAbducao < qtdAbdMed &&
                        aux_texto_abd)
                    {*/
                    Debug.Log("Ângulo usando metacarpo com direção da palma: " + anguloMetaPalma.ToString("n2") + ";" + anguloMetacMed.ToString("n2"));
                    if (anguloMinAbdMed <= anguloMetaPalma && anguloMetaPalma <= 20 && contadorAbducao < qtdAbdMed &&
                        aux_texto_abd)
                    {
                        contadorAbducao++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        aux_texto_abd = false;
                        Debug.Log("Ângulo médio: " + RadianToDegree((double)angulo_dedos(diferencaProxMetacarpo, f3.GetLeapHand().Direction.Normalized)));
                        infoAngulos += anguloMetaPalma.ToString("n2") + "(palma)-" + anguloMetacMed.ToString("n2") + "(médio);";
                        /*Debug.Log("Ângulo usando osso metacarpo médio: " + anguloMetacMed);*/
                        //Debug.Log("Ângulo usando metacarpo com direção da palma: " + anguloMetaPalma.ToString("n2"));
                    }
                    if (anguloMinAbdMed - 2 > anguloMetaPalma)
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

                    nome_exercicio.text = "Contador de aducao/abducao anelar: " + contadorAbducao.ToString();
                    //conta_text_Abducao.text = contadorAbducao.ToString();
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_abd");

                    Vector metacarpo = f4.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint;
                    Vector pontaDedoAnl = f4.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint;
                    Vector diferencaPontaMet = new Vector(pontaDedoAnl.x - metacarpo.x, pontaDedoAnl.y - metacarpo.y, pontaDedoAnl.z - metacarpo.z);
                    if (f4.GetLeapHand().IsRight) diferencaPontaMet.x *= (-1);
                    double anguloMetacarpoAnl = RadianToDegree(diferencaPontaMet.Normalized.AngleTo(f4.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).Direction));
                    double anguloMetacarpo = RadianToDegree(diferencaPontaMet.Normalized.AngleTo(f3.GetLeapHand().Direction.Normalized));
                    //Debug.Log("Ângulo anelar com metacarpo: " + anguloMetacarpo);
                    //Debug.Log("Ângulo anelar com metacarpo médio: " + anguloMetacarpo2);
                    if (anguloMinAbdAnl <= anguloMetacarpo && anguloMetacarpo <= 20 && contadorAbducao < qtdAbdAnl &&
                        aux_texto_abd)
                    {
                        contadorAbducao++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        aux_texto_abd = false;
                        Debug.Log("Ângulo anelar: " + RadianToDegree((double)angulo_dedos(diferencaPontaMet, f3.GetLeapHand().Direction.Normalized)));
                        Debug.Log("Ângulo anelar com direção palma: " + anguloMetacarpo + "; com metacarpo anelar: " + anguloMetacarpoAnl);
                        // Debug.Log("Ângulo anelar com metacarpo médio: " + anguloMetacarpo2);
                        infoAngulos += anguloMetacarpo.ToString("n2") + "(palma) - " + anguloMetacarpoAnl.ToString("n2") + "(metacarpo);";
                    }
                    if (anguloMetacarpo < anguloMinAbdAnl - 2)
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
                    nome_exercicio.text = "Contador de aducao/abducao mindinho: " + contadorAbducao.ToString();
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_abd");

                    Bone proximalAnelar = f4.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL);
                    Bone proximalMindinho = f5.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL);
                    double anguloMidiAnelar = RadianToDegree((double)proximalAnelar.NextJoint.AngleTo(proximalMindinho.NextJoint));
                    // Debug.Log("Angulo proximais: " + anguloMidiAnelar);
                    Vector metacarpoMindinho = f5.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).NextJoint;
                    Vector proximalMind = f5.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint;
                    Vector diferenca = new Vector(proximalMind.x - metacarpoMindinho.x, proximalMind.y - metacarpoMindinho.y, proximalMind.z - metacarpoMindinho.z);
                    diferenca.x *= (-1);
                    double anguloMetacarpo = RadianToDegree(diferenca.Normalized.AngleTo(f5.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).Direction));
                    double anguloMetacarpo2 = RadianToDegree(diferenca.Normalized.AngleTo(f3.GetLeapHand().Direction.Normalized));

                    anguloMetacarpo = anguloMetacarpo - 8;
                    //  Debug.Log("Angulo metacarpo mindinho: " + anguloMetacarpo + "; metacarpo com direção da palma: " + anguloMetacarpo2);
                    if (anguloMinAbdMind <= anguloMetacarpo && anguloMetacarpo <= 20 && contadorAbducao < qtdAbdMindi &&
                        aux_texto_abd)
                    {
                        contadorAbducao++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        aux_texto_abd = false;
                        infoAngulos += anguloMetacarpo.ToString("n2") + "(metacarpo mindinho)-" + anguloMetacarpo2.ToString("n2") + "(palma);";
                        // Debug.Log("Angulo metacarpo mindinho: " + anguloMetacarpo+"; método próprio: "+RadianToDegree((double)angulo_dedos(diferenca.Normalized, f5.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).Direction)));

                    }
                    if (anguloMetacarpo < anguloMinAbdMind - 2)
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

                }
                else if (exerciciosBolean[4])//Levantamento
                {

                    //exerc�cio de levantar o dedo indicador
                    nome_exercicio.text = "Contador de extensão do indicador: " + contadorLevant.ToString();
                    //conta_text_Abducao.text = contadorLevant.ToString();
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_levant");
                    // 
                    Vector ponta = f2.GetLeapFinger().TipPosition;
                    Vector juntaProximal = f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                    Vector distanciaPontaJunta = new Vector(ponta.x - juntaProximal.x, ponta.y - juntaProximal.y, ponta.z - juntaProximal.z);
                    double anguloAlfa = RadianToDegree((double)distanciaPontaJunta.Normalized.AngleTo(f2.GetLeapHand().PalmNormal));
                    //Debug.Log("Normal da palma: " + f2.GetLeapHand().PalmNormal);
                    // double anguloAlfa = RadianToDegree((double)f2.GetLeapFinger().TipPosition.AngleTo(f2.GetLeapHand().PalmPosition));
                    anguloAlfa = anguloAlfa - 90; //precisa diminuir pois o ângulo é mais amplo
                    double anguloProj = anguloProjecao(distanciaPontaJunta, f2.GetLeapHand().Direction);
                    Debug.Log(anguloAlfa + ";projecao: " + anguloProj);
                    if (anguloAlfa >= anguloMinExtensao && anguloAlfa <= 30 && aux_texto_levant && contadorLevant < qtdLevantamento)
                    {

                        aux_texto_levant = false;
                        contadorLevant++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        infoAngulos += anguloAlfa.ToString("n2") + "(normal da palma) - (projeção vetorial): " + anguloProj.ToString("n2") + ";";
                        // Debug.Log(anguloAlfa);
                    }
                    if (anguloAlfa < anguloMinExtensao - 2)
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
                        infoAngulos = "";
                    }

                }
                else if (exerciciosBolean[5])//extensão do dedo médio
                {
                    //exerc�cio de levantar o dedo indicador
                    nome_exercicio.text = "Contador de extensão do dedo médio: " + contadorLevant.ToString();
                    //conta_text_Abducao.text = contadorLevant.ToString();
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_levant");
                    // 
                    Vector ponta = f3.GetLeapFinger().TipPosition;
                    Vector juntaProximal = f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                    Vector distanciaPontaJunta = new Vector(ponta.x - juntaProximal.x, ponta.y - juntaProximal.y, ponta.z - juntaProximal.z);
                    //calcular projeção para ver se fica melhor de pegar o ângulo

                    double anguloAlfa = RadianToDegree((double)distanciaPontaJunta.Normalized.AngleTo(f3.GetLeapHand().PalmNormal));
                    anguloAlfa = (anguloAlfa - 90);
                    double anguloProj = anguloProjecao(distanciaPontaJunta, f3.GetLeapHand().Direction);
                    //double anguloBeta = RadianToDegree((double)distanciaPontaJunta.Normalized.AngleTo(f3.GetLeapHand().Direction));
                    Debug.Log(anguloAlfa + "; projeção: " + anguloProj);
                    if (anguloAlfa >= anguloMinExtensaoMedio && anguloAlfa <= 30 && aux_texto_levant && contadorLevant < qtdExtensaoMedio)
                    {

                        aux_texto_levant = false;
                        contadorLevant++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        infoAngulos += anguloAlfa.ToString("n2") + ";";
                        //Debug.Log(anguloAlfa);
                    }
                    if (anguloAlfa < anguloMinExtensaoMedio)
                    {
                        aux_texto_levant = true;
                    }
                    if (contadorLevant == qtdExtensaoMedio)
                    {
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        info_exercicio.enabled = false;
                        if (qtdExtensaoMedio > 0)
                        {
                            string[] auxiliarAngulos = inputCsv[indiceLinha[5]];
                            auxiliarAngulos[2] = infoAngulos;
                            inputCsv[indiceLinha[5]] = auxiliarAngulos;
                        }
                        infoAngulos = "";
                    }

                }
                else if (exerciciosBolean[6]) //extensão anelar
                {
                    nome_exercicio.text = "Contador de extensão do dedo anelar: " + contadorLevant.ToString();
                    //conta_text_Abducao.text = contadorLevant.ToString();
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_levant");
                    // 
                    Vector ponta = f4.GetLeapFinger().TipPosition;
                    Vector juntaProximal = f4.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                    Vector distanciaPontaJunta = new Vector(ponta.x - juntaProximal.x, ponta.y - juntaProximal.y, ponta.z - juntaProximal.z);
                    //calcular projeção para ver se fica melhor de pegar o ângulo

                    double anguloAlfa = RadianToDegree((double)distanciaPontaJunta.Normalized.AngleTo(f4.GetLeapHand().PalmNormal));
                    anguloAlfa = (anguloAlfa - 90);
                    double anguloProj = anguloProjecao(distanciaPontaJunta, f4.GetLeapHand().Direction);
                    //double anguloBeta = RadianToDegree((double)distanciaPontaJunta.Normalized.AngleTo(f3.GetLeapHand().Direction));
                    Debug.Log(anguloAlfa + "; projeção: " + anguloProj);
                    if (anguloAlfa >= anguloMinExtensaoAnl && anguloAlfa <= 30 && aux_texto_levant && contadorLevant < qtdExtensaoAnl)
                    {

                        aux_texto_levant = false;
                        contadorLevant++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        infoAngulos += anguloAlfa.ToString("n2") + ";";
                        //Debug.Log(anguloAlfa);
                    }
                    if (anguloAlfa < anguloMinExtensaoAnl)
                    {
                        aux_texto_levant = true;
                    }
                    if (contadorLevant == qtdExtensaoAnl)
                    {
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        info_exercicio.enabled = false;
                        if (qtdExtensaoAnl > 0)
                        {
                            string[] auxiliarAngulos = inputCsv[indiceLinha[6]];
                            auxiliarAngulos[2] = infoAngulos;
                            inputCsv[indiceLinha[6]] = auxiliarAngulos;
                        }
                        infoAngulos = "";
                    }

                }
                else if (exerciciosBolean[7]) //extensão mindinho
                {
                    nome_exercicio.text = "Contador de extensão do dedo mindinho: " + contadorLevant.ToString();
                    //conta_text_Abducao.text = contadorLevant.ToString();
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_levant");
                    // 
                    Vector ponta = f5.GetLeapFinger().TipPosition;
                    Vector juntaProximal = f5.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                    Vector distanciaPontaJunta = new Vector(ponta.x - juntaProximal.x, ponta.y - juntaProximal.y, ponta.z - juntaProximal.z);
                    //calcular projeção para ver se fica melhor de pegar o ângulo

                    double anguloAlfa = RadianToDegree((double)distanciaPontaJunta.Normalized.AngleTo(f5.GetLeapHand().PalmNormal));
                    anguloAlfa = (anguloAlfa - 90);
                    double anguloProj = anguloProjecao(distanciaPontaJunta, f5.GetLeapFinger().Bone(Bone.BoneType.TYPE_METACARPAL).Direction);
                    //double anguloBeta = RadianToDegree((double)distanciaPontaJunta.Normalized.AngleTo(f3.GetLeapHand().Direction));
                    Debug.Log(anguloAlfa + "; projeção: " + anguloProj);
                    if (anguloAlfa >= anguloMinExtensaoMindi && anguloAlfa <= 30 && aux_texto_levant && contadorLevant < qtdExtensaoMindi)
                    {

                        aux_texto_levant = false;
                        contadorLevant++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        infoAngulos += anguloAlfa.ToString("n2") + ";";
                        //Debug.Log(anguloAlfa);
                    }
                    if (anguloAlfa < anguloMinExtensaoMindi)
                    {
                        aux_texto_levant = true;
                    }
                    if (contadorLevant == qtdExtensaoMindi)
                    {
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        info_exercicio.enabled = false;
                        if (qtdExtensaoMindi > 0)
                        {
                            string[] auxiliarAngulos = inputCsv[indiceLinha[7]];
                            auxiliarAngulos[2] = infoAngulos;
                            inputCsv[indiceLinha[7]] = auxiliarAngulos;
                        }
                        infoAngulos = "";
                    }

                }
                else if(exerciciosBolean[8])//pinca indicador
                {


                    nome_exercicio.text = "Contador de Pinça (Indicador): " + conta_text_Pinch.text;
                    info_exercicio.enabled = true;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_pinca");
                    
                    exercicioConcluido.enabled = false;

                    
                    if ((contadorPinchInd < qtdPinchInd))//indicador - usa o PinchDetector do LeapMotion, que já é calibrado para ele 
                    {
                        contadorPinch = int.Parse(conta_text_Pinch.text);
                        
                        contadorPinchInd = int.Parse(conta_text_Pinch.text);
                        
                        auxiliarPinch = false;
                    }

                    if ((contadorPinchInd == qtdPinchInd))
                    {
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        info_exercicio.enabled = false;
                        if (qtdPinchInd > 0)
                        {
                            string[] auxiliarAngulos = inputCsv[indiceLinha[8]];
                            auxiliarAngulos[2] = PinchDetector.infoAngulos;
                            inputCsv[indiceLinha[8]] = auxiliarAngulos;
                        }
                        infoAngulos = "";
                    }

                }
                else if (exerciciosBolean[9])//pinchMed
                {

                    nome_exercicio.text = "Contador de Pinça (Médio): " + contadorPinchMed.ToString();
                    exercicioConcluido.enabled = false;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_pinca");
                    //dedo médio
                    if (contadorPinchMed < qtdPinchMed && pinchouDedos(f3.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition) && auxiliarPinch)
                    {
                        if ((qtdPinchMed - contadorPinchMed) == 1) { PinchBolean[1] = false; PinchBolean[2] = true; }

                        contadorPinchMed++;
                        double anguloProximal = RadianToDegree(anguloFlexaoProximalPinch(f3));
                        // Debug.Log("Pinchou médio: " + anguloProximal);
                        // Debug.Log("conta_text_Pinch" + conta_text_Pinch.text);
                        auxiliarPinch = false;
                        infoAngulos += anguloProximal.ToString("n2") + ";";
                        //   conta_text_Abducao.text = contadorPinchMed.ToString();

                    }
                    else if (!pinchouDedos(f3.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition)) auxiliarPinch = true;

                    if (contadorPinchMed == qtdPinchMed)
                    {
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        info_exercicio.enabled = false;
                        if (qtdPinchMed > 0)
                        {
                            string[] auxiliarAngulos = inputCsv[indiceLinha[9]];
                            auxiliarAngulos[2] = infoAngulos;
                            inputCsv[indiceLinha[9]] = auxiliarAngulos;
                        }
                        infoAngulos = "";
                    }

                }
                else if (exerciciosBolean[10])//pinchAnl
                {

                    nome_exercicio.text = "Contador de Pinça (Anelar): " + contadorPinchAnl.ToString();
                    exercicioConcluido.enabled = false;
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_pinca");
                    if (contadorPinchAnl < qtdPinchAnl && pinchouDedos(f4.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition) && auxiliarPinch)
                    {
                        contadorPinchAnl++;
                        double anguloProximal = RadianToDegree(anguloFlexaoProximalPinch(f4));
                        //Debug.Log("Pinchou anelar: " + anguloProximal);
                        auxiliarPinch = false;
                        infoAngulos += anguloProximal.ToString("n2") + ";";
                        //conta_text_Abducao.text = contadorPinchAnl.ToString();
                    }
                    else if (!pinchouDedos(f4.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition)) auxiliarPinch = true;

                    if (contadorPinchAnl == qtdPinchAnl)
                    {
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        info_exercicio.enabled = false;
                        if (qtdPinchAnl > 0)
                        {
                            string[] auxiliarAngulos = inputCsv[indiceLinha[10]];
                            auxiliarAngulos[2] = infoAngulos;
                            inputCsv[indiceLinha[10]] = auxiliarAngulos;
                        }
                        infoAngulos = "";
                    }

                }
                else if (exerciciosBolean[11])//pinchMindi
                {

                    nome_exercicio.text = "Contador de Pinça (Mindinho): " + contadorPinchMindi.ToString();
                    info_exercicio.texture = (Texture)Resources.Load("rv_instru_pinca");
                    //mindinho
                    if (contadorPinchMindi < qtdPinchMindi && pinchouDedos(f5.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition) && auxiliarPinch)
                    {
                        contadorPinchMindi++;
                        double anguloProximal = RadianToDegree(anguloFlexaoProximalPinch(f4));
                        //Debug.Log("Pinchou mindinho: " + anguloProximal);
                        infoAngulos += anguloProximal.ToString("n2") + ";";
                        auxiliarPinch = false;
                        //conta_text_Abducao.text = contadorPinchMindi.ToString();
                        // Debug.Log("Ind " + contadorPinchInd + " Med " + contadorPinchMed + " Anl " + contadorPinchAnl + " Mindi " + contadorPinchMindi);
                    }
                    else if (!pinchouDedos(f5.GetLeapFinger().TipPosition, f1.GetLeapFinger().TipPosition)) auxiliarPinch = true;



                    cuboProximoExercicio.SetActive(false);
                    proximoExercicio.enabled = false;


                    if (contadorPinchMindi == qtdPinchMindi)
                    {
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        info_exercicio.enabled = false;
                        nome_exercicio.text = "";
                        if (qtdPinchMindi > 0)
                        {
                            string[] auxiliarAngulos = inputCsv[indiceLinha[11]];
                            auxiliarAngulos[2] = infoAngulos;
                            inputCsv[indiceLinha[11]] = auxiliarAngulos;
                        }
                        infoAngulos = "";
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
