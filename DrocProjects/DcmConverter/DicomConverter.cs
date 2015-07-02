using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ClearCanvas.Dicom;
using ClearCanvas.Dicom.Utilities;

namespace VietBaIT.DicomConverter
{
    public static class DicomConverter
    {
        static int _bitsStored = 16;
        public static bool Convert2Dicom(string rawFileName, DataRow patientInfo, string AutoWLPath)
        {
            FileStream fs = File.OpenRead(rawFileName);
            var pixels = new byte[fs.Length];
            fs.Read(pixels, 0, (int) fs.Length);
            return Convert2Dicom(pixels, rawFileName, patientInfo, AutoWLPath);
        }
       
        public static bool Convert2Dicom(byte[] pixelData, string rawFileName, DataRow patientInfo,string AutoWLPath)
        {
            try
            {
                const string colPid = "PID";
                const string colKVP = "KVP";
                const string colMAS = "MAS";
                const string colDepartmentName = "Department_Name";
                const string colPatientName = "Patient_Name";
                const string colPatientSex = "Patient_Sex";
                const string colPatientAge = "Patient_Age";
                const string colPatientBirthdate = "Patient_Birthdate";
                const string colRegDate = "Reg_Date";
                const string colRegNum = "Reg_Num";
                const string colMonoChrome = "MonoChrome";
                const string colImgWidth = "IMGWidth";
                const string colImgHeight = "IMGHeigh";
                const string colModalityCode = "Modality_Code";
                const string colAtonomyCode = "Atonomy_Code";
                const string colProjectionCode = "Projection_Code";
                const string colHostpitalName = "Hostpital_Name";
                

                const string _colStudyInstanceUID = "StudyInstanceUID";
                const string colSeriesInstanceUID = "SeriesInstanceUID";
                const string colSOPInstanceUID = "SOPInstanceUID";
                const string colAcqDate = "AcqDate";
                const string colAppName = "AppName";
              
                const string colBitsStored = "BitsStored";
                const string colHightBit = "HightBit";
                const string colBitsAllocated = "BitsAllocated";

                _bitsStored = TryGetBitsStored(patientInfo, colBitsStored);
                var _HightBit = TryGetBitsStored(patientInfo, colHightBit);
                var _BitsAllocated = TryGetBitsStored(patientInfo, colBitsAllocated);

                var MonoChrome = TryGetString(patientInfo, colMonoChrome,"MONOCHROME2");
                var _KVP = TryGetString(patientInfo, colKVP);
                var _MAS = TryGetString(patientInfo, colMAS);
                var pid = TryGetString(patientInfo, colPid);
                var patientName = TryGetString(patientInfo, colPatientName);
                var patientSex = TryGetString(patientInfo, colPatientSex);
                var patientAge = TryGetString(patientInfo, colPatientAge);
                var patientBirthdate = TryGetString(patientInfo, colPatientBirthdate);
                var regDate = TryGetString(patientInfo, colRegDate);
                var regNum = TryGetString(patientInfo, colRegNum);
                var imgWidth = TryGetString(patientInfo, colImgWidth);
                var imgHeigh = TryGetString(patientInfo, colImgHeight);
                var modalityCode = TryGetString(patientInfo, colModalityCode);
                var atonomyCode = TryGetString(patientInfo, colAtonomyCode);
                var projectionCode = TryGetString(patientInfo, colProjectionCode);
                var hostpitalName = TryGetString(patientInfo, colHostpitalName,"BACH MAI HOSTPITAL");
                var departmentName = TryGetString(patientInfo, colDepartmentName, "Khoa chan doan hinh anh");
                string defaultStudyInstanceUID = ClearCanvas.Dicom.DicomUid.GenerateUid().UID;
                string defaultSeriesInstanceUID = defaultStudyInstanceUID + ".1";
                string defaultSOPInstanceUID = defaultSeriesInstanceUID + ".1";
                var StudyInstanceUID = TryGetString(patientInfo, _colStudyInstanceUID, defaultStudyInstanceUID);
                var SeriesInstanceUID = TryGetString(patientInfo, colSeriesInstanceUID, defaultSeriesInstanceUID);
                var SOPInstanceUID = TryGetString(patientInfo, colSOPInstanceUID, defaultSOPInstanceUID);
                var AppName = TryGetString(patientInfo, colAppName, "VBIT");

                string dicomPath = Path.GetDirectoryName(rawFileName);

                // Lấy về tên file Dicom từ file raw
                string dicomFileName = string.Format("{0}{1}{2}.DCM", dicomPath, Path.DirectorySeparatorChar,
                                                     Path.GetFileNameWithoutExtension(rawFileName));
               
                    try2RenameExistedFile(dicomFileName);


                //FileStream fs = File.OpenRead(rawFileName);

                //long length = fs.Length;
                long dataLength = pixelData.Length;

                string col=imgWidth.ToString();
                string row = imgHeigh.ToString();
               // GetSize(dataLength, out col, out row);

                // Tạo File Dicom để lưu thông tin
                var dcmFile = new DicomFile(dicomFileName);
                DicomAttributeCollection dicomDataSet = dcmFile.DataSet;

                //Set Tag For File
                DateTime studyTime = DateTime.Now;
                dicomDataSet[DicomTags.SpecificCharacterSet].SetStringValue("ISO_IR 100");
                dicomDataSet[DicomTags.ImageType].SetStringValue("ORIGINAL\\PRIMARY\\OTHER\\M\\FFE");
                dicomDataSet[DicomTags.InstanceCreationDate].SetStringValue(DateParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.InstanceCreationTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.SopClassUid].SetStringValue(SopClass.MrImageStorageUid);
                dicomDataSet[DicomTags.SopInstanceUid].SetStringValue(SOPInstanceUID);
                dicomDataSet[DicomTags.StudyDate].SetStringValue(regDate);
                dicomDataSet[DicomTags.ApplicationName].SetStringValue(AppName);
                dicomDataSet[DicomTags.StudyTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.SeriesDate].SetStringValue(regDate);
                dicomDataSet[DicomTags.SeriesTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.AccessionNumber].SetStringValue(regNum);
                dicomDataSet[DicomTags.Modality].SetStringValue(modalityCode);
                dicomDataSet[DicomTags.Manufacturer].SetStringValue("VBIT");
                dicomDataSet[DicomTags.ManufacturersModelName].SetNullValue();
                dicomDataSet[DicomTags.InstitutionName].SetStringValue(hostpitalName);
                dicomDataSet[DicomTags.InstitutionalDepartmentName].SetStringValue(departmentName);
                //dicomDataSet[DicomTags.StudyDescription].SetStringValue("HEART");
                //dicomDataSet[DicomTags.SeriesDescription].SetStringValue("Heart 2D EPI BH TRA");
                //dicomDataSet[DicomTags.PatientsName].SetStringValue("Patient^Test");
                dicomDataSet[DicomTags.PatientsName].SetStringValue(patientName);
                dicomDataSet[DicomTags.PatientId].SetStringValue(pid);
                dicomDataSet[DicomTags.PatientsBirthDate].SetStringValue(patientBirthdate);
                dicomDataSet[DicomTags.PatientsSex].SetStringValue(patientSex);
                dicomDataSet[DicomTags.PatientsAge].SetStringValue(patientAge);
                dicomDataSet[DicomTags.PatientsWeight].SetStringValue("70");
                dicomDataSet[DicomTags.SequenceVariant].SetStringValue("OTHER");
                dicomDataSet[DicomTags.ScanOptions].SetStringValue("CG");
                dicomDataSet[DicomTags.MrAcquisitionType].SetStringValue("2D");
                dicomDataSet[DicomTags.SliceThickness].SetStringValue("2");
                dicomDataSet[DicomTags.RepetitionTime].SetStringValue("857.142883");
                dicomDataSet[DicomTags.EchoTime].SetStringValue("8.712100");
                dicomDataSet[DicomTags.NumberOfAverages].SetStringValue("1");
                dicomDataSet[DicomTags.ImagingFrequency].SetStringValue("63.901150");
                dicomDataSet[DicomTags.ImagedNucleus].SetStringValue("1H");
                dicomDataSet[DicomTags.EchoNumbers].SetStringValue("1");
                dicomDataSet[DicomTags.MagneticFieldStrength].SetStringValue("1.500000");
                dicomDataSet[DicomTags.SpacingBetweenSlices].SetStringValue("10.00000");
                dicomDataSet[DicomTags.NumberOfPhaseEncodingSteps].SetStringValue("81");
                dicomDataSet[DicomTags.EchoTrainLength].SetStringValue("0");
                dicomDataSet[DicomTags.PercentSampling].SetStringValue("63.281250");
                dicomDataSet[DicomTags.PercentPhaseFieldOfView].SetStringValue("68.75000");
                dicomDataSet[DicomTags.DeviceSerialNumber].SetStringValue("1234");
                dicomDataSet[DicomTags.SoftwareVersions].SetStringValue("V1.0");
                dicomDataSet[DicomTags.ProtocolName].SetStringValue("2D EPI BH");
                dicomDataSet[DicomTags.TriggerTime].SetStringValue("14.000000");
                dicomDataSet[DicomTags.LowRRValue].SetStringValue("948");
                dicomDataSet[DicomTags.HighRRValue].SetStringValue("1178");
                dicomDataSet[DicomTags.IntervalsAcquired].SetStringValue("102");
                dicomDataSet[DicomTags.IntervalsRejected].SetStringValue("0");
                dicomDataSet[DicomTags.HeartRate].SetStringValue("56");
                dicomDataSet[DicomTags.ReceiveCoilName].SetStringValue("B");
                dicomDataSet[DicomTags.TransmitCoilName].SetStringValue("B");
                dicomDataSet[DicomTags.InPlanePhaseEncodingDirection].SetStringValue("COL");
                dicomDataSet[DicomTags.FlipAngle].SetStringValue("50.000000");
                dicomDataSet[DicomTags.PatientPosition].SetStringValue(atonomyCode);
                dicomDataSet[DicomTags.StudyInstanceUid].SetStringValue(StudyInstanceUID);//DicomUid.GenerateUid().UID
                dicomDataSet[DicomTags.SeriesInstanceUid].SetStringValue(SeriesInstanceUID);//DicomUid.GenerateUid().UID
                //dicomDataSet[DicomTags.StudyId].SetStringValue(pid);
                dicomDataSet[DicomTags.ExposureInMas].SetStringValue(_MAS);
                dicomDataSet[DicomTags.Kvp].SetStringValue(_KVP);
                dicomDataSet[DicomTags.SeriesNumber].SetStringValue("1");
                dicomDataSet[DicomTags.AcquisitionNumber].SetStringValue("7");
                dicomDataSet[DicomTags.InstanceNumber].SetStringValue("1");
                dicomDataSet[DicomTags.ImagePositionPatient].SetStringValue("-61.7564\\-212.04848\\-99.6208");
                dicomDataSet[DicomTags.ImageOrientationPatient].SetStringValue("0.861\\0.492\\0.126\\-0.2965");
                dicomDataSet[DicomTags.FrameOfReferenceUid].SetStringValue(DicomUid.GenerateUid().UID);
                dicomDataSet[DicomTags.PositionReferenceIndicator].SetStringValue(null);
                //dicomDataSet[DicomTags.ImageComments].SetStringValue("Test MR Image");
                dicomDataSet[DicomTags.SamplesPerPixel].SetStringValue("1");
                dicomDataSet[DicomTags.PhotometricInterpretation].SetStringValue(MonoChrome);
                dicomDataSet[DicomTags.Rows].SetStringValue(row);
                dicomDataSet[DicomTags.Columns].SetStringValue(col);

                dicomDataSet[DicomTags.PixelSpacing].SetStringValue("0.168\\0.168");
                dicomDataSet[DicomTags.BitsAllocated].SetStringValue(_BitsAllocated.ToString());
                dicomDataSet[DicomTags.BitsStored].SetStringValue(_bitsStored.ToString());
                dicomDataSet[DicomTags.HighBit].SetStringValue(_HightBit.ToString());
                dicomDataSet[DicomTags.PixelRepresentation].SetStringValue("0");
                if (File.Exists(AutoWLPath))
                {
                    switch (_bitsStored)
                    {
                        case 12:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("2048");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("4095");

                            break;

                        case 14:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("8192");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("16383");

                            break;

                        case 16:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("32767");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("65535");
                            break;

                        default:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("32767");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("65535");
                            break;
                    }

                }

                //dicomDataSet[DicomTags.SmallestImagePixelValue].SetStringValue("32772");
                //dicomDataSet[DicomTags.LargestImagePixelValue].SetStringValue("47745");
                dicomDataSet[DicomTags.RescaleIntercept].SetStringValue("0.");
                dicomDataSet[DicomTags.RescaleSlope].SetStringValue("1.");
                

                dicomDataSet[DicomTags.ViewPosition].SetStringValue(projectionCode);
                dicomDataSet[DicomTags.BodyPartExamined].SetStringValue(atonomyCode);


                //Gán Dữ liệu ảnh
                var pixels = new DicomAttributeOW(DicomTags.PixelData) {Values = pixelData};
                dicomDataSet[DicomTags.PixelData] = pixels;

                var item = new DicomSequenceItem();
                dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);
                item[DicomTags.RequestedProcedureId].SetStringValue("MRR1234");
                item[DicomTags.ScheduledProcedureStepId].SetStringValue("MRS1234");

                item = new DicomSequenceItem();
                dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);

