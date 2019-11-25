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

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitMeter(int maxValue)
    {
        _tracker = this.GetComponent<RectTransform>();
       // _Arrow = this.GetComponentInChildren<Image>();
        _arrowStartPosition = _Arrow.rectTransform.localPosition;
        _arrowDesiredPosition = _arrowStartPosition;
        _maxValue = maxValue;
    }

    public void MoveArrowAlongTrack(float doneness)
    {
        _doneness = doneness;
        StartCoroutine("MoveArrowOnTracker");
    }

     IEnumerator MoveArrowOnTracker()
    {

        _arrowDesiredPosition = new Vector3(_Arrow.transform.localPosition.x, _Arrow.transform.localPosition.y + (_tracker.rect.height * (_doneness / _maxValue)), -0.0f);
        while (true)
        {
          
            if (_Arrow.transform.localPosition.y == _arrowDesiredPosition.y)
            {
                yield break;
            }
            _Arrow.transform.localPosition = Vector3.MoveTowards(_Arrow.transform.localPosition, _arrowDesiredPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void ResetArrowToStartOfTracker()
    {
        _arrowDesiredPosition = _arrowStartPosition;
    }
}
