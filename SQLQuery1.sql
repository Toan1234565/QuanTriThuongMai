-- Đảm bảo không có kết nối nào đang sử dụng cơ sở dữ liệu
USE master;
ALTER DATABASE QuanLyDuLieu SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

-- Xóa cơ sở dữ liệu nếu đã tồn tại
DROP DATABASE IF EXISTS QuanLyDuLieu;

-- Tạo mới cơ sở dữ liệu
CREATE DATABASE QuanLyDuLieu;
USE QuanLyDuLieu;

-- Kiểm tra và xóa các bảng nếu đã tồn tại
IF OBJECT_ID('SanPham', 'U') IS NOT NULL DROP TABLE SanPham;
IF OBJECT_ID('ChiTietSanPham', 'U') IS NOT NULL DROP TABLE ChiTietSanPham;
IF OBJECT_ID('DanhMuc', 'U') IS NOT NULL DROP TABLE DanhMuc;
IF OBJECT_ID('Hang', 'U') IS NOT NULL DROP TABLE Hang;
IF OBJECT_ID('HinhAnh', 'U') IS NOT NULL DROP TABLE HinhAnh;
IF OBJECT_ID('GioHang', 'U') IS NOT NULL DROP TABLE GioHang;
IF OBJECT_ID('ThanhToan', 'U') IS NOT NULL DROP TABLE ThanhToan;
IF OBJECT_ID('DonHang', 'U') IS NOT NULL DROP TABLE DonHang;
IF OBJECT_ID('KhachHang', 'U') IS NOT NULL DROP TABLE KhachHang;
IF OBJECT_ID('LichSuDonHang', 'U') IS NOT NULL DROP TABLE LichSuDonHang;
IF OBJECT_ID('TimKiem', 'U') IS NOT NULL DROP TABLE TimKiem;
IF OBJECT_ID('NoiDung', 'U') IS NOT NULL DROP TABLE NoiDung;
IF OBJECT_ID('MangXaHoi', 'U') IS NOT NULL DROP TABLE MangXaHoi;
IF OBJECT_ID('HoTroKhachHang', 'U') IS NOT NULL DROP TABLE HoTroKhachHang;
IF OBJECT_ID('QuanTriHeThong', 'U') IS NOT NULL DROP TABLE QuanTriHeThong;
IF OBJECT_ID('BaoCaoThongKe', 'U') IS NOT NULL DROP TABLE BaoCaoThongKe;
IF OBJECT_ID('CaiDatHeThong', 'U') IS NOT NULL DROP TABLE CaiDatHeThong;

-- Bảng quản lý sản phẩm
CREATE TABLE SanPham (
    SanPhamID INT PRIMARY KEY IDENTITY(1,1),
    TenSanPham NVARCHAR(100),
	 Soluong INT,
    MoTa NVARCHAR(500),
    Gia FLOAT,
    DanhMucID INT,
    HangID INT,
    HinhAnh NVARCHAR(200),
    KhuyenMai NVARCHAR(100)
);
-- Thêm bảng chi tiết cấu hình sản phẩm
CREATE TABLE ChiTietSanPham (
    ChiTietSanPhamID INT PRIMARY KEY IDENTITY(1,1),
    SanPhamID INT,
    ManHinh NVARCHAR(100),
    HeDieuHanh NVARCHAR(100),
    CameraTruoc NVARCHAR(100),
    CameraSau NVARCHAR(100),
    Chip NVARCHAR(100),
    RAM NVARCHAR(100),
    BoNhoTrong NVARCHAR(100),
    Sim NVARCHAR(100),
    Pin NVARCHAR(100),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID)
);

-- Bảng quản lý khách hàng
CREATE TABLE KhachHang (
    KhachHangID INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    MatKhau NVARCHAR(100),
    NgaySinh DATE,
    DiaChi NVARCHAR(200),
    SoDienThoai NVARCHAR(20)
);

CREATE TABLE DanhMuc (
    DanhMucID INT PRIMARY KEY IDENTITY(1,1),
    TenDanhMuc NVARCHAR(100)
);

