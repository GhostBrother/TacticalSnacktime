using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonenessTracker : MonoBehaviour
{
    RectTransform _tracker;
    SpriteRenderer _Arrow;
    Vector3 _arrowDesiredPosition;
    Vector3 _arrowStartPosition;
    float _maxValue;

    // Update is called once per frame
    void Update()
    {
        _Arrow.transform.localPosition = Vector3.Lerp(_Arrow.transform.localPosition, _arrowDesiredPosition,Time.deltaTime);
    }

    public void InitMeter(int maxValue)
    {
        _tracker = this.GetComponent<RectTransform>();
        _Arrow = this.GetComponentInChildren<SpriteRenderer>();
        _arrowStartPosition = this.transform.position;
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
