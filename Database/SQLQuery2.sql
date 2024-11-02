-- Bảng tồn kho
CREATE TABLE TonKho (
    TonKhoID INT PRIMARY KEY IDENTITY(1,1),
    SanPhamID INT,
    SoLuongTon INT,
    NgayCapNhat DATE,
    FOREIGN KEY (SanPhamID) REFERENCES SanPham(SanPhamID)
);