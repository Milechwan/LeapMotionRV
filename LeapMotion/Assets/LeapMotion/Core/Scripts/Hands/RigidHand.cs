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
    public Text feedback;
    public static int contadorAbducao = 0;
    public static int contadorLevant = 0;//esse é rpa levantar dedo
    public static bool aux_texto_abd = true;
    public static bool aux_texto_levant = true;//esse é pra levantar dedo
    public override bool SupportsEditorPersistence() {
      return true;
    }

    public override void InitHand() {
      base.InitHand();
    }

        public float produto_escalar(Vector3 vetor1, Vector3 vetor2)
        {
            float resultado = 0.0f;
            resultado = (vetor1.x * vetor2.x) + (vetor1.y * vetor2.y) + (vetor1.z * vetor2.z);
            return resultado;
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
            FingerModel f1 = null, f2= null;
      //float angulo= 0.0f;
      for (int f = 0; f < fingers.Length; ++f) {
        if (fingers[f] != null) {
          fingers[f].UpdateFinger();
          //Debug.Log("Dedo "+fingers[f].fingerType+ "Direcao"+ fingers[f].GetBoneDirection(3).ToString()+ "basis osso"+ fingers[f].GetBoneCenter(3));

                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_MIDDLE)
                        f1 = fingers[f];
                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_INDEX)
                        f2 = fingers[f];
                     //   Debug.Log("Angulo: " + produto_escalar();
        }
      }
            // if ((f1 != null) && (f2 != null))
            //{
            //  angulo = Mathf.Acos((produto_escalar(f1.GetBoneDirection(3), f2.GetBoneDirection(3))) / (modulo_vetor(f1.GetBoneDirection(3)) * modulo_vetor(f2.GetBoneDirection(3))));

            //}
            //Debug.Log("Angulo:"+RadianToDegree(angulo));
            if (f2 != null &&  f1 !=null)
            { //contar abdução dos dedos médio e indicador até 10
                //texto está dando null pointer exception!!!
                if (Mathf.Abs(f2.GetLeapFinger().TipPosition.x - f1.GetLeapFinger().TipPosition.x)>0.02 && contadorAbducao<10 && aux_texto_abd)
                {
                    contadorAbducao++;
                    feedback.enabled=false;
                    aux_texto_abd = false;
                    Debug.Log("Contador de movimentos: "+contadorAbducao);
                }//else if(Mathf.Abs(f2.GetLeapFinger().TipPosition.y - f2.GetLeapFinger().Bone(Bone.BoneType.TYPE_PROXIMAL).Basis.yBasis.y))
                //{

                //}
                if(Mathf.Abs(f2.GetLeapFinger().TipPosition.x - f1.GetLeapFinger().TipPosition.x)< 0.02)
                {
                    aux_texto_abd = true;
                }
                if (contadorAbducao==10)
                {
                    feedback.enabled=true;
                    contadorAbducao = 0;

                }
                /*if (contadorAbducao>10)
                {
                    Debug.Log("Posicao ponta do indicador: " + f2.GetLeapFinger().TipPosition);
                   
                }*/
                
                
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
