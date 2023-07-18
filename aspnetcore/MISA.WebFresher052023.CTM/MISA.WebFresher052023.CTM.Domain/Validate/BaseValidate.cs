namespace MISA.WebFresher052023.CTM.Domain
{
    /// <summary>
    /// Class dùng để validate.
    /// </summary>
    /// CreatedBy: TTANH (14/07/2023)
    public abstract class BaseValidate<TEntity> : IBaseValidate<TEntity>
    {
        #region Fields
        private readonly IBaseRepository<TEntity> _baseRepository;
        #endregion

        #region Constructor
        public BaseValidate(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm dùng để validate code
        /// </summary>
        /// <param name="code">Code muốn validate</param>
        /// <returns>true nếu không có lỗi</returns>
        /// <exception cref="ValidateException">Nếu có lỗi thì sẽ trả exception</exception>
        /// CreatedBy: TTANH (14/07/2023)
        public async Task<bool> CodeValidate(string code)
        {
            var check = await _baseRepository.GetByCodeAsync(code);

            if (check != null)
            {
                throw new ValidateException(Convert.ToInt32(StatusErrorCode.CodeDuplicate), $"Đối tượng {code} đã tồn tại", null);
            }

            var lastCharacterCode = code[code.Length - 1];

            if (lastCharacterCode < '0' || lastCharacterCode > '9')
            {
                throw new ValidateException(Convert.ToInt32(StatusErrorCode.WrongFormatCode), ResourceVN.Last_Character_Code_Not_Number, null);
            }

            return true;
        }
        #endregion

    }
}
