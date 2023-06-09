USE [QLBaiGiuXe]

CREATE DATABASE [QLBaiGiuXe]
GO
ALTER TABLE [dbo].[Xe] DROP CONSTRAINT [FK_Xe_TheXeNgay]
GO
ALTER TABLE [dbo].[Xe] DROP CONSTRAINT [FK_Xe_LoaiXe]
GO
ALTER TABLE [dbo].[ViTriDauXe] DROP CONSTRAINT [FK_ViTriDauXe_BaiDauXe]
GO
ALTER TABLE [dbo].[TheXeThang] DROP CONSTRAINT [FK_TheXeThang_ThanhToan]
GO
ALTER TABLE [dbo].[TheXeThang] DROP CONSTRAINT [FK_TheXeThang_LoaiXe]
GO
ALTER TABLE [dbo].[TheXeNgay] DROP CONSTRAINT [FK_TheXeNgay_ViTriDauXe]
GO
ALTER TABLE [dbo].[TheXeNgay] DROP CONSTRAINT [FK_TheXeNgay_ThanhToan]
GO
ALTER TABLE [dbo].[ThanhToan] DROP CONSTRAINT [FK_ThanhToan_LoaiThanhToan]
GO
ALTER TABLE [dbo].[LoaiXe] DROP CONSTRAINT [FK_LoaiXe_BaiDauXe]
GO
ALTER TABLE [dbo].[LichSuXe] DROP CONSTRAINT [FK_LichSuXe_TheXeThang]
GO
ALTER TABLE [dbo].[LichSuXe] DROP CONSTRAINT [FK_LichSuXe_TheXeNgay]
GO
ALTER TABLE [dbo].[LichSuXe] DROP CONSTRAINT [FK_LichSuXe_ThanhToan]
GO
ALTER TABLE [dbo].[DangKyThang] DROP CONSTRAINT [FK_DangKyThang_LoaiThanhToan]
GO
/****** Object:  Table [dbo].[Xe]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Xe]') AND type in (N'U'))
DROP TABLE [dbo].[Xe]
GO
/****** Object:  Table [dbo].[ViTriDauXe]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ViTriDauXe]') AND type in (N'U'))
DROP TABLE [dbo].[ViTriDauXe]
GO
/****** Object:  Table [dbo].[TheXeThang]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheXeThang]') AND type in (N'U'))
DROP TABLE [dbo].[TheXeThang]
GO
/****** Object:  Table [dbo].[TheXeNgay]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TheXeNgay]') AND type in (N'U'))
DROP TABLE [dbo].[TheXeNgay]
GO
/****** Object:  Table [dbo].[ThanhToan]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ThanhToan]') AND type in (N'U'))
DROP TABLE [dbo].[ThanhToan]
GO
/****** Object:  Table [dbo].[LoaiXe]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoaiXe]') AND type in (N'U'))
DROP TABLE [dbo].[LoaiXe]
GO
/****** Object:  Table [dbo].[LoaiThanhToan]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LoaiThanhToan]') AND type in (N'U'))
DROP TABLE [dbo].[LoaiThanhToan]
GO
/****** Object:  Table [dbo].[LichSuXe]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LichSuXe]') AND type in (N'U'))
DROP TABLE [dbo].[LichSuXe]
GO
/****** Object:  Table [dbo].[DangKyThang]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DangKyThang]') AND type in (N'U'))
DROP TABLE [dbo].[DangKyThang]
GO
/****** Object:  Table [dbo].[BaiDauXe]    Script Date: 02/03/2023 10:05:44 SA ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaiDauXe]') AND type in (N'U'))
DROP TABLE [dbo].[BaiDauXe]
GO
/****** Object:  Table [dbo].[BaiDauXe]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaiDauXe](
	[MaBaiDauXe] [int] NOT NULL,
	[MaLoaiXe] [int] NULL,
	[SoChoTrong] [int] NULL,
 CONSTRAINT [PK_BaiDauXe] PRIMARY KEY CLUSTERED 
(
	[MaBaiDauXe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DangKyThang]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DangKyThang](
	[MaDangKy] [int] NOT NULL,
	[HoTenKH] [nvarchar](150) NULL,
	[BienSoXe] [nvarchar](10) NULL,
	[ThoiGianDangKy] [smalldatetime] NULL,
	[LoaiXe] [int] NULL,
	[MaLoaiThanhToan] [int] NULL,
	[TrangThai] [bit] NULL,
	[SDT] [nchar](10) NULL,
	[Email] [nvarchar](150) NULL,
	[ThoiHan] [smalldatetime] NULL,
 CONSTRAINT [PK_DangKyThang] PRIMARY KEY CLUSTERED 
(
	[MaDangKy] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LichSuXe]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LichSuXe](
	[MaLichSu] [int] NOT NULL,
	[LuotVao] [nchar](10) NULL,
	[LuotRa] [nchar](10) NULL,
	[MaTheXeNgay] [int] NULL,
	[MaTheXeThang] [int] NULL,
	[MaThanhToan] [int] NULL,
 CONSTRAINT [PK_LichSuXe] PRIMARY KEY CLUSTERED 
(
	[MaLichSu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiThanhToan]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiThanhToan](
	[MaLoaiThanhToan] [int] NOT NULL,
	[TenLoai] [nvarchar](150) NULL,
 CONSTRAINT [PK_LoaiThanhToan] PRIMARY KEY CLUSTERED 
(
	[MaLoaiThanhToan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiXe]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiXe](
	[MaLoaiXe] [int] NOT NULL,
	[MaBaiDauXe] [int] NULL,
	[TenLoaiXe] [nvarchar](50) NULL,
 CONSTRAINT [PK_LoaiXe] PRIMARY KEY CLUSTERED 
(
	[MaLoaiXe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThanhToan]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThanhToan](
	[MaThanhToan] [int] NOT NULL,
	[MaTheXe] [int] NULL,
	[SoTien] [decimal](18, 0) NULL,
	[TrangThai] [bit] NULL,
	[MaLoaiThanhToan] [int] NULL,
 CONSTRAINT [PK_ThanhToan] PRIMARY KEY CLUSTERED 
(
	[MaThanhToan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TheXeNgay]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheXeNgay](
	[MaTheXeNgay] [int] NOT NULL,
	[MaViTri] [int] NULL,
	[GioVao] [smalldatetime] NULL,
	[GioRa] [smalldatetime] NULL,
	[TrangThai] [bit] NULL,
	[MaThanhToan] [int] NULL,
 CONSTRAINT [PK_TheXeNgay] PRIMARY KEY CLUSTERED 
(
	[MaTheXeNgay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TheXeThang]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TheXeThang](
	[MaTheXeThang] [int] NOT NULL,
	[MaThanhToan] [int] NULL,
	[MaLoaiXe] [int] NULL,
	[TrangThai] [bit] NULL,
	[GioVao] [smalldatetime] NULL,
	[GioRa] [smalldatetime] NULL,
	[ViTri] [int] NULL,
 CONSTRAINT [PK_TheXeThang] PRIMARY KEY CLUSTERED 
(
	[MaTheXeThang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ViTriDauXe]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ViTriDauXe](
	[MaViTri] [int] NOT NULL,
	[MaBaiDauXe] [int] NULL,
	[DonGia] [decimal](18, 0) NULL,
	[TrangThai] [bit] NULL,
	[SoGioDauXe] [int] NULL,
 CONSTRAINT [PK_ViTriDauXe] PRIMARY KEY CLUSTERED 
(
	[MaViTri] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Xe]    Script Date: 02/03/2023 10:05:44 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Xe](
	[BienSo] [nvarchar](10) NOT NULL,
	[MaTheXe] [int] NULL,
	[MaLoaiXe] [int] NULL,
	[HinhAnh] [nvarchar](max) NULL,
 CONSTRAINT [PK_Xe] PRIMARY KEY CLUSTERED 
(
	[BienSo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[DangKyThang]  WITH CHECK ADD  CONSTRAINT [FK_DangKyThang_LoaiThanhToan] FOREIGN KEY([MaLoaiThanhToan])
REFERENCES [dbo].[LoaiThanhToan] ([MaLoaiThanhToan])
GO
ALTER TABLE [dbo].[DangKyThang] CHECK CONSTRAINT [FK_DangKyThang_LoaiThanhToan]
GO
ALTER TABLE [dbo].[LichSuXe]  WITH CHECK ADD  CONSTRAINT [FK_LichSuXe_ThanhToan] FOREIGN KEY([MaThanhToan])
REFERENCES [dbo].[ThanhToan] ([MaThanhToan])
GO
ALTER TABLE [dbo].[LichSuXe] CHECK CONSTRAINT [FK_LichSuXe_ThanhToan]
GO
ALTER TABLE [dbo].[LichSuXe]  WITH CHECK ADD  CONSTRAINT [FK_LichSuXe_TheXeNgay] FOREIGN KEY([MaTheXeNgay])
REFERENCES [dbo].[TheXeNgay] ([MaTheXeNgay])
GO
ALTER TABLE [dbo].[LichSuXe] CHECK CONSTRAINT [FK_LichSuXe_TheXeNgay]
GO
ALTER TABLE [dbo].[LichSuXe]  WITH CHECK ADD  CONSTRAINT [FK_LichSuXe_TheXeThang] FOREIGN KEY([MaTheXeThang])
REFERENCES [dbo].[TheXeThang] ([MaTheXeThang])
GO
ALTER TABLE [dbo].[LichSuXe] CHECK CONSTRAINT [FK_LichSuXe_TheXeThang]
GO
ALTER TABLE [dbo].[LoaiXe]  WITH CHECK ADD  CONSTRAINT [FK_LoaiXe_BaiDauXe] FOREIGN KEY([MaBaiDauXe])
REFERENCES [dbo].[BaiDauXe] ([MaBaiDauXe])
GO
ALTER TABLE [dbo].[LoaiXe] CHECK CONSTRAINT [FK_LoaiXe_BaiDauXe]
GO
ALTER TABLE [dbo].[ThanhToan]  WITH CHECK ADD  CONSTRAINT [FK_ThanhToan_LoaiThanhToan] FOREIGN KEY([MaLoaiThanhToan])
REFERENCES [dbo].[LoaiThanhToan] ([MaLoaiThanhToan])
GO
ALTER TABLE [dbo].[ThanhToan] CHECK CONSTRAINT [FK_ThanhToan_LoaiThanhToan]
GO
ALTER TABLE [dbo].[TheXeNgay]  WITH CHECK ADD  CONSTRAINT [FK_TheXeNgay_ThanhToan] FOREIGN KEY([MaThanhToan])
REFERENCES [dbo].[ThanhToan] ([MaThanhToan])
GO
ALTER TABLE [dbo].[TheXeNgay] CHECK CONSTRAINT [FK_TheXeNgay_ThanhToan]
GO
ALTER TABLE [dbo].[TheXeNgay]  WITH CHECK ADD  CONSTRAINT [FK_TheXeNgay_ViTriDauXe] FOREIGN KEY([MaViTri])
REFERENCES [dbo].[ViTriDauXe] ([MaViTri])
GO
ALTER TABLE [dbo].[TheXeNgay] CHECK CONSTRAINT [FK_TheXeNgay_ViTriDauXe]
GO
ALTER TABLE [dbo].[TheXeThang]  WITH CHECK ADD  CONSTRAINT [FK_TheXeThang_LoaiXe] FOREIGN KEY([MaLoaiXe])
REFERENCES [dbo].[LoaiXe] ([MaLoaiXe])
GO
ALTER TABLE [dbo].[TheXeThang] CHECK CONSTRAINT [FK_TheXeThang_LoaiXe]
GO
ALTER TABLE [dbo].[TheXeThang]  WITH CHECK ADD  CONSTRAINT [FK_TheXeThang_ThanhToan] FOREIGN KEY([MaThanhToan])
REFERENCES [dbo].[ThanhToan] ([MaThanhToan])
GO
ALTER TABLE [dbo].[TheXeThang] CHECK CONSTRAINT [FK_TheXeThang_ThanhToan]
GO
ALTER TABLE [dbo].[ViTriDauXe]  WITH CHECK ADD  CONSTRAINT [FK_ViTriDauXe_BaiDauXe] FOREIGN KEY([MaBaiDauXe])
REFERENCES [dbo].[BaiDauXe] ([MaBaiDauXe])
GO
ALTER TABLE [dbo].[ViTriDauXe] CHECK CONSTRAINT [FK_ViTriDauXe_BaiDauXe]
GO
ALTER TABLE [dbo].[Xe]  WITH CHECK ADD  CONSTRAINT [FK_Xe_LoaiXe] FOREIGN KEY([MaLoaiXe])
REFERENCES [dbo].[LoaiXe] ([MaLoaiXe])
GO
ALTER TABLE [dbo].[Xe] CHECK CONSTRAINT [FK_Xe_LoaiXe]
GO
ALTER TABLE [dbo].[Xe]  WITH CHECK ADD  CONSTRAINT [FK_Xe_TheXeNgay] FOREIGN KEY([MaTheXe])
REFERENCES [dbo].[TheXeNgay] ([MaTheXeNgay])
GO
ALTER TABLE [dbo].[Xe] CHECK CONSTRAINT [FK_Xe_TheXeNgay]
GO

USE [QLBaiGiuXe]
go

SELECT * FROM BaiDauXe
go

INSERT INTO BaiDauXe VALUEs (1, 1, 100);
INSERT INTO BaiDauXe VALUEs (2, 1, 100);

SELECT * FROM Xe
go

INSERT INTO LoaiXe (MaLoaiXe, MaBaiDauXe, TenLoaiXe) VALUEs 
(1, 1, 'Xe may'), 
(2, 1, 'Xe oto'),
(3, 2, 'Xe may'), 
(4, 2, 'Xe oto');


SELECT * FROM ViTriDauXe;
INSERT INTO ViTriDauXe VALUES
(1, 1, 5000, 0, 0),
(2, 1, 5000, 0, 0),
(3, 1, 5000, 0, 0),
(4, 1, 5000, 0, 0),
(5, 1, 5000, 0, 0),
(6, 1, 5000, 0, 0),
(7, 1, 5000, 0, 0),
(8, 1, 5000, 0, 0),
(9, 1, 5000, 0, 0),
(10, 1, 6000, 0, 0),
(11, 1, 6000, 0, 0),
(12, 1, 6000, 0, 0),
(13, 1, 6000, 0, 0),
(14, 1, 6000, 0, 0),
(15, 1, 6000, 0, 0),
(16, 1, 6000, 0, 0),
(17, 1, 6000, 0, 0),
(18, 1, 6000, 0, 0),
(19, 1, 6000, 0, 0),
(20, 1, 10000, 0, 0);



SELECT * FROM TheXeNgay;
INSERT INTO TheXeNgay (MaTheXeNgay, MaViTri, GioVao, GioRa, TrangThai, MaThanhToan) VALUES
(1, 1, NULL, NULL, 0, NULL),
(2, 2, NULL, NULL, 0, NULL),
(3, 3, NULL, NULL, 0, NULL),
(4, 4, NULL, NULL, 0, NULL),
(5, 5, NULL, NULL, 0, NULL),
(6, 6, NULL, NULL, 0, NULL),
(7, 7, NULL, NULL, 0, NULL),
(8, 8, NULL, NULL, 0, NULL),
(9, 9, NULL, NULL, 0, NULL),
(10, 10, NULL, NULL, 0, NULL),
(11, 11, NULL, NULL, 0, NULL),
(12, 12, NULL, NULL, 0, NULL),
(13, 13, NULL, NULL, 0, NULL),
(14, 14, NULL, NULL, 0, NULL),
(15, 15, NULL, NULL, 0, NULL),
(16, 16, NULL, NULL, 0, NULL),
(17, 17, NULL, NULL, 0, NULL),
(18, 18, NULL, NULL, 0, NULL),
(19, 19, NULL, NULL, 0, NULL),
(20, 20, NULL, NULL, 0, NULL);

SELECT * FROM ThanhToan;
SELECT * FROM LoaiThanhToan;
INSERT INTO LoaiThanhToan VALUES
(1, N'Tiền mặt'),
(2, N'Barcode'),
(3, N'Thẻ');