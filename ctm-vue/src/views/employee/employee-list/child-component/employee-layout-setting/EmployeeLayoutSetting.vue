<template>
  <div class="m-overlay" style="justify-content: end">
    <misa-popup
      height="100vh"
      width="50vw"
      :title="$t('employeeSubsystem.layoutSetting.headerTitle')"
    >
      <template #header__close>
        <misa-icon icon="help" :title="$t('common.helpIconTooltip')" />
        <misa-icon
          @click="$emit('close-click')"
          icon="close"
          style="margin-left: 6px"
          :title="$t('common.closeIconTooltip')"
        />
      </template>
      <template #content__input-control>
        <misa-table
          :columnsInfo="columnsInfo"
          :rowsData="employeeColumnsInfo"
          :oneRowSelect="true"
        />
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
              @clickBtnContainer="$emit('close-click')"
              >{{ $t("common.button.cancel") }}</misa-button
            >
          </div>
          <div>
            <misa-button
              type="sub"
              width="142  px"
              borderRadius="var(--border-radius-default)"
              tabindex="21"
              style="margin-right: 10px"
              @clickBtnContainer="$emit('set-default-click')"
              ref="setDefaultBtn"
              >{{ $t("common.button.implicitSampling") }}</misa-button
            >
            <misa-button
              type="main"
              width="56px"
              tabindex="22"
              borderRadius="var(--border-radius-default)"
              @clickBtnContainer="$emit('store-click')"
              ref="storeBtn"
              >{{ $t("common.button.store") }}</misa-button
            >
          </div>
        </div>
      </template>
    </misa-popup>
  </div>
</template>

<script>
import { employeeLayoutColumnsInfo } from "./employeeLayoutColumnsInfo.js";

export default {
  name: "EmployeeLayoutSetting",
  props: {
    /**
     * thông tin cột phải theo định dạn
        {
          id: "EmployeeCode",
          name: "Mã nhân viên",
          size: "150px",
          textAlign: "left", // bao gồm: left, center, right
          format: "text", // bao gồm: text, date, currency,
                          // checkbox, input-combobox, input-text, input-number_no_dot, valid-check
                          //  + với checkbox thì "có thể" thêm isCannotChangeChecked để không cho thay đổi checkbox input ở trường đó
                          //  + với input-combobox thì "phải" có thêm comboboxRowData và chỉ có duy nhất một combobox input??
                          //  + với valid-check chỉ chấp nhận _MISAEnum.RECORD_CHECK
          isShow: true,
          isPin: false,
        },
     * @author: TTANH (26/06/2023)
     */
    employeeColumnsInfoProp: {
      default: [],
    },
  },

  data() {
    return {
      columnsInfo: [...employeeLayoutColumnsInfo],
      employeeColumnsInfo: [],
    };
  },

  created() {
    window.addEventListener("keydown", this.handleKeydown);

    /**
     * dùng để copy giá trị từ prop chứ ko lấy địa chỉ
     */
    this.employeeColumnsInfo = this.employeeColumnsInfoProp.map((e) =>
      Object.assign({}, e)
    );
  },

  unmounted() {
    window.removeEventListener("keydown", this.handleKeydown);
  },

  methods: {
    /**
     * xử lý các phím tắt
     * @author: TTANH (07/08/2023)
     */
    handleKeydown(event) {
      event.stopPropagation();
      if (event.keyCode === this.$_MISAEnum.KEY_CODE.ESC) {
        this.$emit("close-click");
      }
    },

    /**
     * dùng để lấy thông tin trên component cha
     * @author: TTANH (04/08/2023)
     */
    getEmployeeColumnsInfoUpdate() {
      return this.employeeColumnsInfo;
    },
  },
};
</script>
