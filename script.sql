USE [QuanLyVanBan]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspDetailUser]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspDetailUser](
	[IdUser] [bigint] IDENTITY(1,1) NOT NULL,
	[ho_ten] [nvarchar](250) NULL,
	[UsersId] [nvarchar](128) NULL,
	[ngay_sinh] [datetime] NULL,
	[gioi_Tinh] [nvarchar](250) NULL,
	[so_dien_thoai] [nvarchar](250) NULL,
	[is_delete] [bit] NULL,
	[url_avatar] [nvarchar](max) NULL,
	[name_avatar] [nvarchar](250) NULL,
	[id_don_vi] [nchar](10) NULL,
 CONSTRAINT [PK_AspDetailUser_1] PRIMARY KEY CLUSTERED 
(
	[IdUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[View_Session]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Session]
AS
SELECT dbo.AspNetUsers.UserName, dbo.AspDetailUser.ho_ten, dbo.AspDetailUser.UsersId, dbo.AspDetailUser.ngay_sinh, dbo.AspDetailUser.gioi_Tinh, dbo.AspDetailUser.so_dien_thoai, dbo.AspDetailUser.is_delete, 
                  dbo.AspDetailUser.url_avatar, dbo.AspDetailUser.name_avatar, dbo.AspNetUsers.Email, dbo.AspNetUsers.Id, dbo.AspDetailUser.IdUser
FROM     dbo.AspNetUsers INNER JOIN
                  dbo.AspDetailUser ON dbo.AspNetUsers.Id = dbo.AspDetailUser.UsersId
GO
/****** Object:  View [dbo].[View_Online]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[View_Online]
AS
SELECT dbo.AspNetUsers.UserName, dbo.AspDetailUser.ho_ten, dbo.AspDetailUser.ngay_sinh, dbo.AspDetailUser.gioi_Tinh, dbo.AspDetailUser.so_dien_thoai, dbo.AspDetailUser.is_delete, dbo.AspDetailUser.url_avatar, 
                  dbo.AspDetailUser.name_avatar, dbo.AspNetUsers.Email, dbo.AspDetailUser.IdUser, dbo.AspNetUsers.Id
FROM     dbo.AspNetUsers RIGHT OUTER JOIN
                  dbo.AspDetailUser ON dbo.AspNetUsers.Id = dbo.AspDetailUser.UsersId
GO
/****** Object:  Table [dbo].[AspDanhMucHeThong]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspDanhMucHeThong](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[sap_xep] [int] NULL,
	[ten_goi] [nvarchar](250) NULL,
	[ghi_chu] [nvarchar](250) NULL,
	[noi_dung] [nvarchar](250) NULL,
	[ngay_tao] [date] NULL,
	[phan_loai] [nvarchar](250) NULL,
	[is_lock] [bit] NULL,
 CONSTRAINT [PK_AspDanhMucHeThong] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspDSDTThuChi]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspDSDTThuChi](
	[IdTC] [bigint] IDENTITY(1,1) NOT NULL,
	[IdThu] [bigint] NULL,
	[IdChi] [bigint] NULL,
	[phan_loai_doi_tuong] [nvarchar](250) NULL,
	[ho_ten_ca_nhan] [nvarchar](250) NULL,
	[gioi_tinh] [nvarchar](250) NULL,
	[so_hieu_giay_to] [nvarchar](250) NULL,
	[ngay_sinh] [date] NULL,
	[dia_chi_que_quan] [nvarchar](250) NULL,
	[ma_so_dv] [nvarchar](250) NULL,
	[ten_don_vi] [nvarchar](250) NULL,
	[dia_chi_don_vi] [nvarchar](250) NULL,
	[ke_toan_don_vi] [nvarchar](250) NULL,
	[ngay_tao] [date] NULL,
	[so_tien_thu] [bigint] NULL,
 CONSTRAINT [PK_AspDSDTThuChi] PRIMARY KEY CLUSTERED 
(
	[IdTC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspFolder]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspFolder](
	[IdFoder] [bigint] IDENTITY(1,1) NOT NULL,
	[NameFolder] [nvarchar](150) NULL,
	[OderSort] [int] NULL,
	[SessionId] [bigint] NULL,
	[SeesionRemove] [bigint] NULL,
	[IsDeleate] [bit] NULL,
 CONSTRAINT [PK_AspFolder] PRIMARY KEY CLUSTERED 
(
	[IdFoder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspHoSoCanBo]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspHoSoCanBo](
	[IdCanBo] [bigint] IDENTITY(1,1) NOT NULL,
	[ho_ten] [nvarchar](250) NULL,
	[ngay_sinh] [date] NULL,
	[gioi_tinh] [nvarchar](50) NULL,
	[so_dien_thoai] [varchar](150) NULL,
	[url_avatar] [nvarchar](max) NULL,
	[name_avatar] [nvarchar](250) NULL,
	[email] [nvarchar](250) NULL,
	[chuc_vu] [nvarchar](250) NULL,
 CONSTRAINT [PK_AspHoSoCanBo] PRIMARY KEY CLUSTERED 
(
	[IdCanBo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspKeHoachChi]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspKeHoachChi](
	[IdKeHoach] [bigint] IDENTITY(1,1) NOT NULL,
	[noi_dung_chi] [nvarchar](250) NULL,
	[don_vi_chi] [nvarchar](250) NULL,
	[ly_do_chi] [nvarchar](250) NULL,
	[url_minh_chung] [nvarchar](max) NULL,
	[ho_ten_giam_doc] [nvarchar](250) NULL,
	[ke_toan_truong] [nvarchar](250) NULL,
	[phan_loai_tien_chi] [nvarchar](250) NULL,
	[so_tien_chi] [bigint] NULL,
	[so_luong_nguoi_can_chi] [int] NULL,
	[thoi_han] [int] NULL,
	[ngay_tao_phieu] [date] NULL,
	[is_da_chi] [bit] NULL,
	[is_thu_ky_duyet] [bit] NULL,
	[is_giam_doc_duyet] [bit] NULL,
	[is_chuyen_thung_rac] [bit] NULL,
 CONSTRAINT [PK_AspKeHoachChi] PRIMARY KEY CLUSTERED 
(
	[IdKeHoach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspKeHoachThu]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspKeHoachThu](
	[IdKeHoach] [bigint] IDENTITY(1,1) NOT NULL,
	[noi_dung_thu] [nvarchar](250) NULL,
	[so_tien_thu_du_kien] [bigint] NULL,
	[don_vi_thu] [nvarchar](250) NULL,
	[dia_chi_thu] [nvarchar](250) NULL,
	[ly_do_thu] [nvarchar](250) NULL,
	[url_minh_chung] [nvarchar](max) NULL,
	[ho_ten_giam_doc] [nvarchar](250) NULL,
	[ke_toan_truong] [nvarchar](250) NULL,
	[id_lap_phieu] [bigint] NULL,
	[ho_ten_thu_quy] [nvarchar](250) NULL,
	[is_nhan_tien] [bit] NULL,
	[is_da_dong_du] [bit] NULL,
	[ngay_tao_phieu] [date] NULL,
	[thoi_han] [int] NULL,
	[is_chuyen_thung_rac] [bit] NULL,
 CONSTRAINT [PK_AspKeHoachThuChi] PRIMARY KEY CLUSTERED 
(
	[IdKeHoach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspStoreDocument]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspStoreDocument](
	[IdDocument] [bigint] IDENTITY(1,1) NOT NULL,
	[NameDocument] [nvarchar](250) NULL,
	[UrlDocument] [nvarchar](250) NULL,
	[DateNow] [datetime] NULL,
	[IdUser] [bigint] NULL,
	[SumCount] [int] NULL,
	[Note] [nvarchar](max) NULL,
	[IdFoder] [bigint] NULL,
 CONSTRAINT [PK_AspStoreDocument] PRIMARY KEY CLUSTERED 
(
	[IdDocument] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspThuThucTe]    Script Date: 20/06/2024 9:46:04 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspThuThucTe](
	[IdThu] [bigint] IDENTITY(1,1) NOT NULL,
	[IdKeHoach] [bigint] NULL,
	[so_tien_thu] [bigint] NULL,
	[noi_dung_thu] [nvarchar](250) NULL,
	[phan_loai_doi_tuong] [nvarchar](250) NULL,
	[ho_ten_ca_nhan] [nvarchar](250) NULL,
	[so_hieu_giay_to] [nvarchar](250) NULL,
	[ngay_sinh] [date] NULL,
	[dia_chi_que_quan] [nvarchar](250) NULL,
	[gioi_tinh] [nvarchar](250) NULL,
	[ten_don_vi] [nvarchar](250) NOT NULL,
	[ma_so_dv] [nvarchar](50) NULL,
	[dia_chi_don_vi] [nvarchar](250) NULL,
	[ke_toan_don_vi] [nvarchar](250) NULL,
	[ngay_thu] [date] NOT NULL,
	[IsTDSDT] [bit] NULL,
 CONSTRAINT [PK_AspThuThucTe] PRIMARY KEY CLUSTERED 
(
	[IdThu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AspDanhMucHeThong] ON 

INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (1, 1, N'Tiến Sĩ', NULL, NULL, CAST(N'2024-06-08' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (2, 2, N'Giáo viên', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (3, 3, N'Bác sĩ', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (4, 4, N'Kỹ sư', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (5, 5, N'Nhạc sĩ', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (6, 6, N'Ca sĩ', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (7, 7, N'Lập trình viên', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (8, 8, N'Giám đốc', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (9, 9, N'Thư kỹ', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (10, 10, N'Kỹ sư', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (11, 11, N'Trưởng phòng', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (12, 12, N'Nhân viên kế toán', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (13, 13, N'Nhân viên kế toán trưởng', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (14, 14, N'Thạc sĩ', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (15, 15, N'Học sinh', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
INSERT [dbo].[AspDanhMucHeThong] ([Id], [sap_xep], [ten_goi], [ghi_chu], [noi_dung], [ngay_tao], [phan_loai], [is_lock]) VALUES (16, 16, N'Sinh viên ', NULL, NULL, CAST(N'2024-06-09' AS Date), N'Danh mục chức vụ', 1)
SET IDENTITY_INSERT [dbo].[AspDanhMucHeThong] OFF
GO
SET IDENTITY_INSERT [dbo].[AspDetailUser] ON 

INSERT [dbo].[AspDetailUser] ([IdUser], [ho_ten], [UsersId], [ngay_sinh], [gioi_Tinh], [so_dien_thoai], [is_delete], [url_avatar], [name_avatar], [id_don_vi]) VALUES (6, N'Trần Minh Quân', N'a3eced7b-cf16-412b-9446-16cd70681b42', CAST(N'2002-05-09T00:00:00.000' AS DateTime), N'Nam', N'0332581817', NULL, NULL, NULL, NULL)
INSERT [dbo].[AspDetailUser] ([IdUser], [ho_ten], [UsersId], [ngay_sinh], [gioi_Tinh], [so_dien_thoai], [is_delete], [url_avatar], [name_avatar], [id_don_vi]) VALUES (7, N'Nguyễn Phúc Quỳnh Hương', NULL, CAST(N'2002-02-11T00:00:00.000' AS DateTime), N'Nữ', N'087658392', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AspDetailUser] OFF
GO
SET IDENTITY_INSERT [dbo].[AspDSDTThuChi] ON 

INSERT [dbo].[AspDSDTThuChi] ([IdTC], [IdThu], [IdChi], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [gioi_tinh], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [ma_so_dv], [ten_don_vi], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_tao], [so_tien_thu]) VALUES (9, 9, NULL, N'Tập thể', N'', NULL, N'', NULL, N'', N'THPT-CHUYEN', N'THPT Chuyên', N'TT-Mường Thanh', N'', CAST(N'2024-06-19' AS Date), 1000000)
INSERT [dbo].[AspDSDTThuChi] ([IdTC], [IdThu], [IdChi], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [gioi_tinh], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [ma_so_dv], [ten_don_vi], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_tao], [so_tien_thu]) VALUES (10, 10, NULL, N'Tập thể', N'', NULL, N'', NULL, N'', N'SGDT-SONLA', N'Sở Giáo Dục Và Đào Tạo Sơn La', N'', N'', CAST(N'2024-06-19' AS Date), 1000000)
INSERT [dbo].[AspDSDTThuChi] ([IdTC], [IdThu], [IdChi], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [gioi_tinh], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [ma_so_dv], [ten_don_vi], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_tao], [so_tien_thu]) VALUES (13, 21, NULL, N'Cá nhân', N'Trần Minh Quân', NULL, N'SH01', NULL, N'', N'', N'', N'', N'', CAST(N'2024-06-19' AS Date), 1000000)
INSERT [dbo].[AspDSDTThuChi] ([IdTC], [IdThu], [IdChi], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [gioi_tinh], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [ma_so_dv], [ten_don_vi], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_tao], [so_tien_thu]) VALUES (14, 22, NULL, N'Tập thể', N'', NULL, N'', NULL, N'', N'THPT-MUONGLAY', N'THPT Mường Lay', N'', N'', CAST(N'2024-06-19' AS Date), 1000000)
INSERT [dbo].[AspDSDTThuChi] ([IdTC], [IdThu], [IdChi], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [gioi_tinh], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [ma_so_dv], [ten_don_vi], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_tao], [so_tien_thu]) VALUES (15, 24, NULL, N'Cá nhân', N'Trần Minnh quân', NULL, N'080099008349', NULL, N'', N'', N'', N'', N'', CAST(N'2024-06-19' AS Date), 2000000)
SET IDENTITY_INSERT [dbo].[AspDSDTThuChi] OFF
GO
SET IDENTITY_INSERT [dbo].[AspKeHoachThu] ON 

INSERT [dbo].[AspKeHoachThu] ([IdKeHoach], [noi_dung_thu], [so_tien_thu_du_kien], [don_vi_thu], [dia_chi_thu], [ly_do_thu], [url_minh_chung], [ho_ten_giam_doc], [ke_toan_truong], [id_lap_phieu], [ho_ten_thu_quy], [is_nhan_tien], [is_da_dong_du], [ngay_tao_phieu], [thoi_han], [is_chuyen_thung_rac]) VALUES (11, N'Thu tiền nâng cấp P/M Thi đua khen thưởng Sơn La', 20000000, N'Công ty TNHH Chuyển Giao Phần Mềm Diamond', N'26/203 Kim Ngưu', N'', NULL, N'Vũ Hồng Thơm', N'Hoàng Thùy Linh', NULL, N'Đặng Huy Hùng', NULL, NULL, CAST(N'2024-06-18' AS Date), 90, NULL)
INSERT [dbo].[AspKeHoachThu] ([IdKeHoach], [noi_dung_thu], [so_tien_thu_du_kien], [don_vi_thu], [dia_chi_thu], [ly_do_thu], [url_minh_chung], [ho_ten_giam_doc], [ke_toan_truong], [id_lap_phieu], [ho_ten_thu_quy], [is_nhan_tien], [is_da_dong_du], [ngay_tao_phieu], [thoi_han], [is_chuyen_thung_rac]) VALUES (13, N'Thu tiền phần mềm Quản Lý Bảo Tàng', 100000000, N'Công ty TNHH Chuyển Giao Phần Mềm Diamond', N'26/203 Kim Ngưu', N'', NULL, N'Vũ Hồng Thơm', N'Hoàng Thùy Linh', NULL, N'Đặng Huy Hùng', NULL, NULL, CAST(N'2024-06-19' AS Date), 90, NULL)
SET IDENTITY_INSERT [dbo].[AspKeHoachThu] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'8Hu+Y8vwO5c=', N'Manager')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'giVdPkgLQdg=', N'Admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'IystwHsaji8=', N'SuperAdmin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'LNPOusXHL+E=', N'User')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a3eced7b-cf16-412b-9446-16cd70681b42', N'IystwHsaji8=')
GO
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'383c6523-4a9f-40b9-a36a-01ca7e18895f', N'quanlearning2k2@gmail.com', 1, N'AFsuMdvun9T+z5v36XXQn20nsbWaeo0YK2KrbNBDxiZYTQA3Qne3m7/VkLD2r/cmZw==', N'fafea99c-d879-4494-a1c3-a2e985130efc', NULL, 0, 0, NULL, 0, 0, N'quantran3350')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'7566654a-04b7-4ca2-a714-1cdc14294870', N'HuyMusic@gmail.com', 1, N'AJB/BXcZcmr1K1T/aJqyMh4RS7uNyGQyU+bVNnW1TwkhMUwvnJPTQ6GqFMIZ8aR0+Q==', N'243ec114-2aba-4392-a43e-c089f7c4e533', NULL, 0, 0, NULL, 0, 0, N'HuyMusic')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a0c7c5a3-f949-41bc-8122-64cfa51f72b5', N'Maianh0402@gmail.com', 1, N'AJ0tekj8V2aryhKV5NhKeSzGrldjxCx9cnvwIYxxlw7qkStxX4nT4ZT71QEhGEtRnQ==', N'23ce4d66-d0b5-4c55-9aaf-615909d3388f', NULL, 0, 0, NULL, 0, 0, N'MaiAnh0402')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a3eced7b-cf16-412b-9446-16cd70681b42', N'quanma4405@gmail.com', 1, N'AM2Diuo2KXPfeSPV+xa4TzFHBUNynpMt0Ydivp6jfKJkG4ah1kMOxWI4Swr9g3EooQ==', N'30cf6760-73a9-4aa5-adfa-db4861f14d07', NULL, 0, 0, NULL, 0, 0, N'Admin')
GO
SET IDENTITY_INSERT [dbo].[AspThuThucTe] ON 

INSERT [dbo].[AspThuThucTe] ([IdThu], [IdKeHoach], [so_tien_thu], [noi_dung_thu], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [gioi_tinh], [ten_don_vi], [ma_so_dv], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_thu], [IsTDSDT]) VALUES (9, 11, 1000000, N'Tiền nâng cấp phần mềm', N'Tập thể', N'', N'', NULL, N'', NULL, N'THPT Chuyên', N'THPT-CHUYEN', N'TT-Mường Thanh', N'Đào Thị Bích', CAST(N'2024-06-19' AS Date), 1)
INSERT [dbo].[AspThuThucTe] ([IdThu], [IdKeHoach], [so_tien_thu], [noi_dung_thu], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [gioi_tinh], [ten_don_vi], [ma_so_dv], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_thu], [IsTDSDT]) VALUES (10, 11, 1000000, N'Tiền nâng cấp phần mềm', N'Tập thể', N'', N'', NULL, N'', NULL, N'Sở Giáo Dục Và Đào Tạo Sơn La', N'SGDT-SONLA', N'', N'Ngô Duy Thường', CAST(N'2024-06-19' AS Date), 1)
INSERT [dbo].[AspThuThucTe] ([IdThu], [IdKeHoach], [so_tien_thu], [noi_dung_thu], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [gioi_tinh], [ten_don_vi], [ma_so_dv], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_thu], [IsTDSDT]) VALUES (21, 11, 1000000, N'Tiền nâng cấp phần mềm', N'Cá nhân', N'Trần Minh Quân', N'SH01', NULL, N'', NULL, N'', N'', N'', N'', CAST(N'2024-06-19' AS Date), 1)
INSERT [dbo].[AspThuThucTe] ([IdThu], [IdKeHoach], [so_tien_thu], [noi_dung_thu], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [gioi_tinh], [ten_don_vi], [ma_so_dv], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_thu], [IsTDSDT]) VALUES (22, 11, 1000000, N'Nâng cấp phần mềm ', N'Tập thể', N'', N'', NULL, N'', NULL, N'THPT Mường Lay', N'THPT-MUONGLAY', N'', N'', CAST(N'2024-06-19' AS Date), 1)
INSERT [dbo].[AspThuThucTe] ([IdThu], [IdKeHoach], [so_tien_thu], [noi_dung_thu], [phan_loai_doi_tuong], [ho_ten_ca_nhan], [so_hieu_giay_to], [ngay_sinh], [dia_chi_que_quan], [gioi_tinh], [ten_don_vi], [ma_so_dv], [dia_chi_don_vi], [ke_toan_don_vi], [ngay_thu], [IsTDSDT]) VALUES (23, 13, 1000000, N'Thu tiền phần mềm', N'Tập thể', N'', N'', NULL, N'', NULL, N'Công ty H2 Soft', N'', N'', N'', CAST(N'2024-06-19' AS Date), 0)
SET IDENTITY_INSERT [dbo].[AspThuThucTe] OFF
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspStoreDocument]  WITH CHECK ADD  CONSTRAINT [FK_AspStoreDocument_AspDetailUser] FOREIGN KEY([IdUser])
REFERENCES [dbo].[AspDetailUser] ([IdUser])
GO
ALTER TABLE [dbo].[AspStoreDocument] CHECK CONSTRAINT [FK_AspStoreDocument_AspDetailUser]
GO
ALTER TABLE [dbo].[AspStoreDocument]  WITH CHECK ADD  CONSTRAINT [FK_AspStoreDocument_AspFolder] FOREIGN KEY([IdFoder])
REFERENCES [dbo].[AspFolder] ([IdFoder])
GO
ALTER TABLE [dbo].[AspStoreDocument] CHECK CONSTRAINT [FK_AspStoreDocument_AspFolder]
GO
ALTER TABLE [dbo].[AspThuThucTe]  WITH CHECK ADD  CONSTRAINT [FK_AspThuThucTe_AspKeHoachThu] FOREIGN KEY([IdKeHoach])
REFERENCES [dbo].[AspKeHoachThu] ([IdKeHoach])
GO
ALTER TABLE [dbo].[AspThuThucTe] CHECK CONSTRAINT [FK_AspThuThucTe_AspKeHoachThu]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[52] 4[12] 2[13] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 10
               Left = 375
               Bottom = 309
               Right = 635
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspDetailUser"
            Begin Extent = 
               Top = 6
               Left = 878
               Bottom = 293
               Right = 1072
            End
            DisplayFlags = 280
            TopColumn = 1
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 15
         Width = 284
         Width = 1572
         Width = 2316
         Width = 2124
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1908
         Width = 1536
         Width = 2316
         Width = 1200
         Width = 1080
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1380
         Table = 1644
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Online'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Online'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[47] 4[14] 2[17] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "AspDetailUser"
            Begin Extent = 
               Top = 7
               Left = 843
               Bottom = 306
               Right = 1037
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AspNetUsers"
            Begin Extent = 
               Top = 7
               Left = 532
               Bottom = 354
               Right = 792
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 14
         Width = 284
         Width = 4008
         Width = 1836
         Width = 2100
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Session'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'View_Session'
GO
