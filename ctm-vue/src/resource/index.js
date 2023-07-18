const MISAResource = {
  VN: {
    CustomerCodeInvalidError: {
      NewCodeError: "Lấy mã mới thất bại, vui lòng liên hệ với MISA.",
      Empty: "Mã không được phép để trống.",
    },
    FullNameInvalidError: {
      Empty: "Tên không được phép để trống.",
    },
    DepartmentInvalidError: {
      Empty: "Đơn vị không được phép để trống.",
      NotFound: "Dữ liệu <Đơn vị> không có trong danh mục.",
    },
    EmailInvalidError: {
      Format: "Email không đúng định dạng.",
    },

    ToastTitle: {
      Info: "Thông tin!",
      Warning: "Cảnh báo!",
      Error: "Lỗi!",
      Success: "Thành công!",
    },

    Paging: {
      Title: " bản ghi trên 1 trang",
      Error: " lỗi phân trang",
    },

    DepartmentError: {
      Get: "Lấy thông tin phòng ban bị lỗi.",
    },

    EmployeeError: {
      Get: "Lấy thông tin người dùng bị lỗi.",
    },
    AddEmployee: {
      Success: "Nhân viên đã được thêm.",
      Error: "Gặp lỗi khi thêm nhân viên.",
      SameCodeError: "đã tồn tại trong hệ thống, vui lòng kiểm tra lại.",
    },
    UpdateEmployee: {
      Success: "Thông tin nhân viên đã được cập nhật.",
      Error: "Gặp lỗi khi cập nhật thông tin nhân viên.",
    },
    DeleteEmployee: {
      Success: "Xóa thành công nhân viên ",
      NotFound: "Không tìm thấy nhân viên để xóa.",
      Error: "Gặp lỗi khi xóa nhân viên.",
    },
    DeleteMultipleEmployee: {
      Success: "Xóa thành công ",
      NotFound: "Không tìm thấy nhân viên để xóa.",
      Error: "Gặp lỗi khi xóa nhiều nhân viên.",
    },

    ExportExcel: {
      Success: "Xuất thành công dữ liệu ra file excel.",
      Error: "Gặp lỗi khi xuất dữ liệu ra file excel.",
    },
  },

  EN: {
    CustomerCodeInvalidError: {
      NewCodeError: "Get new code error, please contact MISA.",
      Empty: "Code is not empty.",
    },
    FullNameInvalidError: {
      Empty: "Name is not empty.",
    },
    DepartmentInvalidError: {
      Empty: "Department is not empty.",
      NotFound: "Department not found.",
    },
    EmailInvalidError: {
      Format: "Email wrong format.",
    },

    ToastTitle: {
      Info: "Info!",
      Warning: "Warning!",
      Error: "Error!",
      Success: "Success!",
    },

    Paging: {
      Title: " record per page",
      Error: " error paging",
    },

    DepartmentError: {
      Get: "Get the department information is error.",
    },

    EmployeeError: {
      Get: "Get the employee information is error.",
    },
    AddEmployee: {
      Success: "Employee has been added.",
      Error: "Error while adding employee.",
      SameCodeError: "already exists in the system, please check again.",
    },
    UpdateEmployee: {
      Success: "Employee information has been updated.",
      Error: "Error while updating employee information.",
    },
    DeleteEmployee: {
      Success: "Successfully deleted employee ",
      NotFound: "Couldn't find an employee to delete.",
      Error: "Got an error when deleting an employee.",
    },
    DeleteMultipleEmployee: {
      Success: "Successfully deleted ",
      NotFound: "Couldn't find an employee to delete.",
      Error: "Got an error when deleting multiple employee.",
    },

    ExportExcel: {
      Success: "Export excel success.",
      Error: "Export excel error.",
    },
  },
};

export default MISAResource;
