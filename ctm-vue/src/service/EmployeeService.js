import BaseService from "./BaseService.js";

class EmployeeServiceClass extends BaseService {
  constructor() {
    super("Employees");
  }

  /**
   * lấy 1 code mới từ server
   * @author: TTANH (01/07/2023)
   */
  async getNewCode() {
    const res = await this.baseAxios.get(this.endpoint("/new-code"));
    return res;
  }

  /**
   * lấy dữ liệu bằng phân trang và filter
   * @author: TTANH (03/07/2023)
   */
  async filter(dataFilter) {
    const res = await this.baseAxios.get(this.endpoint("/filter"), {
      params: dataFilter,
    });

    return res;
  }

  /**
   * xuất file excel
   * @author: TTANH (16/07/2023)
   */
  async getExcel(data) {
    const res = await this.baseAxios.get(this.endpoint("/excel"), {
      params: data,
      responseType: "blob",
    });

    return res;
  }
}

const EmployeeService = new EmployeeServiceClass();
export default EmployeeService;
