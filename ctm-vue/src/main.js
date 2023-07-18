import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";

import MISAButton from "./components/base/button/MISAButton.vue";
import MISAIcon from "./components/base/icon/MISAIcon.vue";
import MISASeparationLine from "./components/base/separation-line/MISASeparationLine.vue";
import MISASearchInput from "./components/base/input/search-input/MISASearchInput.vue";
import MISADropdownlist from "./components/base/dropdownlist/MISADropdownlist.vue";
import MISATable from "./components/base/table/MISATable.vue";
import MISACheckboxInput from "./components/base/input/MISACheckboxInput.vue";
import MISALoadingSpinner from "./components/base/loading/loading-spinner/MISALoadingSpinner.vue";
import MISALoadingSkeleton from "./components/base/loading/loading-skeleton/MISALoadingSkeleton.vue";
import MISAPopup from "./components/base/popup/MISAPopup.vue";
import MISATextfield from "./components/base/textfield/MISATextfield.vue";
import MISACombobox from "./components/base/combobox/MISACombobox.vue";
import MISADatePicker from "./components/base/date-picker/MISADatePicker.vue";
import MISAArrowDown from "./components/base/arrow-down/MISAArrowDown.vue";
import MISARadioInput from "./components/base/radio-input/MISARadioInput.vue";
import MISATooltip from "./components/base/tooltip/MISATooltip.vue";
import MISAToast from "./components/base/toast/MISAToast.vue";
import MISAPaging from "./components/base/paging/MISAPaging.vue";

import MISAResource from "./resource";
import MISAEnum from "./enum";

const app = createApp(App);

app.use(store).use(router);

app
  .component("misa-separation-line", MISASeparationLine)
  .component("misa-icon", MISAIcon)
  .component("misa-search-input", MISASearchInput)
  .component("misa-checkbox-input", MISACheckboxInput)
  .component("misa-dropdownlist", MISADropdownlist)
  .component("misa-button", MISAButton)
  .component("misa-loading-skeleton", MISALoadingSkeleton)
  .component("misa-loading-spinner", MISALoadingSpinner)
  .component("misa-popup", MISAPopup)
  .component("misa-textfield", MISATextfield)
  .component("misa-combobox", MISACombobox)
  .component("misa-table", MISATable)
  .component("misa-arrow-down", MISAArrowDown)
  .component("misa-date-picker", MISADatePicker)
  .component("misa-radio-input", MISARadioInput)
  .component("misa-tooltip", MISATooltip)
  .component("misa-toast", MISAToast)
  .component("misa-paging", MISAPaging);

app.config.globalProperties.$_MISAResource = MISAResource;
app.config.globalProperties.$_MISAEnum = MISAEnum;

app.mount("#app");
