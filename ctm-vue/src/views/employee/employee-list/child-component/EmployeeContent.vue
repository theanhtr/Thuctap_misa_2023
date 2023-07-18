<template>
  <div class="employee-content">
    <div class="page__action">
      <div class="page__action-left">
        <misa-button
          type="dropdown"
          width="180px"
          borderRadius="var(--border-radius-default)"
          :dataDropdown="batchExecutionDataDropdown"
          :border="batchExecutionDisable ? '' : '2px solid black'"
          :disable="batchExecutionDisable"
          @clickItem="clickItemBatchExecution"
          >Thực hiện hàng loạt</misa-button
        >
      </div>
      <div class="page__action-right">
        <misa-search-input
          :width="tableSearchFocus ? '200px' : '100px'"
          @input-focus="tableSearchFocus = true"
          @input-blur="tableSearchFocus = false"
          :class="{ animated: tableSearchFocus }"
          v-model="searchText"
          ref="searchTextTable"
          tooltip="Ctrl + Shift + F"
        />
        <misa-icon
          :icon="
            'page__reload--' +
            (pageButtonHover['page__reload'] ? 'black' : 'grey')
          "
          tooltip="Lấy lại dữ liệu"
          @mouseenter="pageButtonHover['page__reload'] = true"
          @mouseleave="pageButtonHover['page__reload'] = false"
          @click="reloadDataWithSelectedRows"
        />
        <misa-icon
          :icon="
            'page__excel--' +
            (pageButtonHover['page__excel'] ? 'black' : 'grey')
          "
          tooltip="Xuất ra excel"
          @mouseenter="pageButtonHover['page__excel'] = true"
          @mouseleave="pageButtonHover['page__excel'] = false"
          @click="exportToExcelWithSearchText"
        />
        <misa-icon
          :icon="
            'page__setting--' +
            (pageButtonHover['page__setting'] ? 'black' : 'grey')
          "
          tooltip="Tùy chỉnh giao diện"
          @mouseenter="pageButtonHover['page__setting'] = true"
          @mouseleave="pageButtonHover['page__setting'] = false"
        />
        <misa-button
          type="dropdown"
          width="100px"
          :border="'2px solid black'"
          :dataDropdown="utilitiesDataDropdown"
          >Tiện ích</misa-button
        >
        <misa-button
          type="combo"
          @clickBtnContainer="showAddEmployeePopup"
          :dataDropdown="addDataDropdown"
          tooltip="Thêm (Insert)"
          >Thêm</misa-button
        >
      </div>
    </div>
    <div class="page__table">
      <misa-table
        ref="misaTable"
        :columnsInfo="employeeColumnsInfo"
        :rowsData="computedEmployees"
        :selectedRows="computedSelectedEmployees"
        :noData="computedNoData"
        @checked-all="checkedAllRow"
        @unchecked-all="uncheckedAllRow"
        @checked-row="checkedRow"
        @unchecked-row="uncheckedRow"
        @doubleClickRow="openFormUpdate"
        @clickFixBtn="openFormUpdate"
        @clickContextDeleteBtn="openConfirmDeletePopup"
        @resizeColumn="resizeEmployeeColumn"
      />
    </div>
    <div class="page__footer">
      <misa-paging v-model="pagingData" @reloadData="reloadData" />
    </div>

    <AddEmployeePopup
      v-if="isShowAddEmployeePopup"
      :dataUpdate="dataUpdate"
      @closeAddEmployeePopup="isShowAddEmployeePopup = false"
      @clickCancelBtn="isShowAddEmployeePopup = false"
      @reloadData="reloadData"
    />

    <div
      v-if="isShowConfirmDeletePopup"
      class="m-overlay"
      id="confirm-delete-popup"
    >
      <misa-popup
        :haveHeader="false"
        width="444px"
        height="auto"
        style="padding: 16px 20px 10px"
      >
        <template #content__input-control>
          <div
            style="
              display: flex;
              align-items: center;
              column-gap: 26px;
              padding-top: 8px;
            "
          >
            <misa-icon height="auto" width="auto" icon="warning--large" />
            <span
              >Bạn có thực sự muốn xóa Nhân viên
              {{ `<${employeeCodeDelete}>` }} không?</span
            >
          </div>
        </template>

        <template #footer>
          <misa-separation-line
            style="
              border-color: var(--border-color-default);
              margin: 16px 0px 10px;
            "
          />
          <div
            style="width: 100%; display: flex; justify-content: space-between"
          >
            <misa-button
              type="sub"
              width="72px"
              borderRadius="var(--border-radius-default)"
              padding="0px 12px"
              @clickBtnContainer="noDeleteBtnClick"
              >Không</misa-button
            >
            <misa-button
              type="main"
              width="50px"
              borderRadius="var(--border-radius-default)"
              padding="0px 16px"
              @clickBtnContainer="yesDeleteBtnClick"
              >Có</misa-button
            >
          </div>
        </template>
      </misa-popup>
    </div>

    <div
      v-if="isShowConfirmDeleteMultiplePopup"
      class="m-overlay"
      id="confirm-delete-multiple-popup"
    >
      <misa-popup
        :haveHeader="false"
        width="444px"
        height="auto"
        style="padding: 16px 20px 10px"
      >
        <template #content__input-control>
          <div
            style="
              display: flex;
              align-items: center;
              column-gap: 26px;
              padding-top: 8px;
            "
          >
            <misa-icon height="auto" width="auto" icon="warning--large" />
            <span
              >Bạn có thực sự muốn xóa {{ selectedEmployees.length }} nhân viên
              không?</span
            >
          </div>
        </template>

        <template #footer>
          <misa-separation-line
            style="
              border-color: var(--border-color-default);
              margin: 16px 0px 10px;
            "
          />
          <div
            style="width: 100%; display: flex; justify-content: space-between"
          >
            <misa-button
              type="sub"
              width="72px"
              borderRadius="var(--border-radius-default)"
              padding="0px 12px"
              @clickBtnContainer="isShowConfirmDeleteMultiplePopup = false"
              >Không</misa-button
            >
            <misa-button
              type="main"
              width="50px"
              borderRadius="var(--border-radius-default)"
              padding="0px 16px"
              @clickBtnContainer="deleteMultipleEmployee"
              >Có</misa-button
            >
          </div>
        </template>
      </misa-popup>
    </div>

    <misa-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import EmployeeService from "@/service/EmployeeService.js";
