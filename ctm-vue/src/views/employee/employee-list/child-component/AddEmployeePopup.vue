<template>
  <div class="m-overlay" id="add-employee-popup">
    <misa-popup title="Thông tin nhân viên">
      <template #title__action>
        <div class="add-employee__title-action">
          <div class="title-action__info" @click="clickIsCustomer">
            <misa-checkbox-input :isCheck="isCustomer" />
            <div class="title-action__title">Là khách hàng</div>
          </div>
          <div class="title-action__info" @click="clickIsSupplier">
            <misa-checkbox-input :isCheck="isSupplier" />
            <div class="title-action__title">Là nhà cung cấp</div>
          </div>
        </div>
      </template>
      <template #header__close>
        <misa-icon icon="help" title="Giúp (F1)" />
        <misa-icon
          @click="$emit('closeAddEmployeePopup')"
          icon="close"
          style="margin-left: 3px"
          title="Đóng (ESC)"
        />
      </template>
      <template #content__input-control>
        <div class="w1 flex-row" style="padding-bottom: 12px">
          <div class="w1/2" style="padding-right: 26px">
            <div class="flex-row p-b-8">
              <misa-textfield
                :errorText="errorTextEmployeeData.employeeCode"
                v-model="addEmployeeData.employeeCode"
                type="code"
                idInput="add__employee-code"
                labelText="Mã"
                :inputRequired="true"
                style="padding-right: 6px"
                class="w2/5"
                ref="employeeCode"
                tabindex="1"
              />
              <misa-textfield
                :errorText="errorTextEmployeeData.fullName"
                v-model="addEmployeeData.fullName"
                type="text"
                idInput="add__full-name"
                labelText="Tên"
                :inputRequired="true"
                class="w3/5"
                ref="fullName"
                tabindex="2"
              />
            </div>
            <div class="flex-row p-b-8">
              <misa-combobox
                :errorText="errorTextEmployeeData.departmentId"
                v-model="addEmployeeData.departmentId"
                ref="departmentId"
                type="table"
                labelText="Đơn vị"
                :inputRequired="true"
                :columnsInfo="departmentColumnsInfo"
                :rowsData="computedDepartments"
                class="w1"
                tabindex="3"
                @keydown="onEnterDepartmentId"
              />
            </div>
            <div class="flex-row p-b-8">
              <misa-textfield
                :errorText="errorTextEmployeeData.position"
                v-model="addEmployeeData.position"
                type="text"
                idInput="add__position-code"
                labelText="Chức danh"
                class="w1"
                tabindex="4"
                ref="position"
              />
            </div>
            <div v-show="isCustomer || isSupplier" class="flex-row p-b-8">
              <misa-textfield
                :errorText="errorTextEmployeeData.supplierCustomerGroup"
                v-model="addEmployeeData.supplierCustomerGroup"
                type="text"
                idInput="add__error"
                labelText="Nhóm khách hàng, nhà cung cấp"
                class="w1"
                tabindex="5"
              />
            </div>
          </div>
          <div class="w1/2">
            <div class="flex-row p-b-8">
              <misa-date-picker
                :errorText="errorTextEmployeeData.dateOfBirth"
                v-model="addEmployeeData.dateOfBirth"
                class="w2/5"
                style="padding-right: 6px"
                idInput="add__dob"
                labelText="Ngày sinh"
                tabindex="6"
              />
              <misa-radio-input
                :errorText="errorTextEmployeeData.gender"
                v-model="addEmployeeData.gender"
                :values="genderOptions"
                nameRadioGroup="add__gender"
                :align="'row'"
                type="text"
                labelText="Giới tính"
                class="w3/5"
                style="padding-left: 10px"
                tabindex="7"
              />
            </div>
            <div class="flex-row p-b-8">
              <misa-textfield
                :errorText="errorTextEmployeeData.identityNumber"
                v-model="addEmployeeData.identityNumber"
                type="text"
                idInput="add__indentity-number"
                labelText="Số CMND"
                class="w3/5"
                style="padding-right: 6px"
                tabindex="8"
              />
              <misa-date-picker
                :errorText="errorTextEmployeeData.identityDate"
                v-model="addEmployeeData.identityDate"
                class="w2/5"
                style="padding-right: 6px"
                idInput="add__indentity-date"
                labelText="Ngày cấp"
                tabindex="9"
              />
            </div>
            <div class="flex-row p-b-8">
              <misa-textfield
                :errorText="errorTextEmployeeData.identityPlace"
                v-model="addEmployeeData.identityPlace"
                type="text"
                idInput="add__identity-place"
                labelText="Nơi cấp"
                class="w1"
                tabindex="10"
              />
            </div>
            <div v-show="isCustomer || isSupplier" class="flex-row p-b-8">
              <misa-textfield
                :errorText="errorTextEmployeeData.receiveAccount"
                v-model="addEmployeeData.receiveAccount"
                v-show="isCustomer"
                type="text"
                idInput="add__employee-code"
                labelText="TK công nợ phải thu"
                style="padding-right: 8px"
                class="w1/2"
                tabindex="11"
              />
              <misa-textfield
                :errorText="errorTextEmployeeData.payAccount"
                v-model="addEmployeeData.payAccount"
                v-show="isSupplier"
                type="text"
                idInput="add__employee-code"
                labelText="TK công nợ phải trả"
                class="w1/2"
                tabindex="12"
              />
            </div>
          </div>
        </div>
        <div class="more-info">
          <div class="more-info__nav">
            <misa-button
              type="sub"
              borderRadius="var(--border-radius-default) var(--border-radius-default) 0px 0px"
              style="margin-right: 2px"
              :class="
                currentMoreInfo === this.$_MISAEnum.MORE_INFO_NAV.SALARY_INFO
                  ? 'more-info__nav--active'
                  : ''
              "
              @click="
                currentMoreInfo = this.$_MISAEnum.MORE_INFO_NAV.SALARY_INFO
              "
              >Thông tin tiền lương</misa-button
            >
            <misa-button
              type="sub"
              borderRadius="var(--border-radius-default) var(--border-radius-default) 0px 0px"
              style="margin-right: 2px"
              :class="
                currentMoreInfo === this.$_MISAEnum.MORE_INFO_NAV.BANK_INFO
                  ? 'more-info__nav--active'
                  : ''
              "
              @click="currentMoreInfo = this.$_MISAEnum.MORE_INFO_NAV.BANK_INFO"
              >Thông tin ngân hàng</misa-button
            >
            <misa-button
              type="sub"
              borderRadius="var(--border-radius-default) var(--border-radius-default) 0px 0px"
              :class="
                currentMoreInfo === this.$_MISAEnum.MORE_INFO_NAV.CONTACT_INFO
                  ? 'more-info__nav--active'
                  : ''
              "
              @click="
                currentMoreInfo = this.$_MISAEnum.MORE_INFO_NAV.CONTACT_INFO
              "
              >Thông tin liên hệ</misa-button
            >
          </div>
          <div class="more-info__content">
            <div
              v-show="
                currentMoreInfo === this.$_MISAEnum.MORE_INFO_NAV.SALARY_INFO
              "
              class="salary-info"
            >
              <div class="flex-row w1 p-b-8">
                <misa-textfield
                  :errorText="errorTextEmployeeData.salary"
                  v-model="addEmployeeData.salary"
                  type="money"
                  idInput="add__salary"
                  labelText="Lương thỏa thuận"
                  class="w1/4 p-r-12"
                  tabindex="13"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.salaryCoefficients"
                  v-model="addEmployeeData.salaryCoefficients"
                  type="money"
                  idInput="add__salary-coefficients"
                  labelText="Hệ số lương"
                  class="w1/6 p-r-12"
                  tabindex="14"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.salaryPaidForInsurance"
                  v-model="addEmployeeData.salaryPaidForInsurance"
                  type="money"
                  idInput="add__salary-paid-for-insurance"
                  labelText="Lương đóng bảo hiểm"
                  class="w1/3 p-r-12"
                  tabindex="15"
                />
              </div>

              <div class="flex-row w1 p-b-8">
                <misa-textfield
                  :errorText="errorTextEmployeeData.personalTaxCode"
                  v-model="addEmployeeData.personalTaxCode"
                  type="text"
                  idInput="add__personal-tax-code"
                  labelText="Mã số thuế"
                  class="w1/4 p-r-12"
                  tabindex="16"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.typeOfContract"
                  v-model="addEmployeeData.typeOfContract"
                  type="text"
                  idInput="add__type-of-contract"
                  labelText="Loại hợp đồng"
                  class="w1/2 p-r-12"
                  tabindex="17"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.numberOfDependents"
                  v-model="addEmployeeData.numberOfDependents"
                  type="number_no_dot"
                  idInput="add__number-of-dependents"
                  labelText="Số người phụ thuộc"
                  :haveButtonFunction="true"
                  class="w1/6 p-r-12"
                  tabindex="18"
                />
              </div>
            </div>
            <div
              v-show="
                currentMoreInfo === this.$_MISAEnum.MORE_INFO_NAV.BANK_INFO
              "
              class="bank-info"
            >
              <div class="flex-row w1">
                <misa-textfield
                  :errorText="errorTextEmployeeData.accountNumber"
                  v-model="addEmployeeData.accountNumber"
                  type="text"
                  idInput="add__account-number"
                  labelText="Số tài khoản"
                  class="w1/6 p-r-12"
                  tabindex="13"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.bankName"
                  v-model="addEmployeeData.bankName"
                  type="text"
                  idInput="add__bank-name"
                  labelText="Tên ngân hàng"
                  class="w1/4 p-r-12"
                  tabindex="14"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.bankBranch"
                  v-model="addEmployeeData.bankBranch"
                  type="text"
                  idInput="add__bank-branch"
                  labelText="Chi nhánh"
                  class="w1/5 p-r-12"
                  tabindex="15"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.bankProvince"
                  v-model="addEmployeeData.bankProvince"
                  type="text"
                  idInput="add__bank-province"
                  labelText="Tỉnh/TP của ngân hàng"
                  class="w1/3 p-r-12"
                  tabindex="16"
                />
              </div>
            </div>
            <div
              v-show="
                currentMoreInfo === this.$_MISAEnum.MORE_INFO_NAV.CONTACT_INFO
              "
              class="contact-info"
            >
              <div class="flex-row w1 p-b-8">
                <misa-textfield
                  :errorText="errorTextEmployeeData.contactAddress"
                  v-model="addEmployeeData.contactAddress"
                  type="text"
                  idInput="add__address"
                  labelText="Địa chỉ"
                  class="w1"
                  tabindex="13"
                />
              </div>
              <div class="flex-row w1 p-b-8">
                <misa-textfield
                  :errorText="errorTextEmployeeData.contactPhoneNumber"
                  v-model="addEmployeeData.contactPhoneNumber"
                  type="text"
                  idInput="add__phone-number"
                  labelText="ĐT di động"
                  class="w1/4 p-r-12"
                  tabindex="14"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.contactLandlinePhoneNumber"
                  v-model="addEmployeeData.contactLandlinePhoneNumber"
                  type="text"
                  idInput="add__landline-phone-number"
                  labelText="ĐT cố định"
                  class="w1/4 p-r-12"
                  tabindex="15"
                />
                <misa-textfield
                  :errorText="errorTextEmployeeData.contactEmail"
                  v-model="addEmployeeData.contactEmail"
                  type="text"
                  idInput="add__email"
                  labelText="Email"
                  class="w1/4 p-r-12"
                  tabindex="16"
                />
              </div>
            </div>
          </div>
        </div>
      </template>
      <template #footer>
        <misa-separation-line
          style="border-color: var(--border-color-default); margin: 16px 0px"
        />
        <div
          class="flex-row"
          style="justify-content: space-between; padding-bottom: 16px"
        >
          <div>
            <misa-button
              type="sub"
              width="58px"
              tabindex="20"
              borderRadius="var(--border-radius-default)"
              @clickBtnContainer="$emit('clickCancelBtn')"
              >Hủy</misa-button
            >
          </div>
          <div>
            <misa-button
              type="sub"
              width="56px"
              borderRadius="var(--border-radius-default)"
              tabindex="21"
              style="margin-right: 10px"
              @clickBtnContainer="storeBtnClick"
              ref="storeBtn"
              tooltip="Cất (Ctrl + S)"
              >Cất</misa-button
            >
            <misa-button
              type="main"
              width="112px"
              tabindex="22"
              borderRadius="var(--border-radius-default)"
              @clickBtnContainer="storeAndAddBtnClick"
              @keydown="onStoreAndAddBtnKeyDown"
              ref="storeAndAddBtn"
              tooltip="Cất và thêm (Ctrl + Shift + S)"
              >Cất và Thêm</misa-button
            >
          </div>
        </div>
      </template>
    </misa-popup>

    <div
      v-if="isShowDialogError"
      class="m-overlay"
      id="add-employee-popup--error"
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
            <misa-icon height="auto" width="auto" icon="error--medium" />
            <span>{{ getFirstError.errorText }}</span>
          </div>
        </template>

        <template #footer>
          <misa-separation-line
            style="
              border-color: var(--border-color-default);
              margin: 16px 0px 10px;
            "
          />
          <div style="width: 100%; display: flex; justify-content: center">
            <misa-button
              type="main"
              width="58px"
              borderRadius="var(--border-radius-default)"
              padding="0px 12px"
              @clickBtnContainer="closeBtnDialogErrorClick"
              ref="closeBtnDialogError"
              >Đóng</misa-button
            >
          </div>
        </template>
      </misa-popup>
    </div>

    <misa-loading-spinner v-if="isLoading" size="large" />
  </div>
