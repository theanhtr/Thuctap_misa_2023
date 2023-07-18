const MISAEnum = {
  GENDER: {
    MALE: 0,
    FEMALE: 1,
    ORTHER: 2,
  },

  FORM_MODE: {
    ADD: 0,
    UPDATE: 1,
    VIEW: 2,
  },

  /* enum sử dụng trong more info nav của add employee popup */
  MORE_INFO_NAV: {
    SALARY_INFO: 0,
    BANK_INFO: 1,
    CONTACT_INFO: 2,
  },

  /* enum sử dụng cho sự kiện keypress, keyup, keydown */
  KEY_CODE: {
    TAB: 9,
    ENTER: 13,
    CTRL: 17,
    SHIFT: 16,
    ESC: 27,
    ALT: 18,
    ARROW_UP: 38,
    ARROW_DOWN: 40,
    ARROW_RIGHT: 39,
    ARROW_LEFT: 37,
    INSERT: 45,
    S: 83,
    F: 70,
  },

  ERROR_CODE: {
    CODE_DUPLICATE: 1207,
    WRONG_FORMAT_CODE: 1208,
    NOT_FOUND_DATA: 1209,
  },
};

export default MISAEnum;