import AddEmployeePopup from "./AddEmployeePopup.vue";
import { findIndexByAttribute, sortArrayByAttribute } from "@/helper/common.js";
import { columnsInfo } from "./employeeColumnsInfo.js";
import { debounce } from "@/helper/debounce.js";
import { isProxy, toRaw } from "vue";

export default {
  name: "EmployeeContent",
  components: {
    AddEmployeePopup,
  },
  data() {
    return {
      employees: [],

      /* lưu dữ id các nhân viên đã được chọn */
      selectedEmployees: [],

      employeeColumnsInfo: [...columnsInfo],

      isLoading: false,
      /* các biến để xác định trạng thái trên page_action */
      tableSearchFocus: false,
      pageButtonHover: {
        page__setting: false,
        page__reload: false,
        page__reload: false,
      },

      /* biến xác định nút "Thực hiện hàng loạt" có disable hay không */
      batchExecutionDisable: true,

      /* các hành động cho nút "Thực hiện hàng loạt" ở page action */
      batchExecutionDataDropdown: [
        { id: "delete", title: "Xóa" },
        { id: "merge", title: "Gộp" },
      ],

      /* các hành động cho nút "Tiện ích" ở page action */
      utilitiesDataDropdown: [
        { id: "sync", title: "Đồng bộ với AMIS hệ thống" },
      ],

      /* các hành động cho combo btn "Thêm" ở page action */
      addDataDropdown: [{ id: "excel", title: "Nhập từ Excel" }],

      /*== các biến sử dụng cho add-employee-popup ==*/
      isShowAddEmployeePopup: false,

      dataUpdate: null,

      /* biến sử dụng cho việc xác nhận xóa */
      isShowConfirmDeletePopup: false,
      employeeCodeDelete: "",
      employeeIdDelete: "",

      isShowConfirmDeleteMultiplePopup: false,

      searchText: "",

      /* biến sử dụng cho phân trang */
      pagingData: {
        pageSize: 10,
        pageNumber: 1,
        totalPage: 0,
        totalRecord: 0,
      },

      // xử lý khi không có dữ liệu trả về
      noData: false,

      //biến sử dụng cho việc thao tác giữ shift khi chọn
      previouslySelectedIndex: -1,
    };
  },

  created() {
    //đưa những cột được ghim lên đầu
    this.employeeColumnsInfo = sortArrayByAttribute(
      this.employeeColumnsInfo,
      "isPin"
    );

    window.addEventListener("keydown", this.handleKeydown);

    //lấy dữ liệu nhân viên
    this.getEmployees();
  },

  updated() {
    //nếu add popup đang mở thì bỏ sự kiện keydown đi
    if (this.isShowAddEmployeePopup) {
      window.removeEventListener("keydown", this.handleKeydown);
    } else {
      window.addEventListener("keydown", this.handleKeydown);
    }
  },

  unmounted() {
    window.removeEventListener("keydown", this.handleKeydown);
  },

  methods: {
    /**
     * hàm thực hiện mở thêm nhân viên
     * @author: TTANH (11/07/2023)
     */
    showAddEmployeePopup() {
      this.isShowAddEmployeePopup = true;
      this.dataUpdate = null;
    },

    /**
     * thực hiện get dữ liệu nhân viên khi component được render
     * @author: TTANH (30/06/2023)
     */
    async getEmployees() {
      try {
        const res = await EmployeeService.filter({
          pageSize: this.pagingData.pageSize,
          pageNumber: this.pagingData.pageNumber,
          searchText: this.searchText,
        });

        if (res.success) {
          if (res.data.Data.length != 0) {
            this.employees = res.data.Data;
            this.pagingData.totalPage = res.data.TotalPage;
            this.pagingData.totalRecord = res.data.TotalRecord;
            this.noData = false;
          } else {
            this.noData = true;
          }
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: this.$_MISAResource[this.$store.state.langCode].EmployeeError
              .Get,
          });
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:116 ~ getEmployees ~ error:",
          error
        );
      }
    },

    /**
     * sự kiện click vào các item con của "Thực hiện hàng loạt":
     * { id: "delete", title: "Xóa" },
     * { id: "merge", title: "Gộp" },
     * @author: TTANH (17/07/2023)
     * @param {string} idItem id của nút được click
     */
    clickItemBatchExecution(idItem) {
      if (idItem === "delete") {
        this.isShowConfirmDeleteMultiplePopup = true;
      }
    },

    /**
     * xóa nhiều nhân viên
     * @author: TTANH (17/07/2023)
     */
    async deleteMultipleEmployee() {
      var dataSendApi = null;

      if (isProxy(this.selectedEmployees)) {
        dataSendApi = toRaw(this.selectedEmployees);
      } else {
        dataSendApi = this.selectedEmployees;
      }

      const res = await EmployeeService.deleteMultiple(dataSendApi);

      if (res.success) {
        this.$store.commit("addToast", {
          type: "success",
          text:
            this.$_MISAResource[this.$store.state.langCode]
              .DeleteMultipleEmployee.Success +
            res.data +
            " nhân viên.",
        });

        this.selectedEmployees = [];
        this.isShowConfirmDeleteMultiplePopup = false;

        this.reloadData();
      } else {
        this.$store.commit("addToast", {
          type: "error",
          text: this.$_MISAResource[this.$store.state.langCode]
            .DeleteMultipleEmployee.Error,
        });
      }
    },

    /**
     * cập nhật lại employees mới
     * @author: TTANH (03/07/2023)
     */
    reloadData() {
      try {
        this.previouslySelectedIndex = -1;
        this.employees = [];
        this.getEmployees();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:465 ~ reloadData ~ error:",
          error
        );
      }
    },

    /**
     * xóa thêm dữ liệu đã chọn
     * @author: TTANH (03/07/2023)
     */
    reloadDataWithSelectedRows() {
      try {
        this.selectedEmployees = [];
        this.reloadData();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:282 ~ reloadDataWithSelectedRows ~ error:",
          error
        );
      }
    },

    /**
     * xuất dữ liệu đang tìm kiếm ra file excel
     * @author: TTANH (16/07/2023)
     */
    async exportToExcelWithSearchText() {
      try {
        const res = await EmployeeService.getExcel({
          searchText: this.searchText,
        });

        if (res.success) {
          this.$store.commit("addToast", {
            type: "success",
            text: this.$_MISAResource[this.$store.state.langCode].ExportExcel
              .Success,
          });
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: this.$_MISAResource[this.$store.state.langCode].ExportExcel
              .Error,
          });
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:346 ~ exportToExcelWithSearchText ~ error:",
          error
        );
      }
    },

    /**
     * thêm một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2023)
     * @param {string} rowId id của dòng được chọn
     */
    addSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedEmployees, "", rowId);

      if (index === -1) {
        this.selectedEmployees.push(rowId);
      }
    },

    /**
     * xóa một id vào mảng dòng đã chọn
     * @author: TTANH (11/07/2023)
     * @param {string} rowId id của dòng được chọn
     */
    deleteSelectedRow(rowId) {
      let index = findIndexByAttribute(this.selectedEmployees, "", rowId);

      if (index !== -1) {
        this.selectedEmployees.splice(index, 1);
      }
    },

    /**
     * xử lý khi chọn checkbox ở header
     * @author: TTANH (27/06/2023)
     */
    checkedAllRow() {
      try {
        this.employees.forEach((employee) => {
          this.addSelectedRow(employee.EmployeeId);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:463 ~ checkedAllRow ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở header
     * @author: TTANH (27/06/2023)
     */
    uncheckedAllRow() {
      try {
        this.employees.forEach((employee) => {
          this.deleteSelectedRow(employee.EmployeeId);
        });
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:475 ~ uncheckedAllRow ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi chọn checkbox ở 1 row
     * @author: TTANH (27/06/2023)
     * @param {string} rowId: id của record được chọn
     */
    checkedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(
          this.employees,
          "EmployeeId",
          rowId
        );

        if (event.shiftKey) {
          event.preventDefault();

          if (this.previouslySelectedIndex === -1) {
            this.addSelectedRow(rowId);
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (
                let index = indexNewChecked;
                index <= this.previouslySelectedIndex;
                index++
              ) {
                const employee = this.employees[index];

                this.addSelectedRow(employee.EmployeeId);
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (
                let index = this.previouslySelectedIndex;
                index <= indexNewChecked;
                index++
              ) {
                const employee = this.employees[index];

                this.addSelectedRow(employee.EmployeeId);
              }
            } else {
            }
          }
        } else {
          this.addSelectedRow(rowId);
        }

        this.previouslySelectedIndex = indexNewChecked;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:492 ~ uncheckedAllRow ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi bỏ chọn checkbox ở 1 row
     * @author: TTANH (27/06/2023)
     * @param {string} rowId: id của record được bỏ chọn
     */
    uncheckedRow(rowId) {
      try {
        let indexNewChecked = findIndexByAttribute(
          this.employees,
          "EmployeeId",
          rowId
        );

        if (event.shiftKey) {
          event.preventDefault();

          if (this.previouslySelectedIndex === -1) {
            this.deleteSelectedRow(rowId);
          } else {
            if (this.previouslySelectedIndex > indexNewChecked) {
              for (
                let index = indexNewChecked;
                index <= this.previouslySelectedIndex;
                index++
              ) {
                const employee = this.employees[index];

                this.deleteSelectedRow(employee.EmployeeId);
              }
            } else if (this.previouslySelectedIndex < indexNewChecked) {
              for (
                let index = this.previouslySelectedIndex;
                index <= indexNewChecked;
                index++
              ) {
                const employee = this.employees[index];

                this.deleteSelectedRow(employee.EmployeeId);
              }
            } else {
            }
          }
        } else {
          this.deleteSelectedRow(rowId);
        }

        this.previouslySelectedIndex = indexNewChecked;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:492 ~ uncheckedAllRow ~ error:",
          error
        );
      }
    },

    /**
     * mở form update
     * @param {string} rowId id của đối tượng muốn update
     */
    openFormUpdate(rowId) {
      try {
        let indexRow = findIndexByAttribute(
          this.employees,
          "EmployeeId",
          rowId
        );

        this.isShowAddEmployeePopup = true;
        this.dataUpdate = this.employees[indexRow];
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:529 ~ openFormUpdate ~ error:",
          error
        );
      }
    },

    /**
     * mở form xác nhận xóa
     * @author: TTANH (01/07/2023)
     * @param {string} id id của bản ghi cần xóa
     */
    openConfirmDeletePopup(id) {
      try {
        let index = findIndexByAttribute(this.employees, "EmployeeId", id);

        if (index !== -1) {
          this.employeeCodeDelete = this.employees[index].EmployeeCode;
          this.employeeIdDelete = id;
          this.isShowConfirmDeletePopup = true;
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: this.$_MISAResource[this.$store.state.langCode].DeleteEmployee
              .NotFound,
          });
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:351 ~ openConfirmDeletePopup ~ error:",
          error
        );
      }
    },

    /**
     * đóng form xác nhận xóa
     * @author: TTANH (01/07/2023)
     */
    closeConfirmDeletePopup() {
      try {
        this.employeeCodeDelete = "";
        this.employeeIdDelete = "";
        this.isShowConfirmDeletePopup = false;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:386 ~ closeConfirmDeletePopup ~ error:",
          error
        );
      }
    },

    /**
     * hủy xóa
     * @author: TTANH (01/07/2023)
     */
    noDeleteBtnClick() {
      try {
        this.closeConfirmDeletePopup();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:401 ~ noDeleteBtnClick ~ error:",
          error
        );
      }
    },

    /**
     * xác nhận xóa
     * @author: TTANH (01/07/2023)
     */
    yesDeleteBtnClick() {
      try {
        this.deleteRecord();
        this.closeConfirmDeletePopup();
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:416 ~ yesDeleteBtnClick ~ error:",
          error
        );
      }
    },

    /**
     * thực hiện xóa 1 bản ghi
     * @author: TTANH (01/07/2023)
     */
    async deleteRecord() {
      try {
        this.isLoading = true;
        const employeeCode = this.employeeCodeDelete;
        const res = await EmployeeService.delete(this.employeeIdDelete);

        if (res.success) {
          this.$store.commit("addToast", {
            type: "success",
            text:
              this.$_MISAResource[this.$store.state.langCode].DeleteEmployee
                .Success +
              "<" +
              employeeCode +
              ">" +
              ".",
          });

          this.reloadData();
        } else {
          if (res.errorCode === this.$_MISAEnum.ERROR_CODE.NOT_FOUND_DATA) {
            this.$store.commit("addToast", {
              type: "error",
              text: this.$_MISAResource[this.$store.state.langCode]
                .DeleteEmployee.NotFound,
            });
          } else {
            this.$store.commit("addToast", {
              type: "error",
              text: this.$_MISAResource[this.$store.state.langCode]
                .DeleteEmployee.Error,
            });
          }
        }

        this.isLoading = false;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:582 ~ deleteRecord ~ error:",
          error
        );
      }
    },

    /**
     * thực hiện thay đổi kích thước của cột
     * @author: TTANH (04/07/2023)
     */
    resizeEmployeeColumn(index, resizeWidth) {
      try {
        this.employeeColumnsInfo[index].size = resizeWidth;
        this.employeeColumnsInfo[index].size;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeContent.vue:524 ~ resizeEmployeeColumn ~ error:",
          error
        );
      }
    },

    /**
     * xử lý các phím tắt
     * @author: TTANH (11/07/2023)
     */
    handleKeydown(event) {
      if (event.keyCode === this.$_MISAEnum.KEY_CODE.INSERT) {
        this.showAddEmployeePopup();
      } else if (
        event.keyCode === this.$_MISAEnum.KEY_CODE.F &&
        event.shiftKey &&
        event.ctrlKey
      ) {
        this.$refs.searchTextTable.focus();
      }
    },
  },
  computed: {
    /* thêm id để phân biệt các phần tử với nhau */
    computedEmployees() {
      try {
        let haveIdEmployees = [];

        this.employees.forEach((employee, index) => {
          let id = employee.EmployeeId;
          haveIdEmployees.push({
            id,
            ...employee,
          });
        });

        return haveIdEmployees;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:",
          error
        );
      }
    },

    computedSelectedEmployees() {
      if (this.selectedEmployees.length <= 1) {
        this.batchExecutionDisable = true;
      } else {
        this.batchExecutionDisable = false;
      }
      return this.selectedEmployees;
    },

    computedNoData() {
      return this.noData;
    },
  },
  watch: {
    searchText: debounce(function () {
      this.pagingData.pageNumber = 1;
      this.reloadData();
    }, 500),
  },
};
</script>

<style scoped>
@import url(./employee-content.css);
</style>
