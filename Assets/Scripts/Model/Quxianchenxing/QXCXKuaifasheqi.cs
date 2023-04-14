using UnityEngine;

public class QXCXKuaifasheqi : MonoBehaviour
{
     bool isFashe;
    public float HZ;
    float time;
    float targetTime;
    private void Awake()
    {
        targetTime = 1 / HZ;
        time = 0;
    }
    private void Update()
    {
        if (isFashe)
        {
            time += Time.deltaTime;
            if (time >= targetTime)
            {
                Shengcheng();
                time = 0;
            }
        }
    }
    public void IsShengcheng(bool isShengcheng)
    {
        isFashe = isShengcheng;
    }
   public void Shengcheng()
    {
        GameObject go = Instantiate(transform.Find("TargetKuai").gameObject, transform);
        float excursion_z = Random.Range(-0.0424f, 0.02679f);
        go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, excursion_z);
        go.SetActive(true);
        go.AddComponent<Rigidbody>();
        StartCoroutine(UIManage.Instance.enumerator(3, () => {
            Rigidbody rigidbody = go.GetComponent<Rigidbody>();
            Destroy(rigidbody);
        }));
    }
}
