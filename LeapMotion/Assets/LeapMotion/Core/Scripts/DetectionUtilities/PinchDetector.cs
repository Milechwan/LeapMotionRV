/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2018.                                 *
 * Leap Motion proprietary and confidential.                                  *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using UnityEngine;
using Leap.Unity.Attributes;
using UnityEngine.Serialization;
using UnityEngine.UI;
namespace Leap.Unity {

  /// <summary>
  /// A basic utility class to aid in creating pinch based actions.  Once linked with a HandModelBase, it can
  /// be used to detect pinch gestures that the hand makes.
  /// </summary>
  public class PinchDetector : AbstractHoldDetector {
    protected const float MM_TO_M = 0.001f;

    [Tooltip("The distance at which to enter the pinching state.")]
    [Header("Distance Settings")]
    [MinValue(0)]
    [Units("meters")]
    [FormerlySerializedAs("_activatePinchDist")]
    public float ActivateDistance = .03f; //meters
    [Tooltip("The distance at which to leave the pinching state.")]
    [MinValue(0)]
    [Units("meters")]
    [FormerlySerializedAs("_deactivatePinchDist")]
    public float DeactivateDistance = .04f; //meters

    public bool IsPinching { get { return this.IsHolding; } }
    public bool DidStartPinch { get { return this.DidStartHold; } }
    public bool DidEndPinch { get { return this.DidRelease; } }

    protected bool _isPinching = false;

    protected float _lastPinchTime = 0.0f;
    protected float _lastUnpinchTime = 0.0f;

    protected Vector3 _pinchPos;
    protected Quaternion _pinchRotation;

    //public Text texto;
    public Text contadorPinch;
    public Text mostraAngulo;
    public static string infoAngulos = "";
    public int temp = 0;
    protected virtual void OnValidate() {
      ActivateDistance = Mathf.Max(0, ActivateDistance);
      DeactivateDistance = Mathf.Max(0, DeactivateDistance);

      //Activate value cannot be less than deactivate value
      if (DeactivateDistance < ActivateDistance) {
        DeactivateDistance = ActivateDistance;
      }
    }

    private float GetPinchDistance(Hand hand) {
      var indexTipPosition = hand.GetIndex().TipPosition.ToVector3();
      var thumbTipPosition = hand.GetThumb().TipPosition.ToVector3();
      return Vector3.Distance(indexTipPosition, thumbTipPosition);
    }

    protected override void ensureUpToDate() {
      if (Time.frameCount == _lastUpdateFrame) {
        return;
      }
      _lastUpdateFrame = Time.frameCount;

      _didChange = false;

      Hand hand = _handModel.GetLeapHand();

      if (hand == null || !_handModel.IsTracked) {
       // texto.enabled = false;
        contadorPinch.enabled = false;
        changeState(false);
        return;
      }

      _distance = GetPinchDistance(hand);
      _rotation = hand.Basis.CalculateRotation();
      _position = ((hand.Fingers[0].TipPosition + hand.Fingers[1].TipPosition) * .5f).ToVector3();

      if (IsActive) {
        if (_distance > DeactivateDistance) {
                    // texto.enabled = false;
            contadorPinch.enabled = false;
            changeState(false);
            
          //return;
        }
      } else {
        if (_distance < ActivateDistance) {
            if (contadorPinch.text.Equals("0")) temp = 0;
            // texto.enabled = true;
            if (!pausarCena.scenePaused)
            {
                temp = temp + 1;
                Vector juntaProximal1 = hand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
                Vector juntaProximal2 = hand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).NextJoint;
                Vector juntaIntermedial = hand.Fingers[1].Bone(Bone.BoneType.TYPE_INTERMEDIATE).NextJoint;
                Vector v1 = new Vector(juntaProximal2.x - juntaProximal1.x, juntaProximal2.y - juntaProximal1.y, juntaProximal2.z - juntaProximal1.z);
                Vector v2 = new Vector(juntaIntermedial.x - juntaProximal2.x, juntaIntermedial.y - juntaProximal2.y, juntaIntermedial.z - juntaProximal2.z);
                // Debug.Log("contPinch " + temp);
                double ang = (double)(v2.Normalized.AngleTo(v1.Normalized) * 180.0 / Mathf.PI);
                infoAngulos += ang.ToString("n2") + ";";//somente por causa do pinch do dedo indicador
                                                        // Debug.Log(infoAngulos);
                mostraAngulo.text = "Ângulo obtido: " + ang.ToString("n2");
                contadorPinch.text = temp.ToString();
            }
            changeState(true);
        }
      }

      if (IsActive) {
        _lastPosition = _position;
        _lastRotation = _rotation;
        _lastDistance = _distance;
        _lastDirection = _direction;
        _lastNormal = _normal;
      }
      if (ControlsTransform) {
        transform.position = _position;
        transform.rotation = _rotation;
      }
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos () {
      if (ShowGizmos && _handModel != null && _handModel.IsTracked) {
        Color centerColor = Color.clear;
        Vector3 centerPosition = Vector3.zero;
        Quaternion circleRotation = Quaternion.identity;
        if (IsHolding) {
          centerColor = Color.green;
          centerPosition = Position;
          circleRotation = Rotation;
        } else {
          Hand hand = _handModel.GetLeapHand();
          if (hand != null) {
            Finger thumb = hand.Fingers[0];
            Finger index = hand.Fingers[1];
            centerColor = Color.red;
            centerPosition = ((thumb.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint + index.Bone(Bone.BoneType.TYPE_DISTAL).NextJoint) / 2).ToVector3();
            circleRotation = hand.Basis.CalculateRotation();
          }
        }
        Vector3 axis;
        float angle;
        circleRotation.ToAngleAxis(out angle, out axis);
        Utils.DrawCircle(centerPosition, axis, ActivateDistance / 2, centerColor);
        Utils.DrawCircle(centerPosition, axis, DeactivateDistance / 2, Color.blue);
      }
    }
    #endif
  }
}
