using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher052023.CTM.Application
{
    /// <summary>
    /// Dto cập nhật phòng ban
    /// </summary>
    public class DepartmentUpdateDto
    {
        #region Property
        /// <summary>
        /// Mã code của phòng ban
        /// </summary>
        [Required]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên của phòng ban
        /// </summary>
        [Required]
        public string DepartmentName { get; set; }
        #endregion
    }
}
