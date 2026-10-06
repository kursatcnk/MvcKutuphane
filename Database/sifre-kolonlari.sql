-- Şifreler PBKDF2 özeti olarak saklandığı için sifre sütunları 100 karaktere çıkarılıyor.
-- Uygulama açılırken bunu kendisi de yapıyor (Global.asax.cs).
IF COL_LENGTH('TblUyeler', 'sifre') < 100 ALTER TABLE TblUyeler ALTER COLUMN sifre varchar(100) NULL;
IF COL_LENGTH('TblAdmin', 'sifre') < 100 ALTER TABLE TblAdmin ALTER COLUMN sifre varchar(100) NULL;
