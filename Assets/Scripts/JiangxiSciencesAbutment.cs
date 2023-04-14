using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class JiangxiSciencesAbutment : MonoBehaviour
{
    [Tooltip("Appid")]
    [SerializeField]private int _appid;
    /// <summary>
    /// 测试IP
    /// </summary>
    //private const string _IP= "http://192.168.101.31:8200/";
    /// <summary>
    /// 正式IP
    /// </summary>
    private const string _IP= "https://ilab-x.jxust.edu.cn/jxustapi";
    //private const string _IP= "https://ilab-x.jxust.edu.cn/jxust/#/details/";

    #region 编辑器测试
    //public string OptionJson;
    //public HTTP.QuestionBank questionBank;
    //public void 解析题目()
    //{
    //    questionBank= JsonUtility.FromJson<HTTP.QuestionBank>(OptionJson);
    //}
    //[ContextMenu("测试回传")]
    //public void TestSubmitData()
    //{
    //    SubmitData(Josn, Token, null, null);
    //}
    //public string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1c2VyX2lkIjoibHVvd2VucnVpIiwibmFtZSI6Imx1b3dlbnJ1aSIsImlkIjoibHVvd2VucnVpIiwiZXhwIjoxNjYyMDAyMTE1fQ.lTOBTaWn3YroaonY_ED3JO1DWH8DDXxxGd85CYlOycM";
    //[ContextMenu("测试提交实验数据")]
    //public void TestSubmitData()
    //{
    //    SubmitData(JsonUtility.ToJson(ScoreManager.Instance.IlabData), token, null, null);
    //}

    //[ContextMenu("转json")]
    //public void Tojson()
    //{
    //    Josn = JsonUtility.ToJson(IlbData);
    //}
    #endregion

    /// <summary>
    /// 获取题库
    /// </summary>
    /// <returns></returns>
    [ContextMenu("获取题库")]
    public void GetQuestionBank()
    {
        StartCoroutine(GetQuestion());
    }
    IEnumerator GetQuestion()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", _appid);
        form.AddField("des", CreateDes());
        using (UnityWebRequest webRequest = UnityWebRequest.Post(_IP + "/api/currency/record/topic", form))
        {
            yield return webRequest.SendWebRequest();
            string response = null;
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                response = webRequest.error;
                Debug.LogError("获取题库请求失败 : " + response);
            }
            else
            {
                response = webRequest.downloadHandler.text;
                Debug.Log("获取题库回传请求成功收到回包 : " + response);
                //解析题库
                if (OptionManager.Instance.SetQuestionBank(response))
                {
                    Debug.Log("获取题库解析成功");
                }
                else
                {
                    Debug.Log("获取题库解析失败");
                }
            }
            webRequest.Dispose();
        }
    }

    /// <summary>
    /// 提交答题数据
    /// </summary>
    /// <param name="report">答题数据和之前的答题数据一样</param>OptionListClass类转json
    /// <param name="succeed">成功回调</param>
    /// <param name="fail">失败回调</param>
    public void SubmitProblem(string report, Action succeed, Action fail)
    {
        StartCoroutine(UnityWebRequestPostProblem(report, succeed, fail));
    }

    IEnumerator UnityWebRequestPostProblem(string report, Action succeed, Action fail)
    {
        WWWForm empiricalDataForm = new WWWForm();
        empiricalDataForm.AddField("id", _appid);
        empiricalDataForm.AddField("report", report);
        empiricalDataForm.AddField("des", CreateDes());
        Debug.Log("----提交答题数据数据----");
        Debug.Log("Json:" + report);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(_IP + "/api/currency/record/recTopic", empiricalDataForm))
        {
            webRequest.timeout = 10;
            yield return webRequest.SendWebRequest();
            string response = null;
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                response = webRequest.error;
                Debug.LogError("答题数据回传请求失败 : " + response);
                fail?.Invoke();
            }
            else
            {
                response = webRequest.downloadHandler.text;
                Debug.Log("答题数据回传请求成功收到回包 : " + response);
                succeed?.Invoke();
            }
            webRequest.Dispose();
        }
    }
    /// <summary>
    /// 提交实验数据
    /// </summary>
    /// <param name="report">和Ilab网站的数据一样的</param>IlabData类转json
    /// <param name="token">Acctoken</param>
    /// <param name="succeed">成功回调</param>
    /// <param name="fail">失败回调</param>
    public void SubmitData(string report,string accessToken, Action succeed, Action fail)
    {
        StartCoroutine(UnityWebRequestPostExperiment( report, accessToken, succeed, fail));
    }
    IEnumerator UnityWebRequestPostExperiment(string report,string accessToken, Action succeed, Action fail)
    {
        WWWForm empiricalDataForm = new WWWForm();
        empiricalDataForm.AddField("report", report);
        empiricalDataForm.AddField("des", CreateDes());
        empiricalDataForm.AddField("access_token", accessToken);
        Debug.Log("----提交后台实验数据----");
        Debug.Log("Json:" + report);
        Debug.Log("access_token:" + accessToken);
        using (UnityWebRequest webRequest = UnityWebRequest.Post(_IP+ "/api/currency/record/receive", empiricalDataForm))
        {
            yield return webRequest.SendWebRequest();
            string response = null;
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                response = webRequest.error;
                Debug.LogError("实验数据回传请求失败 : " + response);
                fail?.Invoke();
            }
            else
            {
                response = webRequest.downloadHandler.text;
                Debug.Log("实验数据回传请求成功收到回包 : " + response);
                succeed?.Invoke();
            }
            webRequest.Dispose();
        }
    }


    [ContextMenu("DES加密")]
    public  string CreateDes()
    {
        string key;
        var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        var timestamp = ((long)(DateTime.Now - startTime).TotalMilliseconds);
        key = "华畅股份" + timestamp;
        string des = toHex(Encrypt(key, "huaChang"));
        return des;
    }
    private static char[] DIGITS = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
    /// <summary>
    /// Base64编码十六进制
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string toHex(byte[] data)
    {
        StringBuilder sb = new StringBuilder(data.Length * 2);
        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(DIGITS[(data[i] >> 4) & 0x0F]);
            sb.Append(DIGITS[data[i] & 0x0F]);
        }
        return sb.ToString();
    }
    
    [ContextMenu("DES解密")]
    public string Decrypt()
    {
        return Decrypt("73cb3abea588be43e87fe877901bdb52f72bc34fb3550f444c752805d61ee706", "huaChang");
    }


    
    public static byte[]  Encrypt(string pToEncrypt, string sKey)
    {
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {

            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                cs.Close();
            }
            return ms.ToArray();
        }
    }
    public static string Decrypt(string pToDecrypt, string sKey)
    {
        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            byte[] inputByteArray = Convert.FromBase64String(pToDecrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                cs.Close();
            }
            string str = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            Debug.Log(pToDecrypt + "--通过--" + sKey + "--解密为--" + str);
            return str;
        }
    }
}
