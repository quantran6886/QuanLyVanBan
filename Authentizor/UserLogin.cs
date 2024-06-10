using System;
namespace AppNetShop.Authentizor
{
    [Serializable]
    public class UserLogin
    {
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public long IdUser { get; set; }
        public string ho_ten { get; set; }
        public string gioi_Tinh { get; set; }
        public string so_dien_thoai { get; set; }
        public string url_avatar { get; set; }
        public DateTime? ngay_sinh { get; set; }
        public string name_avatar { get; set; }

    }
}