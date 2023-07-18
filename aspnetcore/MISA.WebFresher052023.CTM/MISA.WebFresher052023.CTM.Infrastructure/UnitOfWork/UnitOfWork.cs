using MISA.WebFresher052023.CTM.Domain;
using System.Data.Common;

namespace MISA.WebFresher052023.CTM.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbConnection Connection => throw new NotImplementedException();

        public DbTransaction? Transaction => throw new NotImplementedException();

        #region Methods
        /// <summary>
        /// Bắt đầu transaction
        /// </summary>
        /// CreatedBy: TTANH (18/07/2023)
        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Bắt đầu transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: TTANH (18/07/2023)
        public Task BeginTransactionAsync()
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// Xác nhận transaction
        /// </summary>
        /// CreatedBy: TTANH (18/07/2023)
        public void Commit()
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// Xác nhận transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: TTANH (18/07/2023)
        public Task CommitAsync()
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// Giải phóng transaction và đóng connection
        /// </summary>
        /// CreatedBy: TTANH (18/07/2023)
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Giải phóng transaction và đóng connection bất đồng bộ
        /// </summary>
        /// CreatedBy: TTANH (18/07/2023)
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Quay trở lại từ điểm bắt đầu transaction
        /// </summary>
        /// CreatedBy: TTANH (18/07/2023)
        public void Rollback()
        {
            throw new NotImplementedException ();
        }

        /// <summary>
        /// Quay trở lại từ điểm bắt đầu transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: TTANH (18/07/2023)
        public Task RollbackAsync()
        {
            throw new NotImplementedException ();
        }
        #endregion
    }
}
