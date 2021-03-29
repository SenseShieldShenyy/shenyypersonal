/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 4.0.2
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

package com.virbox;

public class StDeviceInfo {
  private transient long swigCPtr;
  protected transient boolean swigCMemOwn;

  protected StDeviceInfo(long cPtr, boolean cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = cPtr;
  }

  protected static long getCPtr(StDeviceInfo obj) {
    return (obj == null) ? 0 : obj.swigCPtr;
  }

  @SuppressWarnings("deprecation")
  protected void finalize() {
    delete();
  }

  public synchronized void delete() {
    if (swigCPtr != 0) {
      if (swigCMemOwn) {
        swigCMemOwn = false;
        SSRuntimeApiModuleJNI.delete_StDeviceInfo(swigCPtr);
      }
      swigCPtr = 0;
    }
  }

  public void setUUID(String value) {
    SSRuntimeApiModuleJNI.StDeviceInfo_UUID_set(swigCPtr, this, value);
  }

  public String getUUID() {
    return SSRuntimeApiModuleJNI.StDeviceInfo_UUID_get(swigCPtr, this);
  }

  public void setMac(String value) {
    SSRuntimeApiModuleJNI.StDeviceInfo_Mac_set(swigCPtr, this, value);
  }

  public String getMac() {
    return SSRuntimeApiModuleJNI.StDeviceInfo_Mac_get(swigCPtr, this);
  }

  public void setCpuSerialID(String value) {
    SSRuntimeApiModuleJNI.StDeviceInfo_CpuSerialID_set(swigCPtr, this, value);
  }

  public String getCpuSerialID() {
    return SSRuntimeApiModuleJNI.StDeviceInfo_CpuSerialID_get(swigCPtr, this);
  }

  public void setCpuVendor(String value) {
    SSRuntimeApiModuleJNI.StDeviceInfo_CpuVendor_set(swigCPtr, this, value);
  }

  public String getCpuVendor() {
    return SSRuntimeApiModuleJNI.StDeviceInfo_CpuVendor_get(swigCPtr, this);
  }

  public void setCpuBrand(String value) {
    SSRuntimeApiModuleJNI.StDeviceInfo_CpuBrand_set(swigCPtr, this, value);
  }

  public String getCpuBrand() {
    return SSRuntimeApiModuleJNI.StDeviceInfo_CpuBrand_get(swigCPtr, this);
  }

  public void setPartition(String value) {
    SSRuntimeApiModuleJNI.StDeviceInfo_Partition_set(swigCPtr, this, value);
  }

  public String getPartition() {
    return SSRuntimeApiModuleJNI.StDeviceInfo_Partition_get(swigCPtr, this);
  }

  public StDeviceInfo() {
    this(SSRuntimeApiModuleJNI.new_StDeviceInfo(), true);
  }

}