                item[DicomTags.RequestedProcedureId].SetStringValue("MR2R1234");
                item[DicomTags.ScheduledProcedureStepId].SetStringValue("MR2S1234");

                var studyItem = new DicomSequenceItem();

                item[DicomTags.ReferencedStudySequence].AddSequenceItem(studyItem);

                studyItem[DicomTags.ReferencedSopClassUid].SetStringValue(SopClass.MrImageStorageUid);
                studyItem[DicomTags.ReferencedSopInstanceUid].SetStringValue("1.2.3.4.5.6.7.8.9");

                //Set Meta Info
                dicomDataSet[DicomTags.MediaStorageSopClassUid].SetStringValue(
                    dcmFile.DataSet[DicomTags.SopClassUid].GetString(0, ""));
                dicomDataSet[DicomTags.MediaStorageSopInstanceUid].SetStringValue(
                    dcmFile.DataSet[DicomTags.SopInstanceUid].GetString(0, ""));

                dcmFile.TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;

                dicomDataSet[DicomTags.ImplementationClassUid].SetStringValue("1.1.1.1.1.11.1");
                dicomDataSet[DicomTags.ImplementationVersionName].SetStringValue("DICOM 1.0");

                // Lưu File
                dcmFile.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool Convert2Dicom(byte[] pixelData, string rawFileName, DataRow patientInfo, string AutoWLPath,ref string newDCMFile)
        {
            try
            {
                const string colPid = "PID";
                const string colKVP = "KVP";
                const string colMAS = "MAS";
                const string colDepartmentName = "Department_Name";
                const string colPatientName = "Patient_Name";
                const string colPatientSex = "Patient_Sex";
                const string colPatientAge = "Patient_Age";
                const string colPatientBirthdate = "Patient_Birthdate";
                const string colRegDate = "Reg_Date";
                const string colRegNum = "Reg_Num";
                const string colMonoChrome = "MonoChrome";
                const string colImgWidth = "IMGWidth";
                const string colImgHeight = "IMGHeigh";
                const string colModalityCode = "Modality_Code";
                const string colAtonomyCode = "Atonomy_Code";
                const string colProjectionCode = "Projection_Code";
                const string colHostpitalName = "Hostpital_Name";


                const string _colStudyInstanceUID = "StudyInstanceUID";
                const string colSeriesInstanceUID = "SeriesInstanceUID";
                const string colSOPInstanceUID = "SOPInstanceUID";
                const string colAcqDate = "AcqDate";
                const string colAppName = "AppName";

                const string colBitsStored = "BitsStored";
                const string colHightBit = "HightBit";
                const string colBitsAllocated = "BitsAllocated";

                _bitsStored = TryGetBitsStored(patientInfo, colBitsStored);
                var _HightBit = TryGetBitsStored(patientInfo, colHightBit);
                var _BitsAllocated = TryGetBitsStored(patientInfo, colBitsAllocated);

                var MonoChrome = TryGetString(patientInfo, colMonoChrome, "MONOCHROME2");
                var _KVP = TryGetString(patientInfo, colKVP);
                var _MAS = TryGetString(patientInfo, colMAS);
                var pid = TryGetString(patientInfo, colPid);
                var patientName = TryGetString(patientInfo, colPatientName);
                var patientSex = TryGetString(patientInfo, colPatientSex);
                var patientAge = TryGetString(patientInfo, colPatientAge);
                var patientBirthdate = TryGetString(patientInfo, colPatientBirthdate);
                var regDate = TryGetString(patientInfo, colRegDate);
                var regNum = TryGetString(patientInfo, colRegNum);
                var imgWidth = TryGetString(patientInfo, colImgWidth);
                var imgHeigh = TryGetString(patientInfo, colImgHeight);
                var modalityCode = TryGetString(patientInfo, colModalityCode);
                var atonomyCode = TryGetString(patientInfo, colAtonomyCode);
                var projectionCode = TryGetString(patientInfo, colProjectionCode);
                var hostpitalName = TryGetString(patientInfo, colHostpitalName, "BACH MAI HOSTPITAL");
                var departmentName = TryGetString(patientInfo, colDepartmentName, "Khoa chan doan hinh anh");
                string defaultStudyInstanceUID = ClearCanvas.Dicom.DicomUid.GenerateUid().UID;
                string defaultSeriesInstanceUID = defaultStudyInstanceUID + ".1";
                string defaultSOPInstanceUID = defaultSeriesInstanceUID + ".1";
                var StudyInstanceUID = TryGetString(patientInfo, _colStudyInstanceUID, defaultStudyInstanceUID);
                var SeriesInstanceUID = TryGetString(patientInfo, colSeriesInstanceUID, defaultSeriesInstanceUID);
                var SOPInstanceUID = TryGetString(patientInfo, colSOPInstanceUID, defaultSOPInstanceUID);
                var AppName = TryGetString(patientInfo, colAppName, "VBIT");

                string dicomPath = Path.GetDirectoryName(rawFileName);

                // Lấy về tên file Dicom từ file raw
                string dicomFileName = string.Format("{0}{1}{2}.DCM", dicomPath, Path.DirectorySeparatorChar,
                                                     Path.GetFileNameWithoutExtension(rawFileName));

                try2RenameExistedFile(dicomFileName,ref newDCMFile);


                //FileStream fs = File.OpenRead(rawFileName);

                //long length = fs.Length;
                long dataLength = pixelData.Length;

                string col = imgWidth.ToString();
                string row = imgHeigh.ToString();
                // GetSize(dataLength, out col, out row);

                // Tạo File Dicom để lưu thông tin
                var dcmFile = new DicomFile(dicomFileName);
                DicomAttributeCollection dicomDataSet = dcmFile.DataSet;

                //Set Tag For File
                DateTime studyTime = DateTime.Now;
                dicomDataSet[DicomTags.SpecificCharacterSet].SetStringValue("ISO_IR 100");
                dicomDataSet[DicomTags.ImageType].SetStringValue("ORIGINAL\\PRIMARY\\OTHER\\M\\FFE");
                dicomDataSet[DicomTags.InstanceCreationDate].SetStringValue(DateParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.InstanceCreationTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.SopClassUid].SetStringValue(SopClass.MrImageStorageUid);
                dicomDataSet[DicomTags.SopInstanceUid].SetStringValue(SOPInstanceUID);
                dicomDataSet[DicomTags.StudyDate].SetStringValue(regDate);
                dicomDataSet[DicomTags.ApplicationName].SetStringValue(AppName);
                dicomDataSet[DicomTags.StudyTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.SeriesDate].SetStringValue(regDate);
                dicomDataSet[DicomTags.SeriesTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.AccessionNumber].SetStringValue(regNum);
                dicomDataSet[DicomTags.Modality].SetStringValue(modalityCode);
                dicomDataSet[DicomTags.Manufacturer].SetStringValue("VBIT");
                dicomDataSet[DicomTags.ManufacturersModelName].SetNullValue();
                dicomDataSet[DicomTags.InstitutionName].SetStringValue(hostpitalName);
                dicomDataSet[DicomTags.InstitutionalDepartmentName].SetStringValue(departmentName);
                //dicomDataSet[DicomTags.StudyDescription].SetStringValue("HEART");
                //dicomDataSet[DicomTags.SeriesDescription].SetStringValue("Heart 2D EPI BH TRA");
                //dicomDataSet[DicomTags.PatientsName].SetStringValue("Patient^Test");
                dicomDataSet[DicomTags.PatientsName].SetStringValue(patientName);
                dicomDataSet[DicomTags.PatientId].SetStringValue(pid);
                dicomDataSet[DicomTags.PatientsBirthDate].SetStringValue(patientBirthdate);
                dicomDataSet[DicomTags.PatientsSex].SetStringValue(patientSex);
                dicomDataSet[DicomTags.PatientsAge].SetStringValue(patientAge);
                dicomDataSet[DicomTags.PatientsWeight].SetStringValue("70");
                dicomDataSet[DicomTags.SequenceVariant].SetStringValue("OTHER");
                dicomDataSet[DicomTags.ScanOptions].SetStringValue("CG");
                dicomDataSet[DicomTags.MrAcquisitionType].SetStringValue("2D");
                dicomDataSet[DicomTags.SliceThickness].SetStringValue("2");
                dicomDataSet[DicomTags.RepetitionTime].SetStringValue("857.142883");
                dicomDataSet[DicomTags.EchoTime].SetStringValue("8.712100");
                dicomDataSet[DicomTags.NumberOfAverages].SetStringValue("1");
                dicomDataSet[DicomTags.ImagingFrequency].SetStringValue("63.901150");
                dicomDataSet[DicomTags.ImagedNucleus].SetStringValue("1H");
                dicomDataSet[DicomTags.EchoNumbers].SetStringValue("1");
                dicomDataSet[DicomTags.MagneticFieldStrength].SetStringValue("1.500000");
                dicomDataSet[DicomTags.SpacingBetweenSlices].SetStringValue("10.00000");
                dicomDataSet[DicomTags.NumberOfPhaseEncodingSteps].SetStringValue("81");
                dicomDataSet[DicomTags.EchoTrainLength].SetStringValue("0");
                dicomDataSet[DicomTags.PercentSampling].SetStringValue("63.281250");
                dicomDataSet[DicomTags.PercentPhaseFieldOfView].SetStringValue("68.75000");
                dicomDataSet[DicomTags.DeviceSerialNumber].SetStringValue("1234");
                dicomDataSet[DicomTags.SoftwareVersions].SetStringValue("V1.0");
                dicomDataSet[DicomTags.ProtocolName].SetStringValue("2D EPI BH");
                dicomDataSet[DicomTags.TriggerTime].SetStringValue("14.000000");
                dicomDataSet[DicomTags.LowRRValue].SetStringValue("948");
                dicomDataSet[DicomTags.HighRRValue].SetStringValue("1178");
                dicomDataSet[DicomTags.IntervalsAcquired].SetStringValue("102");
                dicomDataSet[DicomTags.IntervalsRejected].SetStringValue("0");
                dicomDataSet[DicomTags.HeartRate].SetStringValue("56");
                dicomDataSet[DicomTags.ReceiveCoilName].SetStringValue("B");
                dicomDataSet[DicomTags.TransmitCoilName].SetStringValue("B");
                dicomDataSet[DicomTags.InPlanePhaseEncodingDirection].SetStringValue("COL");
                dicomDataSet[DicomTags.FlipAngle].SetStringValue("50.000000");
                dicomDataSet[DicomTags.PatientPosition].SetStringValue(atonomyCode);
                dicomDataSet[DicomTags.StudyInstanceUid].SetStringValue(StudyInstanceUID);//DicomUid.GenerateUid().UID
                dicomDataSet[DicomTags.SeriesInstanceUid].SetStringValue(SeriesInstanceUID);//DicomUid.GenerateUid().UID
                //dicomDataSet[DicomTags.StudyId].SetStringValue(pid);
                dicomDataSet[DicomTags.ExposureInMas].SetStringValue(_MAS);
                dicomDataSet[DicomTags.Kvp].SetStringValue(_KVP);
                dicomDataSet[DicomTags.SeriesNumber].SetStringValue("1");
                dicomDataSet[DicomTags.AcquisitionNumber].SetStringValue("7");
                dicomDataSet[DicomTags.InstanceNumber].SetStringValue("1");
                dicomDataSet[DicomTags.ImagePositionPatient].SetStringValue("-61.7564\\-212.04848\\-99.6208");
                dicomDataSet[DicomTags.ImageOrientationPatient].SetStringValue("0.861\\0.492\\0.126\\-0.2965");
                dicomDataSet[DicomTags.FrameOfReferenceUid].SetStringValue(DicomUid.GenerateUid().UID);
                dicomDataSet[DicomTags.PositionReferenceIndicator].SetStringValue(null);
                //dicomDataSet[DicomTags.ImageComments].SetStringValue("Test MR Image");
                dicomDataSet[DicomTags.SamplesPerPixel].SetStringValue("1");
                dicomDataSet[DicomTags.PhotometricInterpretation].SetStringValue(MonoChrome);
                dicomDataSet[DicomTags.Rows].SetStringValue(row);
                dicomDataSet[DicomTags.Columns].SetStringValue(col);

                dicomDataSet[DicomTags.PixelSpacing].SetStringValue("0.168\\0.168");
                dicomDataSet[DicomTags.BitsAllocated].SetStringValue(_BitsAllocated.ToString());
                dicomDataSet[DicomTags.BitsStored].SetStringValue(_bitsStored.ToString());
                dicomDataSet[DicomTags.HighBit].SetStringValue(_HightBit.ToString());
                dicomDataSet[DicomTags.PixelRepresentation].SetStringValue("0");
                if (File.Exists(AutoWLPath))
                {
                    switch (_bitsStored)
                    {
                        case 12:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("2048");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("4095");

                            break;

                        case 14:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("8192");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("16383");

                            break;

                        case 16:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("32767");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("65535");
                            break;

                        default:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("32767");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("65535");
                            break;
                    }

                }

                //dicomDataSet[DicomTags.SmallestImagePixelValue].SetStringValue("32772");
                //dicomDataSet[DicomTags.LargestImagePixelValue].SetStringValue("47745");
                dicomDataSet[DicomTags.RescaleIntercept].SetStringValue("0.");
                dicomDataSet[DicomTags.RescaleSlope].SetStringValue("1.");


                dicomDataSet[DicomTags.ViewPosition].SetStringValue(projectionCode);
                dicomDataSet[DicomTags.BodyPartExamined].SetStringValue(atonomyCode);


                //Gán Dữ liệu ảnh
                var pixels = new DicomAttributeOW(DicomTags.PixelData) { Values = pixelData };
                dicomDataSet[DicomTags.PixelData] = pixels;

                var item = new DicomSequenceItem();
                dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);
                item[DicomTags.RequestedProcedureId].SetStringValue("MRR1234");
                item[DicomTags.ScheduledProcedureStepId].SetStringValue("MRS1234");

                item = new DicomSequenceItem();
                dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);

