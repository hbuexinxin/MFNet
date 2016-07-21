﻿/* license

mfcaptureengine.cs - Part of MediaFoundationLib, which provide access to MediaFoundation interfaces via .NET

Copyright (C) 2015, by the Administrators of the Media Foundation .NET SourceForge Project
http://mfnet.sourceforge.net

This is free software; you can redistribute it and/or modify it under the terms of either:

a) The Lesser General Public License version 2.1 (see license.txt)
b) The BSD License (see BSDL.txt)

*/

using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using MediaFoundation.Misc;
using MediaFoundation.EVR;

namespace MediaFoundation
{
    #region Declarations

    [UnmanagedName("MF_CAPTURE_ENGINE_DEVICE_TYPE")]
    public enum MF_CAPTURE_ENGINE_DEVICE_TYPE
    {
        Audio = 0x00000000,
        Video = 0x00000001
    }

    [UnmanagedName("MF_CAPTURE_ENGINE_SINK_TYPE")]
    public enum MF_CAPTURE_ENGINE_SINK_TYPE
    {
        Record = 0x00000000,
        Preview = 0x00000001,
        Photo = 0x00000002
    }

    [UnmanagedName("MF_CAPTURE_ENGINE_STREAM_CATEGORY")]
    public enum MF_CAPTURE_ENGINE_STREAM_CATEGORY
    {
        VideoPreview = 0x00000000,
        VideoCapture = 0x00000001,
        PhotoIndependent = 0x00000002,
        PhotoDependent = 0x00000003,
        Audio = 0x00000004,
        Unsupported = 0x00000005
    }

    #endregion

