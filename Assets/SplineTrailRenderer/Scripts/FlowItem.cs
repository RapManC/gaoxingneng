using UnityEngine;
public class FlowItem : MonoBehaviour
{
    [Header("延迟删除")]
    public float DelayDestroy = 0.5f;
    private bool _isFlowing = false;
    private PipeFlow _pipeFlow;
    private int _flowIndex = 0;
    private int _nextIndex = 1;
    private float _flowPosition = 0f;
    private Coroutine _stopCoroutine;
    private void Reset()
    {
        transform.position = _pipeFlow.FlowPath[0].position;
        TrailRenderer[] trails = GetComponentsInChildren<TrailRenderer>();
        for (int i = 0; i < trails.Length; i++)
        {
            trails[i].Clear();
        }
        ParticleSystem[] pss = GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < pss.Length; i++)
        {
            pss[i].SetParticles(null, 0);
        }
    }
    private void FixedUpdate()
    {
        if (_isFlowing)
        {
            _flowPosition += _pipeFlow.FlowSpeeds[_flowIndex];
            transform.position = Vector3.Lerp(_pipeFlow.FlowPath[_flowIndex].position, _pipeFlow.FlowPath[_nextIndex].position, _flowPosition);
            if (_flowPosition >= 1f)
            {
                if (_nextIndex >= _pipeFlow.FlowPath.Count - 1)
                {
                    _isFlowing = false;
                    _stopCoroutine = StartCoroutine(PipeFlow.DelayExecute(() =>
                    {
                        Stop();
                    }, DelayDestroy));
                }
                else
                {
                    _flowIndex += 1;
                    _nextIndex = _flowIndex + 1;
                    _flowPosition = 0f;
                }
            }
        }
    }
    public void Shoot(PipeFlow pipeFlow)
    {
        _pipeFlow = pipeFlow;
        Reset();
        gameObject.SetActive(true);
        _isFlowing = true;
        _flowIndex = 0;
        _nextIndex = 1;
        _flowPosition = 0f;
        if (_stopCoroutine != null)
        {
            StopCoroutine(_stopCoroutine);
            _stopCoroutine = null;
        }
    }
    public void Stop()
    {
        gameObject.SetActive(false);
        _isFlowing = false;
        if (!_pipeFlow.Items.Contains(this))
        {
            _pipeFlow.Items.Add(this);
        }
        if (_stopCoroutine != null)
        {
            StopCoroutine(_stopCoroutine);
            _stopCoroutine = null;
        }
    }
}