using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DonenessTracker : MonoBehaviour
{
    RectTransform _tracker;
    [SerializeField]
    Image _Arrow;
    Vector3 _arrowDesiredPosition;
    Vector3 _arrowStartPosition;
    Vector3 uiVelocity = Vector3.zero;
    float speed = 5f;
    float _maxValue;
    float _doneness;

    public delegate void OnArrowStopMoving();
    public OnArrowStopMoving onStopMoving;

    public void InitMeter()
    {
        _tracker = this.GetComponent<RectTransform>();
        _arrowStartPosition = _Arrow.rectTransform.localPosition;
    }

    public void SetMaxValue(int maxValue)
    {
        _maxValue = maxValue;
    }

    public void SetArrowOnDonessTracker(int doneness)
    {
        ResetArrowToStartOfTracker();
        _Arrow.rectTransform.localPosition = new Vector3(_Arrow.transform.localPosition.x, _arrowStartPosition.y + (_tracker.rect.height * (doneness / _maxValue)), -0.0f);
    }

    public void MoveArrowsAlongTrack(float doneness)
    {
        _doneness = doneness;
        StartCoroutine("MoveArrowOnTracker");
    }

     IEnumerator MoveArrowOnTracker()
    {

        _arrowDesiredPosition = new Vector3(_Arrow.transform.localPosition.x, _arrowStartPosition.y + (_tracker.rect.height * (_doneness / _maxValue)), -0.0f);
        while (true)
        {
          
            if (_Arrow.transform.localPosition.y == _arrowDesiredPosition.y)
            {
                onStopMoving.Invoke();
                yield break;
            }
            _Arrow.transform.localPosition = Vector3.MoveTowards(_Arrow.transform.localPosition, _arrowDesiredPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void ResetArrowToStartOfTracker()
    {
        _Arrow.rectTransform.localPosition = _arrowStartPosition; 
    }
}
