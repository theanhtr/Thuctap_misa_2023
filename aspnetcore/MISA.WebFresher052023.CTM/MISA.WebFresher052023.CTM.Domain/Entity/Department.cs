using System.ComponentModel.DataAnnotations;

namespace MISA.WebFresher052023.CTM.Domain
{
    /// <summary>
    /// Thực thể của Department
    /// </summary>
    /// Created by: TTANH (12/07/2023)
    public class Department : BaseAuditEntity
    {
        #region Property
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        [Required]
        public Guid DepartmentId { get; set; }

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
