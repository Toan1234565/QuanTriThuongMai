CREATE TABLE admins (
    admin_id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    role VARCHAR(20) NOT NULL DEFAULT 'admin',
    status VARCHAR(20) NOT NULL DEFAULT 'active'
);

CREATE TABLE KhuyenMai (
    KhuyenMaiid INT PRIMARY KEY IDENTITY(1,1),
    TenKhuyenMai VARCHAR(100) NOT NULL,
    Mota TEXT,
    NgayBD DATE NOT NULL,
   NgayKT DATE NOT NULL,
    LoaiKM VARCHAR(50) NOT NULL,
    DieuKien TEXT,
   SanPhamApDung TEXT
);
