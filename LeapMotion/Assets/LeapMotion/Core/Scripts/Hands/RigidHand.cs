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


namespace Leap.Unity {
  /** A physics model for our rigid hand made out of various Unity Collider. */
  public class RigidHand : SkeletalHand {
    public override ModelType HandModelType {
      get {
        return ModelType.Physics;
      }
    }
    public float filtering = 0.5f;
    public Text exercicioConcluido;
    public Text proximoExercicio;
    public Text conta_text_Abducao;
    public Text booleano_botao;
    public Text nome_exercicio;
    public Text conta_text_Pinch;
    public GameObject cuboProximoExercicio;
    public static int contadorAbducao = 0;
    public static int qtdAbducao = int.Parse(comecarExercicio.passarAbdAdu);
    public static int contadorLevant = 0;//esse é pra levantar dedo
    public static int qtdLevantamento = int.Parse(comecarExercicio.passarLevantamento);
    public static int contadorNumeroDeExercicios = 0;
    public static int contadorPinch;
    public static int qtdPinch = int.Parse(comecarExercicio.passarPinch);
  //public static bool bol = false;
    public static bool[] exerciciosBolean = { true, false , false, false };
    public static bool[] PinchBolean = { true, false, false, false };
    public static bool concluido = false;
    public static bool aux_texto_abd = true;
    public static bool aux_texto_levant = true;//esse é pra levantar dedo
    int contadorPinchInd = 0;
    int contadorPinchMed = 0;
    int contadorPinchAnl = 0;
    int contadorPinchMindi = 0;
    public override bool SupportsEditorPersistence() {
      return true;
    }

