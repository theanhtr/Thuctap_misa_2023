<template>
  <div class="paging">
    <div class="paging-left">
      <div class="total-records">
        Tổng số: <strong>{{ modelValue.totalRecord }}</strong> bản ghi
      </div>
    </div>
    <div class="paging-right">
      <misa-button
        type="dropdown"
        width="200px"
        borderRadius="var(--border-radius-default)"
        :dataDropdown="pagingDataDropdown"
        :dataDropdownTop="true"
        @clickItem="changePageSize"
        >{{ pagingTitle }}</misa-button
      >
      <div class="paging-nav">
        <misa-button
          type="link"
          colorText="#111"
          :disable="modelValue.pageNumber === 1"
          fontWeight="400"
          class="m-r-13"
          @clickBtnContainer="clickPreviousPage"
        >
          Trước
        </misa-button>
        <misa-button
          type="link"
          colorText="#111"
          v-for="page in modelValue.totalPage"
          :key="page"
          :fontWeight="page === modelValue.pageNumber ? '700' : '400'"
          :border="
            page === modelValue.pageNumber
              ? '1px solid var(--border-color-default)'
              : ''
          "
          padding="0px 6px"
          borderRadius="var(--border-radius-default)"
          @clickBtnContainer="selectPage(page)"
        >
          {{ page }}
        </misa-button>
        <misa-button
          type="link"
          colorText="#111"
          fontWeight="400"
          :disable="modelValue.pageNumber === modelValue.totalPage"
          class="m-l-13"
          @clickBtnContainer="clickNextPage"
        >
          Sau
        </misa-button>
      </div>
    </div>
  </div>
</template>

<script>
import { findIndexByAttribute } from "@/helper/common.js";

export default {
  name: "MISAPaging",
  props: {
    /**
     * modelValue bao gồm các thuộc tính:
     * {
     *  pageSize: 10,
        pageNumber: 1,
        totalPage: 0,
        totalRecord: 0,
     * }
     */
    modelValue: {
      required: true,
    },
  },
  data() {
    return {
      pagingDataDropdown: [
        {
          id: 10,
          value: 10,
          title:
            10 + this.$_MISAResource[this.$store.state.langCode].Paging.Title,
        },
        {
          id: 20,
          value: 20,
          title:
            20 + this.$_MISAResource[this.$store.state.langCode].Paging.Title,
        },
        {
          id: 30,
          value: 30,
          title:
            30 + this.$_MISAResource[this.$store.state.langCode].Paging.Title,
        },
        {
          id: 50,
          value: 50,
          title:
            50 + this.$_MISAResource[this.$store.state.langCode].Paging.Title,
        },
        {
          id: 100,
          value: 100,
          title:
            100 + this.$_MISAResource[this.$store.state.langCode].Paging.Title,
        },
      ],
    };
  },
  methods: {
    /**
     * thay đổi số lượng bản ghi trên 1 page
     * @param {*} id id của số lượng muốn thay đổi
     */
    changePageSize(id) {
      try {
        const index = findIndexByAttribute(this.pagingDataDropdown, "id", id);

        if (index !== -1) {
          this.$emit("update:modelValue", {
            ...this.modelValue,
            pageNumber: 1,
            pageSize: this.pagingDataDropdown[index].value,
          });
          this.$emit("reloadData");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: MISAPaging.vue:128 ~ changePageSize ~ error:",
          error
        );
      }
    },

    /**
     * chuyển sang page tiếp theo
     * @author: TTANH (03/07/2023)
     */
    clickNextPage() {
      try {
        this.$emit("update:modelValue", {
          ...this.modelValue,
          pageNumber: this.modelValue.pageNumber + 1,
        });
        this.$emit("reloadData");
      } catch (error) {
        console.log(
          "🚀 ~ file: MISAPaging.vue:154 ~ clickNextPage ~ error:",
          error
        );
      }
    },

    /**
     * chọn 1 stt page mới
     * @author: TTANH (03/07/2023)
     */
    selectPage(newPageNumber) {
      try {
        if (newPageNumber !== this.modelValue.pageNumber) {
          this.$emit("update:modelValue", {
            ...this.modelValue,
            pageNumber: newPageNumber,
          });
          this.$emit("reloadData");
        }
      } catch (error) {
        console.log(
          "🚀 ~ file: MISAPaging.vue:174 ~ selectPage ~ error:",
          error
        );
      }
    },

    /**
     * trở về page trước đó
     * @author: TTANH (03/07/2023)
     */
    clickPreviousPage() {
      try {
        this.$emit("update:modelValue", {
          ...this.modelValue,
          pageNumber: this.modelValue.pageNumber - 1,
        });
        this.$emit("reloadData");
      } catch (error) {
        console.log(
          "🚀 ~ file: MISAPaging.vue:191 ~ clickPreviousPage ~ error:",
          error
        );
      }
    },
  },
  computed: {
    pagingTitle() {
      const index = findIndexByAttribute(
        this.pagingDataDropdown,
        "value",
        this.modelValue.pageSize
      );

      if (index !== -1) {
        return (
          this.pagingDataDropdown[index].value +
          this.$_MISAResource[this.$store.state.langCode].Paging.Title
        );
      } else {
        return (
          this.pagingDataDropdown[index].value +
          this.$_MISAResource[this.$store.state.langCode].Paging.Error
        );
      }
    },
  },
};
</script>

<style>
@import url(./paging.css);
</style>
