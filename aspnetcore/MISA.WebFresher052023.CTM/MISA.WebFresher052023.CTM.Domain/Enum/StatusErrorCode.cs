namespace MISA.WebFresher052023.CTM.Domain
{
    /// <summary>
    /// Enum của mã lỗi
    /// </summary>
    /// Created by: TTANH (12/07/2023)
    public enum StatusErrorCode
    {
        /// <summary>
        /// Lỗi trùng code, code đã tồn tại trong hệ thống
        /// </summary>
        CodeDuplicate = 1207,

        /// <summary>
        /// Lỗi sai định dạng của code
        /// </summary>
        WrongFormatCode = 1208,

        /// <summary>
        /// Lỗi không tìm thấy dữ liệu
        /// </summary>
        NotFoundData = 1209,
    }
}
