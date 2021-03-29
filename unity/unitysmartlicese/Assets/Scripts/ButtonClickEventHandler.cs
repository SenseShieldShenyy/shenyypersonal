using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;

public class ButtonClickEventHandler : MonoBehaviour
{
    private const int SSRUNTIMEAPI_SUCCESS = 0;

    enum MSG_TYPE
    {
        MSG_TYPE_INFO,
        MSG_TYPE_SUCCESS,
        MSG_TYPE_ERROR
    };

    private InputField m_EditApiPassword;
    private InputField m_EditLicenseKey;
    private InputField m_EditLicense;
    private Text m_TextMsg;
    private Scrollbar m_VerScrollbar;

    private string m_MsgContent;

    private AndroidJavaClass m_SSRuntimeApiCls;

    private long m_LicenseLoginHandle = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_SSRuntimeApiCls = new AndroidJavaClass("com.virbox.SSRuntimeApi");

        GameObject.Find("Button_init").GetComponent<Button>().onClick.AddListener(OnClickBtnInit);
        GameObject.Find("Button_uninit").GetComponent<Button>().onClick.AddListener(OnClickBtnUninit);

        GameObject.Find("Button_bind").GetComponent<Button>().onClick.AddListener(OnClickLicenseKeyBind);
        GameObject.Find("Button_unbind").GetComponent<Button>().onClick.AddListener(OnClickBtnLicenseKeyUnbind);
        GameObject.Find("Dropdown_more").GetComponent<Dropdown>().onValueChanged.AddListener(OnClickBtnLicenseKeyMore);

        GameObject.Find("Button_find_license").GetComponent<Button>().onClick.AddListener(OnClickBtnLicenseFind);
        GameObject.Find("Button_login").GetComponent<Button>().onClick.AddListener(OnClickBtnLicenseLogin);
        GameObject.Find("Button_keep_alive").GetComponent<Button>().onClick.AddListener(OnClickBtnLicenseKeepAlive);
        GameObject.Find("Button_logout").GetComponent<Button>().onClick.AddListener(OnClickBtnLicenseLogout);

        GameObject.Find("Button_clear_msg").GetComponent<Button>().onClick.AddListener(OnClickBtnClearMsg);

        m_EditApiPassword = GameObject.Find("InputField_api_password").GetComponent<InputField>();
        m_EditLicenseKey = GameObject.Find("InputField_license_key").GetComponent<InputField>();
        m_EditLicense = GameObject.Find("InputField_license_id").GetComponent<InputField>();
        m_TextMsg = GameObject.Find("Text_msg").GetComponent<Text>();
        m_VerScrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();

        AddMsg("欢迎使用Unity SSRuntimeApi Demo!", MSG_TYPE.MSG_TYPE_INFO);
        AddMsg("请横屏操作!", MSG_TYPE.MSG_TYPE_INFO);