                item[DicomTags.RequestedProcedureId].SetStringValue("MR2R1234");
                item[DicomTags.ScheduledProcedureStepId].SetStringValue("MR2S1234");

                var studyItem = new DicomSequenceItem();

                item[DicomTags.ReferencedStudySequence].AddSequenceItem(studyItem);

                studyItem[DicomTags.ReferencedSopClassUid].SetStringValue(SopClass.MrImageStorageUid);
                studyItem[DicomTags.ReferencedSopInstanceUid].SetStringValue("1.2.3.4.5.6.7.8.9");

                //Set Meta Info
                dicomDataSet[DicomTags.MediaStorageSopClassUid].SetStringValue(
                    dcmFile.DataSet[DicomTags.SopClassUid].GetString(0, ""));
                dicomDataSet[DicomTags.MediaStorageSopInstanceUid].SetStringValue(
                    dcmFile.DataSet[DicomTags.SopInstanceUid].GetString(0, ""));

                dcmFile.TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;

                dicomDataSet[DicomTags.ImplementationClassUid].SetStringValue("1.1.1.1.1.11.1");
                dicomDataSet[DicomTags.ImplementationVersionName].SetStringValue("DICOM 1.0");

                // Lưu File
                dcmFile.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool Convert2Dicom_v1(byte[] pixelData, string rawFileName, DataRow patientInfo, string AutoWLPath,bool overiddefile,ref string errMsg)
        {
            try
            {
                const string colPid = "PID";
                const string colKVP = "KVP";
                const string colMAS = "MAS";
                const string colDepartmentName = "Department_Name";
                const string colPatientName = "Patient_Name";
                const string colPatientSex = "Patient_Sex";
                const string colPatientAge = "Patient_Age";
                const string colPatientBirthdate = "Patient_Birthdate";
                const string colRegDate = "Reg_Date";
                const string colRegNum = "Reg_Num";
                const string colMonoChrome = "MonoChrome";
                const string colImgWidth = "IMGWidth";
                const string colImgHeight = "IMGHeigh";
                const string colModalityCode = "Modality_Code";
                const string colAtonomyCode = "Atonomy_Code";
                const string colProjectionCode = "Projection_Code";
                const string colHostpitalName = "Hostpital_Name";


                const string _colStudyInstanceUID = "StudyInstanceUID";
                const string colSeriesInstanceUID = "SeriesInstanceUID";
                const string colSOPInstanceUID = "SOPInstanceUID";
                const string colAcqDate = "AcqDate";
                const string colAppName = "AppName";

                const string colBitsStored = "BitsStored";
                const string colHightBit = "HightBit";
                const string colBitsAllocated = "BitsAllocated";

                _bitsStored = TryGetBitsStored(patientInfo, colBitsStored);
                var _HightBit = TryGetBitsStored(patientInfo, colHightBit);
                var _BitsAllocated = TryGetBitsStored(patientInfo, colBitsAllocated);

                var MonoChrome = TryGetString(patientInfo, colMonoChrome, "MONOCHROME2");
                var _KVP = TryGetString(patientInfo, colKVP);
                var _MAS = TryGetString(patientInfo, colMAS);
                var pid = TryGetString(patientInfo, colPid);
                var patientName = TryGetString(patientInfo, colPatientName);
                var patientSex = TryGetString(patientInfo, colPatientSex);
                var patientAge = TryGetString(patientInfo, colPatientAge);
                var patientBirthdate = TryGetString(patientInfo, colPatientBirthdate);
                var regDate = TryGetString(patientInfo, colRegDate);
                var regNum = TryGetString(patientInfo, colRegNum);
                var imgWidth = TryGetString(patientInfo, colImgWidth);
                var imgHeigh = TryGetString(patientInfo, colImgHeight);
                var modalityCode = TryGetString(patientInfo, colModalityCode);
                var atonomyCode = TryGetString(patientInfo, colAtonomyCode);
                var projectionCode = TryGetString(patientInfo, colProjectionCode);
                var hostpitalName = TryGetString(patientInfo, colHostpitalName, "BACH MAI HOSTPITAL");
                var departmentName = TryGetString(patientInfo, colDepartmentName, "Khoa chan doan hinh anh");
                string defaultStudyInstanceUID = ClearCanvas.Dicom.DicomUid.GenerateUid().UID;
                string defaultSeriesInstanceUID = defaultStudyInstanceUID + ".1";
                string defaultSOPInstanceUID = defaultSeriesInstanceUID + ".1";
                var StudyInstanceUID = TryGetString(patientInfo, _colStudyInstanceUID, defaultStudyInstanceUID);
                var SeriesInstanceUID = TryGetString(patientInfo, colSeriesInstanceUID, defaultSeriesInstanceUID);
                var SOPInstanceUID = TryGetString(patientInfo, colSOPInstanceUID, defaultSOPInstanceUID);
                var AppName = TryGetString(patientInfo, colAppName, "VBIT");

                string dicomPath = Path.GetDirectoryName(rawFileName);

                // Lấy về tên file Dicom từ file raw
                string dicomFileName = string.Format("{0}{1}{2}.DCM", dicomPath, Path.DirectorySeparatorChar,
                                                     Path.GetFileNameWithoutExtension(rawFileName));

                Try2DelFile(dicomFileName);


                //FileStream fs = File.OpenRead(rawFileName);

                //long length = fs.Length;
                long dataLength = pixelData.Length;

                string col = imgWidth.ToString();
                string row = imgHeigh.ToString();
                // GetSize(dataLength, out col, out row);

                // Tạo File Dicom để lưu thông tin
                var dcmFile = new DicomFile(dicomFileName);
                DicomAttributeCollection dicomDataSet = dcmFile.DataSet;

                //Set Tag For File
                DateTime studyTime = DateTime.Now;
                dicomDataSet[DicomTags.StationName].SetStringValue(AppName);
                dicomDataSet[DicomTags.SpecificCharacterSet].SetStringValue("ISO_IR 100");
                dicomDataSet[DicomTags.ImageType].SetStringValue("ORIGINAL\\PRIMARY\\OTHER\\M\\FFE");
                dicomDataSet[DicomTags.InstanceCreationDate].SetStringValue(DateParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.InstanceCreationTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.SopClassUid].SetStringValue(SopClass.MrImageStorageUid);
                dicomDataSet[DicomTags.SopInstanceUid].SetStringValue(SOPInstanceUID);
                dicomDataSet[DicomTags.StudyDate].SetStringValue(regDate);
                dicomDataSet[DicomTags.ApplicationName].SetStringValue(AppName);
                dicomDataSet[DicomTags.StudyTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.SeriesDate].SetStringValue(regDate);
                dicomDataSet[DicomTags.SeriesTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.AccessionNumber].SetStringValue(regNum);
                dicomDataSet[DicomTags.Modality].SetStringValue(modalityCode);
                dicomDataSet[DicomTags.Manufacturer].SetStringValue(AppName);
                dicomDataSet[DicomTags.ManufacturersModelName].SetNullValue();
                dicomDataSet[DicomTags.InstitutionName].SetStringValue(hostpitalName);
                dicomDataSet[DicomTags.InstitutionalDepartmentName].SetStringValue(departmentName);
                //dicomDataSet[DicomTags.StudyDescription].SetStringValue("HEART");
                //dicomDataSet[DicomTags.SeriesDescription].SetStringValue("Heart 2D EPI BH TRA");
                //dicomDataSet[DicomTags.PatientsName].SetStringValue("Patient^Test");
                dicomDataSet[DicomTags.PatientsName].SetStringValue(patientName);
                dicomDataSet[DicomTags.PatientId].SetStringValue(pid);
                dicomDataSet[DicomTags.PatientsBirthDate].SetStringValue(patientBirthdate);
                dicomDataSet[DicomTags.PatientsSex].SetStringValue(patientSex);
                dicomDataSet[DicomTags.PatientsAge].SetStringValue(patientAge);
                dicomDataSet[DicomTags.PatientsWeight].SetStringValue("70");
                dicomDataSet[DicomTags.SequenceVariant].SetStringValue("OTHER");
                dicomDataSet[DicomTags.ScanOptions].SetStringValue("CG");
                dicomDataSet[DicomTags.MrAcquisitionType].SetStringValue("2D");
                dicomDataSet[DicomTags.SliceThickness].SetStringValue("2");
                dicomDataSet[DicomTags.RepetitionTime].SetStringValue("857.142883");
                dicomDataSet[DicomTags.EchoTime].SetStringValue("8.712100");
                dicomDataSet[DicomTags.NumberOfAverages].SetStringValue("1");
                dicomDataSet[DicomTags.ImagingFrequency].SetStringValue("63.901150");
                dicomDataSet[DicomTags.ImagedNucleus].SetStringValue("1H");
                dicomDataSet[DicomTags.EchoNumbers].SetStringValue("1");
                dicomDataSet[DicomTags.MagneticFieldStrength].SetStringValue("1.500000");
                dicomDataSet[DicomTags.SpacingBetweenSlices].SetStringValue("10.00000");
                dicomDataSet[DicomTags.NumberOfPhaseEncodingSteps].SetStringValue("81");
                dicomDataSet[DicomTags.EchoTrainLength].SetStringValue("0");
                dicomDataSet[DicomTags.PercentSampling].SetStringValue("63.281250");
                dicomDataSet[DicomTags.PercentPhaseFieldOfView].SetStringValue("68.75000");
                dicomDataSet[DicomTags.DeviceSerialNumber].SetStringValue("1234");
                dicomDataSet[DicomTags.SoftwareVersions].SetStringValue("V1.0");
                dicomDataSet[DicomTags.ProtocolName].SetStringValue("2D EPI BH");
                dicomDataSet[DicomTags.TriggerTime].SetStringValue("14.000000");
                dicomDataSet[DicomTags.LowRRValue].SetStringValue("948");
                dicomDataSet[DicomTags.HighRRValue].SetStringValue("1178");
                dicomDataSet[DicomTags.IntervalsAcquired].SetStringValue("102");
                dicomDataSet[DicomTags.IntervalsRejected].SetStringValue("0");
                dicomDataSet[DicomTags.HeartRate].SetStringValue("56");
                dicomDataSet[DicomTags.ReceiveCoilName].SetStringValue("B");
                dicomDataSet[DicomTags.TransmitCoilName].SetStringValue("B");
                dicomDataSet[DicomTags.InPlanePhaseEncodingDirection].SetStringValue("COL");
                dicomDataSet[DicomTags.FlipAngle].SetStringValue("50.000000");
                dicomDataSet[DicomTags.PatientPosition].SetStringValue(atonomyCode);
                dicomDataSet[DicomTags.StudyInstanceUid].SetStringValue(StudyInstanceUID);//DicomUid.GenerateUid().UID
                dicomDataSet[DicomTags.SeriesInstanceUid].SetStringValue(SeriesInstanceUID);//DicomUid.GenerateUid().UID
                //dicomDataSet[DicomTags.StudyId].SetStringValue(pid);
                dicomDataSet[DicomTags.ExposureInMas].SetStringValue(_MAS);
                dicomDataSet[DicomTags.Kvp].SetStringValue(_KVP);
                dicomDataSet[DicomTags.SeriesNumber].SetStringValue("1");
                dicomDataSet[DicomTags.AcquisitionNumber].SetStringValue("7");
                dicomDataSet[DicomTags.InstanceNumber].SetStringValue("1");
                dicomDataSet[DicomTags.ImagePositionPatient].SetStringValue("-61.7564\\-212.04848\\-99.6208");
                dicomDataSet[DicomTags.ImageOrientationPatient].SetStringValue("0.861\\0.492\\0.126\\-0.2965");
                dicomDataSet[DicomTags.FrameOfReferenceUid].SetStringValue(DicomUid.GenerateUid().UID);
                dicomDataSet[DicomTags.PositionReferenceIndicator].SetStringValue(null);
                //dicomDataSet[DicomTags.ImageComments].SetStringValue("Test MR Image");
                dicomDataSet[DicomTags.SamplesPerPixel].SetStringValue("1");
                dicomDataSet[DicomTags.PhotometricInterpretation].SetStringValue(MonoChrome);
                dicomDataSet[DicomTags.Rows].SetStringValue(row);
                dicomDataSet[DicomTags.Columns].SetStringValue(col);

                dicomDataSet[DicomTags.PixelSpacing].SetStringValue("0.168\\0.168");
                dicomDataSet[DicomTags.BitsAllocated].SetStringValue(_BitsAllocated.ToString());
                dicomDataSet[DicomTags.BitsStored].SetStringValue(_bitsStored.ToString());
                dicomDataSet[DicomTags.HighBit].SetStringValue(_HightBit.ToString());
                dicomDataSet[DicomTags.PixelRepresentation].SetStringValue("0");
                if (File.Exists(AutoWLPath))
                {
                    switch (_bitsStored)
                    {
                        case 12:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("2048");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("4095");

                            break;

                        case 14:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("8192");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("16383");

                            break;

                        case 16:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("32767");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("65535");
                            break;

                        default:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("32767");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("65535");
                            break;
                    }

                }

                //dicomDataSet[DicomTags.SmallestImagePixelValue].SetStringValue("32772");
                //dicomDataSet[DicomTags.LargestImagePixelValue].SetStringValue("47745");
                dicomDataSet[DicomTags.RescaleIntercept].SetStringValue("0.");
                dicomDataSet[DicomTags.RescaleSlope].SetStringValue("1.");


                dicomDataSet[DicomTags.ViewPosition].SetStringValue(projectionCode);
                dicomDataSet[DicomTags.BodyPartExamined].SetStringValue(atonomyCode);


                //Gán Dữ liệu ảnh
                var pixels = new DicomAttributeOW(DicomTags.PixelData) { Values = pixelData };
                dicomDataSet[DicomTags.PixelData] = pixels;

                var item = new DicomSequenceItem();
                dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);
                item[DicomTags.RequestedProcedureId].SetStringValue("MRR1234");
                item[DicomTags.ScheduledProcedureStepId].SetStringValue("MRS1234");

                item = new DicomSequenceItem();
                dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);

