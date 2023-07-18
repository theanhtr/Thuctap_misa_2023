<template>
  <div class="layout-main">
    <div class="toast-group">
      <misa-toast
        v-for="toast in $store.state.toasts"
        :key="toast.id"
        :type="toast.type"
        @clickCloseToast="closeToast(toast.id)"
      >
        {{ toast.text }}
      </misa-toast>
    </div>
    <TheSidebar />
    <TheMainContent>
      <router-view />
    </TheMainContent>
  </div>
</template>

<script>
import TheSidebar from "./components/layout/sidebar/TheSidebar.vue";
import TheMainContent from "./components/layout/main-content/TheMainContent.vue";

export default {
  name: "App",
  components: {
    TheMainContent,
    TheSidebar,
  },

  methods: {
    /**
     * xử lý xóa 1 toast
     * @author: TTANH (01/07/2023)
     * @param {string} id id của toast
     */
    closeToast(id) {
      try {
        this.$store.commit("removeToast", { id: id });
      } catch (error) {
        console.log("🚀 ~ file: App.vue:36 ~ closeToast ~ error:", error);
      }
    },
  },
};
</script>

<style>
@import url(./css/main.css);

.layout-main {
  display: flex;
  height: 100vh;
  width: 100vw;
}
</style>
