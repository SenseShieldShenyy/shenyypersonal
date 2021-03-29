package com.virbox;

/**
 * @author hudw
 * @version 1.0
 */

public class SSRuntimeApi {
    static{
        try{
            System.loadLibrary("slm_runtime_android");
        }catch(UnsatisfiedLinkError e){
            System.err.println("Native code library failed to load.\n" + e);
            //System.exit(1);
        }
    }

    private enum SLM_API_INIT_STATE{
        SLM_API_INIT_STATE_UNINITIALIZED,
        SLM_API_INIT_STATE_INITIALIZED
    }

    private static SLM_API_INIT_STATE SlmApiInitState = SLM_API_INIT_STATE.SLM_API_INIT_STATE_UNINITIALIZED;

    /**
     * 判断Api是否已经初始化
     * @return 布尔值
     */
    public static boolean IsSlmApiInitialized(){
        if (SlmApiInitState == SLM_API_INIT_STATE.SLM_API_INIT_STATE_INITIALIZED){
            return true;
        }
        else
        {
            return false;
        }
    }

    /**
     * 初始化函数，调用许可登录等函数前需先调用该函数
     * @param APIPassword Api密码，十六进制字符串，可登录开发者云平台查询
     * @return 错误码
     */
    public static long SlmInit(String APIPassword) {
        if (SlmApiInitState == SLM_API_INIT_STATE.SLM_API_INIT_STATE_INITIALIZED){
            return SSRuntimeApiModule.SS_OK;
        }

        long Ret = SSRuntimeApiBase.SlmInit(APIPassword);
        if (Ret == SSRuntimeApiModule.SS_OK)
        {
            SlmApiInitState = SLM_API_INIT_STATE.SLM_API_INIT_STATE_INITIALIZED;
        }

        return Ret;
    }

    /**
     * 反初始化，和SlmInit配套使用
     * @return 错误码，对应SlmInit使用
     */
    public static long SlmCleanup() {
        if (SlmApiInitState == SLM_API_INIT_STATE.SLM_API_INIT_STATE_UNINITIALIZED){
            return SSRuntimeApiModule.SS_OK;
        }

        long Ret = SSRuntimeApiBase.SlmCleanup();
        if (Ret == SSRuntimeApiModule.SS_OK){
            SlmApiInitState = SLM_API_INIT_STATE.SLM_API_INIT_STATE_UNINITIALIZED;
        }

        return Ret;
    }

    /**
     * 查找相应许可的信息
     * @param LicenseID 许可ID
     * @return 错误码 + 许可描述(JSON格式)
     */
    public static StFindLicenseRet SlmFindLicense(long LicenseID) {
        return SSRuntimeApiBase.SlmFindLicense(LicenseID);
    }

    /**
     * 许可登录，成功后会分配一个句柄标识，部分函数调用需传入该句柄标识
     * @param LicenseID 许可ID
     * @return 错误码 + 句柄标识
     */
    public static StLicenseLoginRet SlmLogin(long LicenseID) {
        return SSRuntimeApiBase.SlmLogin(LicenseID);
    }

    /**
     * 许可登出
     * @param Handle SlmLogin生成的句柄标识
     * @return 错误码
     */
    public static long SlmLogout(long Handle) {
        return SSRuntimeApiBase.SlmLogout(Handle);
    }

    /**
     * 心跳激活
     * @param Handle SlmLogin生成的句柄标识
     * @return 错误码
     */
    public static long SlmKeepAlive(long Handle) {
        return SSRuntimeApiBase.SlmKeepAlive(Handle);
    }

    /**
     * 数据加密
     * @param Handle SlmLogin生成的句柄标识
     * @param InBufferBase64 需要加密的二进制数据的Base64编码
     * @return 错误码 + 加密后的数据(Base64编码)
     */
    public static StEncDecRet SlmEncrypt(long Handle, String InBufferBase64) {
        return SSRuntimeApiBase.SlmEncrypt(Handle, InBufferBase64);
    }

    /**
     * 数据解密
     * @param Handle SlmLogin生成的句柄标识
     * @param InBufferBase64 需要解密的二进制数据的Base64编码
     * @return 错误码 + 解密后的数据(Base64编码)
     */
    public static StEncDecRet SlmDecrypt(long Handle, String InBufferBase64) {
        return SSRuntimeApiBase.SlmDecrypt(Handle, InBufferBase64);
    }

    /**
     * 获取许可数据区总大小；该数据区大小只能在签发许可时指定
     * @param Handle SlmLogin生成的句柄标识
     * @param Type 公开区/读写区/只读区
     * @return 错误码 + 数据区大小
     */
    public static StUserDataGetSizeRet SlmUserDataGetSize(long Handle, EnumLicenseUserDataType Type) {
        return SSRuntimeApiBase.SlmUserDataGetSize(Handle, Type);
    }

    /**
     * 获取数据区数据
     * @param Handle SlmLogin生成的句柄标识
     * @param Type 公开区/读写区/只读区
     * @param Offset 偏移位置后开始读取
     * @param Length 读取总长度
     * @return 错误码 + 读取数据(Base64编码）
     */
    public static StUserDataGetContentRet SlmUserDataRead(long Handle, EnumLicenseUserDataType Type, long Offset, long Length) {
        return SSRuntimeApiBase.SlmUserDataRead(Handle, Type, Offset, Length);
    }