                item[DicomTags.RequestedProcedureId].SetStringValue("MR2R1234");
                item[DicomTags.ScheduledProcedureStepId].SetStringValue("MR2S1234");

                var studyItem = new DicomSequenceItem();

                item[DicomTags.ReferencedStudySequence].AddSequenceItem(studyItem);

                studyItem[DicomTags.ReferencedSopClassUid].SetStringValue(SopClass.MrImageStorageUid);
                studyItem[DicomTags.ReferencedSopInstanceUid].SetStringValue("1.2.3.4.5.6.7.8.9");

                //Set Meta Info
                dicomDataSet[DicomTags.MediaStorageSopClassUid].SetStringValue(
                    dcmFile.DataSet[DicomTags.SopClassUid].GetString(0, ""));
                dicomDataSet[DicomTags.MediaStorageSopInstanceUid].SetStringValue(
                    dcmFile.DataSet[DicomTags.SopInstanceUid].GetString(0, ""));

                dcmFile.TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;

                dicomDataSet[DicomTags.ImplementationClassUid].SetStringValue("1.1.1.1.1.11.1");
                dicomDataSet[DicomTags.ImplementationVersionName].SetStringValue("DICOM 1.0");

                // Lưu File
                dcmFile.Save();
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
       static void Try2DelFile(string file)
        {
            try
            {
                if (File.Exists(file)) File.Delete(file);
            }
            catch
            {
            }
        }
       public static bool Convert2Dicom(byte[] pixelData, string rawFileName, DataRow patientInfo, bool delbeforeSave, string AutoWLPath)
        {
            try
            {
                const string colPid = "PID";
                const string colKVP = "KVP";
                const string colMAS = "MAS";
                const string colDepartmentName = "Department_Name";
                const string colPatientName = "Patient_Name";
                const string colPatientSex = "Patient_Sex";
                const string colPatientAge = "Patient_Age";
                const string colPatientBirthdate = "Patient_Birthdate";
                const string colRegDate = "Reg_Date";
                const string colRegNum = "Reg_Num";
                const string colImgWidth = "IMGWidth";
                const string colImgHeight = "IMGHeigh";
                const string colModalityCode = "Modality_Code";
                const string colAtonomyCode = "Atonomy_Code";
                const string colProjectionCode = "Projection_Code";
                const string colHostpitalName = "Hostpital_Name";
                const string colMonoChrome = "MonoChrome";

                const string _colStudyInstanceUID = "StudyInstanceUID";
                const string colSeriesInstanceUID = "SeriesInstanceUID";
                const string colSOPInstanceUID = "SOPInstanceUID";
                const string colAcqDate = "AcqDate";
                const string colAppName = "AppName";

                const string colBitsStored = "BitsStored";
                const string colHightBit = "HightBit";
                const string colBitsAllocated = "BitsAllocated";

                _bitsStored = TryGetBitsStored(patientInfo, colBitsStored);
                var _HightBit = TryGetBitsStored(patientInfo, colHightBit);
                var _BitsAllocated = TryGetBitsStored(patientInfo, colBitsAllocated);

                var MonoChrome = TryGetString(patientInfo, colMonoChrome, "MONOCHROME2");
                
                var pid = TryGetString(patientInfo, colPid);
                var _KVP = TryGetString(patientInfo, colKVP);
                var _MAS = TryGetString(patientInfo, colMAS);
                var patientName = TryGetString(patientInfo, colPatientName);
                var patientSex = TryGetString(patientInfo, colPatientSex);
                var patientAge = TryGetString(patientInfo, colPatientAge);
                var patientBirthdate = TryGetString(patientInfo, colPatientBirthdate);
                var regDate = TryGetString(patientInfo, colRegDate);
                var regNum = TryGetString(patientInfo, colRegNum);
                var imgWidth = TryGetString(patientInfo, colImgWidth);
                var imgHeigh = TryGetString(patientInfo, colImgHeight);
                var modalityCode = TryGetString(patientInfo, colModalityCode);
                var atonomyCode = TryGetString(patientInfo, colAtonomyCode);
                var projectionCode = TryGetString(patientInfo, colProjectionCode);
                var hostpitalName = TryGetString(patientInfo, colHostpitalName, "BACH MAI HOSTPITAL");
                var departmentName = TryGetString(patientInfo, colDepartmentName, "Khoa chan doan hinh anh");
                string defaultStudyInstanceUID = ClearCanvas.Dicom.DicomUid.GenerateUid().UID;
                string defaultSeriesInstanceUID = defaultStudyInstanceUID + ".1";
                string defaultSOPInstanceUID = defaultSeriesInstanceUID + ".1";
                var StudyInstanceUID = TryGetString(patientInfo, _colStudyInstanceUID, defaultStudyInstanceUID);
                var SeriesInstanceUID = TryGetString(patientInfo, colSeriesInstanceUID, defaultSeriesInstanceUID);
                var SOPInstanceUID = TryGetString(patientInfo, colSOPInstanceUID, defaultSOPInstanceUID);
                var AppName = TryGetString(patientInfo, colAppName, "VBIT");
                string dicomPath = Path.GetDirectoryName(rawFileName);

                // Lấy về tên file Dicom từ file raw
                string dicomFileName = string.Format("{0}{1}{2}.DCM", dicomPath, Path.DirectorySeparatorChar,
                                                     Path.GetFileNameWithoutExtension(rawFileName));

                if (delbeforeSave && File.Exists(dicomFileName)) Try2DelFile(dicomFileName);
                else
                    try2RenameExistedFile(dicomFileName);
                


                //FileStream fs = File.OpenRead(rawFileName);

                //long length = fs.Length;
                long dataLength = pixelData.Length;

                string col = imgWidth.ToString();
                string row = imgHeigh.ToString();
                // GetSize(dataLength, out col, out row);

                // Tạo File Dicom để lưu thông tin
                var dcmFile = new DicomFile(dicomFileName);
                DicomAttributeCollection dicomDataSet = dcmFile.DataSet;

                //Set Tag For File
                DateTime studyTime = DateTime.Now;
                dicomDataSet[DicomTags.SpecificCharacterSet].SetStringValue("ISO_IR 100");
                dicomDataSet[DicomTags.ImageType].SetStringValue("ORIGINAL\\PRIMARY\\OTHER\\M\\FFE");
                dicomDataSet[DicomTags.InstanceCreationDate].SetStringValue(DateParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.InstanceCreationTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.SopClassUid].SetStringValue(SopClass.MrImageStorageUid);
                dicomDataSet[DicomTags.SopInstanceUid].SetStringValue(SOPInstanceUID);
                dicomDataSet[DicomTags.StudyDate].SetStringValue(regDate);
                dicomDataSet[DicomTags.ApplicationName].SetStringValue(AppName);
                dicomDataSet[DicomTags.StudyTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.SeriesDate].SetStringValue(regDate);
                dicomDataSet[DicomTags.SeriesTime].SetStringValue(TimeParser.ToDicomString(studyTime));
                dicomDataSet[DicomTags.AccessionNumber].SetStringValue(regNum);
                dicomDataSet[DicomTags.Modality].SetStringValue(modalityCode);
                dicomDataSet[DicomTags.Manufacturer].SetStringValue("VBIT");
                dicomDataSet[DicomTags.StationName].SetStringValue(AppName);
                dicomDataSet[DicomTags.ExposureInMas].SetStringValue(_MAS);
                dicomDataSet[DicomTags.Kvp].SetStringValue(_KVP);
                dicomDataSet[DicomTags.ManufacturersModelName].SetNullValue();
                dicomDataSet[DicomTags.InstitutionName].SetStringValue(hostpitalName);
                dicomDataSet[DicomTags.InstitutionalDepartmentName].SetStringValue(departmentName);
                //dicomDataSet[DicomTags.StudyDescription].SetStringValue("HEART");
                //dicomDataSet[DicomTags.SeriesDescription].SetStringValue("Heart 2D EPI BH TRA");
                //dicomDataSet[DicomTags.PatientsName].SetStringValue("Patient^Test");
                dicomDataSet[DicomTags.PatientsName].SetStringValue(patientName);
                dicomDataSet[DicomTags.PatientId].SetStringValue(pid);
                dicomDataSet[DicomTags.PatientsBirthDate].SetStringValue(patientBirthdate);
                dicomDataSet[DicomTags.PatientsSex].SetStringValue(patientSex);
                dicomDataSet[DicomTags.PatientsAge].SetStringValue(patientAge);
                dicomDataSet[DicomTags.PatientsWeight].SetStringValue("70");
                dicomDataSet[DicomTags.SequenceVariant].SetStringValue("OTHER");
                dicomDataSet[DicomTags.ScanOptions].SetStringValue("CG");
                dicomDataSet[DicomTags.MrAcquisitionType].SetStringValue("2D");
                dicomDataSet[DicomTags.SliceThickness].SetStringValue("2");
                dicomDataSet[DicomTags.RepetitionTime].SetStringValue("857.142883");
                dicomDataSet[DicomTags.EchoTime].SetStringValue("8.712100");
                dicomDataSet[DicomTags.NumberOfAverages].SetStringValue("1");
                dicomDataSet[DicomTags.ImagingFrequency].SetStringValue("63.901150");
                dicomDataSet[DicomTags.ImagedNucleus].SetStringValue("1H");
                dicomDataSet[DicomTags.EchoNumbers].SetStringValue("1");
                dicomDataSet[DicomTags.MagneticFieldStrength].SetStringValue("1.500000");
                dicomDataSet[DicomTags.SpacingBetweenSlices].SetStringValue("10.00000");
                dicomDataSet[DicomTags.NumberOfPhaseEncodingSteps].SetStringValue("81");
                dicomDataSet[DicomTags.EchoTrainLength].SetStringValue("0");
                dicomDataSet[DicomTags.PercentSampling].SetStringValue("63.281250");
                dicomDataSet[DicomTags.PercentPhaseFieldOfView].SetStringValue("68.75000");
                dicomDataSet[DicomTags.DeviceSerialNumber].SetStringValue("1234");
                dicomDataSet[DicomTags.SoftwareVersions].SetStringValue("V1.0");
                dicomDataSet[DicomTags.ProtocolName].SetStringValue("2D EPI BH");
                dicomDataSet[DicomTags.TriggerTime].SetStringValue("14.000000");
                dicomDataSet[DicomTags.LowRRValue].SetStringValue("948");
                dicomDataSet[DicomTags.HighRRValue].SetStringValue("1178");
                dicomDataSet[DicomTags.IntervalsAcquired].SetStringValue("102");
                dicomDataSet[DicomTags.IntervalsRejected].SetStringValue("0");
                dicomDataSet[DicomTags.HeartRate].SetStringValue("56");
                dicomDataSet[DicomTags.ReceiveCoilName].SetStringValue("B");
                dicomDataSet[DicomTags.TransmitCoilName].SetStringValue("B");
                dicomDataSet[DicomTags.InPlanePhaseEncodingDirection].SetStringValue("COL");
                dicomDataSet[DicomTags.FlipAngle].SetStringValue("50.000000");
                dicomDataSet[DicomTags.PatientPosition].SetStringValue("HFS");
                dicomDataSet[DicomTags.StudyInstanceUid].SetStringValue(StudyInstanceUID);
                dicomDataSet[DicomTags.SeriesInstanceUid].SetStringValue(SeriesInstanceUID);
                //dicomDataSet[DicomTags.StudyId].SetStringValue(pid);
                dicomDataSet[DicomTags.SeriesNumber].SetStringValue("1");
                dicomDataSet[DicomTags.AcquisitionNumber].SetStringValue("7");
                dicomDataSet[DicomTags.InstanceNumber].SetStringValue("1");
                dicomDataSet[DicomTags.ImagePositionPatient].SetStringValue("-61.7564\\-212.04848\\-99.6208");
                dicomDataSet[DicomTags.ImageOrientationPatient].SetStringValue("0.861\\0.492\\0.126\\-0.2965");
                dicomDataSet[DicomTags.FrameOfReferenceUid].SetStringValue(DicomUid.GenerateUid().UID);
                dicomDataSet[DicomTags.PositionReferenceIndicator].SetStringValue(null);
                //dicomDataSet[DicomTags.ImageComments].SetStringValue("Test MR Image");
                dicomDataSet[DicomTags.SamplesPerPixel].SetStringValue("1");
                dicomDataSet[DicomTags.PhotometricInterpretation].SetStringValue(MonoChrome);
                dicomDataSet[DicomTags.Rows].SetStringValue(row);
                dicomDataSet[DicomTags.Columns].SetStringValue(col);

                dicomDataSet[DicomTags.PixelSpacing].SetStringValue("0.168\\0.168");
                dicomDataSet[DicomTags.BitsAllocated].SetStringValue(_BitsAllocated.ToString());
                dicomDataSet[DicomTags.BitsStored].SetStringValue(_bitsStored.ToString());
                dicomDataSet[DicomTags.HighBit].SetStringValue(_HightBit.ToString());
                dicomDataSet[DicomTags.PixelRepresentation].SetStringValue("0");

                if (File.Exists(AutoWLPath))
                {
                    switch (_bitsStored)
                    {
                        case 12:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("2048");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("4095");

                            break;

                        case 14:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("8192");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("16383");

                            break;

                        case 16:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("32767");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("65535");
                            break;

                        default:
                            dicomDataSet[DicomTags.WindowCenter].SetStringValue("32767");
                            dicomDataSet[DicomTags.WindowWidth].SetStringValue("65535");
                            break;
                    }

                }

                //dicomDataSet[DicomTags.SmallestImagePixelValue].SetStringValue("32772");
                //dicomDataSet[DicomTags.LargestImagePixelValue].SetStringValue("47745");
                dicomDataSet[DicomTags.RescaleIntercept].SetStringValue("0.");
                dicomDataSet[DicomTags.RescaleSlope].SetStringValue("1.");


                dicomDataSet[DicomTags.ViewPosition].SetStringValue(projectionCode);
                dicomDataSet[DicomTags.BodyPartExamined].SetStringValue(atonomyCode);


                //Gán Dữ liệu ảnh
                var pixels = new DicomAttributeOW(DicomTags.PixelData) { Values = pixelData };
                dicomDataSet[DicomTags.PixelData] = pixels;

                var item = new DicomSequenceItem();
                dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);
                item[DicomTags.RequestedProcedureId].SetStringValue("MRR1234");
                item[DicomTags.ScheduledProcedureStepId].SetStringValue("MRS1234");

                item = new DicomSequenceItem();
                dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);

