using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panzi : MonoBehaviour
{
   public int index = 0;

    public void SetClick()
    {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<HighlightPlus.HighlightEffect>().highlighted = true;
    }
    private void OnMouseDown()
    {
        if (index == 0)
        {
            MainSceneGuide.Instance.StopAutoMove();
            List<GameObject> biaoPianList = new List<GameObject>();
            foreach (Transform temp in this.transform)
            {

                if (temp.GetComponent<Rigidbody>() != null)
                {
                    temp.GetComponent<Rigidbody>().isKinematic = false;
                    temp.GetComponent<Rigidbody>().useGravity = false;
                    temp.GetComponent<BoxCollider>().enabled = false;
                }
            }
            foreach (GameObject go in biaoPianList)
            {
                go.transform.parent = this.transform;
            }
            this.GetComponent<Animator>().enabled = true;
            UIManage.Instance.SetHint("点击产品查看详细信息");
            AudioManage.Instance.PlayMusicSource("点击产品查看详细信息", 0.5f);
        }
        if (index == 1)
        {
            ShuningruTestManlag.Instance.ShowBaopianxingxi();
            GetComponent<HighlightPlus.HighlightEffect>().highlighted = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        index++;
    }
}
