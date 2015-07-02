using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VietBaIT.CommonLibrary;

using VietBaIT.DROC.Objects.ObjectController;
using VietBaIT.DROC.Objects.ObjectInfor;
namespace VietBaIT.DROC.Objects
{
   public class WLRules
    {
        public ActionResult Insert(DataSet dsEntity, PatientInfor _PatientInfor, RegInfor _RegInfor)
        {
            ActionResult act;
            try
            {
                DataTable dtPatient = dsEntity.Tables["PatientEntity"];
                DataTable dtReg = dsEntity.Tables["RegEntity"];
               
                Utility.MapValueFromEntityIntoObjectInfor(_PatientInfor, dtPatient.Rows[0]);
                Utility.MapValueFromEntityIntoObjectInfor(_RegInfor, dtReg.Rows[0]);
               
                PatientController _PatientController = new PatientController(_PatientInfor);
               
              act=  _PatientController.Insert();
              if (act != ActionResult.Success) return act;
                _RegInfor.PATIENT_ID = _PatientInfor.Patient_ID;
                RegController _RegController = new RegController(_RegInfor);
               act= _RegController.Insert();
                if (act != ActionResult.Success) return act;
                if (dsEntity.Tables.Contains("RegDetailEntity") && dsEntity.Tables["RegDetailEntity"] != null && dsEntity.Tables["RegDetailEntity"].Columns.Count > 0 && dsEntity.Tables["RegDetailEntity"].Rows.Count>0)
                {
                    DataTable dtRegDetail = dsEntity.Tables["RegDetailEntity"];
                    RegDetailInfor[] arrDetailInfor = new RegDetailInfor[dtRegDetail.Rows.Count];
                    for (int i = 0; i <= dtRegDetail.Rows.Count - 1; i++)
                    {
                        arrDetailInfor[i] = new RegDetailInfor();
                        Utility.MapValueFromEntityIntoObjectInfor(arrDetailInfor[i], dtRegDetail.Rows[i]);
                    }
                    RegDetailController _RegDetailController = new RegDetailController();
                    foreach (RegDetailInfor infor in arrDetailInfor)
                    {
                        infor.REG_ID = _RegInfor.REG_ID;
                        _RegDetailController.Infor = infor;
                        _RegDetailController.Insert();
                    }
                }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                return ActionResult.Exception; 
            }
        }
        public ActionResult Update(DataSet dsEntity, bool Hasresult)
        {
            try
            {
                PatientInfor _PatientInfor=new PatientInfor();
                RegInfor _RegInfor=new RegInfor();
                DataTable dtPatient = dsEntity.Tables["PatientEntity"];
                DataTable dtReg = dsEntity.Tables["RegEntity"];
                DataTable dtRegDetail = dsEntity.Tables["RegDetailEntity"];
                RegDetailInfor[] arrDetailInfor = new RegDetailInfor[dtRegDetail.Rows.Count];
                Utility.MapValueFromEntityIntoObjectInfor(_PatientInfor, dtPatient.Rows[0]);
                Utility.MapValueFromEntityIntoObjectInfor(_RegInfor, dtReg.Rows[0]);
                for (int i = 0; i <= dtRegDetail.Rows.Count - 1; i++)
                {
                    arrDetailInfor[i] = new RegDetailInfor();
                    Utility.MapValueFromEntityIntoObjectInfor(arrDetailInfor[i], dtRegDetail.Rows[i]);
                }
                PatientController _PatientController = new PatientController(_PatientInfor);
                _PatientController.Update();
                    RegController _RegController = new RegController(_RegInfor);
                    _RegController.UpdateDateAndPhysical();
                    if (!Hasresult)//Nếu BN chưa có dịch vụ nào có kết quả-->Xóa toàn bộ chi tiết và thêm các chi tiết mới
                    {
                        _RegController.Update();
                        if (dtRegDetail.Rows.Count > 0)//Không cập nhật chi tiết thì ko làm gì cả
                        {
                            new RegDetailController().Delete(_RegInfor.REG_ID);
                            RegDetailController _RegDetailController = new RegDetailController();
                            foreach (RegDetailInfor infor in arrDetailInfor)
                            {
                                infor.REG_ID = _RegInfor.REG_ID;
                                _RegDetailController.Infor = infor;
                                _RegDetailController.Insert();
                            }
                        }
                    }
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                return ActionResult.Exception;
            }
        }
        public ActionResult Delete(Int64 Patient_ID,Int64 Reg_ID)
        {
            try
            {
                new PatientController().Delete(Patient_ID);
                new RegController().Delete(Reg_ID);
                new RegDetailController().Delete(Reg_ID);
                return ActionResult.Success;
            }
            catch (Exception ex)
            {
                return ActionResult.Exception;
            }
        }
    }
}