                item[DicomTags.RequestedProcedureId].SetStringValue("MR2R1234");
                item[DicomTags.ScheduledProcedureStepId].SetStringValue("MR2S1234");

                var studyItem = new DicomSequenceItem();

                item[DicomTags.ReferencedStudySequence].AddSequenceItem(studyItem);

                studyItem[DicomTags.ReferencedSopClassUid].SetStringValue(SopClass.MrImageStorageUid);
                studyItem[DicomTags.ReferencedSopInstanceUid].SetStringValue("1.2.3.4.5.6.7.8.9");

                //Set Meta Info
                dicomDataSet[DicomTags.MediaStorageSopClassUid].SetStringValue(
                    dcmFile.DataSet[DicomTags.SopClassUid].GetString(0, ""));
                dicomDataSet[DicomTags.MediaStorageSopInstanceUid].SetStringValue(
                    dcmFile.DataSet[DicomTags.SopInstanceUid].GetString(0, ""));

                dcmFile.TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;

                dicomDataSet[DicomTags.ImplementationClassUid].SetStringValue("1.1.1.1.1.11.1");
                dicomDataSet[DicomTags.ImplementationVersionName].SetStringValue("droc 1.0");

                // Lưu File
                dcmFile.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       public static bool Convert2Dicom(byte[] pixelData, string rawFileName, DataRow patientInfo,int WW,int WC, bool delbeforeSave)
       {
           try
           {
               const string colMonoChrome = "MonoChrome";
               const string colPid = "PID";
               const string colKVP = "KVP";
               const string colMAS = "MAS";
               const string colDepartmentName = "Department_Name";
               const string colPatientName = "Patient_Name";
               const string colPatientSex = "Patient_Sex";
               const string colPatientAge = "Patient_Age";
               const string colPatientBirthdate = "Patient_Birthdate";
               const string colRegDate = "Reg_Date";
               const string colRegNum = "Reg_Num";
               const string colImgWidth = "IMGWidth";
               const string colImgHeight = "IMGHeigh";
               const string colModalityCode = "Modality_Code";
               const string colAtonomyCode = "Atonomy_Code";
               const string colProjectionCode = "Projection_Code";
               const string colHostpitalName = "Hostpital_Name";
              

               const string _colStudyInstanceUID = "StudyInstanceUID";
               const string colSeriesInstanceUID = "SeriesInstanceUID";
               const string colSOPInstanceUID = "SOPInstanceUID";
               const string colAcqDate = "AcqDate";
               const string colAppName = "AppName";

               const string colBitsStored = "BitsStored";
               const string colHightBit = "HightBit";
               const string colBitsAllocated = "BitsAllocated";

               _bitsStored = TryGetBitsStored(patientInfo, colBitsStored);
               var _HightBit = TryGetBitsStored(patientInfo, colHightBit);
               var _BitsAllocated = TryGetBitsStored(patientInfo, colBitsAllocated);
               var MonoChrome = TryGetString(patientInfo, colMonoChrome, "MONOCHROME2");
               var pid = TryGetString(patientInfo, colPid);
               var _KVP = TryGetString(patientInfo, colKVP);
               var _MAS = TryGetString(patientInfo, colMAS);
               var patientName = TryGetString(patientInfo, colPatientName);
               var patientSex = TryGetString(patientInfo, colPatientSex);
               var patientAge = TryGetString(patientInfo, colPatientAge);
               var patientBirthdate = TryGetString(patientInfo, colPatientBirthdate);
               var regDate = TryGetString(patientInfo, colRegDate);
               var regNum = TryGetString(patientInfo, colRegNum);
               var imgWidth = TryGetString(patientInfo, colImgWidth);
               var imgHeigh = TryGetString(patientInfo, colImgHeight);
               var modalityCode = TryGetString(patientInfo, colModalityCode);
               var atonomyCode = TryGetString(patientInfo, colAtonomyCode);
               var projectionCode = TryGetString(patientInfo, colProjectionCode);
               var hostpitalName = TryGetString(patientInfo, colHostpitalName, "BACH MAI HOSTPITAL");
               var departmentName = TryGetString(patientInfo, colDepartmentName, "Khoa chan doan hinh anh");
               string defaultStudyInstanceUID = ClearCanvas.Dicom.DicomUid.GenerateUid().UID;
               string defaultSeriesInstanceUID = defaultStudyInstanceUID + ".1";
               string defaultSOPInstanceUID = defaultSeriesInstanceUID + ".1";
               var StudyInstanceUID = TryGetString(patientInfo, _colStudyInstanceUID, defaultStudyInstanceUID);
               var SeriesInstanceUID = TryGetString(patientInfo, colSeriesInstanceUID, defaultSeriesInstanceUID);
               var SOPInstanceUID = TryGetString(patientInfo, colSOPInstanceUID, defaultSOPInstanceUID);
               var AppName = TryGetString(patientInfo, colAppName, "VBIT");
               string dicomPath = Path.GetDirectoryName(rawFileName);

               // Lấy về tên file Dicom từ file raw
               string dicomFileName = string.Format("{0}{1}{2}.DCM", dicomPath, Path.DirectorySeparatorChar,
                                                    Path.GetFileNameWithoutExtension(rawFileName));

               if (delbeforeSave && File.Exists(dicomFileName)) Try2DelFile(dicomFileName);
               else
                   try2RenameExistedFile(dicomFileName);



               //FileStream fs = File.OpenRead(rawFileName);

               //long length = fs.Length;
               long dataLength = pixelData.Length;

               string col = imgWidth.ToString();
               string row = imgHeigh.ToString();
               // GetSize(dataLength, out col, out row);

               // Tạo File Dicom để lưu thông tin
               var dcmFile = new DicomFile(dicomFileName);
               DicomAttributeCollection dicomDataSet = dcmFile.DataSet;

               //Set Tag For File
               DateTime studyTime = DateTime.Now;
               dicomDataSet[DicomTags.SpecificCharacterSet].SetStringValue("ISO_IR 100");
               dicomDataSet[DicomTags.ImageType].SetStringValue("ORIGINAL\\PRIMARY\\OTHER\\M\\FFE");
               dicomDataSet[DicomTags.InstanceCreationDate].SetStringValue(DateParser.ToDicomString(studyTime));
               dicomDataSet[DicomTags.InstanceCreationTime].SetStringValue(TimeParser.ToDicomString(studyTime));
               dicomDataSet[DicomTags.SopClassUid].SetStringValue(SopClass.MrImageStorageUid);
               dicomDataSet[DicomTags.SopInstanceUid].SetStringValue(SOPInstanceUID);
               dicomDataSet[DicomTags.StudyDate].SetStringValue(regDate);
               dicomDataSet[DicomTags.ApplicationName].SetStringValue(AppName);
               dicomDataSet[DicomTags.StudyTime].SetStringValue(TimeParser.ToDicomString(studyTime));
               dicomDataSet[DicomTags.SeriesDate].SetStringValue(regDate);
               dicomDataSet[DicomTags.SeriesTime].SetStringValue(TimeParser.ToDicomString(studyTime));
               dicomDataSet[DicomTags.AccessionNumber].SetStringValue(regNum);
               dicomDataSet[DicomTags.Modality].SetStringValue(modalityCode);
               dicomDataSet[DicomTags.Manufacturer].SetStringValue("VBIT");
               dicomDataSet[DicomTags.StationName].SetStringValue(AppName);
               dicomDataSet[DicomTags.ExposureInMas].SetStringValue(_MAS);
               dicomDataSet[DicomTags.Kvp].SetStringValue(_KVP);
               dicomDataSet[DicomTags.ManufacturersModelName].SetNullValue();
               dicomDataSet[DicomTags.InstitutionName].SetStringValue(hostpitalName);
               dicomDataSet[DicomTags.InstitutionalDepartmentName].SetStringValue(departmentName);
               //dicomDataSet[DicomTags.StudyDescription].SetStringValue("HEART");
               //dicomDataSet[DicomTags.SeriesDescription].SetStringValue("Heart 2D EPI BH TRA");
               //dicomDataSet[DicomTags.PatientsName].SetStringValue("Patient^Test");
               dicomDataSet[DicomTags.PatientsName].SetStringValue(patientName);
               dicomDataSet[DicomTags.PatientId].SetStringValue(pid);
               dicomDataSet[DicomTags.PatientsBirthDate].SetStringValue(patientBirthdate);
               dicomDataSet[DicomTags.PatientsSex].SetStringValue(patientSex);
               dicomDataSet[DicomTags.PatientsAge].SetStringValue(patientAge);
               dicomDataSet[DicomTags.PatientsWeight].SetStringValue("70");
               dicomDataSet[DicomTags.SequenceVariant].SetStringValue("OTHER");
               dicomDataSet[DicomTags.ScanOptions].SetStringValue("CG");
               dicomDataSet[DicomTags.MrAcquisitionType].SetStringValue("2D");
               dicomDataSet[DicomTags.SliceThickness].SetStringValue("2");
               dicomDataSet[DicomTags.RepetitionTime].SetStringValue("857.142883");
               dicomDataSet[DicomTags.EchoTime].SetStringValue("8.712100");
               dicomDataSet[DicomTags.NumberOfAverages].SetStringValue("1");
               dicomDataSet[DicomTags.ImagingFrequency].SetStringValue("63.901150");
               dicomDataSet[DicomTags.ImagedNucleus].SetStringValue("1H");
               dicomDataSet[DicomTags.EchoNumbers].SetStringValue("1");
               dicomDataSet[DicomTags.MagneticFieldStrength].SetStringValue("1.500000");
               dicomDataSet[DicomTags.SpacingBetweenSlices].SetStringValue("10.00000");
               dicomDataSet[DicomTags.NumberOfPhaseEncodingSteps].SetStringValue("81");
               dicomDataSet[DicomTags.EchoTrainLength].SetStringValue("0");
               dicomDataSet[DicomTags.PercentSampling].SetStringValue("63.281250");
               dicomDataSet[DicomTags.PercentPhaseFieldOfView].SetStringValue("68.75000");
               dicomDataSet[DicomTags.DeviceSerialNumber].SetStringValue("1234");
               dicomDataSet[DicomTags.SoftwareVersions].SetStringValue("V1.0");
               dicomDataSet[DicomTags.ProtocolName].SetStringValue("2D EPI BH");
               dicomDataSet[DicomTags.TriggerTime].SetStringValue("14.000000");
               dicomDataSet[DicomTags.LowRRValue].SetStringValue("948");
               dicomDataSet[DicomTags.HighRRValue].SetStringValue("1178");
               dicomDataSet[DicomTags.IntervalsAcquired].SetStringValue("102");
               dicomDataSet[DicomTags.IntervalsRejected].SetStringValue("0");
               dicomDataSet[DicomTags.HeartRate].SetStringValue("56");
               dicomDataSet[DicomTags.ReceiveCoilName].SetStringValue("B");
               dicomDataSet[DicomTags.TransmitCoilName].SetStringValue("B");
               dicomDataSet[DicomTags.InPlanePhaseEncodingDirection].SetStringValue("COL");
               dicomDataSet[DicomTags.FlipAngle].SetStringValue("50.000000");
               dicomDataSet[DicomTags.PatientPosition].SetStringValue("HFS");
               dicomDataSet[DicomTags.StudyInstanceUid].SetStringValue(StudyInstanceUID);
               dicomDataSet[DicomTags.SeriesInstanceUid].SetStringValue(SeriesInstanceUID);
               //dicomDataSet[DicomTags.StudyId].SetStringValue(pid);
               dicomDataSet[DicomTags.SeriesNumber].SetStringValue("1");
               dicomDataSet[DicomTags.AcquisitionNumber].SetStringValue("7");
               dicomDataSet[DicomTags.InstanceNumber].SetStringValue("1");
               dicomDataSet[DicomTags.ImagePositionPatient].SetStringValue("-61.7564\\-212.04848\\-99.6208");
               dicomDataSet[DicomTags.ImageOrientationPatient].SetStringValue("0.861\\0.492\\0.126\\-0.2965");
               dicomDataSet[DicomTags.FrameOfReferenceUid].SetStringValue(DicomUid.GenerateUid().UID);
               dicomDataSet[DicomTags.PositionReferenceIndicator].SetStringValue(null);
               //dicomDataSet[DicomTags.ImageComments].SetStringValue("Test MR Image");
               dicomDataSet[DicomTags.SamplesPerPixel].SetStringValue("1");
               dicomDataSet[DicomTags.PhotometricInterpretation].SetStringValue(MonoChrome);
               dicomDataSet[DicomTags.Rows].SetStringValue(row);
               dicomDataSet[DicomTags.Columns].SetStringValue(col);
               //switch (_bitsStored)
               //{
               //case 16:
               dicomDataSet[DicomTags.PixelSpacing].SetStringValue("0.168\\0.168");
               dicomDataSet[DicomTags.BitsAllocated].SetStringValue(_BitsAllocated.ToString());
               dicomDataSet[DicomTags.BitsStored].SetStringValue(_bitsStored.ToString());
               dicomDataSet[DicomTags.HighBit].SetStringValue(_HightBit.ToString());
               dicomDataSet[DicomTags.PixelRepresentation].SetStringValue("0");
               dicomDataSet[DicomTags.WindowCenter].SetStringValue(WC.ToString());
               dicomDataSet[DicomTags.WindowWidth].SetStringValue(WW.ToString());
               
             
               dicomDataSet[DicomTags.RescaleIntercept].SetStringValue("0.");
               dicomDataSet[DicomTags.RescaleSlope].SetStringValue("1.");


               dicomDataSet[DicomTags.ViewPosition].SetStringValue(projectionCode);
               dicomDataSet[DicomTags.BodyPartExamined].SetStringValue(atonomyCode);


               //Gán Dữ liệu ảnh
               var pixels = new DicomAttributeOW(DicomTags.PixelData) { Values = pixelData };
               dicomDataSet[DicomTags.PixelData] = pixels;

               var item = new DicomSequenceItem();
               dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);
               item[DicomTags.RequestedProcedureId].SetStringValue("MRR1234");
               item[DicomTags.ScheduledProcedureStepId].SetStringValue("MRS1234");

               item = new DicomSequenceItem();
               dicomDataSet[DicomTags.RequestAttributesSequence].AddSequenceItem(item);

               item[DicomTags.RequestedProcedureId].SetStringValue("MR2R1234");
               item[DicomTags.ScheduledProcedureStepId].SetStringValue("MR2S1234");

               var studyItem = new DicomSequenceItem();

               item[DicomTags.ReferencedStudySequence].AddSequenceItem(studyItem);

               studyItem[DicomTags.ReferencedSopClassUid].SetStringValue(SopClass.MrImageStorageUid);
               studyItem[DicomTags.ReferencedSopInstanceUid].SetStringValue("1.2.3.4.5.6.7.8.9");

               //Set Meta Info
               dicomDataSet[DicomTags.MediaStorageSopClassUid].SetStringValue(
                   dcmFile.DataSet[DicomTags.SopClassUid].GetString(0, ""));
               dicomDataSet[DicomTags.MediaStorageSopInstanceUid].SetStringValue(
                   dcmFile.DataSet[DicomTags.SopInstanceUid].GetString(0, ""));

               dcmFile.TransferSyntax = TransferSyntax.ExplicitVrLittleEndian;

               dicomDataSet[DicomTags.ImplementationClassUid].SetStringValue("1.1.1.1.1.11.1");
               dicomDataSet[DicomTags.ImplementationVersionName].SetStringValue("droc 1.0");

               // Lưu File
               dcmFile.Save();
               return true;
           }
           catch (Exception ex)
           {
               return false;
           }
       }
        static void try2RenameExistedFile(string newFile)
        {
            try
            {
                if (File.Exists(newFile))
                {
                    int _max = 0;
                    string[] _files = Directory.GetFiles(Path.GetDirectoryName(newFile));
                    foreach (string _file in _files)
                    {
                        if (_file.ToUpper().Contains("_IDX"))
                        {
                            string _value = Path.GetFileNameWithoutExtension(_file).ToUpper().Replace(Path.GetFileNameWithoutExtension(newFile).ToUpper(), "").Replace("_IDX", "");
                            if (Microsoft.VisualBasic.Information.IsNumeric(_value))
                            {
                                int _idx = Convert.ToInt32(_value);
                                if (_idx > _max) _max = _idx;
                            }
                        }
                    }
                    _max += 1;
                    string Ext = Path.GetExtension(newFile).ToUpper();
                    string _newFileWithIdx = newFile.ToUpper().Replace(Ext, "") + "_IDX" + _max.ToString() + Ext;
                    //Đổi tên file đang có cộng thêm Idx
                    File.Move(newFile, _newFileWithIdx);
                }
            }
            catch
            {
            }
        }
        static void try2RenameExistedFile(string newFile,ref string newDCMFile)
        {
            try
            {
                if (File.Exists(newFile))
                {
                    int _max = 0;
                    string[] _files = Directory.GetFiles(Path.GetDirectoryName(newFile));
                    foreach (string _file in _files)
                    {
                        if (_file.ToUpper().Contains("_IDX"))
                        {
                            string _value = Path.GetFileNameWithoutExtension(_file).ToUpper().Replace(Path.GetFileNameWithoutExtension(newFile).ToUpper(), "").Replace("_IDX", "");
                            if (Microsoft.VisualBasic.Information.IsNumeric(_value))
                            {
                                int _idx = Convert.ToInt32(_value);
                                if (_idx > _max) _max = _idx;
                            }
                        }
                    }
                    _max += 1;
                    string Ext = Path.GetExtension(newFile).ToUpper();
                    string _newFileWithIdx = newFile.ToUpper().Replace(Ext, "") + "_IDX" + _max.ToString() + Ext;
                    newDCMFile = _newFileWithIdx;
                    //Đổi tên file đang có cộng thêm Idx
                    File.Move(newFile, _newFileWithIdx);
                }
            }
            catch
            {
            }
        }
      

