/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2017.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;


namespace Leap.Unity
{
    /** A physics model for our rigid hand made out of various Unity Collider. */
    public class RigidHand : SkeletalHand
    {
        public override ModelType HandModelType
        {
            get
            {
                return ModelType.Physics;
            }
        }
        public RawImage info_exercicio;
        public float filtering = 0.5f;
        public Text exercicioConcluido;
        public Text proximoExercicio;
        public Text conta_text_Abducao;
        public Text booleano_botao;
        public Text nome_exercicio;
        public Text conta_text_Pinch;
        public GameObject cuboProximoExercicio;
        public static int contadorAbducao = 0;
        public static int qtdAbducao = int.Parse(comecarExercicio.passAbdAduInd);// vai tirar depois
        public static int qtdAbdInd = int.Parse(comecarExercicio.passAbdAduInd);
        public static int qtdAbdMed = int.Parse(comecarExercicio.passAbdAduMed);
        public static int qtdAbdAnl = int.Parse(comecarExercicio.passAbdAduAnl);
        public static int qtdAbdMindi = int.Parse(comecarExercicio.passAbdAduMindi);
        public static int contadorLevant = 0;//esse � pra levantar dedo
        public static int qtdLevantamento = int.Parse(comecarExercicio.passarLevantamento);
        public static int contadorNumeroDeExercicios = 0;
        public static int contadorPinch;
        public static int qtdPinch = int.Parse(comecarExercicio.passPinchInd);// vai tirar depois
        public static int qtdPinchInd = int.Parse(comecarExercicio.passPinchInd);
        public static int qtdPinchMed = int.Parse(comecarExercicio.passPinchMed);
        public static int qtdPinchAnl = int.Parse(comecarExercicio.passPinchAnl);
        public static int qtdPinchMindi = int.Parse(comecarExercicio.passPinchMindi);
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
        public override bool SupportsEditorPersistence()
        {
            return true;
        }

        public override void InitHand()
        {
            base.InitHand();
        }
        public bool pinchouDedos(Vector3 b1, Vector3 b2)
        {
            bool retorno = false;
            Vector3 distancia = new Vector3(b1.x - b2.x, b1.y - b2.y, b1.z - b2.z);
            if (distancia.x < 0.01 || distancia.y < 0.01)
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

        public float angulo_dedos(Bone b1, Bone b2)
        {
            Vector3 direcao1 = new Vector3(b1.NextJoint.x - b1.PrevJoint.x, b1.NextJoint.y - b1.PrevJoint.y, b1.NextJoint.z - b1.PrevJoint.z);
            Vector3 direcao2 = new Vector3(b2.NextJoint.x - b2.PrevJoint.x, b2.NextJoint.y - b2.PrevJoint.y, b2.NextJoint.z - b2.PrevJoint.z);
            return Mathf.Acos((produto_escalar(direcao1, direcao2)) / (modulo_vetor(direcao1) * modulo_vetor(direcao2)));
        }

        public float modulo_vetor(Vector3 vet)
        {
            return Mathf.Sqrt(Mathf.Pow(vet.x, 2) + Mathf.Pow(vet.y, 2) + Mathf.Pow(vet.z, 2));
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Mathf.PI);
        }

        public override void UpdateHand()
        {
            //Finger f1, f2;
            FingerModel f1 = null, f2 = null, f3 = null, f4 = null, f5 = null;
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
                    //   Debug.Log("Angulo: " + produto_escalar();
                }
            }
            // if ((f1 != null) && (f2 != null))
            //{
            //  angulo = Mathf.Acos((produto_escalar(f1.GetBoneDirection(3), f2.GetBoneDirection(3))) / (modulo_vetor(f1.GetBoneDirection(3)) * modulo_vetor(f2.GetBoneDirection(3))));

            //}
            //Debug.Log("Angulo:"+RadianToDegree(angulo));