    /**
     * 向读写区写入数据；若想成功，签发许可时，需指定该许可存在读写区
     * @param Handle SlmLogin生成的句柄标识
     * @param WriteBufferBase64 待写数据(Base64编码)
     * @param Offset 偏移位置后开始写入
     * @return 错误码
     */
    public static long SlmUserDataWrite(long Handle, String WriteBufferBase64, long Offset) {
        return SSRuntimeApiBase.SlmUserDataWrite(Handle, WriteBufferBase64, Offset);
    }

    /**
     * 获取当前登录许可的相关信息
     * @param Handle SlmLogin生成的句柄标识
     * @param Type 锁信息/会话信息/许可信息/文件信息
     * @return 错误码 + 数据(JSON格式)
     */
    public static StGetInfoRet SlmGetInfo(long Handle, EnumInfoType Type) {
        return SSRuntimeApiBase.SlmGetInfo(Handle, Type);
    }

    /**
     * 检查当前登录许可是否存在相应模块
     * @param Handle SlmLogin生成的句柄标识
     * @param ModuleID 模块ID
     * @return 错误码
     */
    public static long SlmCheckModule(long Handle, long ModuleID) {
        return SSRuntimeApiBase.SlmCheckModule(Handle, ModuleID);
    }

    /**
     * 获取SlmApi版本号
     * @return 错误码 + Api版本 + SS版本(始终为0)
     */
    public static StGetVersionRet SlmGetVersion() {
        return SSRuntimeApiBase.SlmGetVersion();
    }

    /**
     * 获取当前SlmApi所属开发者
     * @return 开发商ID，十六进制字符串
     */
    public static StGetDeveloperIDRet SlmGetDeveloperID() {
        return SSRuntimeApiBase.SlmGetDeveloperID();
    }

    /**
     * 获取错误码描述
     * @param ErrorCode 错误码
     * @param LangID 语言ID
     * @return 字符串，目前均为空
     */
    public static String SlmErrorFormat(long ErrorCode, EnumLangID LangID) {
        return SSRuntimeApiBase.SlmErrorFormat(ErrorCode, LangID);
    }

    /**
     * 在线绑定授权码
     * @param LicenseKey 授权码
     * @return 错误码
     */
    public static long SlmOnlineBind(String LicenseKey) {
        return SSRuntimeApiBase.SlmOnlineBind(LicenseKey);
    }

    /**
     * 在线刷新授权码
     * @param LicenseKey 授权码
     * @return 错误码
     */
    public static long SlmOnlineRefresh(String LicenseKey) {
        return SSRuntimeApiBase.SlmOnlineRefresh(LicenseKey);
    }

    /**
     * 在线解绑授权码
     * @param LicenseKey 授权码
     * @return 错误码
     */
    public static long SlmOnlineUnbind(String LicenseKey) {
        return SSRuntimeApiBase.SlmOnlineUnbind(LicenseKey);
    }

    /**
     * 离线绑定时，生成本地机器信息C2D文本，该文本需要保存到文件并上传到云平台，兑换离线绑定D2C文件
     * @return 错误码 + C2D(JSON格式)
     */
    public static StOfflineGetC2DRet SlmOfflineGetBindC2D() {
        return SSRuntimeApiBase.SlmOfflineGetBindC2D();
    }

    /**
     * 离线解绑时，生成该授权码的离线解绑信息C2D文本，该文本需要保存到文件并及时上传到云平台
     * @param LicenseKey 授权码
     * @return 错误码 + C2D(JSON格式)，该C2D需要保存到文件并上传到云平台
     */
    public static StOfflineGetC2DRet SlmOfflineGetUnbindC2D(String LicenseKey) {
        return SSRuntimeApiBase.SlmOfflineGetUnbindC2D(LicenseKey);
    }

    /**
     * 离线绑定
     * @param D2C 从云平台兑换的授权码离线绑定D2C文件内容
     * @return 错误码
     */
    public static long SlmOfflineBind(String D2C) {
        return SSRuntimeApiBase.SlmOfflineBind(D2C);
    }

    /**
     * 获取本地所有的授权码绑定信息
     * @return 错误码 + 本地的所有授权码绑定信息
     */
    public static StLocalBindInfoRet SlmEnumLocalBindInfo() {
        return SSRuntimeApiBase.SlmEnumLocalBindInfo();
    }

    /**
     * 获取指定授权码的绑定信息
     * @param LicenseKey 授权码
     * @return 错误码 + 该授权码绑定本地绑定信息
     */
    public static StLocalBindInfoRet SlmQueryLocalBindInfo(String LicenseKey) {
        return SSRuntimeApiBase.SlmQueryLocalBindInfo(LicenseKey);
    }

    /**
     * 获取指定授权码下的所有许可信息
     * @param LicenseKey 授权码
     * @return 错误码 + 该授权码下所有的许可信息
     */
    public static StGetLocalLicenseInfoRet SlmGetLocalLicenseInfo(String LicenseKey) {
        return SSRuntimeApiBase.SlmGetLocalLicenseInfo(LicenseKey);
    }

    /**
     * 设置UUID
     * @param DeviceUUIDHexStr UUID的十六进制文本
     * @return 错误码
     */
    public static long SlmSetArmDeviceUUID(String DeviceUUIDHexStr) {
        return SSRuntimeApiBase.SlmSetArmDeviceUUID(DeviceUUIDHexStr);
    }

    /**
     * 获取设备信息
     * @return 错误码 + 设备信息
     */
    public static StGetArmDeviceInfoRet SlmGetArmDeviceInfo() {
        return SSRuntimeApiBase.SlmGetArmDeviceInfo();
    }
}
