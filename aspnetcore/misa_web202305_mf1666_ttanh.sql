-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th7 16, 2023 lúc 11:47 AM
-- Phiên bản máy phục vụ: 10.4.28-MariaDB
-- Phiên bản PHP: 8.1.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `misa.web202305_mf1666_ttanh`
--
CREATE DATABASE IF NOT EXISTS `misa.web202305_mf1666_ttanh` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `misa.web202305_mf1666_ttanh`;

DELIMITER $$
--
-- Thủ tục
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `Proc_Department_Filter` (IN `pageSize` INT, IN `pageNumber` INT, IN `searchText` VARCHAR(255))  SQL SECURITY INVOKER COMMENT 'Procedure lọc phòng ban.' BEGIN
  DECLARE startIndex int;
  SET startIndex = (pageNumber - 1) * pageSize;
  
  SELECT *
  FROM department d
  WHERE 
    (searchText IS NULL 
      OR (d.DepartmentCode LIKE CONCAT('%', searchText, '%')
          OR d.DepartmentName LIKE CONCAT('%', searchText, '%')
         )
    )
  LIMIT startIndex, pageSize;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Proc_Department_Insert` (IN `DepartmentId` CHAR(36), IN `DepartmentCode` VARCHAR(20), IN `DepartmentName` VARCHAR(100), IN `CreatedDate` DATETIME, IN `CreatedBy` VARCHAR(100))  SQL SECURITY INVOKER COMMENT 'Procedure thêm phòng ban.' BEGIN
  INSERT INTO department (DepartmentId, DepartmentCode, DepartmentName, CreatedDate, CreatedBy)
  VALUES (DepartmentId, DepartmentCode, DepartmentName, CreatedDate, CreatedBy);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Proc_Department_Update` (IN `DepartmentId` CHAR(36), IN `DepartmentCode` VARCHAR(20), IN `DepartmentName` VARCHAR(100), IN `ModifiedDate` DATETIME, IN `ModifiedBy` VARCHAR(255), IN `id` CHAR(36))  SQL SECURITY INVOKER COMMENT 'Procedure cập nhật thông tin phòng ban.' BEGIN
  UPDATE department d 
  SET d.DepartmentId = DepartmentId,
      d.DepartmentCode = DepartmentCode,
      d.DepartmentName = DepartmentName,
      d.ModifiedDate = ModifiedDate,
      d.ModifiedBy = ModifiedBy
  WHERE d.DepartmentId = id;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Proc_Employee_Filter` (IN `pageSize` INT, IN `pageNumber` INT, IN `searchText` VARCHAR(255))  SQL SECURITY INVOKER COMMENT 'Procedure lọc nhân viên.' BEGIN
  DECLARE startIndex int;
  SET startIndex = (pageNumber - 1) * pageSize;
  
  SELECT *
  FROM employee e
  WHERE 
    (searchText IS NULL 
      OR (e.EmployeeCode LIKE CONCAT('%', searchText, '%')
          OR e.FullName LIKE CONCAT('%', searchText, '%') 
          OR e.DepartmentCode LIKE CONCAT('%', searchText, '%') 
          OR e.DepartmentName LIKE CONCAT('%', searchText, '%') 
          OR e.IdentityNumber LIKE CONCAT('%', searchText, '%') 
          OR e.`Position` LIKE CONCAT('%', searchText, '%') 
          OR e.PersonalTaxCode LIKE CONCAT('%', searchText, '%') 
          OR e.TypeOfContract LIKE CONCAT('%', searchText, '%') 
          OR e.AccountNumber LIKE CONCAT('%', searchText, '%') 
          OR e.BankName LIKE CONCAT('%', searchText, '%') 
          OR e.BankBranch LIKE CONCAT('%', searchText, '%') 
          OR e.BankProvince LIKE CONCAT('%', searchText, '%') 
          OR e.ContactAddress LIKE CONCAT('%', searchText, '%') 
          OR e.ContactPhoneNumber LIKE CONCAT('%', searchText, '%') 
          OR e.ContactLandlinePhoneNumber LIKE CONCAT('%', searchText, '%') 
          OR e.ContactEmail LIKE CONCAT('%', searchText, '%')
         )
    )
  LIMIT startIndex, pageSize;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Proc_Employee_Insert` (IN `EmployeeId` CHAR(36), IN `EmployeeCode` VARCHAR(20), IN `FullName` VARCHAR(100), IN `DepartmentId` CHAR(36), IN `DepartmentCode` VARCHAR(20), IN `DepartmentName` VARCHAR(255), IN `Gender` INT(2), IN `DateOfBirth` DATE, IN `Position` VARCHAR(100), IN `SupplierCustomerGroup` VARCHAR(255), IN `IdentityNumber` VARCHAR(25), IN `IdentityDate` DATE, IN `IdentityPlace` VARCHAR(255), IN `PayAccount` VARCHAR(100), IN `ReceiveAccount` VARCHAR(100), IN `Salary` DECIMAL(18,4), IN `SalaryCoefficients` DECIMAL(18,4), IN `SalaryPaidForInsurance` DECIMAL(18,4), IN `PersonalTaxCode` VARCHAR(25), IN `TypeOfContract` VARCHAR(25), IN `NumberOfDependents` INT(10), IN `AccountNumber` VARCHAR(25), IN `BankName` VARCHAR(255), IN `BankBranch` VARCHAR(255), IN `BankProvince` VARCHAR(255), IN `ContactAddress` VARCHAR(255), IN `ContactPhoneNumber` VARCHAR(50), IN `ContactLandlinePhoneNumber` VARCHAR(50), IN `ContactEmail` VARCHAR(100), IN `CreatedDate` DATETIME, IN `CreatedBy` VARCHAR(100))  SQL SECURITY INVOKER COMMENT 'Procedure thêm nhân viên.' BEGIN
  INSERT INTO employee (EmployeeId, EmployeeCode, FullName, DepartmentId, DepartmentCode, DepartmentName, Gender, DateOfBirth, `Position`, SupplierCustomerGroup, IdentityNumber, IdentityDate, IdentityPlace, PayAccount, ReceiveAccount, Salary, SalaryCoefficients, SalaryPaidForInsurance, PersonalTaxCode, TypeOfContract, NumberOfDependents, AccountNumber, BankName, BankBranch, BankProvince, ContactAddress, ContactPhoneNumber, ContactLandlinePhoneNumber, ContactEmail, CreatedDate, CreatedBy)
  VALUES (EmployeeId, EmployeeCode, FullName, DepartmentId, DepartmentCode, DepartmentName, Gender, DateOfBirth, Position, SupplierCustomerGroup, IdentityNumber, IdentityDate, IdentityPlace, PayAccount, ReceiveAccount, Salary, SalaryCoefficients, SalaryPaidForInsurance, PersonalTaxCode, TypeOfContract, NumberOfDependents, AccountNumber, BankName, BankBranch, BankProvince, ContactAddress, ContactPhoneNumber, ContactLandlinePhoneNumber, ContactEmail, CreatedDate, CreatedBy);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Proc_Employee_Update` (IN `EmployeeId` CHAR(36), IN `EmployeeCode` VARCHAR(20), IN `FullName` VARCHAR(100), IN `DepartmentId` CHAR(36), IN `DepartmentCode` VARCHAR(20), IN `DepartmentName` VARCHAR(255), IN `Gender` INT(2), IN `DateOfBirth` DATE, IN `Position` VARCHAR(100), IN `SupplierCustomerGroup` VARCHAR(255), IN `IdentityNumber` VARCHAR(25), IN `IdentityDate` DATE, IN `IdentityPlace` VARCHAR(255), IN `PayAccount` VARCHAR(100), IN `ReceiveAccount` VARCHAR(100), IN `Salary` DECIMAL(18,4), IN `SalaryCoefficients` DECIMAL(18,4), IN `SalaryPaidForInsurance` DECIMAL(18,4), IN `PersonalTaxCode` VARCHAR(25), IN `TypeOfContract` VARCHAR(25), IN `NumberOfDependents` INT(10), IN `AccountNumber` VARCHAR(25), IN `BankName` VARCHAR(255), IN `BankBranch` VARCHAR(255), IN `BankProvince` VARCHAR(255), IN `ContactAddress` VARCHAR(255), IN `ContactPhoneNumber` VARCHAR(50), IN `ContactLandlinePhoneNumber` VARCHAR(50), IN `ContactEmail` VARCHAR(100), IN `ModifiedDate` DATETIME, IN `ModifiedBy` VARCHAR(255), IN `id` CHAR(36))  MODIFIES SQL DATA SQL SECURITY INVOKER COMMENT 'Procedure cập nhật thông tin nhân viên.' BEGIN
  UPDATE employee e
  SET e.EmployeeCode = EmployeeCode,
    e.FullName = FullName,
    e.DepartmentId = DepartmentId,
    e.DepartmentCode = DepartmentCode,
    e.DepartmentName = DepartmentName,
    e.Gender = Gender,
    e.DateOfBirth = DateOfBirth,
    e.`Position` = Position,
    e.SupplierCustomerGroup = SupplierCustomerGroup,
    e.IdentityNumber = IdentityNumber,
    e.IdentityDate = IdentityDate,
    e.IdentityPlace = IdentityPlace,
    e.PayAccount = PayAccount,
    e.ReceiveAccount = ReceiveAccount,
    e.Salary = Salary,
    e.SalaryCoefficients = SalaryCoefficients,
    e.SalaryPaidForInsurance = SalaryPaidForInsurance,
    e.PersonalTaxCode = PersonalTaxCode,
    e.TypeOfContract = TypeOfContract,
    e.NumberOfDependents = NumberOfDependents,
    e.AccountNumber = AccountNumber,
    e.BankName = BankName,
    e.BankBranch = BankBranch,
    e.BankProvince = BankProvince,
    e.ContactAddress = ContactAddress,
    e.ContactPhoneNumber = ContactPhoneNumber,
    e.ContactLandlinePhoneNumber = ContactLandlinePhoneNumber,
    e.ContactEmail = ContactEmail,
    e.ModifiedDate = ModifiedDate,
    e.ModifiedBy = ModifiedBy
  WHERE e.EmployeeId = id;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `department`
--

CREATE TABLE `department` (
  `DepartmentId` char(36) NOT NULL COMMENT 'Id phòng ban',
  `DepartmentCode` varchar(20) NOT NULL COMMENT 'Mã phòng ban',
  `DepartmentName` varchar(255) NOT NULL COMMENT 'Tên phòng ban',
  `CreatedDate` datetime NOT NULL COMMENT 'Ngày tạo',
  `CreatedBy` varchar(100) DEFAULT NULL COMMENT 'Tạo bởi',
  `ModifiedDate` datetime DEFAULT NULL COMMENT 'Ngày sửa',
  `ModifiedBy` varchar(255) DEFAULT NULL COMMENT 'Sửa bởi'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Thông tin phòng ban';

--
-- Đang đổ dữ liệu cho bảng `department`
--

INSERT INTO `department` (`DepartmentId`, `DepartmentCode`, `DepartmentName`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy`) VALUES
('142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', '1970-01-01 00:07:13', 'Letha Bolt', '1970-01-01 00:00:04', 'Abraham Acevedo'),
('17120d02-6ab5-3e43-18cb-66948daf6128', 'PB-9483', 'Phòng đào tạo', '2001-10-05 03:39:34', 'Miyoko Mckinney', '1971-05-16 09:45:54', 'Hong Beaudoin'),
('469b3ece-744a-45d5-957d-e8c757976496', 'PB-5222', 'Phòng sản xuất', '2019-06-30 12:29:53', 'Vanita Kelleher', '1975-01-31 18:53:18', 'Retta Lord'),
('4e272fc4-7875-78d6-7d32-6a1673ffca7c', 'PB-2675', 'Phòng hành chính', '2015-02-25 11:35:34', 'Marcos Abraham', '1970-01-01 00:00:08', 'Treena Lind'),
('80de23af-ea3d-45cf-bc91-bfa0ef4e3677', 'CHange-1', 'Sửa phòng ban', '2023-07-16 16:45:18', 'TTANH', '2023-07-16 16:46:07', 'ANHTT');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `employee`
--