            if (Input.GetKeyDown("m")) {
                comecarExercicio.passarMenu();
            }

            if (concluido)
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
                    conta_text_Abducao.enabled = false;
                    exercicioConcluido.enabled = true;
                    exercicioConcluido.text = "TODOS OS EXERCICIOS CONCLUIDOS! Aperte M para ir ao menu!";
                    nome_exercicio.text = "";
                    Debug.Log("Parabens");
                }

            }
            if (f2 != null)//se um dedo � vis�vel, os outros provavelmente est�o vis�veis - nosso setup far� com que todos estejam vis�veis
            { //contar abdu��o dos dedos m�dio e indicador at� 10
                //texto est� dando null pointer exception!!!
                if (exerciciosBolean[0]) //AbdInd
                {
                        conta_text_Abducao.text = contadorAbducao.ToString();
                        info_exercicio.enabled = true;
                        if (Mathf.Abs(f2.GetLeapFinger().TipPosition.x - f3.GetLeapFinger().TipPosition.x) > 0.02 && contadorAbducao < qtdAbdInd &&
                            aux_texto_abd)
                        {
                            contadorAbducao++;
                            exercicioConcluido.enabled = false;
                            cuboProximoExercicio.SetActive(false);
                            proximoExercicio.enabled = false;
                            aux_texto_abd = false;
                            Debug.Log(RadianToDegree(angulo_dedos(f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL), f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL))));

                            //Debug.Log("Contador de abrir-fechar: " + contadorAbducao);
                        }
                        if (Mathf.Abs(f2.GetLeapFinger().TipPosition.x - f3.GetLeapFinger().TipPosition.x) < 0.02)
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
                        }
                  
                    

                }
                else if (exerciciosBolean[1]) //AbdMedio
                {
                        // proximoExercicio.enabled = true;
                        nome_exercicio.text = "Contador de aducao/abducao medio:";
                        conta_text_Abducao.text = contadorAbducao.ToString();
                        info_exercicio.enabled = true;

                        if (Mathf.Abs(f3.GetLeapFinger().TipPosition.x - f2.GetLeapFinger().TipPosition.x) > 0.02 && contadorAbducao < qtdAbdMed &&
                            aux_texto_abd)
                        {
                            contadorAbducao++;
                            exercicioConcluido.enabled = false;
                            cuboProximoExercicio.SetActive(false);
                            proximoExercicio.enabled = false;
                            aux_texto_abd = false;
                            Debug.Log(RadianToDegree(angulo_dedos(f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL), f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL))));

                        }
                        if (Mathf.Abs(f3.GetLeapFinger().TipPosition.x - f2.GetLeapFinger().TipPosition.x) < 0.02)
                        {
                            aux_texto_abd = true;
                        }
                        if (contadorAbducao == qtdAbdMed)
                        {
                            concluido = true;
                            exercicioConcluido.enabled = true;
                            info_exercicio.enabled = false;
                        }
                    
                }
                else if (exerciciosBolean[2]) //abdAnl 
                {
                  
                        nome_exercicio.text = "Contador de aducao/abducao anelar:";
                        conta_text_Abducao.text = contadorAbducao.ToString();
                        info_exercicio.enabled = true;
                        if (Mathf.Abs(f4.GetLeapFinger().TipPosition.x - f3.GetLeapFinger().TipPosition.x) > 0.02 && contadorAbducao < qtdAbdAnl &&
                            aux_texto_abd)
                        {
                            contadorAbducao++;
                            exercicioConcluido.enabled = false;
                            cuboProximoExercicio.SetActive(false);
                            proximoExercicio.enabled = false;
                            aux_texto_abd = false;
                            Debug.Log(RadianToDegree(angulo_dedos(f4.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL), f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL))));
                        }
                        if (Mathf.Abs(f4.GetLeapFinger().TipPosition.x - f3.GetLeapFinger().TipPosition.x) < 0.02)
                        {
                            aux_texto_abd = true;
                        }
                        if (contadorAbducao == qtdAbdAnl)
                        {
                            concluido = true;
                            exercicioConcluido.enabled = true;
                            info_exercicio.enabled = false;
                        }

                    
                }
                else if (exerciciosBolean[3]) // abdMindi
                {
                        nome_exercicio.text = "Contador de aducao/abducao mindinho:";
                        conta_text_Abducao.text = contadorAbducao.ToString();
                        info_exercicio.enabled = true;
                        if (Mathf.Abs(f5.GetLeapFinger().TipPosition.x - f4.GetLeapFinger().TipPosition.x) > 0.02 && contadorAbducao < qtdAbdMindi &&
                            aux_texto_abd)
                        {
                            contadorAbducao++;
                            exercicioConcluido.enabled = false;
                            cuboProximoExercicio.SetActive(false);
                            proximoExercicio.enabled = false;
                            aux_texto_abd = false;
                            Debug.Log(RadianToDegree(angulo_dedos(f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL), f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL))));
                        }
                        if (Mathf.Abs(f5.GetLeapFinger().TipPosition.x - f4.GetLeapFinger().TipPosition.x) < 0.02)
                        {
                            aux_texto_abd = true;
                        }
                        if (contadorAbducao == qtdAbdMindi)
                        {
                            concluido = true;
                            exercicioConcluido.enabled = true;
                            info_exercicio.enabled = false;
                        }
                    
                }
                else if (exerciciosBolean[4])//Levantamento
                {
                   
                        //exerc�cio de levantar o dedo indicador
                        nome_exercicio.text = "Contador de levantamento: ";
                        conta_text_Abducao.text = contadorLevant.ToString();
                        info_exercicio.enabled = true;
                        info_exercicio.texture = (Texture)Resources.Load("rv_instru_levant");

                        if (f2.GetBoneDirection((int)Bone.BoneType.TYPE_DISTAL).y > 0.04 && aux_texto_levant && contadorLevant < qtdLevantamento)
                        {

                            aux_texto_levant = false;
                            contadorLevant++;
                            exercicioConcluido.enabled = false;
                            cuboProximoExercicio.SetActive(false);
                            proximoExercicio.enabled = false;

                        }
                        if (f2.GetBoneDirection((int)Bone.BoneType.TYPE_DISTAL).y < 0.02)
                        {
                            aux_texto_levant = true;
                        }
                        if (contadorLevant == qtdLevantamento)
                        {
                            concluido = true;
                            exercicioConcluido.enabled = true;
                            info_exercicio.enabled = false;
                        }
                    
                }
                else if (exerciciosBolean[5]) //pinchInd
                {
                    
                        nome_exercicio.text = "Contador de Pinch Indicador: ";
                        info_exercicio.enabled = true;
                        info_exercicio.texture = (Texture)Resources.Load("rv_instru_pinca");
                        conta_text_Abducao.text = contadorPinchInd.ToString();
                        exercicioConcluido.enabled = false;
                        if ((contadorPinchInd < qtdPinchInd))//indicador - usa o PinchDetector do LeapMotion, que j� � calibrado para ele 
                        {
                            contadorPinch = int.Parse(conta_text_Pinch.text);
                            Debug.Log("conta_text_Pinch" + conta_text_Pinch.text);
                            contadorPinchInd = int.Parse(conta_text_Pinch.text);
                            Debug.Log("contadorPinchInd" + contadorPinchInd.ToString());
                            auxiliarPinch = false;
                            conta_text_Abducao.text = conta_text_Pinch.text;
                        }

                        if ((contadorPinchInd == qtdPinchInd))
                        {
                            concluido = true;
                            exercicioConcluido.enabled = true;
                            info_exercicio.enabled = false;
                        }
                    
                }
                else if (exerciciosBolean[6])//pinchMed
                {
                   
                        nome_exercicio.text = "Contador de Pinch Medio: ";
                        exercicioConcluido.enabled = false;
                        //dedo m�dio
                        if (contadorPinchMed < qtdPinchMed && pinchouDedos(f3.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL)) && auxiliarPinch)
                        {
                            if ((qtdPinchMed - contadorPinchMed) == 1) { PinchBolean[1] = false; PinchBolean[2] = true; }

                            contadorPinchMed++;
                            Debug.Log("Pinchou m�dio: " + contadorPinchMed.ToString());
                            Debug.Log("conta_text_Pinch" + conta_text_Pinch.text);
                            auxiliarPinch = false;
                            conta_text_Abducao.text = contadorPinchMed.ToString();

                        }
                        else if (!pinchouDedos(f3.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL))) auxiliarPinch = true;

                        if (contadorPinchMed == qtdPinchMed)
                        {
                            concluido = true;
                            exercicioConcluido.enabled = true;
                            info_exercicio.enabled = false;
                        }
                    
                }
                else if (exerciciosBolean[7])//pinchAnl
                {
                   
                        nome_exercicio.text = "Contador de Pinch Anelar: ";
                        exercicioConcluido.enabled = false;
                        if (contadorPinchAnl < qtdPinchAnl && pinchouDedos(f4.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL)) && auxiliarPinch)
                        {
                            contadorPinchAnl++;
                            Debug.Log("Pinchou anelar: " + contadorPinchAnl.ToString());
                            auxiliarPinch = false;
                            conta_text_Abducao.text = contadorPinchAnl.ToString();
                        }
                        else if (!pinchouDedos(f3.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL))) auxiliarPinch = true;

                        if (contadorPinchAnl == qtdPinchAnl)
                        {
                            concluido = true;
                            exercicioConcluido.enabled = true;
                            info_exercicio.enabled = false;
                        }
                    
                }
                else if (exerciciosBolean[8])//pinchMindi
                {
                   
                        nome_exercicio.text = "Contador de Pinch Mindinho: ";
                        //mindinho
                        if (contadorPinchMindi < qtdPinchMindi && pinchouDedos(f5.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL)) && auxiliarPinch)
                        {
                            contadorPinchMindi++;
                            Debug.Log("Pinchou mindinho: " + contadorPinchMindi.ToString());
                            auxiliarPinch = false;
                            conta_text_Abducao.text = contadorPinchMindi.ToString();
                            Debug.Log("Ind " + contadorPinchInd + " Med " + contadorPinchMed + " Anl " + contadorPinchAnl + " Mindi " + contadorPinchMindi);
                        }
                        else if (!pinchouDedos(f3.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL))) auxiliarPinch = true;



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

            if (palm != null)
            {
                Rigidbody palmBody = palm.GetComponent<Rigidbody>();
                if (palmBody)
                {
                    palmBody.MovePosition(GetPalmCenter());
                    palmBody.MoveRotation(GetPalmRotation());
                }
                else
                {
                    palm.position = GetPalmCenter();
                    palm.rotation = GetPalmRotation();
                }
            }

            if (forearm != null)
            {
                // Set arm dimensions.
                CapsuleCollider capsule = forearm.GetComponent<CapsuleCollider>();
                if (capsule != null)
                {
                    // Initialization
                    capsule.direction = 2;
                    forearm.localScale = new Vector3(1f / transform.lossyScale.x, 1f / transform.lossyScale.y, 1f / transform.lossyScale.z);

                    // Update
                    capsule.radius = GetArmWidth() / 2f;
                    capsule.height = GetArmLength() + GetArmWidth();
                }

                Rigidbody forearmBody = forearm.GetComponent<Rigidbody>();
                if (forearmBody)
                {
                    forearmBody.MovePosition(GetArmCenter());
                    forearmBody.MoveRotation(GetArmRotation());
                }
                else
                {
                    forearm.position = GetArmCenter();
                    forearm.rotation = GetArmRotation();
                }
            }
        }
    }
}