using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonenessTracker : MonoBehaviour
{
    RectTransform _tracker;
    Image _Arrow;
    Vector3 _arrowDesiredPosition;
    Vector3 _arrowStartPosition;
    Vector3 uiVelocity = Vector3.zero;
    float smoothTime = 0.5f;
    float _maxValue;

    private void Awake()
    {
        InitMeter(0);
    }

    // Update is called once per frame
    void Update()
    {
        _Arrow.rectTransform.localPosition = Vector3.SmoothDamp(_Arrow.rectTransform.localPosition, _arrowDesiredPosition, ref uiVelocity, smoothTime);
    }

    public void InitMeter(int maxValue)
    {
        _tracker = this.GetComponent<RectTransform>();
        _Arrow = this.GetComponentInChildren<Image>();
        _arrowStartPosition = _Arrow.rectTransform.localPosition;
        _arrowDesiredPosition = _arrowStartPosition;
        _maxValue = maxValue;
    }

    public void MoveArrowOnTracker(float doneness)
    {
        _arrowDesiredPosition = new Vector3(_Arrow.transform.localPosition.x, _Arrow.transform.localPosition.y + (_tracker.sizeDelta.y * (doneness / _maxValue)), -0.0f); 
    }

    public void ResetArrowToStartOfTracker()
    {
        _arrowDesiredPosition = _arrowStartPosition;
    }
}
