using System;
using System.Collections.Generic;
using System.Windows.Forms;
using VietBaIT.DROC;
using System.Threading;
using VietBaIT;

namespace DevExpress.XtraBars.Demos.RibbonSimplePad {
    internal static class Program
    {
        [STAThread]
        public static void Main()
        {
            try
            {
                bool Created = false;
                Mutex _Mutex = new Mutex(false, "DROC",out Created);
                if (Created)
                {
                    //DevExpress.UserSkins.OfficeSkins.Register();
                    //DevExpress.UserSkins.BonusSkins.Register();
                    //DevExpress.Skins.SkinManager.EnableFormSkins();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);


                    DROC_Ribbon f = new DROC_Ribbon();
                    Application.Run(f);
                }
                else
                {
                    if (System.IO.File.Exists("Mutex.Warning"))
                        new frm_LargeMsgBoxOK("THÔNG BÁO", "MỘT CHƯƠNG TRÌNH DROC ĐANG CHẠY. HÃY NHẤN NÚT <<TÔI ĐÃ HIỂU>> VÀ VUI LÒNG KIỂM TRA LẠI HOẶC CHỜ TRONG GIÂY LÁT...", "TÔI ĐÃ HIỂU", "TÔI KHÔNG HIỂU").ShowDialog();
                }

            }
            catch
            {
            }
        }
       
        

    }
}
