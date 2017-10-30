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

namespace Leap.Unity {
  /** A physics model for our rigid hand made out of various Unity Collider. */
  public class RigidHand : SkeletalHand {
    public override ModelType HandModelType {
      get {
        return ModelType.Physics;
      }
    }
    public float filtering = 0.5f;

    public override bool SupportsEditorPersistence() {
      return true;
    }

    public override void InitHand() {
      base.InitHand();
    }

        public float produto_escalar(FingerModel vetor1, FingerModel vetor2)
        {
            float resultado = 0.0f;
            resultado = (vetor1.GetBoneDirection(3).x * vetor2.GetBoneDirection(3).x) + (vetor1.GetBoneDirection(3).y * vetor2.GetBoneDirection(3).y) + (vetor1.GetBoneDirection(3).z * vetor2.GetBoneDirection(3).z);
            return resultado;
        }

        public float modulo_vetor(FingerModel vet)
        {
            return Mathf.Sqrt(Mathf.Pow(vet.GetBoneDirection(3).x, 2) + Mathf.Pow(vet.GetBoneDirection(3).y, 2) + Mathf.Pow(vet.GetBoneDirection(3).z, 2));
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Mathf.PI);
        }

        public override void UpdateHand() {
            //Finger f1, f2;
            FingerModel f1 = null, f2= null;
      float angulo= 0.0f;
      for (int f = 0; f < fingers.Length; ++f) {
        if (fingers[f] != null) {
          fingers[f].UpdateFinger();
          Debug.Log("Dedo "+fingers[f].fingerType+ "Direcao"+ fingers[f].GetBoneDirection(3).ToString()+ "basis osso"+ fingers[f].GetBoneCenter(3));

                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_MIDDLE)
                        f1 = fingers[f];
                    if (fingers[f].GetLeapFinger().Type == Finger.FingerType.TYPE_THUMB)
                        f2 = fingers[f];
                     //   Debug.Log("Angulo: " + produto_escalar();
        }
      }
            if ((f1 != null) && (f2 != null))
            {
                angulo = Mathf.Acos((produto_escalar(f1, f2)) / (modulo_vetor(f1) * modulo_vetor(f2)));
                
            }
            Debug.Log("Angulo:"+RadianToDegree(angulo));

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
