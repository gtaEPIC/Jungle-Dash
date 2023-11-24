using UnityEngine;

public class SignScript : MonoBehaviour
{
    private bool _triggered;
    private GameObject _bubble;
    private bool _stretched;

    private float _bubbleFullScale = 1.7206f;
    [SerializeField] private float _bubbleScaleSpeed = 10f;
    private float _bubbleScale = 0;

    public void Start()
    {
        _bubble = transform.GetChild(0).gameObject;
        _bubble.SetActive(false);
    }
    
    private void StretchIn()
    {
        _bubble.SetActive(true);
        _bubbleScale += _bubbleScaleSpeed * Time.deltaTime;
        _bubble.transform.localScale = new Vector3(_bubbleScale, _bubbleScale, 1);
        if (_bubbleScale >= _bubbleFullScale)
        {
            _bubbleScale = _bubbleFullScale;
            _bubble.transform.localScale = new Vector3(_bubbleScale, _bubbleScale, 1);
            _stretched = true;
        }
    }
    private void StretchOut()
    {
        _bubbleScale -= _bubbleScaleSpeed * Time.deltaTime;
        _bubble.transform.localScale = new Vector3(_bubbleScale, _bubbleScale, 1);
        if (_bubbleScale <= 0)
        {
            _bubbleScale = 0;
            _bubble.transform.localScale = new Vector3(_bubbleScale, _bubbleScale, 1);
            _bubble.SetActive(false);
            _stretched = false;
        }
    }
    
    private void Update()
    {
        if (_triggered && (!_stretched || !_bubble.transform.localScale.Equals(new Vector3(_bubbleFullScale, _bubbleFullScale, 1))))
        {
            StretchIn();
        }
        else if (!_triggered && (_stretched || _bubble.activeSelf))
        {
            StretchOut();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _triggered = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _triggered = false;
        }
    }
}