        //TODO:测试代码
        m_EditApiPassword.text = "D7AA1DEE3495FAA0E81161FABB752687";
        m_EditLicenseKey.text = "UYHX-PYRE-2MPL-6BHS";
        m_EditLicense.text = "1025";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickBtnInit()
    {
        string ApiPassword = m_EditApiPassword.text;

        do
        {  
            try
            {
                if (ApiPassword.Length == 0)
                {
                    AddMsg("请输入正确的Api密码！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                bool bInit = m_SSRuntimeApiCls.CallStatic<bool>("IsSlmApiInitialized");
                if (bInit)
                {
                    AddMsg("你已经初始化过了！", MSG_TYPE.MSG_TYPE_SUCCESS);
                    break;
                }

                long InitRet = m_SSRuntimeApiCls.CallStatic<long>("SlmInit", ApiPassword);
                if(InitRet != SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg(string.Format("初始化失败，错误码：0x{0:X8}！", InitRet), MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AndroidJavaObject versionRet = m_SSRuntimeApiCls.CallStatic<AndroidJavaObject>("SlmGetVersion");
                long Ret = versionRet.Call<long>("getRetValue");                
                if(Ret == SSRUNTIMEAPI_SUCCESS)
                {
                    long ApiVersion = versionRet.Call<long>("getApiVersion");
                    AddMsg(string.Format("Api版本号：{0:D}", ApiVersion), MSG_TYPE.MSG_TYPE_SUCCESS);
                }

                AndroidJavaObject IDRet = m_SSRuntimeApiCls.CallStatic<AndroidJavaObject>("SlmGetDeveloperID");
                Ret = IDRet.Call<long>("getRetValue");
                if(Ret == SSRUNTIMEAPI_SUCCESS)
                {
                    string id = IDRet.Call<string>("getDeveloperID");
                    AddMsg(string.Format("开发商ID: {0}", id), MSG_TYPE.MSG_TYPE_SUCCESS);
                }
            }
            catch(System.Exception e)
            {
                AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
                break;
            }


        } while (false);
    }

    public void OnClickBtnUninit()
    {
        do
        {
            try
            {
                bool bInit = m_SSRuntimeApiCls.CallStatic<bool>("IsSlmApiInitialized");
                if (!bInit)
                {
                    AddMsg("请先进行初始化！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                long Ret = m_SSRuntimeApiCls.CallStatic<long>("SlmCleanup");
                if (Ret == SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg("反初始化成功！", MSG_TYPE.MSG_TYPE_SUCCESS);
                }
                else
                {
                    AddMsg(string.Format("反初始化失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                }
            }
            catch(Exception e)
            {
                AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
                break;
            }

        } while (false);
    }

    public void OnClickLicenseKeyBind()
    {
        string LicenseKey = m_EditLicenseKey.text;

        try
        {
            do
            {
                if(LicenseKey.Length == 0)
                {
                    AddMsg("请输入正确的授权码！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                long Ret = m_SSRuntimeApiCls.CallStatic<long>("SlmOnlineBind", LicenseKey);
                if(Ret != SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg(string.Format("绑定授权码失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                }
                else
                {
                    AddMsg("绑定授权码成功！", MSG_TYPE.MSG_TYPE_SUCCESS);
                }

            } while (false);
        }
        catch(Exception e)
        {
            AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);            
        }
    }

    public void OnClickBtnLicenseKeyUnbind()
    {
        string LicenseKey = m_EditLicenseKey.text;

        try
        {
            do
            {
                if (LicenseKey.Length == 0)
                {
                    AddMsg("请输入正确的授权码！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                long Ret = m_SSRuntimeApiCls.CallStatic<long>("SlmOnlineUnbind", LicenseKey);
                if (Ret != SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg(string.Format("解绑授权码失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                }
                else
                {
                    AddMsg("解绑授权码成功！", MSG_TYPE.MSG_TYPE_SUCCESS);
                }

            } while (false);
        }
        catch (Exception e)
        {
            AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
        }
    }

    public void OnClickBtnLicenseKeyMore(int index)
    {
        if(index == 0)
        {
            GenerateC2D();
        }
        else if(index == 1)
        {
            ImportD2C();
        }
    }     

    private void GenerateC2D()
    {
        string Dir = Application.persistentDataPath;
        if (!Directory.Exists(Dir))
        {
            Directory.CreateDirectory(Dir);
            if (!Directory.Exists(Dir))
            {
                AddMsg(string.Format("文件夹{0}创建失败！", Dir), MSG_TYPE.MSG_TYPE_ERROR);
            }
        }

        string filePath = Dir + "/Virbox_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".c2d";

        try
        {
            do
            {
                bool bInit = m_SSRuntimeApiCls.CallStatic<bool>("IsSlmApiInitialized");
                if (!bInit)
                {
                    AddMsg("请先进行初始化！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AddMsg(string.Format("即将创建C2D文件，保存路径：{0}", filePath), MSG_TYPE.MSG_TYPE_INFO);

                AndroidJavaObject c2dRet = m_SSRuntimeApiCls.CallStatic<AndroidJavaObject>("SlmOfflineGetBindC2D");
                long Ret = c2dRet.Call<long>("getRetValue");
                if(Ret == SSRUNTIMEAPI_SUCCESS)
                {
                    string data = c2dRet.Call<string>("getData");
                    File.WriteAllText(filePath, data);

                    AddMsg("创建C2D文件成功！", MSG_TYPE.MSG_TYPE_SUCCESS);
                }
                else
                {
                    AddMsg(string.Format("创建离线绑定C2D失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                }

            } while (false);
        }
        catch(Exception e)
        {
            AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
        }
    }

    private void ImportD2C()
    {
        string Dir = Application.persistentDataPath;
        if (!Directory.Exists(Dir))
        {
            Directory.CreateDirectory(Dir);
            if (!Directory.Exists(Dir))
            {
                AddMsg(string.Format("文件夹{0}创建失败！", Dir), MSG_TYPE.MSG_TYPE_ERROR);
            }
        }

        string fileName = "Virbox_offline_bind.d2c";

        string filePath = Dir + "/" + fileName;

        try
        {
            do
            {
                bool bInit = m_SSRuntimeApiCls.CallStatic<bool>("IsSlmApiInitialized");
                if (!bInit)
                {
                    AddMsg("请先进行初始化！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AddMsg(string.Format("即将导入文件{0}!", filePath), MSG_TYPE.MSG_TYPE_INFO);

                if (!File.Exists(filePath))
                {
                    AddMsg(string.Format("请将待导入文件拷贝到目录{0}下，并命名为{1}。", Dir, fileName), MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                string fileContent = File.ReadAllText(filePath);
                //AddMsg(string.Format("d2c文件内容为：{0}", fileContent), MSG_TYPE.MSG_TYPE_INFO);

                long Ret = m_SSRuntimeApiCls.CallStatic<long>("SlmOfflineBind", fileContent);                
                if (Ret == SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg("离线绑定d2c成功！", MSG_TYPE.MSG_TYPE_SUCCESS);
                }
                else
                {
                    AddMsg(string.Format("离线绑定d2c失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                }

            } while (false);
        }
        catch (Exception e)
        {
            AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
        }
    }

    public void OnClickBtnLicenseFind()
    {
        string LicenseID = m_EditLicense.text;

        try
        {
            do
            {
                if (LicenseID.Length == 0)
                {
                    AddMsg("请输入正确的许可ID！", MSG_TYPE.MSG_TYPE_SUCCESS);
                    break;
                }

                long uLicenseID = (long)Convert.ToUInt64(LicenseID);
                if (uLicenseID < 0 || uLicenseID > 4294967295L)
                {
                    AddMsg("请输入正确的许可ID！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                bool bInit = m_SSRuntimeApiCls.CallStatic<bool>("IsSlmApiInitialized");
                if (!bInit)
                {
                    AddMsg("请先进行初始化！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AndroidJavaObject FindRet = m_SSRuntimeApiCls.CallStatic<AndroidJavaObject>("SlmFindLicense", uLicenseID);
                long Ret = FindRet.Call<long>("getRetValue");
                if (Ret != SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg(string.Format("查找许可失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AddMsg("查找许可成功！", MSG_TYPE.MSG_TYPE_SUCCESS);

                string LicenseDesc = FindRet.Call<string>("getData");
                AddMsg(string.Format("{0}", LicenseDesc), MSG_TYPE.MSG_TYPE_SUCCESS);

            } while (false);
        }
        catch (Exception e)
        {
            AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
        }
    }

    public void OnClickBtnLicenseLogin()
    {
        string LicenseID = m_EditLicense.text;

        try
        {
            do
            {
                if (LicenseID.Length == 0)
                {
                    AddMsg("请输入正确的许可ID！", MSG_TYPE.MSG_TYPE_SUCCESS);
                    break;
                }

                long uLicenseID = (long)Convert.ToUInt64(LicenseID);
                if (uLicenseID < 0 || uLicenseID > 4294967295L)
                {
                    AddMsg("请输入正确的许可ID！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                bool bInit = m_SSRuntimeApiCls.CallStatic<bool>("IsSlmApiInitialized");
                if (!bInit)
                {
                    AddMsg("请先进行初始化！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                if(m_LicenseLoginHandle != 0)
                {
                    AddMsg("你已经登录过许可，请先进行许可登出！", MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AndroidJavaObject LoginRet = m_SSRuntimeApiCls.CallStatic<AndroidJavaObject>("SlmLogin", uLicenseID);
                long Ret = LoginRet.Call<long>("getRetValue");
                if(Ret != SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg(string.Format("许可登录失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AddMsg("许可登录成功！", MSG_TYPE.MSG_TYPE_SUCCESS);

                m_LicenseLoginHandle = LoginRet.Call<long>("getSlmHandle");

            } while (false);
        }
        catch (Exception e)
        {
            AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
        }
    }

    public void OnClickBtnLicenseKeepAlive()
    {
        try
        {
            do
            {
                if (m_LicenseLoginHandle == 0)
                {
                    AddMsg("许可未登录，无法执行该操作！", MSG_TYPE.MSG_TYPE_SUCCESS);
                    break;
                }               

                long Ret = m_SSRuntimeApiCls.CallStatic<long>("SlmKeepAlive", m_LicenseLoginHandle);                
                if (Ret != SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg(string.Format("执行心跳保持失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AddMsg("执行心跳保持成功！", MSG_TYPE.MSG_TYPE_SUCCESS);                

            } while (false);
        }
        catch (Exception e)
        {
            AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
        }
    }

    public void OnClickBtnLicenseLogout()
    {
        try
        {
            do
            {
                if (m_LicenseLoginHandle == 0)
                {
                    AddMsg("许可未登录，无法执行该操作！", MSG_TYPE.MSG_TYPE_SUCCESS);
                    break;
                }

                long Ret = m_SSRuntimeApiCls.CallStatic<long>("SlmLogout", m_LicenseLoginHandle);
                if (Ret != SSRUNTIMEAPI_SUCCESS)
                {
                    AddMsg(string.Format("许可登出失败，错误码：0x{0:X8}！", Ret), MSG_TYPE.MSG_TYPE_ERROR);
                    break;
                }

                AddMsg("许可登出成功！", MSG_TYPE.MSG_TYPE_SUCCESS);

                m_LicenseLoginHandle = 0;

            } while (false);
        }
        catch (Exception e)
        {
            AddMsg(e.ToString(), MSG_TYPE.MSG_TYPE_ERROR);
        }
    }

    public void OnClickBtnClearMsg()
    {
        m_MsgContent = "";
        m_TextMsg.text = m_MsgContent;
    }

    private void AddMsg(string Msg, MSG_TYPE type)
    {
        string szMsg = Msg;

        if(type == MSG_TYPE.MSG_TYPE_INFO)
        {
          szMsg = string.Format("<color=grey>{0}\r\n</color>", Msg);
        }
        else if(type == MSG_TYPE.MSG_TYPE_SUCCESS)
        {
            szMsg = string.Format("<color=blue>{0}\r\n</color>", Msg);
        }
        else
        {
            szMsg = string.Format("<color=red>{0}\r\n</color>", Msg);
        }

        m_MsgContent += szMsg;

        m_TextMsg.text = m_MsgContent;

        StartCoroutine("InsScrollbar");
    }

    IEnumerator InsScrollbar()
    {
        yield return new WaitForEndOfFrame();
        m_VerScrollbar.value = 0;
    }
}