CREATE TABLE `employee` (
  `EmployeeId` char(36) NOT NULL COMMENT 'Id nhân viên',
  `EmployeeCode` varchar(20) NOT NULL COMMENT 'Mã nhân viên',
  `FullName` varchar(100) NOT NULL COMMENT 'Tên nhân viên',
  `DepartmentId` char(36) NOT NULL COMMENT 'Id phòng ban',
  `DepartmentCode` varchar(20) NOT NULL COMMENT 'Mã code phòng ban',
  `DepartmentName` varchar(255) NOT NULL COMMENT 'Tên phòng ban',
  `Gender` int(2) DEFAULT NULL COMMENT 'Giới tính (0 - Nam; 1 - Nữ; 2 - Khác)',
  `DateOfBirth` date DEFAULT NULL COMMENT 'Ngày sinh',
  `Position` varchar(100) DEFAULT NULL COMMENT 'Chức danh',
  `SupplierCustomerGroup` varchar(255) DEFAULT NULL COMMENT 'Nhóm khách hàng, nhà cung cấp',
  `IdentityNumber` varchar(25) DEFAULT NULL COMMENT 'Số CMND',
  `IdentityDate` date DEFAULT NULL COMMENT 'Ngày cấp',
  `IdentityPlace` varchar(255) DEFAULT NULL COMMENT 'Nơi cấp',
  `PayAccount` varchar(100) DEFAULT NULL COMMENT 'TK công nợ phải trả',
  `ReceiveAccount` varchar(100) DEFAULT NULL COMMENT 'TK công nợ phải thu',
  `Salary` decimal(18,4) DEFAULT NULL COMMENT 'Lương thỏa thuận',
  `SalaryCoefficients` decimal(18,4) DEFAULT NULL COMMENT 'Hệ số lương',
  `SalaryPaidForInsurance` decimal(18,4) DEFAULT NULL COMMENT 'Lương đóng bảo hiểm',
  `PersonalTaxCode` varchar(25) DEFAULT NULL COMMENT 'Mã số thuế',
  `TypeOfContract` varchar(25) DEFAULT NULL COMMENT 'Loại hợp đồng',
  `NumberOfDependents` int(10) DEFAULT NULL COMMENT 'Số người phụ thuộc',
  `AccountNumber` varchar(25) DEFAULT NULL COMMENT 'Số tài khoản ngân hàng',
  `BankName` varchar(255) DEFAULT NULL COMMENT 'Tên ngân hàng',
  `BankBranch` varchar(255) DEFAULT NULL COMMENT 'Chi nhánh ngân hàng',
  `BankProvince` varchar(255) DEFAULT NULL COMMENT 'Tỉnh/TP của ngân hàng',
  `ContactAddress` varchar(255) DEFAULT NULL COMMENT 'Địa chỉ',
  `ContactPhoneNumber` varchar(50) DEFAULT NULL COMMENT 'Điện thoại di động',
  `ContactLandlinePhoneNumber` varchar(50) DEFAULT NULL COMMENT 'ĐT cố định',
  `ContactEmail` varchar(100) DEFAULT NULL COMMENT 'Email',
  `CreatedDate` datetime DEFAULT NULL COMMENT 'Ngày tạo',
  `CreatedBy` varchar(100) DEFAULT NULL COMMENT 'Người tạo',
  `ModifiedDate` datetime DEFAULT NULL COMMENT 'Ngày sửa',
  `ModifiedBy` varchar(100) DEFAULT NULL COMMENT 'Người sửa'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci COMMENT='Thông tin nhân viên';

--
-- Đang đổ dữ liệu cho bảng `employee`
--

INSERT INTO `employee` (`EmployeeId`, `EmployeeCode`, `FullName`, `DepartmentId`, `DepartmentCode`, `DepartmentName`, `Gender`, `DateOfBirth`, `Position`, `SupplierCustomerGroup`, `IdentityNumber`, `IdentityDate`, `IdentityPlace`, `PayAccount`, `ReceiveAccount`, `Salary`, `SalaryCoefficients`, `SalaryPaidForInsurance`, `PersonalTaxCode`, `TypeOfContract`, `NumberOfDependents`, `AccountNumber`, `BankName`, `BankBranch`, `BankProvince`, `ContactAddress`, `ContactPhoneNumber`, `ContactLandlinePhoneNumber`, `ContactEmail`, `CreatedDate`, `CreatedBy`, `ModifiedDate`, `ModifiedBy`) VALUES
('4b35daf6-99cb-4d8f-bffc-ebe5a47c79e7', 'A123', 'string', '17120d02-6ab5-3e43-18cb-66948daf6128', 'PB-9483', 'Phòng đào tạo', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 18:32:43', 'TTANH', '2023-07-15 21:21:05', 'ANHTT'),
('4bf649df-5d61-4487-9e8b-9e6d8a82651a', 'NV-1125', 'Trần Thế Anh', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', 1, '2002-06-09', 'Giám đốc', 'cung cap vip', '01233556', '2022-12-12', 'Nam Định', '12', '12', 10293.0000, 1.0000, 1.0000, '10923903', '123123', 1, '91111166', 'BIDV', 'Nam ĐỊnh', 'Nam ĐỊnh', 'Nam Định', '01111111111', '01111111111', 'theanh@gmail.com', '2023-07-14 18:39:00', 'TTANH', '2023-07-15 21:55:33', 'ANHTT'),
('5e622b4c-f5e1-4789-9aca-958dc0e16ad0', 'KT-1001', 'aaa', '4e272fc4-7875-78d6-7d32-6a1673ffca7c', 'PB-2675', 'Phòng hành chính', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-15 09:30:21', 'TTANH', NULL, NULL),
('6da27ec3-ceb0-45fb-bce6-64eb6ebbddb3', 'NV-2243', 'Thế anh test last', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', 2, NULL, 'Giám đốc', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 12:23:42', 'TTANH', NULL, NULL),
('7116b737-fb8c-49c1-b3ce-73cd9d66f460', 'NV-3815', 'New employee code 2', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-13 18:41:13', 'TTANH', NULL, NULL),
('72475332-6864-43e5-a42a-b8bbaaf74023', 'BA-1111', 'Thế Anh Base', '469b3ece-744a-45d5-957d-e8c757976496', 'PB-5222', 'Phòng sản xuất', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-15 15:44:02', 'TTANH', NULL, NULL),
('757ac98f-04ed-4a75-acda-f9bb64f8b304', 'TA-1234', 'Thế Anh đẹp trai', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-12 22:42:37', 'TTANH', '2023-07-13 09:07:45', 'ANHTT'),
('77dcfb30-862f-4778-b42e-7ef94e680af9', 'NV-3814', 'New employee code 2', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-13 18:39:28', 'TTANH', NULL, NULL),
('7aacd741-e279-4a62-8141-f902343d3300', 'NV-104', 'a', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 14:08:24', 'TTANH', NULL, NULL),
('857333a9-27f9-476b-9042-5a704b80cb01', 'NV-999', 'New employee code 3', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 08:21:15', 'TTANH', NULL, NULL),
('91745d4e-f454-4a7e-b90a-aed7c33c836c', 'TA-4221', 'Thế Anh ngầu', '469b3ece-744a-45d5-957d-e8c757976496', 'PB-5222', 'Phòng sản xuất', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-13 16:45:59', 'TTANH', NULL, NULL),
('94214889-ec2f-4439-b129-e3ad032ee5ad', 'NV-3813', 'New employee code 2', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-13 18:28:23', 'TTANH', NULL, NULL),
('9f600cf1-2c9c-4e8a-ad9e-5273b4160a6c', 'NV-99', 'Thế anh test last 2', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', 2, NULL, 'Giám đốc', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 12:36:49', 'TTANH', NULL, NULL),
('a68c971d-8aef-40db-a9ac-77dcdaaf00d8', 'NV-9999', 'New employee code 3', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 08:21:38', 'TTANH', NULL, NULL),
('b0ef630e-8bd3-4036-b7e2-82a2e74669c4', 'NV-107', 'a', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 14:33:26', 'TTANH', NULL, NULL),
('b1751c73-68c4-4770-acc3-d711ff9c08ff', 'NV-101', 'Nhân viên test', '4e272fc4-7875-78d6-7d32-6a1673ffca7c', 'PB-2675', 'Phòng hành chính', 1, '2002-01-01', 'Phó phòng', '', '091235538', '2012-02-09', 'Nam Định', '', '', 1000.0000, 1.0000, 1.0000, '1', '1', 1, NULL, 'ACB', 'Nam Từ Liêm', 'Hà Nội', 'Hà NỘi', '091239121', '091239121', 'theanh@gmail.com', '2023-07-14 13:46:30', 'TTANH', NULL, NULL),
('b8ce4561-6894-4e7e-8804-90b802a0ae6c', 'NV-3817', 'New employee code 2', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-13 18:39:19', 'TTANH', NULL, NULL),
('cad48d5b-e582-4553-988c-da9eeca9c0e8', 'NV-105', 'ádfkáldfj', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', 0, '2002-06-09', 'thăklfjdlskạ', NULL, 'thkạdfkjdfk', '1970-01-01', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 14:10:41', 'TTANH', NULL, NULL),
('d01a2202-377b-4e21-aa22-886dfa02508a', 'NV-1111', 'thế anh vui vẻ', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', 2, NULL, 'Giám đốc', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 11:55:07', 'TTANH', '2023-07-14 11:56:30', 'ANHTT'),
('d4676db8-9a0a-42a0-a1e6-dad66d71b1be', 'NV-100', 'thế anh trần', '17120d02-6ab5-3e43-18cb-66948daf6128', 'PB-9483', 'Phòng đào tạo', 0, '2002-06-09', 'Giám đốc điều hành', '', '036202012169', '2023-01-01', 'Nam Định', '', '', 10000000.0000, 1.0000, 100000.0000, '012345', 'vip', 3, NULL, 'BIDV', 'Cầu Giấy', 'Hà Nội', 'Nam Định', '0912945494', '0912945494', 'theanh090602@gmail.com', '2023-07-14 13:06:12', 'TTANH', '2023-07-14 13:06:52', 'ANHTT'),
('dc8d7869-852b-4e93-8c3b-abcd4bd3991c', 'KT-1002', 'thêm nhân viên', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-15 09:36:03', 'TTANH', NULL, NULL),
('e8e6782c-9a63-4219-a6cd-4b7d7ef025d8', 'BA-1112', 'insert base', '469b3ece-744a-45d5-957d-e8c757976496', 'PB-5222', 'Phòng sản xuất', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-15 15:49:41', 'TTANH', NULL, NULL),
('e8faf240-96a0-4208-990c-305c59d072c2', 'NV-99999', 'Thế Anh vui vẻ', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 11:28:35', 'TTANH', NULL, NULL),
('f2901552-0d40-4bbf-ab50-7d0ae2d8bf1a', 'NV-106', 'a', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 14:33:04', 'TTANH', NULL, NULL),
('fb0c3086-fec4-4921-ba4d-e18d5c56efeb', 'NV-3819', 'New employee code 3', '142cb08f-7c31-21fa-8e90-67245e8b283e', 'PB-0460', 'Phòng nhân sự', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-07-14 08:20:59', 'TTANH', NULL, NULL);

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`DepartmentId`),
  ADD UNIQUE KEY `UK_department_DepartmentCode` (`DepartmentCode`),
  ADD KEY `IDX_department_DepartmentName` (`DepartmentName`);

--
-- Chỉ mục cho bảng `employee`
--
ALTER TABLE `employee`
  ADD PRIMARY KEY (`EmployeeId`),
  ADD UNIQUE KEY `UK_employee_EmployeeCode` (`EmployeeCode`),
  ADD KEY `FK_employee_DepartmentId` (`DepartmentId`),
  ADD KEY `IDX_employee_EmployeeName` (`FullName`);

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `employee`
--
ALTER TABLE `employee`
  ADD CONSTRAINT `FK_employee_DepartmentId` FOREIGN KEY (`DepartmentId`) REFERENCES `department` (`DepartmentId`) ON DELETE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