    public override void InitHand() {
      base.InitHand();
    }
        public bool pinchouDedos(Vector3 b1, Vector3 b2)
        {
            bool retorno = false;
            Vector3 distancia = new Vector3(b1.x - b2.x, b1.y - b2.y, b1.z - b2.z);
            if (distancia.x<0.01||distancia.y<0.01)
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

        public override void UpdateHand() {
            //Finger f1, f2;
            FingerModel f1 = null, f2= null, f3=null, f4=null, f5=null;
      //float angulo= 0.0f;
            for (int f = 0; f < fingers.Length; ++f) {
                if (fingers[f] != null) {
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

            if (booleano_botao.text.Equals("sim") && concluido)
            {
                //bol = !bol;
                //Debug.Log("bol:" + bol);
                Debug.Log("Proximo exercicio");
                booleano_botao.text = "";
                exerciciosBolean[contadorNumeroDeExercicios] = false;
                contadorNumeroDeExercicios++;
                contadorAbducao = 0;
                contadorLevant = 0;
                conta_text_Pinch.text = "0";
                if (nome_exercicio.text.Contains("levantamento"))
                {
                    nome_exercicio.text = "Contador de Pinch: ";
                }

                if (nome_exercicio.text.Contains("aducao"))
                {
                    nome_exercicio.text = "Contador de levantamento: ";
                }
           
                //System.Threading.Thread.Sleep(1000);
                if (contadorNumeroDeExercicios < exerciciosBolean.Length)
                {
                    exerciciosBolean[contadorNumeroDeExercicios] = true;
                    concluido = false;
                }
                else {
                    nome_exercicio.text = "GG";
                    Debug.Log("Parabens");
                }

            }
            if (f2 != null)//se um dedo é visível, os outros provavelmente estão visíveis - nosso setup fará com que todos estejam visíveis
            { //contar abdução dos dedos médio e indicador até 10
                //texto está dando null pointer exception!!!
                
                if (exerciciosBolean[0])
                {
                    conta_text_Abducao.text = contadorAbducao.ToString();

                    if (Mathf.Abs(f2.GetLeapFinger().TipPosition.x - f3.GetLeapFinger().TipPosition.x) > 0.02 && contadorAbducao < qtdAbducao &&
                        Mathf.Abs(f3.GetLeapFinger().TipPosition.x - f4.GetLeapFinger().TipPosition.x) > 0.02 &&
                        Mathf.Abs(f4.GetLeapFinger().TipPosition.x - f5.GetLeapFinger().TipPosition.x) > 0.02 &&
                        aux_texto_abd)
                    {
                        contadorAbducao++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                        aux_texto_abd = false;
                        Debug.Log(RadianToDegree(angulo_dedos(f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL), f3.GetLeapFinger().Bone(Bone.BoneType.TYPE_DISTAL))));

                        //Debug.Log("Contador de abrir-fechar: " + contadorAbducao);
                    }//else if(Mathf.Abs(f2.GetLeapFinger().TipPosition.y - f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).Basis.yBasis.y))
                     //{

                    //}
                    if (Mathf.Abs(f2.GetLeapFinger().TipPosition.x - f3.GetLeapFinger().TipPosition.x) < 0.02 &&
                        Mathf.Abs(f3.GetLeapFinger().TipPosition.x - f4.GetLeapFinger().TipPosition.x) < 0.02 &&
                        Mathf.Abs(f4.GetLeapFinger().TipPosition.x - f5.GetLeapFinger().TipPosition.x) < 0.02)
                    {
                        aux_texto_abd = true;
                    }
                    if (contadorAbducao == qtdAbducao)
                    {
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        cuboProximoExercicio.SetActive(true);
                        proximoExercicio.enabled = true;
                       
                       // cuboProximoExercicio.SetActive(true); 
                       // proximoExercicio.enabled = true;
                        
                    }
                    /*if (contadorAbducao>10)
                    {
                        Debug.Log("Posicao ponta do indicador: " + f2.GetLeapFinger().TipPosition);

                    }*/

                } else if (exerciciosBolean[1]) {
                    //exercício de levantar o dedo indicador
                    conta_text_Abducao.text = contadorLevant.ToString();

                    if (f2.GetBoneDirection((int)Bone.BoneType.TYPE_DISTAL).y > 0.04 && aux_texto_levant && contadorLevant< qtdLevantamento)
                    {
                        
                        aux_texto_levant = false;
                        contadorLevant++;
                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                      //  Debug.Log("Contador de levantamentos: " + contadorLevant);

                    }
                    if (f2.GetBoneDirection((int)Bone.BoneType.TYPE_DISTAL).y < 0.02)
                    {
                        aux_texto_levant = true;
                    }
                    if (contadorLevant == qtdLevantamento)
                    {
                        concluido = true;  
                        exercicioConcluido.enabled = true;
                        cuboProximoExercicio.SetActive(true);
                        proximoExercicio.enabled = true;
                    }
                } else if (exerciciosBolean[2]) {
                    //contadores auxiliares para pinça em cada dedo

                    if (contadorPinch < 4*qtdPinch) {//multiplicado por 4 pois será dito como concluído quando terminar com todos os dedos
                        if (contadorPinchInd<qtdPinch && PinchBolean[0])//indicador - usa o PinchDetector do LeapMotion, que já é calibrado para ele 
                        {
                            if ((qtdPinch - contadorPinchInd) == 1) { PinchBolean[0] = false; PinchBolean[1] = true; }
                            conta_text_Abducao.text = conta_text_Pinch.text;
                            contadorPinch = int.Parse(conta_text_Pinch.text);
                            contadorPinchInd = int.Parse(conta_text_Pinch.text);
                            
                        }
                        //dedo médio
                        if (contadorPinchMed<qtdPinch && pinchouDedos(f3.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL)) && PinchBolean[1])
                        {
                            if ((qtdPinch - contadorPinchMed) == 1) { PinchBolean[1] = false; PinchBolean[2] = true; }
                            contadorPinch++;
                            contadorPinchMed++;
                            Debug.Log("Pinchou médio: " + contadorPinchMed.ToString());
                        }
                        //anelar
                        if (contadorPinchAnl < qtdPinch && pinchouDedos(f4.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL)) && PinchBolean[2])
                        {
                            if ((qtdPinch - contadorPinchAnl) == 1) { PinchBolean[2] = false; PinchBolean[3] = true; }
                            contadorPinch++;
                            contadorPinchAnl++;
                            Debug.Log("Pinchou anelar: " + contadorPinchAnl.ToString());
                        }
                        //mindinho
                        if (contadorPinchMindi < qtdPinch && pinchouDedos(f5.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL), f1.GetBoneCenter((int)Bone.BoneType.TYPE_DISTAL)) && PinchBolean[3])
                        {
                            if ((qtdPinch - contadorPinchMindi) == 1) { PinchBolean[3] = false; PinchBolean[0] = true; }
                            contadorPinch++;
                            contadorPinchMindi++;
                            Debug.Log("Pinchou mindinho: "+contadorPinchMindi.ToString());
                            Debug.Log("Contador global: "+contadorPinch);
                        }

                        exercicioConcluido.enabled = false;
                        cuboProximoExercicio.SetActive(false);
                        proximoExercicio.enabled = false;
                    }
                    
                    if (contadorPinch == 4*qtdPinch)
                    {
                        Debug.Log("ACABOU");
                        concluido = true;
                        exercicioConcluido.enabled = true;
                        cuboProximoExercicio.SetActive(true);
                        proximoExercicio.enabled = true;
                    }
                } else if (exerciciosBolean[3]) {

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