</template>

<script>
import DepartmentService from "@/service/DepartmentService.js";
import EmployeeService from "@/service/EmployeeService.js";
import { findIndexByAttribute, generateUuid } from "@/helper/common.js";

export default {
  name: "AddEmployeePopup",
  props: {
    dataUpdate: {
      default: null,
    },
  },

  created() {
    window.addEventListener("keydown", this.handleKeydown);

    //cập nhật thông tin cho form: form_mode, data
    this.addInfoForm();

    //lấy dữ liệu phòng ban
    this.getDepartments();
  },

  mounted() {
    //foucs vào employee code lần đầu mở form
    this.$refs.employeeCode.focus();
  },

  updated() {
    //focus vào button đóng ở dialog lỗi khi hiện dialog lỗi lên
    if (this.isShowDialogError) {
      this.$refs.closeBtnDialogError.focusButtonContainer();
    }
  },

  unmounted() {
    window.removeEventListener("keydown", this.handleKeydown);
  },

  data() {
    return {
      isShowDialogError: false,
      isLoading: false,

      formMode: this.$_MISAEnum.FORM_MODE.ADD,

      isSupplier: false,
      isCustomer: false,

      currentMoreInfo: this.$_MISAEnum.MORE_INFO_NAV.SALARY_INFO,

      genderOptions: [
        {
          id: this.$_MISAEnum.GENDER.MALE,
          name: "Nam",
        },
        {
          id: this.$_MISAEnum.GENDER.FEMALE,
          name: "Nữ",
        },
        {
          id: this.$_MISAEnum.GENDER.ORTHER,
          name: "Khác",
        },
      ],

      addEmployeeData: {
        employeeId: "",
        employeeCode: "",
        fullName: "",
        gender: this.$_MISAEnum.GENDER.MALE,
        dateOfBirth: "",

        identityNumber: "",
        identityDate: "",
        identityPlace: "",

        departmentId: "",
        departmentCode: "",
        departmentName: "",

        supplierCustomerGroup: "",
        payAccount: "",
        receiveAccount: "",

        position: "",

        /* thông tin tiền lương */
        salary: 0,
        salaryPaidForInsurance: 0,
        salaryCoefficients: 0,

        personalTaxCode: "",
        typeOfContract: "",
        numberOfDependents: 0,

        /* tài khoản ngân hàng */
        accountNumber: "",
        bankName: "",
        bankBranch: "",
        bankProvince: "",

        /* thông tin liên hệ */
        contactAddress: "",
        contactEmail: "",
        contactPhoneNumber: "",
        contactLandlinePhoneNumber: "",
      },

      errorTextEmployeeData: {
        employeeId: "",
        employeeCode: "",
        fullName: "",
        gender: "",
        dateOfBirth: "",

        identityNumber: "",
        identityDate: "",
        identityPlace: "",

        departmentId: "",
        departmentCode: "",
        departmentName: "",

        supplierCustomerGroup: "",
        payAccount: "",
        receiveAccount: "",

        position: "",

        /* thông tin tiền lương */
        salary: "",
        salaryPaidForInsurance: "",
        salaryCoefficients: "",

        personalTaxCode: "",
        typeOfContract: "",
        numberOfDependents: "",

        /* tài khoản ngân hàng */
        accountNumber: "",
        bankName: "",
        bankBranch: "",
        bankProvince: "",

        /* thông tin liên hệ */
        contactAddress: "",
        contactEmail: "",
        contactPhoneNumber: "",
        contactLandlinePhoneNumber: "",
      },

      departmentColumnsInfo: [
        {
          id: "DepartmentCode",
          name: "Mã đơn vị",
          size: "50px",
          textAlign: "left",
          format: "text",
          isShow: true,
          isPin: false,
        },
        {
          id: "DepartmentName",
          name: "Tên đơn vị",
          size: "150px",
          textAlign: "left",
          format: "text",
          isShow: true,
          isPin: false,
        },
      ],
      departments: [],
    };
  },

  methods: {
    /**
     * thực hiện get dữ liệu phòng ban
     * @author: TTANH (30/06/2023)
     */
    async getDepartments() {
      try {
        const res = await DepartmentService.get();

        if (res.success) {
          this.departments = res.data;
        } else {
          this.$store.commit("addToast", {
            type: "error",
            text: this.$_MISAResource[this.$store.state.langCode]
              .DepartmentError.Error,
          });
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddEmployeePopup.vue:454 ~ getDepartments ~ error:",
          error
        );
      }
    },

    /**
     * cập nhật thông tin cho form: form_mode, data
     * @author: TTANH (01/07/2023)
     */
    addInfoForm() {
      this.formMode = this.computedFormMode;

      if (this.formMode === this.$_MISAEnum.FORM_MODE.ADD) {
        this.resetAddEmployeeData();
        this.getNewEmployeeCode();
      } else if (this.formMode === this.$_MISAEnum.FORM_MODE.UPDATE) {
        for (let attr in this.dataUpdate) {
          let formatAttr = attr[0].toLowerCase() + attr.slice(1, attr.length);

          this.addEmployeeData[formatAttr] =
            this.dataUpdate[attr] !== null ? this.dataUpdate[attr] : "";
        }
      }
    },

    /**
     * lấy employee code mới
     * @author: TTANH (01/07/2023)
     */
    async getNewEmployeeCode() {
      try {
        const res = await EmployeeService.getNewCode();

        if (res.success) {
          this.addEmployeeData.employeeCode = res.data;
        } else {
          this.isShowDialogError = true;
          this.errorTextEmployeeData.employeeCode =
            this.$_MISAResource[
              this.$store.state.langCode
            ].CustomerCodeInvalidError.NewCodeError;
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddEmployeePopup.vue:688 ~ getNewEmployeeCode ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi ấn vào nút "Cất"
     * @author: TTANH (01/07/2023)
     */
    async storeBtnClick() {
      try {
        this.addEmployeeData.departmentId =
          this.$refs.departmentId.getIdSelectedData();

        this.updateDepartmentInfo(this.addEmployeeData.departmentId);

        let isSuccess = await this.createNewEmployee();

        if (isSuccess) {
          this.$emit("clickCancelBtn");
          this.$emit("reloadData");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddEmployeePopup.vue:688 ~ storeBtnClick ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi ấn vào nút "Cất và thêm"
     * @author: TTANH (01/07/2023)
     */
    async storeAndAddBtnClick() {
      try {
        this.addEmployeeData.departmentId =
          this.$refs.departmentId.getIdSelectedData();

        this.updateDepartmentInfo(this.addEmployeeData.departmentId);

        let isSuccess = await this.createNewEmployee();

        if (isSuccess) {
          this.resetAddEmployeeData();
          this.$refs.employeeCode.focus();
          this.$emit("reloadData");

          this.getNewEmployeeCode();
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddEmployeePopup.vue:688 ~ storeAndAddBtnClick ~ error:",
          error
        );
      }
    },

    /**
     * validate và tạo 1 nhân viên mới hoặc update thông tin nhân viên
     * @author: TTANH (01/07/2023)
     */
    async createNewEmployee() {
      if (this.validateData()) {
        let isSuccess = true;
        this.isLoading = true;

        //lọc loại những trường rỗng
        var dataSendApi = {};

        for (const key in this.addEmployeeData) {
          if (key === "gender") {
            if (this.addEmployeeData[key] !== "") {
              dataSendApi[key] = this.addEmployeeData[key];
            }
          } else if (this.addEmployeeData[key]) {
            dataSendApi[key] = this.addEmployeeData[key];
          }
        }

        if (this.formMode === this.$_MISAEnum.FORM_MODE.ADD) {
          const res = await EmployeeService.post(dataSendApi);

          if (res.success) {
            this.$store.commit("addToast", {
              type: "success",
              text: this.$_MISAResource[this.$store.state.langCode].AddEmployee
                .Success,
            });
          } else {
            this.$store.commit("addToast", {
              type: "error",
              text: res.userMsg,
            });

            if (
              res.errorCode === this.$_MISAEnum.ERROR_CODE.CODE_DUPLICATE ||
              res.errorCode === this.$_MISAEnum.ERROR_CODE.WRONG_FORMAT_CODE
            ) {
              this.$refs.employeeCode.focus();
            }

            isSuccess = false;
          }
        } else if (this.formMode === this.$_MISAEnum.FORM_MODE.UPDATE) {
          const res = await EmployeeService.put(
            this.addEmployeeData.employeeId,
            dataSendApi
          );

          if (res.success) {
            this.$store.commit("addToast", {
              type: "success",
              text: this.$_MISAResource[this.$store.state.langCode]
                .UpdateEmployee.Success,
            });
          } else {
            this.$store.commit("addToast", {
              type: "error",
              text: res.devMsg,
            });

            if (
              res.errorCode === this.$_MISAEnum.ERROR_CODE.CODE_DUPLICATE ||
              res.errorCode === this.$_MISAEnum.ERROR_CODE.WRONG_FORMAT_CODE
            ) {
              this.$refs.employeeCode.focus();
            }

            isSuccess = false;
          }
        }

        this.isLoading = false;
        return isSuccess;
      } else {
        this.isShowDialogError = true;
        this.$refs.storeBtn.focusout();
        this.$refs.storeAndAddBtn.focusout();
      }
    },

    /**
     * thực hiện validate dữ liệu
     * @author: TTANH (01/07/2023)
     * @returns: true nếu form đã được validate
     */
    validateData() {
      try {
        let isValidate = true;

        this.resetErrorText();

        //không được để trống
        if (this.addEmployeeData.employeeCode === "") {
          this.errorTextEmployeeData.employeeCode =
            this.$_MISAResource[
              this.$store.state.langCode
            ].CustomerCodeInvalidError.Empty;

          isValidate = false;
        }

        if (this.addEmployeeData.fullName === "") {
          this.errorTextEmployeeData.fullName =
            this.$_MISAResource[
              this.$store.state.langCode
            ].FullNameInvalidError.Empty;

          isValidate = false;
        }

        if (this.addEmployeeData.departmentId === "") {
          this.errorTextEmployeeData.departmentId =
            this.$_MISAResource[
              this.$store.state.langCode
            ].DepartmentInvalidError.Empty;

          isValidate = false;
        }

        // không tìm thấy
        if (this.$refs.departmentId.getCurrentInputValue() !== "") {
          if (
            findIndexByAttribute(
              this.departments,
              "DepartmentId",
              this.addEmployeeData.departmentId
            ) === -1
          ) {
            this.errorTextEmployeeData.departmentId =
              this.$_MISAResource[
                this.$store.state.langCode
              ].DepartmentInvalidError.NotFound;

            isValidate = false;
          }
        }

        //đúng định dạng
        //ngày tháng

        return isValidate;
      } catch (error) {
        console.log(
          "🚀 ~ file: AddEmployeePopup.vue:509 ~ validateData ~ error:",
          error
        );
      }
    },

    /**
     * làm mới lại thông báo lỗi
     * @author: TTANH (01/07/2023)
     */
    resetErrorText() {
      try {
        for (let attr in this.errorTextEmployeeData) {
          this.errorTextEmployeeData[attr] = "";
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddEmployeePopup.vue:594 ~ resetErrorText ~ error:",
          error
        );
      }
    },

    /**
     * làm mới lại thông tin thêm
     * @author: TTANH (01/07/2023)
     */
    resetAddEmployeeData() {
      try {
        for (let attr in this.addEmployeeData) {
          this.addEmployeeData[attr] = "";
        }

        if (this.$refs.departmentId) {
          this.$refs.departmentId.getInputRef().value = "";
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: AddEmployeePopup.vue:594 ~ resetErrorText ~ error:",
          error
        );
      }
    },

    /**
     * xử lý khi ấn ẩn dialog thông báo lỗi
     * @author: TTANH (01/07/2023)
     */
    closeBtnDialogErrorClick() {
      try {
        this.isShowDialogError = false;

        this.$refs[this.getFirstError.errorAttr].focus();
        this.$refs.closeBtnDialogError.focusout();
      } catch (error) {
        console.log(
          "🚀 ~ file: AddEmployeePopup.vue:777 ~ closeBtnDialogErrorClick ~ error:",
          error
        );
      }
    },

    /**
     * xử lý sự kiện keydown của nút "cất và thêm"
     * @param {*} event
     */
    onStoreAndAddBtnKeyDown(event) {
      if (event.keyCode === this.$_MISAEnum.KEY_CODE.TAB && !event.shiftKey) {
        event.preventDefault();
        this.$refs.employeeCode.focus();
      }
    },

    /**
     * xử lý các phím tắt
     * @author: TTANH (11/07/2023)
     */
    handleKeydown(event) {
      event.stopPropagation();

      if (event.keyCode === this.$_MISAEnum.KEY_CODE.ESC) {
        this.$emit("closeAddEmployeePopup");
      } else if (
        event.keyCode === this.$_MISAEnum.KEY_CODE.S &&
        event.shiftKey &&
        event.ctrlKey
      ) {
        event.preventDefault();
        this.storeAndAddBtnClick();
      } else if (
        event.keyCode === this.$_MISAEnum.KEY_CODE.S &&
        event.ctrlKey
      ) {
        event.preventDefault();
        this.storeBtnClick();
      }
    },

    /**
     * xử lý khi nhấn vào nút enter của combobox
     * @param {*} event
     */
    onEnterDepartmentId(event) {
      if (event.keyCode === this.$_MISAEnum.KEY_CODE.ENTER) {
        this.$refs.position.focus();
      }
    },

    /**
     * hàm cập nhật thông tin department khi department id thay đổi
     * @param {*} departmentId id của department
     * @author: TTANH (14/07/2023)
     */
    updateDepartmentInfo(departmentId) {
      this.errorTextEmployeeData.departmentId = "";

      let index = findIndexByAttribute(
        this.departments,
        "DepartmentId",
        departmentId
      );

      if (index === -1) {
        this.addEmployeeData.departmentCode = "";
        this.addEmployeeData.departmentName = "";
      } else {
        this.addEmployeeData.departmentCode =
          this.departments[index].DepartmentCode;
        this.addEmployeeData.departmentName =
          this.departments[index].DepartmentName;
      }
    },

    /**
     * ẩn hiện thông tin thêm: là nhà cung cấp
     * @author: TTANH (14/07/2023)
     */
    clickIsSupplier() {
      this.isSupplier = !this.isSupplier;
    },

    /**
     * ẩn hiện thông tin thêm: là khách hàng
     * @author: TTANH (14/07/2023)
     */
    clickIsCustomer() {
      this.isCustomer = !this.isCustomer;
    },
  },

  computed: {
    /* thêm id để phân biệt các phần tử với nhau */
    computedDepartments() {
      try {
        let departmentsFormat = [];

        this.departments.forEach((department) => {
          let id = department.DepartmentId;
          let name = department.DepartmentName;
          let code = department.DepartmentCode;

          departmentsFormat.push({
            id,
            name,
            code,
            ...department,
          });
        });

        return departmentsFormat;
      } catch (error) {
        console.log(
          "🚀 ~ file: EmployeeList.vue:457 ~ computedEmployees ~ error:",
          error
        );
      }
    },

    computedFormMode() {
      if (!this.dataUpdate) {
        return this.$_MISAEnum.FORM_MODE.ADD;
      } else {
        return this.$_MISAEnum.FORM_MODE.UPDATE;
      }
    },

    getFirstError() {
      let errorAttr = "";
      let errorText = "";

      for (let attr in this.errorTextEmployeeData) {
        if (this.errorTextEmployeeData[attr] !== "") {
          errorText = this.errorTextEmployeeData[attr];
          errorAttr = attr;
          break;
        }
      }

      return {
        errorAttr,
        errorText,
      };
    },
  },
  watch: {
    "addEmployeeData.employeeCode": function () {
      this.errorTextEmployeeData.employeeCode = "";
    },

    "addEmployeeData.fullName": function () {
      this.errorTextEmployeeData.fullName = "";
    },

    "addEmployeeData.departmentId": function (newValue) {
      this.updateDepartmentInfo(newValue);
    },
  },
};
</script>

<style scoped>
@import url(./add-employee-popup.css);
</style>