        /// <summary>
        /// Hàm trả về giá trị cell của một colum trong data row
        /// </summary>
        /// <param name="dataRow">dòng cần lấy dữ liệu</param>
        /// <param name="colName">tên cột</param>
        /// <returns></returns>
        private static string TryGetString(DataRow dataRow, string colName)
        {
            return TryGetString(dataRow, colName, "");
        }
        private static Int32 TryGetBitsStored(DataRow dataRow, string colName)
        {
            try
            {
                if (dataRow.Table.Columns.Contains(colName))
                {
                    if (dataRow[colName] == null || dataRow[colName].Equals(DBNull.Value))
                        return 16;
                    return Convert.ToInt32(dataRow[colName]);
                }
                else
                    return 16;
            }
            catch
            {
                return 16;
            }
        }
        /// <summary>
        /// Hàm trả về giá trị cell của một colum trong data row
        /// </summary>
        /// <param name="dataRow">dòng cần lấy dữ liệu</param>
        /// <param name="colName">tên cột</param>
        /// <param name="defaultValue">Giá trị mặc định</defaultValue>
        /// <returns></returns>
        private static string TryGetString(DataRow dataRow, string colName, string defaultValue)
        {
            try
            {
                DateTime _tempDataType=DateTime.Now;
                if (!dataRow.Table.Columns.Contains(colName)) return defaultValue;
                if (dataRow.Table.Columns[colName].DataType.GetType().Equals(_tempDataType.GetType())) return ToDicomString(Convert.ToDateTime(dataRow[colName]));
                return sDbnull(dataRow[colName], defaultValue);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
        public static string sDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return DefaultVal.ToString();
            }
            else
            {
                return obj.ToString();
            }
        }
        private static string ToDicomString(DateTime dtmValue)
        {
            return dtmValue.Year.ToString() + Microsoft.VisualBasic.Strings.Right("00" + dtmValue.Month.ToString(), 2) + Microsoft.VisualBasic.Strings.Right("00" + dtmValue.Day.ToString(), 2);
        }
        /// <summary>
        /// Hàm lấy về kích thước của ảnh
        /// </summary>
        /// <param name="iTotalSize">Số Byte</param>
        /// <param name="col">Số cột</param>
        /// <param name="row">Số dòng</param>
        private static void GetSize(long iTotalSize, out string col, out string row)
        {
            var numberOfPixels = (int) (iTotalSize/2);

            List<int> listOfFactors = Factors(numberOfPixels);
            int noFactors = listOfFactors.Count;
            col = listOfFactors[noFactors - 2].ToString();
            row = listOfFactors[noFactors - 1].ToString();
        }

        private static List<int> Factors(int number)
        {
            var factors = new List<int>();
            var max = (int) Math.Sqrt(number); //round down 
            for (int factor = 1; factor <= max; ++factor)
            {
                //test from 1 to the square root, or the int below it, inclusive. 
                if (number%factor == 0)
                {
                    factors.Add(factor);
                    if (factor != max)
                    {
                        // Don't add the square root twice!  Thanks Jon 
                        factors.Add(number/factor);
                    }
                }
            }
            return factors;
        }
    }
}