    #region Interfaces

#if ALLOW_UNTESTED_INTERFACES
    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("e37ceed7-340f-4514-9f4d-9c2ae026100b")]
    public interface IMFCaptureEngineOnSampleCallback2 : IMFCaptureEngineOnSampleCallback
    {
        #region IMFCaptureEngineOnSampleCallback methods

        [PreserveSig]
        new int OnSample(
            IMFSample pSample
            );

        #endregion

        [PreserveSig]
        int OnSynchronizedEvent(
            IMFMediaEvent pEvent
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("f9e4219e-6197-4b5e-b888-bee310ab2c59")]
    public interface IMFCaptureSink2 : IMFCaptureSink
    {
        #region IMFCaptureEngineOnSampleCallback methods

        [PreserveSig]
        new int GetOutputMediaType(
            int dwSinkStreamIndex,
            out IMFMediaType ppMediaType
            );

        [PreserveSig]
        new int GetService(
            int dwSinkStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
            );

        [PreserveSig]
        new int AddStream(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType,
            IMFAttributes pAttributes,
            out int pdwSinkStreamIndex
            );

        [PreserveSig]
        new int Prepare();

        [PreserveSig]
        new int RemoveAllStreams();

        #endregion

        [PreserveSig]
        int SetOutputMediaType(
            int dwStreamIndex,
            IMFMediaType pMediaType,
            IMFAttributes pEncodingAttributes
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
    Guid("19f68549-ca8a-4706-a4ef-481dbc95e12c")]
    public interface IMFCapturePhotoConfirmation
    {
        [PreserveSig]
        int SetPhotoConfirmationCallback(
            IMFAsyncCallback pNotificationCallback
            );

        [PreserveSig]
        int SetPixelFormat(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid subtype
            );

        [PreserveSig]
        int GetPixelFormat(
            out Guid subtype
            );
    }
#endif

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("aeda51c0-9025-4983-9012-de597b88b089"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngineOnEventCallback
    {
        [PreserveSig]
        int OnEvent(
            IMFMediaEvent pEvent
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("52150b82-ab39-4467-980f-e48bf0822ecd"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngineOnSampleCallback
    {
        [PreserveSig]
        int OnSample(
            IMFSample pSample
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("72d6135b-35e9-412c-b926-fd5265f2a885"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureSink
    {
        [PreserveSig]
        int GetOutputMediaType(
            int dwSinkStreamIndex,
            out IMFMediaType ppMediaType
            );

        [PreserveSig]
        int GetService(
            int dwSinkStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
            );

        [PreserveSig]
        int AddStream(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType,
            IMFAttributes pAttributes,
            out int pdwSinkStreamIndex
            );

        [PreserveSig]
        int Prepare();

        [PreserveSig]
        int RemoveAllStreams();
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("3323b55a-f92a-4fe2-8edc-e9bfc0634d77"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureRecordSink : IMFCaptureSink
    {
        #region IMFCaptureSink Methods

        [PreserveSig]
        new int GetOutputMediaType(
            int dwSinkStreamIndex,
            out IMFMediaType ppMediaType
            );

        [PreserveSig]
        new int GetService(
            int dwSinkStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
            );

        [PreserveSig]
        new int AddStream(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType,
            IMFAttributes pAttributes,
            out int pdwSinkStreamIndex
            );

        [PreserveSig]
        new int Prepare();

        [PreserveSig]
        new int RemoveAllStreams();

        #endregion

        [PreserveSig]
        int SetOutputByteStream(
            IMFByteStream pByteStream,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid guidContainerType
            );

        [PreserveSig]
        int SetOutputFileName(
            [MarshalAs(UnmanagedType.LPWStr)] string fileName
            );

        [PreserveSig]
        int SetSampleCallback(
            int dwStreamSinkIndex,
            IMFCaptureEngineOnSampleCallback pCallback
            );

        [PreserveSig]
        int SetCustomSink(
            IMFMediaSink pMediaSink
            );

        [PreserveSig]
        int GetRotation(
            int dwStreamIndex,
            out int pdwRotationValue
            );

        [PreserveSig]
        int SetRotation(
            int dwStreamIndex,
            int dwRotationValue
        );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("77346cfd-5b49-4d73-ace0-5b52a859f2e0"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCapturePreviewSink : IMFCaptureSink
    {
        #region IMFCaptureSink Methods

        [PreserveSig]
        new int GetOutputMediaType(
            int dwSinkStreamIndex,
            out IMFMediaType ppMediaType
            );

        [PreserveSig]
        new int GetService(
            int dwSinkStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
            );

        [PreserveSig]
        new int AddStream(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType,
            IMFAttributes pAttributes,
            out int pdwSinkStreamIndex
            );

        [PreserveSig]
        new int Prepare();

        [PreserveSig]
        new int RemoveAllStreams();

        #endregion

        [PreserveSig]
        int SetRenderHandle(
            IntPtr handle
            );

        [PreserveSig]
        int SetRenderSurface(
            [MarshalAs(UnmanagedType.IUnknown)] object pSurface
            );

        [PreserveSig]
        int UpdateVideo(
            [In] MFVideoNormalizedRect pSrc,
            [In] MFRect pDst,
            [In] MFInt pBorderClr
            );

        [PreserveSig]
        int SetSampleCallback(
            int dwStreamSinkIndex,
            IMFCaptureEngineOnSampleCallback pCallback
            );

        [PreserveSig]
        int GetMirrorState(
            [MarshalAs(UnmanagedType.Bool)] out bool pfMirrorState
            );

        [PreserveSig]
        int SetMirrorState(
            [MarshalAs(UnmanagedType.Bool)] bool fMirrorState
            );

        [PreserveSig]
        int GetRotation(
            int dwStreamIndex,
            out int pdwRotationValue
            );

        [PreserveSig]
        int SetRotation(
            int dwStreamIndex,
            int dwRotationValue
            );

        [PreserveSig]
        int SetCustomSink(
            IMFMediaSink pMediaSink
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("d2d43cc8-48bb-4aa7-95db-10c06977e777"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCapturePhotoSink : IMFCaptureSink
    {
        #region IMFCaptureSink Methods

        [PreserveSig]
        new int GetOutputMediaType(
            int dwSinkStreamIndex,
            out IMFMediaType ppMediaType
            );

        [PreserveSig]
        new int GetService(
            int dwSinkStreamIndex,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
            );

        [PreserveSig]
        new int AddStream(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType,
            IMFAttributes pAttributes,
            out int pdwSinkStreamIndex
            );

        [PreserveSig]
        new int Prepare();

        [PreserveSig]
        new int RemoveAllStreams();

        #endregion

        [PreserveSig]
        int SetOutputFileName(
            [MarshalAs(UnmanagedType.LPWStr)] string fileName
            );

        [PreserveSig]
        int SetSampleCallback(
            IMFCaptureEngineOnSampleCallback pCallback
            );

        [PreserveSig]
        int SetOutputByteStream(
            IMFByteStream pByteStream
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("439a42a8-0d2c-4505-be83-f79b2a05d5c4"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureSource
    {
        [PreserveSig]
        int GetCaptureDeviceSource(
            MF_CAPTURE_ENGINE_DEVICE_TYPE mfCaptureEngineDeviceType,
            out IMFMediaSource ppMediaSource
            );

        [PreserveSig]
        int GetCaptureDeviceActivate(
            MF_CAPTURE_ENGINE_DEVICE_TYPE mfCaptureEngineDeviceType,
            out IMFActivate ppActivate
            );

        [PreserveSig]
        int GetService(
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid rguidService,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [MarshalAs(UnmanagedType.IUnknown)] out object ppUnknown
            );

        [PreserveSig]
        int AddEffect(
            int dwSourceStreamIndex,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnknown
            );

        [PreserveSig]
        int RemoveEffect(
            int dwSourceStreamIndex,
            [MarshalAs(UnmanagedType.IUnknown)] object pUnknown
            );

        [PreserveSig]
        int RemoveAllEffects(
            int dwSourceStreamIndex
            );

        [PreserveSig]
        int GetAvailableDeviceMediaType(
            int dwSourceStreamIndex,
            int dwMediaTypeIndex,
            out IMFMediaType ppMediaType
            );

        [PreserveSig]
        int SetCurrentDeviceMediaType(
            int dwSourceStreamIndex,
            IMFMediaType pMediaType
            );

        [PreserveSig]
        int GetCurrentDeviceMediaType(
            int dwSourceStreamIndex,
            out IMFMediaType ppMediaType
            );

        [PreserveSig]
        int GetDeviceStreamCount(
            out int pdwStreamCount
            );

        [PreserveSig]
        int GetDeviceStreamCategory(
            int dwSourceStreamIndex,
            out MF_CAPTURE_ENGINE_STREAM_CATEGORY pStreamCategory
            );

        [PreserveSig]
        int GetMirrorState(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] out bool pfMirrorState
            );

        [PreserveSig]
        int SetMirrorState(
            int dwStreamIndex,
            [MarshalAs(UnmanagedType.Bool)] bool fMirrorState
            );

        [PreserveSig]
        int GetStreamIndexFromFriendlyName(
            int uifriendlyName,
            out int pdwActualStreamIndex
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("a6bba433-176b-48b2-b375-53aa03473207"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngine
    {
        [PreserveSig]
        int Initialize(
            IMFCaptureEngineOnEventCallback pEventCallback,
            IMFAttributes pAttributes,
            [MarshalAs(UnmanagedType.IUnknown)] object pAudioSource,
            [MarshalAs(UnmanagedType.IUnknown)] object pVideoSource
            );

        [PreserveSig]
        int StartPreview();

        [PreserveSig]
        int StopPreview();

        [PreserveSig]
        int StartRecord();

        [PreserveSig]
        int StopRecord(
            [MarshalAs(UnmanagedType.Bool)] bool bFinalize,
            [MarshalAs(UnmanagedType.Bool)] bool bFlushUnprocessedSamples
            );

        [PreserveSig]
        int TakePhoto();

        [PreserveSig]
        int GetSink(
            MF_CAPTURE_ENGINE_SINK_TYPE mfCaptureEngineSinkType,
            out IMFCaptureSink ppSink
            );

        [PreserveSig]
        int GetSource(
            out IMFCaptureSource ppSource
            );
    }

    [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
    Guid("8f02d140-56fc-4302-a705-3a97c78be779"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMFCaptureEngineClassFactory
    {
        [PreserveSig]
        int CreateInstance(
                [In, MarshalAs(UnmanagedType.LPStruct)] Guid clsid,
                [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid,
                [MarshalAs(UnmanagedType.IUnknown)] out object ppvObject
                );

    }
    #endregion
}