CREATE TABLE Hang (
    HangID INT PRIMARY KEY IDENTITY(1,1),
    TenHang NVARCHAR(100)
);

CREATE TABLE HinhAnh (
    HinhAnhID INT PRIMARY KEY IDENTITY(1,1),
    SanPhamID INT,
    DuongDan NVARCHAR(200),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID)
);

-- Bảng giỏ hàng và thanh toán
CREATE TABLE GioHang (
    GioHangID INT PRIMARY KEY IDENTITY(1,1),
    KhachHangID INT,
    SanPhamID INT,
    SoLuong INT,
    TongTien FLOAT,
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID)
);

CREATE TABLE ThanhToan (
    ThanhToanID INT PRIMARY KEY IDENTITY(1,1),
    KhachHangID INT,
    PhuongThuc NVARCHAR(50),
    NgayThanhToan DATE,
    TongTien FLOAT,
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID)
);

-- Bảng quản lý đơn hàng
CREATE TABLE DonHang (
    DonHangID INT PRIMARY KEY IDENTITY(1,1),
    KhachHangID INT,
    NgayDatHang DATE,
    TrangThai NVARCHAR(50),
    TongTien FLOAT,
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID)
);

CREATE TABLE LichSuDonHang (
    LichSuDonHangID INT PRIMARY KEY IDENTITY(1,1),
    KhachHangID INT,
    DonHangID INT,
    NgayDatHang DATE,
    TrangThai NVARCHAR(50),
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID),
    FOREIGN KEY (DonHangID) REFERENCES DonHang(DonHangID)
);

-- Bảng tìm kiếm và lọc sản phẩm
CREATE TABLE TimKiem (
    TimKiemID INT PRIMARY KEY IDENTITY(1,1),
    SanPhamID INT,
    TenSanPham NVARCHAR(100),
    MaSanPham NVARCHAR(50),
    DanhMucID INT,
    Gia FLOAT,
    HangID INT,
    CauHinh NVARCHAR(200),
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID),
    FOREIGN KEY (DanhMucID) REFERENCES DanhMuc(DanhMucID),
    FOREIGN KEY (HangID) REFERENCES Hang(HangID)
);

-- Bảng nội dung
CREATE TABLE NoiDung (
    NoiDungID INT PRIMARY KEY IDENTITY(1,1),
    TieuDe NVARCHAR(100),
    NoiDung TEXT,
    LoaiNoiDung NVARCHAR(50)
);

-- Bảng tích hợp mạng xã hội
CREATE TABLE MangXaHoi (
    MXHID INT PRIMARY KEY IDENTITY(1,1),
    KhachHangID INT,
    TenMangXaHoi NVARCHAR(50),
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID)
);

-- Bảng hỗ trợ khách hàng
CREATE TABLE HoTroKhachHang (
    HoTroID INT PRIMARY KEY IDENTITY(1,1),
    KhachHangID INT,
    LoaiHoTro NVARCHAR(50),
    NoiDungHoTro TEXT,
    NgayHoTro DATE,
    TrangThai NVARCHAR(50),
    FOREIGN KEY (KhachHangID) REFERENCES KhachHang(KhachHangID)
);

-- Bảng quản trị hệ thống
CREATE TABLE QuanTriHeThong (
    QTHID INT PRIMARY KEY IDENTITY(1,1),
    TenQTH NVARCHAR(100),
    MoTa NVARCHAR(500)
);

-- Bảng báo cáo và thống kê
CREATE TABLE BaoCaoThongKe (
    BaoCaoID INT PRIMARY KEY IDENTITY(1,1),
    LoaiBaoCao NVARCHAR(50),
    NoiDungBaoCao TEXT,
    NgayBaoCao DATE
);

-- Bảng cài đặt hệ thống và bảo mật
CREATE TABLE CaiDatHeThong (
    CaiDatID INT PRIMARY KEY IDENTITY(1,1),
    TenCaiDat NVARCHAR(100),
    GiaTri NVARCHAR(100)
);

-- Đặt lại cơ sở dữ liệu về chế độ multi-user
ALTER DATABASE QuanLyDuLieu SET MULTI_USER;
